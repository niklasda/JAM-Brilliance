using System;
using System.Net;
using System.Net.Mail;
using System.Web.Configuration;
using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Core.Models;
using JetBrains.Annotations;
using SendGrid;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class EmailService : IEmailService
    {
        public string PrependBody(string body)
        {
            return string.Format("{0}{0}----------{0}{1}", Environment.NewLine, body);
        }

        public string GetYear(Survey survey)
        {
            if (survey.Birth.HasValue)
            {
                return survey.Birth.Value.ToString("yy");
            }

            return "--";
        }

        public bool SendVerificationMail(string userName, string email, string activationUrl)
        {
            var mailMessage = new SendGridMessage();

            mailMessage.From = new MailAddress(Constants.SenderEmailAddress, "Brilliance Dating");
            mailMessage.Subject = "Verifiera din anmälan";

            mailMessage.Html = string.Format("<div>Hej {0}<br />Verifiera din e-post adress och aktivera ditt konto hos Brilliance Dating genom att <a href=\"{1}\">klicka här</a>.<br /><br />Vänliga hälsningar<br />J&A</div>", userName, activationUrl);

            mailMessage.To = new[] { new MailAddress(email, userName) };
            mailMessage.Bcc = new[] { new MailAddress("brilliancedating+register@gmail.com", "Brilliance "), new MailAddress("niklas@dahlman.biz", "Admin") };

            return SendMail(mailMessage);
        }

        public bool SendReminderMail(string email, string reminderUrl)
        {
            var mailMessage = new SendGridMessage();

            mailMessage.From = new MailAddress(Constants.SenderEmailAddress, "Brilliance Dating");
            mailMessage.Subject = "Påminnelse";

            mailMessage.Html = string.Format("<div>Hej {0}<br />Få reda på ditt bortglömda lösenord hos Brilliance Dating genom att <a href=\"{1}\">klicka här</a>.<br /><br />Vänliga hälsningar<br />J&A</div>", email, reminderUrl);

            mailMessage.To = new[] { new MailAddress(email) };
            mailMessage.Bcc = new[] { new MailAddress("brilliancedating+remind@gmail.com", "Brilliance "), new MailAddress("niklas@dahlman.biz", "Admin") };

            return SendMail(mailMessage);
        }

        private bool SendMail(SendGridMessage mailMessage)
        {
            if (mailMessage.To.Length == 1 && (mailMessage.To[0].Address.EndsWith("@nida.se") || mailMessage.To[0].Address.EndsWith("@.net")))
            {
                return true;
            }

            try
            {
                var password = WebConfigurationManager.AppSettings[Constants.SendGridPasswordName];

                var credentials = new NetworkCredential("jamatchmaking", password);

                // Create an Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email.
                transportWeb.Deliver(mailMessage);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}