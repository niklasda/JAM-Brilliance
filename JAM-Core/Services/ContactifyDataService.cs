using System.Collections.Generic;
using System.Linq;
using Dapper;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class ContactifyDataService : IContactifyDataService
    {
        private readonly IDatabaseConfigurationService _config;
        private readonly IMessageDataService _messageDataService;

        public ContactifyDataService(IDatabaseConfigurationService configurationService, IMessageDataService messageDataService)
        {
            _config = configurationService;
            _messageDataService = messageDataService;
        }

        public IEnumerable<ConversationHead> GetConversationHeads(int surveyId)
        {
            const string sqlSelectHeads = "SELECT A.*, Surveys.IsDisabled, Surveys.Name AS OtherName FROM " +
                                             "(SELECT 1 AS Category, FromSurveyid AS OtherSurveyId, COUNT(FromSurveyid) AS NbrOf, MAX(COALESCE(ReadDate, SendDate)) AS LastActionDate, SUM(CASE WHEN ReadDate IS NULL THEN 1 ELSE 0 END) NbrOfUnread FROM Messages WHERE ToSurveyId = @SelfSurveyId GROUP BY FromSurveyid " +
                                             "UNION " +
                                             "SELECT 2 AS Category, ToSurveyId AS OtherSurveyId, COUNT(ToSurveyId) AS NbrOf, MAX(COALESCE(ReadDate, SendDate)) AS LastActionDate, 0 AS NbrOfUnread FROM Messages WHERE FromSurveyid = @SelfSurveyId GROUP BY ToSurveyId " +
                                             "UNION " +
                                             "SELECT 3 AS Category, OtherSurveyId AS OtherSurveyId, 0 AS NbrOf, AddedDate AS LastActionDate, 0 AS NbrOfUnread FROM Favourites WHERE SelfSurveyId = @SelfSurveyId " +
                                             "UNION " +
                                             "SELECT 4 AS Category, SelfSurveyId AS OtherSurveyId, 0 AS NbrOf, AddedDate AS LastActionDate, 0 AS NbrOfUnread FROM Favourites WHERE OtherSurveyId = @SelfSurveyId) " +
                                             "AS A INNER JOIN Surveys ON A.OtherSurveyId = Surveys.SurveyId ORDER BY Category";

            using (var cn = _config.CreateConnection())
            {
                var heads = cn.Query<ConversationHead>(sqlSelectHeads, new { SelfSurveyId = surveyId });

                return heads;
            }
        }

        public Conversation GetConversation(int surveyId, int otherSurveyId)
        {
            const string sqlSelectSurveyName = "SELECT Name FROM Surveys WHERE SurveyId = @SurveyId";

            const string sqlSelectMessages = "SELECT Messages.*, FromSurvey.Name AS FromName, FromSurvey.IsDisabled AS IsFromDisabled, ToSurvey.Name AS ToName, ToSurvey.IsDisabled AS IsToDisabled FROM Messages "
                                            + "INNER JOIN Surveys AS FromSurvey ON FromSurvey.SurveyId = Messages.FromSurveyId "
                                            + "INNER JOIN Surveys AS ToSurvey ON ToSurvey.SurveyId = Messages.ToSurveyId " 
                                            + "WHERE Messages.FromSurveyId = @FromSurveyId AND Messages.ToSurveyId = @ToSurveyId";

            using (var cn = _config.CreateConnection())
            {
                string otherSurveyName = cn.Query<string>(sqlSelectSurveyName, new { SurveyId = otherSurveyId }).SingleOrDefault();

                var fromSelf = cn.Query<SendMessage>(sqlSelectMessages, new { FromSurveyId = surveyId, ToSurveyId = otherSurveyId });
                var fromOther = cn.Query<SendMessage>(sqlSelectMessages, new { FromSurveyId = otherSurveyId, ToSurveyId = surveyId });

                var cvm = new Conversation();
                cvm.OriginalMessage = null;
                cvm.OtherSurveyName = otherSurveyName;
                cvm.Messages = fromSelf.Union(fromOther).OrderBy(x => x.SendDate);

                return cvm;
            }
        }

        public Conversation GetConversationTail(int surveyId, int otherSurveyId, int lastMessageId)
        {
            const string sqlSelectSurveyName = "SELECT Name FROM Surveys WHERE SurveyId = @SurveyId";

            const string sqlSelectMessages = "SELECT Messages.*, FromSurvey.Name AS FromName, FromSurvey.IsDisabled AS IsFromDisabled, ToSurvey.Name AS ToName, ToSurvey.IsDisabled AS IsToDisabled FROM Messages "
                                            + "INNER JOIN Surveys AS FromSurvey ON FromSurvey.SurveyId = Messages.FromSurveyId "
                                            + "INNER JOIN Surveys AS ToSurvey ON ToSurvey.SurveyId = Messages.ToSurveyId " 
                                            + "WHERE Messages.FromSurveyId = @FromSurveyId AND Messages.ToSurveyId = @ToSurveyId AND Messages.MessageId > @LastMessageId";

            using (var cn = _config.CreateConnection())
            {
                string otherSurveyName = cn.Query<string>(sqlSelectSurveyName, new { SurveyId = otherSurveyId }).SingleOrDefault();

                var fromSelf = cn.Query<SendMessage>(sqlSelectMessages, new { FromSurveyId = surveyId, ToSurveyId = otherSurveyId, LastMessageId = lastMessageId });
                var fromOther = cn.Query<SendMessage>(sqlSelectMessages, new { FromSurveyId = otherSurveyId, ToSurveyId = surveyId, LastMessageId = lastMessageId });

                var cvm = new Conversation();
                cvm.OriginalMessage = null;
                cvm.OtherSurveyName = otherSurveyName;
                cvm.Messages = fromSelf.Union(fromOther).OrderBy(x => x.SendDate);

                return cvm;
            }
        }
    }
}