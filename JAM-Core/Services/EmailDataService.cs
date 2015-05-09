using System;
using System.Data;
using System.Linq;
using Dapper;

using JAM.Core.Interfaces;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class EmailDataService : IEmailDataService
    {
        private readonly IDatabaseConfigurationService _c;

        public EmailDataService(IDatabaseConfigurationService configurationService)
        {
            _c = configurationService;
        }

        public string VerifyEmail(Guid guid)
        {
            const string sqlGetVerificationEmail = "SELECT Email FROM EmailVerification WHERE uid = @uid";
            const string sqlUpdateVerification = "UPDATE EmailVerification SET IsVerified = 1, VerifiedDate = getdate() WHERE uid = @uid";

            using (var cn = _c.CreateConnection())
            {
                var email = cn.Query<string>(sqlGetVerificationEmail, new { uid = guid }).FirstOrDefault();
                if (!string.IsNullOrEmpty(email))
                {
                    cn.Execute(sqlUpdateVerification, new { uid = guid });
                    return email;
                }
            }

            return string.Empty;
        }

        public Guid GetVerificationGuid(string email)
        {
            const string sqlGetVerificationUid = "SELECT uid FROM EmailVerification WHERE Email = @Email";

            using (var cn = _c.CreateConnection())
            {
                var guid = cn.Query<Guid>(sqlGetVerificationUid, new { Email = email }).FirstOrDefault();
                return guid;
            }
        }

        public string RemindPasswordEmail(Guid guid)
        {
            const string sqlGetReminderEmail = "SELECT Email FROM PasswordReminders WHERE uid = @uid";
            const string sqlUpdateReminder = "UPDATE PasswordReminders SET IsUsed = 1, UsedDate = getdate() WHERE uid = @uid";

            using (var cn = _c.CreateConnection())
            {
                var email = cn.Query<string>(sqlGetReminderEmail, new { uid = guid }).FirstOrDefault();
                if (!string.IsNullOrEmpty(email))
                {
                    cn.Execute(sqlUpdateReminder, new { uid = guid });
                    return email;
                }
            }

            return string.Empty;
        }

        public Guid StoreVerificationGuid(string email, Guid guid)
        {
            const string sqlInsertVerification = "INSERT INTO EmailVerification (uid, Email, IsVerified) VALUES (@uid, @Email, 0)";

            using (var cn = _c.CreateConnection())
            {
                var existingGuid = VerificationGuidExists(cn, email, guid);
                if (existingGuid == guid)
                {
                    cn.Execute(sqlInsertVerification, new { Email = email, uid = guid });
                }

                return existingGuid;
            }
        }

        public Guid StoreReminderGuid(string email, Guid guid)
        {
            const string sqlInsertReminder = "INSERT INTO PasswordReminders (uid, Email, IsUsed) VALUES (@uid, @Email, 0)";

            using (var cn = _c.CreateConnection())
            {
                var existingGuid = ReminderGuidExists(cn, email, guid);
                if (existingGuid == guid)
                {
                    cn.Execute(sqlInsertReminder, new { Email = email, uid = guid });
                }

                return existingGuid;
            }
        }

        private Guid VerificationGuidExists(IDbConnection cn, string email, Guid guid)
        {
            const string sqlVerificationExists = "SELECT uid FROM EmailVerification WHERE Email = @Email OR uid = @uid";

            var existingGuid = cn.Query<Guid>(sqlVerificationExists, new { Email = email, uid = guid }).FirstOrDefault();
            return existingGuid != Guid.Empty ? existingGuid : guid;
        }

        private Guid ReminderGuidExists(IDbConnection cn, string email, Guid guid)
        {
            const string sqlReminderExists = "SELECT uid FROM PasswordReminders WHERE Email = @Email OR uid = @uid";

            var existingGuid = cn.Query<Guid>(sqlReminderExists, new { Email = email, uid = guid }).FirstOrDefault();
            return existingGuid != Guid.Empty ? existingGuid : guid;
        }
    }
}