using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;
using AutoMapper;
using JAM.Core.Interfaces;
using JAM.Core.Interfaces.Admin;
using JAM.Core.Logic;
using JAM.Core.Models;
using JAM.Brilliance.Models.ViewModels;
using PagedList;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    [Authorize(Roles = MemberRoles.Administrator)]
    public class MessageAdminController : Controller
    {
        private readonly IMessageAdminDataService _messageAdminDataService;
        private readonly ISurveyAdminDataService _surveyAdminDataService;
        private readonly IEmailService _emailService;
        private readonly IDataCache _dataCache;
        private readonly IMapper _mapper;

        public MessageAdminController(IMessageAdminDataService messageAdminDataService, ISurveyAdminDataService surveyAdminDataService, IEmailService emailService, IDataCache dataCache, IMapper mapper)
        {
            _messageAdminDataService = messageAdminDataService;
            _surveyAdminDataService = surveyAdminDataService;
            _emailService = emailService;
            _dataCache = dataCache;
            _mapper = mapper;
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult RemoveSupportMessage(int messageId)
        {
            _messageAdminDataService.DeleteSupportMessage(messageId);
            return RedirectToAction("DevPage", "Home");
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult ReadConversation(int messageId)
        {
            ViewBag.CurrentPageId = PageIds.DevPage;

            Conversation cm = _messageAdminDataService.ReadConversation(messageId);
            var cvm = _mapper.Map<Conversation, ConversationViewModel>(cm);

            return View(cvm);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult SupportMessages(int surveyId, int? page)
        {
            ViewBag.CurrentPageId = PageIds.DevPage;

            var smvms = GetSortedSupportMessages(surveyId);
            var pageNumber = page ?? 1;
            var onePageOfSupportMessages = smvms.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfSupportMessages = onePageOfSupportMessages;

            return View(onePageOfSupportMessages);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult ReadSupportMessage(int messageId)
        {
            ViewBag.CurrentPageId = PageIds.DevPage;

            SendMessage sm = _messageAdminDataService.ReadSupportMessage(messageId);
            var smvm = _mapper.Map<SendMessageViewModel>(sm);
            return View(smvm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult DeleteReplyMessage(SendMessageViewModel smvm, string action)
        {
            if (action.Equals(PageButtons.DeleteMessage))
            {
                int surveyId = _surveyAdminDataService.GetCurrentUserSurveyId();
                var sm = _mapper.Map<SendMessage>(smvm);
                if (sm.ToSurveyId == surveyId)
                {
                    _messageAdminDataService.DeleteMessage(sm.MessageId, surveyId);
                }
            }
            else if (action.Equals(PageButtons.ReplyToMessage))
            {
                int surveyId = _surveyAdminDataService.GetCurrentUserSurveyId();
                var sm = _mapper.Map<SendMessage>(smvm);
                if (sm.ToSurveyId == surveyId || (User.IsInRole(MemberRoles.Administrator) && (sm.ToSurveyId == _dataCache.SupportSurveyId || sm.ToSurveyId == _dataCache.SupportAnonSurveyId)))
                {
                    return RedirectToAction("ReplyToMessage", new { messageId = sm.MessageId });
                }
            }

            return RedirectToAction("DevPage", "Home");
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult ReplyToMessage(int messageId)
        {
            var sm = _messageAdminDataService.ReadSupportMessage(messageId);
            var smvm = _mapper.Map<SendMessageViewModel>(sm);

            smvm.FromSurveyId = sm.ToSurveyId;
            smvm.FromName = sm.ToName;
            smvm.IsFromDisabled = sm.IsToDisabled;

            smvm.ToSurveyId = sm.FromSurveyId;
            smvm.ToName = sm.FromName;
            smvm.IsToDisabled = sm.IsFromDisabled;

            smvm.Body = _emailService.PrependBody(sm.Body);

            return View("BroadcastMessage", smvm);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ActionResult BroadcastMessage()
        {
            var smvm = new SendMessageViewModel();

            Survey fromSurvey = _surveyAdminDataService.GetSurvey(_dataCache.SupportSurveyId);
            smvm.FromSurveyId = fromSurvey.SurveyId;
            smvm.FromName = fromSurvey.Name;
            smvm.IsFromDisabled = fromSurvey.IsDisabled;

            Survey toSurvey = _surveyAdminDataService.GetSurvey(_dataCache.BroadcastSurveyId);
            smvm.ToSurveyId = toSurvey.SurveyId;
            smvm.ToName = toSurvey.Name;
            smvm.IsToDisabled = toSurvey.IsDisabled;

            return View(smvm);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        [HttpPost]
        public ActionResult BroadcastMessage(SendMessageViewModel mvm)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            if (ModelState.IsValid)
            {
                var m = _mapper.Map<SendMessage>(mvm);

                if (m.ToSurveyId == _dataCache.BroadcastSurveyId)
                {
                    if (_messageAdminDataService.BroadcastMessage(m))
                    {
                        return RedirectToAction("DevPage", "Home");
                    }
                }
                else
                {
                    if (_messageAdminDataService.SendMessage(m))
                    {
                        return RedirectToAction("DevPage", "Home");
                    }
                }
            }

            return View();
        }

        private IEnumerable<SendMessageViewModel> GetSortedSupportMessages(int surveyId)
        {
            var rm = _messageAdminDataService.GetReceivedMessages(surveyId);
            var rmvm = _mapper.Map<IEnumerable<SendMessage>, IList<SendMessageViewModel>>(rm);

            IEnumerable<SendMessageViewModel> smvms = rmvm.OrderByDescending(x => x.SendDate);
            return smvms;
        }
    }
}