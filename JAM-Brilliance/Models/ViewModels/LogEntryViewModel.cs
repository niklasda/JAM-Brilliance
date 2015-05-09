using System;

namespace JAM.Brilliance.Models.ViewModels
{
    public class LogEntryViewModel
    {
        public string Id { get; set; }

        public string HostName { get; set; }

        public string Ip { get; set; }

        public DateTime EventDateTime { get; set; }

        public string EventLevel { get; set; }

        public string UserName { get; set; }

        public string MachineName { get; set; }

        public string EventMessage { get; set; }

        public string ErrorSource { get; set; }

        public string ErrorClass { get; set; }

        public string ErrorMethod { get; set; }

        public string ErrorMessage { get; set; }

        public string InnerErrorMessage { get; set; }
    }
}