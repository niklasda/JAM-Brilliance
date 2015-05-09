using System;

namespace JAM.Core.Interfaces
{
    public interface IEmailDataService
    {
        Guid StoreVerificationGuid(string email, Guid guid);

        string VerifyEmail(Guid guid);

        Guid GetVerificationGuid(string email);

        Guid StoreReminderGuid(string email, Guid guid);

        string RemindPasswordEmail(Guid guid);
    }
}