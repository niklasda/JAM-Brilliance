//using System;
//using System.Net;
//using System.Net.Mail;
//using System.Text;
//using JAM.Core.Interfaces;
//using JAM.Core.Logic;
//using JAM.Core.Models;
//using JetBrains.Annotations;

//namespace JAM.Core.Services
//{
//    [UsedImplicitly]
//    public class EmailService : IEmailService
//    {
//        public string PrependBody(string body)
//        {
//            return string.Format("{0}{0}----------{0}{1}", Environment.NewLine, body);
//        }

//        public string GetYear(Survey survey)
//        {
//            if (survey.Birth.HasValue)
//            {
//                return survey.Birth.Value.ToString("yy");
//            }

//            return "--";
//        }

//        public bool SendVerificationMail(string userName, string email, string activationUrl)
//        {
//            var mailMessage = new MailMessage();

//            mailMessage.From = new MailAddress(Constants.SenderEmailAddress, "Brilliance Dating");
//            mailMessage.Subject = "Verifiera din anmälan";
//            mailMessage.BodyEncoding = Encoding.UTF8;
//            mailMessage.IsBodyHtml = true;

//            mailMessage.Body = string.Format("<div>Hej {0}<br />Verifiera din e-post adress och aktivera ditt konto hos Brilliance Dating genom att <a href=\"{1}\">klicka här</a>.<br /><br />Vänliga hälsningar<br />J&A</div>", userName, activationUrl);

//            mailMessage.To.Add(new MailAddress(email, userName));
//            mailMessage.Bcc.Add(new MailAddress("brilliancedating+register@gmail.com", "Brilliance"));
//            mailMessage.Bcc.Add(new MailAddress("niklas@dahlman.biz", "Admin"));

//            return SendMail(mailMessage);
//        }

//        public bool SendReminderMail(string email, string reminderUrl)
//        {
//            var mailMessage = new MailMessage();

//            mailMessage.From = new MailAddress(Constants.SenderEmailAddress, "Brilliance Dating");
//            mailMessage.Subject = "Påminnelse";
//            mailMessage.BodyEncoding = Encoding.UTF8;
//            mailMessage.IsBodyHtml = true;

//            mailMessage.Body = string.Format("<div>Hej {0}<br />Få reda på ditt bortglömda lösenord hos Brilliance Dating genom att <a href=\"{1}\">klicka här</a>.<br /><br />Vänliga hälsningar<br />J&A</div>", email, reminderUrl);

//            mailMessage.To.Add(new MailAddress(email));
//            mailMessage.Bcc.Add(new MailAddress("brilliancedating+remind@gmail.com", "Brilliance"));
//            mailMessage.Bcc.Add(new MailAddress("niklas@dahlman.biz", "Admin"));

//            return SendMail(mailMessage);
//        }

//        private bool SendMail(MailMessage mailMessage)
//        {
//            var smtp = new SmtpClient("smtp.gmail.com", 25);
//            smtp.EnableSsl = true;
//            smtp.UseDefaultCredentials = false;
//            smtp.Credentials = new NetworkCredential("brilliancedating@gmail.com", "Brilliance1234");

//            try
//            {
//                smtp.Send(mailMessage);
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }
//    }
//}