using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JAM.Brilliance.Areas.Mobile.Attributes;
using JAM.Brilliance.Areas.Mobile.Models;
using JAM.Brilliance.Areas.Mobile.Models.Response;
using JAM.Core.Interfaces;
using JAM.Core.Interfaces.App;
using JAM.Core.Models;

namespace JAM.Brilliance.Areas.Mobile.Controllers
{
    public class AppContactivityController : AppBaseController
    {
        private readonly IContactifyDataService _contactifyDataService;
        private readonly IMessageDataService _messageDataService;
        private readonly IFavouriteDataService _favouriteDataService;

        public AppContactivityController(IAccountService accountService, IAccountTokenDataService accountTokenDataService, IContactifyDataService contactifyDataService, IMessageDataService messageDataService, IFavouriteDataService favouriteDataService)
            : base(accountService, accountTokenDataService)
        {
            _contactifyDataService = contactifyDataService;
            _messageDataService = messageDataService;
            _favouriteDataService = favouriteDataService;
        }

        [HttpGet, ValidateToken]
        public JsonResult GetConversationHeads()
        {
            var surveyId = AccountTokenDataService.GetSurveyIdForToken(Token);
            var heads = _contactifyDataService.GetConversationHeads(surveyId);

            heads = heads.OrderBy(x => x.Category).ThenByDescending(x => x.LastActionDate);
            heads = heads.GroupBy(x => x.OtherSurveyId).Select(y => y.First());

            var response = new ConversationHeadsModel { Success = true, Message = "OK", Heads = heads };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, ValidateToken]
        public JsonResult GetConversation(int id)
        {
            int otherSurveyId = id;
            var selfSurveyId = AccountTokenDataService.GetSurveyIdForToken(Token);
            var conversation = _contactifyDataService.GetConversation(selfSurveyId, otherSurveyId);

            var response = new ConversationModel { Success = true, Message = "OK", Conversation = conversation };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPut, ValidateToken]
        public JsonResult PutNewMessage(NewMessageModel model)
        {
            var selfSurveyId = AccountTokenDataService.GetSurveyIdForToken(Token);

            var msg = new SendMessage();
            msg.SendDate = DateTime.Now;
            msg.FromSurveyId = selfSurveyId;
            msg.ToSurveyId = model.OtherSurveyId;
            msg.Body = model.NewMessageBody;

            if (_messageDataService.SendMessage(msg))
            {
                MessageModel response = new MessageModel { Success = true, Message = "OK", AddedMessage = msg };
                return Json(response);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var response = new MessageModel { Success = false, Message = "NOK", AddedMessage = null };
                return Json(response);
            }
        }

        [HttpGet, ValidateToken]
        public JsonResult GetNewMessages(GetNewMessagesModel model)
        {
            var selfSurveyId = AccountTokenDataService.GetSurveyIdForToken(Token);

            var conversation = _contactifyDataService.GetConversationTail(selfSurveyId, model.OtherSurveyId, model.LastMessageId);

            var msg = new SendMessage();
            msg.SendDate = DateTime.Now;
            msg.FromSurveyId = selfSurveyId;
            msg.ToSurveyId = model.OtherSurveyId;
            msg.MessageId = model.LastMessageId + 1;
            msg.Body = "Hej " + msg.MessageId;

            var msgs = conversation.Messages.ToList();
            msgs.Add(msg);

            msg = new SendMessage();
            msg.SendDate = DateTime.Now;
            msg.ToSurveyId = selfSurveyId;
            msg.FromSurveyId = model.OtherSurveyId;
            msg.MessageId = model.LastMessageId + 2;
            msg.Body = "Det verkar söt " + msg.MessageId;

            // msgs = conversation.Messages.ToList();
            msgs.Add(msg);

            conversation.Messages = msgs;

            var response = new ConversationModel { Success = true, Message = "OK", Conversation = conversation };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPut, ValidateToken]
        public JsonResult PutNewFavourite(NewFavouriteModel model)
        {
            var selfSurveyId = AccountTokenDataService.GetSurveyIdForToken(Token);

            var fav = new Favourite();
            fav.SelfSurveyId = selfSurveyId;
            fav.OtherSurveyId = model.OtherSurveyId;
            // handle 0
            _favouriteDataService.AddFavourite(fav);

            var response = new StatusModel { Success = true, Message = "OK" };

            return Json(response);
        }

        [AcceptVerbs(HttpVerbs.Delete)]
        [HttpDelete, ValidateToken]
        public JsonResult DeleteFavourite(NewMessageModel model)
        {
            var response = new StatusModel { Success = true, Message = "OK" };

            return Json(response);
        }
    }
}
