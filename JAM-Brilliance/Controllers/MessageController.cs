using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

using AutoMapper;

using JAM.Core.Attributes;
using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Core.Models;
using JAM.Brilliance.Models.ViewModels;

using PagedList;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class MessageController : Controller
    {
        private readonly ISurveyDataService _surveyDataService;
        private readonly IMessageDataService _messageDataService;
        private readonly IVisitorDataService _visitorDataService;
        private readonly IFavouriteDataService _favouriteDataService;
        private readonly IAccountService _accountService;
        private readonly IDiagnosticsService _diagnosticsService;
        private readonly IEmailService _emailService;
        private readonly IDataCache _dataCache;

        public MessageController(ISurveyDataService surveyDataService, IMessageDataService messageDataService, IVisitorDataService visitorDataService, IFavouriteDataService favouriteDataService, IAccountService accountService, IDiagnosticsService diagnosticsService, IEmailService emailService, IDataCache dataCache)
        {
            _surveyDataService = surveyDataService;
            _messageDataService = messageDataService;
            _visitorDataService = visitorDataService;
            _favouriteDataService = favouriteDataService;
            _accountService = accountService;
            _diagnosticsService = diagnosticsService;
            _emailService = emailService;
            _dataCache = dataCache;
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        public JsonResult GetNumberOfUnreadMessages()
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            int messageCount = _messageDataService.GetUnreadMessagesCount(surveyId);

            return Json(messageCount);
        }

        public ViewResult SendMessageToUs()
        {
            ViewBag.CurrentPageId = PageIds.Contact;

            var smvm = new SendMessageViewModel();

            if (User.IsInRole(MemberRoles.Member))
            {
                Survey fromSurvey = _surveyDataService.GetCurrentUserSurvey();
                smvm.FromSurveyId = fromSurvey.SurveyId;
                smvm.FromName = fromSurvey.Name;
                smvm.IsFromDisabled = fromSurvey.IsDisabled;

                Survey toSurvey = _surveyDataService.GetSurvey(_dataCache.SupportSurveyId);
                smvm.ToSurveyId = toSurvey.SurveyId;
                smvm.ToName = toSurvey.Name;
                smvm.IsToDisabled = toSurvey.IsDisabled;
            }
            else
            {
                Survey fromSurvey = _surveyDataService.GetSurvey(_dataCache.AnonSurveyId);
                smvm.FromSurveyId = fromSurvey.SurveyId;
                smvm.FromName = fromSurvey.Name;
                smvm.IsFromDisabled = fromSurvey.IsDisabled;
                smvm.IsFromAnonymous = smvm.FromSurveyId == _dataCache.AnonSurveyId;

                Survey toSurvey = _surveyDataService.GetSurvey(_dataCache.SupportAnonSurveyId);
                smvm.ToSurveyId = toSurvey.SurveyId;
                smvm.ToName = toSurvey.Name;
                smvm.IsToDisabled = toSurvey.IsDisabled;
            }

            return View("SendMessage", smvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Throttle(Name = "SendMessageToUsThrottle")]
        public RedirectToRouteResult SendMessageToUs(SendMessageViewModel mvm)
        {
            ViewBag.CurrentPageId = PageIds.Contact;

            if (ModelState.IsValid)
            {
                if (User.IsInRole(MemberRoles.Member))
                {
                    int surveyId = _surveyDataService.GetCurrentUserSurveyId();
                    mvm.FromSurveyId = surveyId;
                    mvm.IsFromAnonymous = mvm.FromSurveyId == _dataCache.AnonSurveyId;
                    mvm.ToSurveyId = _dataCache.SupportSurveyId;
                }
                else
                {
                    mvm.FromSurveyId = _dataCache.AnonSurveyId;
                    mvm.IsFromAnonymous = mvm.FromSurveyId == _dataCache.AnonSurveyId;
                    mvm.ToSurveyId = _dataCache.SupportAnonSurveyId;
                }

                var m = Mapper.Map<SendMessage>(mvm);
                var ok = _messageDataService.SendMessage(m);
            }

            // since its anonymous child partial we cant handle failure
            return RedirectToAction("MessageSent", mvm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult SendMessage(int toSurveyId)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            Survey fromSurvey = _surveyDataService.GetCurrentUserSurvey();
            Survey toSurvey = _surveyDataService.GetSurvey(toSurveyId);

            var mvm = new SendMessageViewModel();
            mvm.ToSurveyId = toSurveyId;
            mvm.ToName = toSurvey.Name;
            mvm.FromSurveyId = fromSurvey.SurveyId;
            mvm.FromName = fromSurvey.Name;

            return View(mvm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(SendMessageViewModel mvm)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            if (ModelState.IsValid)
            {
                int surveyId = _surveyDataService.GetCurrentUserSurveyId();
                mvm.FromSurveyId = surveyId;
                mvm.IsFromAnonymous = mvm.FromSurveyId == _dataCache.AnonSurveyId;
                var m = Mapper.Map<SendMessage>(mvm);

                if (_messageDataService.SendMessage(m))
                {
                    return RedirectToAction("MessageSent", mvm);
                }
            }

            return View(mvm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult MyMessages(int? page)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            _diagnosticsService.ThrowUnlessSurveyComplete();

            var smvms = GetMySortedMessages();

            var pageNumber = page ?? 1;
            var onePageOfMessages = smvms.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfMessages = onePageOfMessages;

            return View(onePageOfMessages);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult MySentMessages(int? page)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            _diagnosticsService.ThrowUnlessSurveyComplete();

            var smvms = GetMySortedSentMessages();

            var pageNumber = page ?? 1;
            var onePageOfSentMessages = smvms.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfSentMessages = onePageOfSentMessages;

            return View(onePageOfSentMessages);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult ReadMessage(int messageId)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            int surveyId = _surveyDataService.GetCurrentUserSurveyId();
            SendMessage sm = _messageDataService.ReadMessage(surveyId, messageId);
            var smvm = Mapper.Map<SendMessageViewModel>(sm);
            return View(smvm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult ReadSentMessage(int messageId)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            int surveyId = _surveyDataService.GetCurrentUserSurveyId();
            SendMessage sm = _messageDataService.ReadMessage(surveyId, messageId);
            var smvm = Mapper.Map<SendMessageViewModel>(sm);
            smvm.IsSentMessage = true;
            return View("ReadMessage", smvm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        public RedirectToRouteResult DeleteReplyMessage(SendMessageViewModel smvm, string action)
        {
            if (action.Equals(PageButtons.DeleteMessage))
            {
                int surveyId = _surveyDataService.GetCurrentUserSurveyId();
                var sm = Mapper.Map<SendMessage>(smvm);
                if (sm.ToSurveyId == surveyId)
                {
                    _messageDataService.DeleteMessage(sm.MessageId, surveyId);
                }
            }
            else if (action.Equals(PageButtons.ReplyToMessage))
            {
                int surveyId = _surveyDataService.GetCurrentUserSurveyId();
                var sm = Mapper.Map<SendMessage>(smvm);
                if (sm.ToSurveyId == surveyId)
                {
                    return RedirectToAction("ReplyToMessage", new { messageId = sm.MessageId });
                }
            }

            return RedirectToAction("MyMessages");
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult ReplyToMessage(int messageId)
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            var sm = _messageDataService.ReadMessage(surveyId, messageId);
            var smvm = Mapper.Map<SendMessageViewModel>(sm);

            smvm.FromSurveyId = sm.ToSurveyId;
            smvm.FromName = sm.ToName;
            smvm.IsFromDisabled = sm.IsToDisabled;
            
            smvm.ToSurveyId = sm.FromSurveyId;
            smvm.ToName = sm.FromName;
            smvm.IsToDisabled = sm.IsFromDisabled;

            smvm.Body = _emailService.PrependBody(sm.Body);

            return View("SendMessage", smvm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        [ValidateAntiForgeryToken]
        public ActionResult ReplyToMessage(SendMessageViewModel mvm)
        {
            return SendMessage(mvm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public RedirectToRouteResult RemoveMessage(int messageId)
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();
            _messageDataService.DeleteMessage(messageId, surveyId);
            return RedirectToAction("MyMessages");
        }

        public ViewResult MessageSent(SendMessageViewModel mvm)
        {
            return View(mvm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        public JsonResult GetNumbersOf()
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            int messagesCount = _messageDataService.GetUnreadMessagesCount(surveyId);
            int visitorsCount = _visitorDataService.GetVisitorsCount(surveyId);
            int fansCount = _favouriteDataService.GetFansCount(surveyId);
            int onlineUsersCount = _accountService.GetOnlineUsersCount();

            var counts = new { messagesCount, visitorsCount, fansCount, onlineUsersCount };
            return Json(counts);
        }

        private IEnumerable<SendMessageViewModel> GetMySortedMessages()
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            var rm = _messageDataService.GetReceivedMessages(surveyId);
            var rmvm = Mapper.Map<IEnumerable<SendMessage>, IList<SendMessageViewModel>>(rm);

            IEnumerable<SendMessageViewModel> smvms = rmvm.OrderByDescending(x => x.SendDate);
            return smvms;
        }

        private IEnumerable<SendMessageViewModel> GetMySortedSentMessages()
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            var rm = _messageDataService.GetSentMessages(surveyId);
            var rmvm = Mapper.Map<IEnumerable<SendMessage>, IList<SendMessageViewModel>>(rm);

            IEnumerable<SendMessageViewModel> smvms = rmvm.OrderByDescending(x => x.SendDate);
            return smvms;
        }
    }
}