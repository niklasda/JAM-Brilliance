using System.Collections.Generic;
using System.Linq;

using Dapper;

using JAM.Core.Interfaces;
using JAM.Core.Interfaces.Admin;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services.Admin
{
    [UsedImplicitly]
    public class MessageAdminDataService : MessageDataService, IMessageAdminDataService
    {
        private readonly IDataCache _dataCache;

        public MessageAdminDataService(IDataCache dataCache, IDatabaseConfigurationService configurationService)
            : base(configurationService)
        {
            _dataCache = dataCache;
        }

        public bool BroadcastMessage(SendMessage sm)
        {
            const string sqlGetRecipients = "SELECT SurveyId FROM Surveys WHERE SurveyId > 0 AND SurveyId <> @BroadcastSurveyId";

            const string sqlInsertMessage = "INSERT INTO Messages (FromSurveyId, ToSurveyId, Body) VALUES (@FromSurveyId, @ToSurveyId, @Body)";
            using (var cn = Config.CreateConnection())
            {
                IEnumerable<SendMessage> sms = cn.Query<int>(sqlGetRecipients, new { BroadcastSurveyId = _dataCache.BroadcastSurveyId }).Select(x => new SendMessage { Body = sm.Body, FromSurveyId = sm.FromSurveyId, ToSurveyId = x });

                int nbrOfRows = cn.Execute(sqlInsertMessage, sms);
                return nbrOfRows == sms.Count();
            }
        }

        public Conversation ReadConversation(int messageId)
        {
            const string sqlSelectOriginalMessage = "SELECT Messages.*, FromSurvey.Name AS FromName, FromSurvey.IsDisabled AS IsFromDisabled, ToSurvey.Name AS ToName, ToSurvey.IsDisabled AS IsToDisabled FROM Messages "
                                            + "INNER JOIN Surveys AS FromSurvey ON FromSurvey.SurveyId = Messages.FromSurveyId "
                                            + "INNER JOIN Surveys AS ToSurvey ON ToSurvey.SurveyId = Messages.ToSurveyId WHERE MessageId = @MessageId";

            const string sqlSelectMessages = "SELECT Messages.*, FromSurvey.Name AS FromName, FromSurvey.IsDisabled AS IsFromDisabled, ToSurvey.Name AS ToName, ToSurvey.IsDisabled AS IsToDisabled FROM Messages "
                                            + "INNER JOIN Surveys AS FromSurvey ON FromSurvey.SurveyId = Messages.FromSurveyId "
                                            + "INNER JOIN Surveys AS ToSurvey ON ToSurvey.SurveyId = Messages.ToSurveyId WHERE Messages.FromSurveyId = @FromSurveyId AND Messages.ToSurveyId = @ToSurveyId";

            using (var cn = Config.CreateConnection())
            {
                var originalMessage = cn.Query<SendMessage>(sqlSelectOriginalMessage, new { MessageId = messageId }).Single();

                var fromSender = cn.Query<SendMessage>(sqlSelectMessages, new { FromSurveyId = originalMessage.FromSurveyId, ToSurveyId = originalMessage.ToSurveyId });
                var fromRecipient = cn.Query<SendMessage>(sqlSelectMessages, new { FromSurveyId = originalMessage.ToSurveyId, ToSurveyId = originalMessage.FromSurveyId });

                var cvm = new Conversation();
                cvm.OriginalMessage = originalMessage;
                cvm.Messages = fromSender.Union(fromRecipient);
//                cvm.FromRecipient = fromRecipient;

                return cvm;
            }
        }

        public SendMessage ReadSupportMessage(int messageId)
        {
            const string sqlSelectMessage = "SELECT Messages.*, FromSurvey.Name AS FromName, FromSurvey.IsDisabled AS IsFromDisabled, ToSurvey.Name AS ToName, ToSurvey.IsDisabled AS IsToDisabled FROM Messages "
                                            + "INNER JOIN Surveys AS FromSurvey ON FromSurvey.SurveyId = Messages.FromSurveyId "
                                            + "INNER JOIN Surveys AS ToSurvey ON ToSurvey.SurveyId = Messages.ToSurveyId WHERE MessageId = @MessageId";
            const string sqlMarkAsRead = "UPDATE Messages SET ReadDate = getdate() WHERE MessageId = @MessageId";
            using (var cn = Config.CreateConnection())
            {
                var sm = cn.Query<SendMessage>(sqlSelectMessage, new { MessageId = messageId }).Single();
                if (sm != null)
                {
                    cn.Execute(sqlMarkAsRead, new { MessageId = messageId });
                }

                return sm;
            }
        }

        public void DeleteSupportMessage(int messageId)
        {
            const string sqlSoftDeleteMessage = "UPDATE Messages SET IsDeleted = 1 WHERE MessageId = @MessageId AND (ToSurveyId = @SupportAnonSurveyId OR ToSurveyId = @SupportSurveyId)";
            using (var cn = Config.CreateConnection())
            {
                cn.Execute(sqlSoftDeleteMessage, new { MessageId = messageId, SupportAnonSurveyId = _dataCache.SupportAnonSurveyId, SupportSurveyId = _dataCache.SupportSurveyId });
            }
        }
    }
}