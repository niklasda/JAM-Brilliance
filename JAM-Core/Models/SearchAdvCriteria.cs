using JAM.Core.Logic;

namespace JAM.Core.Models
{
    public class SearchAdvCriteria
    {
        public string City { get; set; }

        public string CityPattern { get { return string.Format("%{0}%", City); } }

        public int OriginId { get { return DataCache.Origins.JamDating; } }

        public int WhatSearchingForWhatId { get; set; }
    }
}