using System.Collections.Generic;
using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface IContactifyDataService
    {
        IEnumerable<ConversationHead> GetConversationHeads(int surveyId);

        Conversation GetConversation(int surveyId, int otherSurveyId);
        
        Conversation GetConversationTail(int selfSurveyId, int otherSurveyId, int lastMessageId);
    }
}