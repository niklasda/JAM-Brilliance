using System;

using JAM.Core.Logic;

namespace JAM.Core.Models
{
    public class SearchCriteria
    {
        public int SelfSurveyId { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string CityPattern { get { return string.Format("%{0}%", City); } }

        public int AgeMin { get; set; }

        public int AgeMax { get; set; }

        public int HeightMin { get; set; }

        public int HeightMax { get; set; }

        public bool HasKids { get; set; }

        public int StartYear { get { return DateTime.Today.Year - AgeMax; } }

        public int StopYear { get { return DateTime.Today.Year - AgeMin; } }

        public int OriginId { get { return DataCache.Origins.JamDating; } }

        public int WhatSearchingForWhatId { get; set; }
    }
}