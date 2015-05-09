using System.Collections.Generic;
using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface IMessageDataService
    {
        IEnumerable<SendMessage> GetReceivedMessages(int surveyId);

        SendMessage ReadMessage(int surveyId, int messageId);

        void DeleteMessage(int messageId, int surveyId);

        bool SendMessage(SendMessage sendMessage);

        int GetUnreadMessagesCount(int surveyId);

        IEnumerable<SendMessage> GetSentMessages(int surveyId);
    }
}