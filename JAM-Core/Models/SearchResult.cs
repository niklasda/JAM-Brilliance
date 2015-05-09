using System;

namespace JAM.Core.Models
{
    public class SearchResult
    {
        public int SurveyId { get; set; }

        public string Name { get; set; }

        public string PostalCode { get; set; }

        public int Distance { get; set; }

        public string City { get; set; }

        public DateTime Birth { get; set; }

        public int Height { get; set; }
    }
}