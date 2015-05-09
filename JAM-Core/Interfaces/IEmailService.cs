using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface IEmailService
    {
        bool SendVerificationMail(string userName, string email, string activationUrl);

        bool SendReminderMail(string email, string reminderUrl);

        string PrependBody(string body);

        string GetYear(Survey survey);
    }
}