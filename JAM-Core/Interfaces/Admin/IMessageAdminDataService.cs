using JAM.Core.Models;

namespace JAM.Core.Interfaces.Admin
{
    public interface IMessageAdminDataService : IMessageDataService
    {
        Conversation ReadConversation(int messageId);

        SendMessage ReadSupportMessage(int messageId);
        
        void DeleteSupportMessage(int messageId);

        bool BroadcastMessage(SendMessage sendMessage);
    }
}