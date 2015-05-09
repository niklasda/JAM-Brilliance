using System;
using System.Net;
using System.Net.Mail;
using System.Text;

using JAM.Models;

namespace JAM.Logic
{
    public static class Mailer
    {
        internal const int PlatinumId = 3;
        internal const int GoldId = 2;
        internal const int BaseId = 1;
        internal const int CustomId = 0;

        public static string BuildHtmlText(int packageId, int id)
        {
            string message = BuildText(packageId, id);
            return message.Replace(Environment.NewLine, "<br />");
        }

        public static void SendMail(Survey survey, string machine)
        {
            if (!machine.ToLower().Contains("homebrew"))
            {
                try
                {
                    int id = survey.SurveyId;
                    string name = survey.Name;
                    string email = survey.Email;
                    int packageId = survey.DatePackageId;

                    var mailMessage = new MailMessage();

                    mailMessage.From = new MailAddress(Constants.SenderEmailAddress, "J&A Matchmaking");
                    mailMessage.Subject = "Tack för din anmälan";
                    mailMessage.BodyEncoding = Encoding.UTF8;

                    string message = BuildMailText(packageId, name, id);

                    mailMessage.Body = message;

                    mailMessage.To.Add(new MailAddress(email, name));
                    mailMessage.Bcc.Add(new MailAddress("ja@dahlman.biz", "J&A"));
                    mailMessage.Bcc.Add(new MailAddress("niklas@dahlman.biz", "Niklas"));

                    bool ok = SendMail(mailMessage);
                }
                catch (Exception ex)
                {
                    throw new Exception(machine, ex);
                }
            }
        }

        public static string GetYear(Survey survey)
        {
            if (survey.Birth.Year != 1900)
            {
                return survey.Birth.Year.ToString().Substring(2, 2);
            }

            return "--";
        }

        public static string PackageCode(Survey survey)
        {
            if (survey.DatePackage != null)
            {
                return survey.DatePackage.Name.Substring(0, 1);
            }

            return "-";
        }

        internal static string BuildMailText(int packageId, string name, int id)
        {
            string message = BuildText(packageId, id);
            return string.Format("Hej {0}.{1}{2}", name, Environment.NewLine, message);
        }

        private static string BuildText(int packageId, int id)
        {
            const string BestChoise = "Du har valt ";
            const string PaymentInfo = " Så snart betalning skett till bg: 839-2714 så kommer vi kontakta dig för att boka en personlig intervju.";
            const string CustomPackage = "Vi kommer kontakta dig inom 1 vecka för att komma överens om ett möte där vi kan skräddarsy tjänsten utifrån just dina behov.";
            const string BestService = "Välkommen till Sveriges bästa matchmakingservice!";
            const string LookingFwd = "Vi ser fram emot att få träffa dig!";
            const string BestRegards = "Med vänliga hälsningar";
            const string UnderWriters = "Anna och Jeanette";

            string payment = string.Empty;
            if (packageId == PlatinumId)
            {
                payment += BestChoise + "platinapaketet för 12 000:-" + PaymentInfo + Environment.NewLine;

                payment += Environment.NewLine + BestService + Environment.NewLine;

                payment += Environment.NewLine + LookingFwd + Environment.NewLine + Environment.NewLine;

                payment += BestRegards + Environment.NewLine;
                payment += UnderWriters + Environment.NewLine;
            }
            else if (packageId == GoldId)
            {
                payment += BestChoise + "guldpaketet för 6 000:-" + PaymentInfo + Environment.NewLine;

                payment += Environment.NewLine + BestService + Environment.NewLine;

                payment += Environment.NewLine + LookingFwd + Environment.NewLine + Environment.NewLine;

                payment += BestRegards + Environment.NewLine;
                payment += UnderWriters + Environment.NewLine;
            }
            else if (packageId == BaseId)
            {
                payment += BestChoise + "baspaketet för 3 500:-" + PaymentInfo + Environment.NewLine;

                payment += Environment.NewLine + BestService + Environment.NewLine;

                payment += Environment.NewLine + LookingFwd + Environment.NewLine + Environment.NewLine;

                payment += BestRegards + Environment.NewLine;
                payment += UnderWriters + Environment.NewLine;
            }
            else if (packageId == CustomId)
            {
                payment += CustomPackage + Environment.NewLine;

                payment += Environment.NewLine + BestService + Environment.NewLine;

                payment += Environment.NewLine + LookingFwd + Environment.NewLine + Environment.NewLine;

                payment += BestRegards + Environment.NewLine;
                payment += UnderWriters + Environment.NewLine;
            }

            string message = string.Format(
                "Tack för din anmälan till J&A Matchmaking!{0}Ditt kundnummer är {1}{2}{3}", Environment.NewLine, id, Environment.NewLine, payment);

            return message;
        }

        private static bool SendMail(MailMessage mailMessage)
        {
            var smtp = new SmtpClient("smtp.gmail.com", 25);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("jamatchmaking@gmail.com", "Matchmaking123");

            try
            {
                smtp.Send(mailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}