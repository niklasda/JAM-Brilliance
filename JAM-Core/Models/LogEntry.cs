using System;
using System.Net;
using JAM.Core.Logic;
using Microsoft.Data.Edm.Library.Values;

namespace JAM.Core.Models
{
    public class LogEntry
    {
        public string Id { get; set; }

        public string HostName
        {
            get
            {
                try
                {
                    return Dns.GetHostEntry(Ip).HostName;
                }
                catch (Exception)
                {
                    return Ip;
                }
            }
        }

        private string Ip 
        {
            get
            {
                if (EventMessage.Contains(Constants.IpPrefix))
                {
                    return EventMessage.Substring(EventMessage.IndexOf(Constants.IpPrefix, StringComparison.CurrentCulture));
                }

                return string.Empty;
            }
        }

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