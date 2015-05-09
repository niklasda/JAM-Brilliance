using System.Collections.Generic;
using System.Linq;
using Dapper;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class MessageDataService : IMessageDataService
    {
        protected readonly IDatabaseConfigurationService Config;

        public MessageDataService(IDatabaseConfigurationService configurationService)
        {
            Config = configurationService;
        }

        public bool SendMessage(SendMessage sm)
        {
            const string sqlInsertMessage = "INSERT INTO Messages (FromSurveyId, ToSurveyId, Subject, Body) VALUES (@FromSurveyId, @ToSurveyId, '-', @Body)";
            using (var cn = Config.CreateConnection())
            {
                int nbrOfRows = cn.Execute(sqlInsertMessage, sm);
                return nbrOfRows == 1;
            }
        }

        public SendMessage ReadMessage(int surveyId, int messageId)
        {
            const string sqlSelectMessage = "SELECT Messages.*, FromSurvey.Name AS FromName, FromSurvey.IsDisabled AS IsFromDisabled, ToSurvey.Name AS ToName, ToSurvey.IsDisabled AS IsToDisabled FROM Messages "
                                            + "INNER JOIN Surveys AS FromSurvey ON FromSurvey.SurveyId = Messages.FromSurveyId "
                                            + "INNER JOIN Surveys AS ToSurvey ON ToSurvey.SurveyId = Messages.ToSurveyId WHERE MessageId = @MessageId AND (ToSurveyId = @ToSurveyId OR FromSurveyId = @ToSurveyId)";
            const string sqlMarkAsRead = "UPDATE Messages SET ReadDate = getdate() WHERE MessageId = @MessageId AND ToSurveyId = @ToSurveyId";
            using (var cn = Config.CreateConnection())
            {
                var sm = cn.Query<SendMessage>(sqlSelectMessage, new { MessageId = messageId, ToSurveyId = surveyId }).Single();
                if (sm != null)
                {
                    cn.Execute(sqlMarkAsRead, new { MessageId = messageId, ToSurveyId = surveyId });
                }

                return sm;
            }
        }

        public void DeleteMessage(int messageId, int surveyId)
        {
            const string sqlSoftDeleteMessage = "UPDATE Messages SET IsDeleted = 1 WHERE MessageId = @MessageId AND ToSurveyId = @ToSurveyId";
            using (var cn = Config.CreateConnection())
            {
                cn.Execute(sqlSoftDeleteMessage, new { MessageId = messageId, ToSurveyId = surveyId });
            }
        }

        public IEnumerable<SendMessage> GetReceivedMessages(int toSurveyId)
        {
            const string sqlGetMessages = "SELECT Messages.*, FromSurvey.Name AS FromName, FromSurvey.IsDisabled AS IsFromDisabled FROM Messages "
                                          + "INNER JOIN Surveys AS FromSurvey ON FromSurvey.SurveyId = Messages.FromSurveyId WHERE IsDeleted = 0 AND Messages.ToSurveyId = @ToSurveyId";
            using (var cn = Config.CreateConnection())
            {
                IEnumerable<SendMessage> messages = cn.Query<SendMessage>(sqlGetMessages, new { ToSurveyId = toSurveyId });
                return messages;
            }
        }

        public int GetUnreadMessagesCount(int surveyId)
        {
            const string sqlGetUnreadMessagesCount = "SELECT COUNT(*) FROM Messages WHERE IsDeleted = 0 AND ReadDate IS NULL AND ToSurveyId = @ToSurveyId";
            using (var cn = Config.CreateConnection())
            {
                int unreadMessageCount = cn.Query<int>(sqlGetUnreadMessagesCount, new { ToSurveyId = surveyId }).SingleOrDefault();
                return unreadMessageCount;
            }
        }

        public IEnumerable<SendMessage> GetSentMessages(int fromSurveyId)
        {
            const string sqlGetSentMessages = "SELECT Messages.*, ToSurvey.Name AS ToName, ToSurvey.IsDisabled AS IsToDisabled FROM Messages "
                                              + "INNER JOIN Surveys AS ToSurvey ON ToSurvey.SurveyId=Messages.ToSurveyId WHERE IsDeleted=0 AND Messages.FromSurveyId = @FromSurveyId";
            using (var cn = Config.CreateConnection())
            {
                IEnumerable<SendMessage> messages = cn.Query<SendMessage>(sqlGetSentMessages, new { FromSurveyId = fromSurveyId });
                return messages;
            }
        }
    }
}