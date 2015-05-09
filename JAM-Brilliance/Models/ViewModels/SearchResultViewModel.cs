using System;

namespace JAM.Brilliance.Models.ViewModels
{
    public class SearchResultViewModel
    {
        public int SurveyId { get; set; }

        public string Name { get; set; }

        public string PostalCode { get; set; }

        public int Distance { get; set; }

        public string City { get; set; }

        public DateTime Birth { get; set; }

        public int Height { get; set; }

        public int Age
        {
            get
            {
                int yearDiff = DateTime.Today.Year - Birth.Year;
                return yearDiff;
            }
        }
    }
}