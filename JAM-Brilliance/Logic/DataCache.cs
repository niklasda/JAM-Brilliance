using System.Collections.Generic;

using JAM.Dating.Interfaces;
using JAM.Dating.Models.Hard;

namespace JAM.Dating.Logic
{
    public class DataCache : IDataCache
    {
        public IEnumerable<KidCount> Kids
        {
            get
            {
                return new List<KidCount>
                   {
                       new KidCount(1, "Nej", true),
                       new KidCount(2, "Ja", true)
                   };
            }
        }

        public IEnumerable<KidWantedCount> KidsWanted
        {
            get
            {
                return new List<KidWantedCount>
                         {
                             new KidWantedCount(1, "Spelar ingen roll", true),
                             new KidWantedCount(2, "Nej", true),
                             new KidWantedCount(3, "Ja", true)
                         };
            }
        }

        public IEnumerable<Referrer> Referrers
        {
            get
            {
                return new List<Referrer>
                        {
                            new Referrer(1, "Via sökmotor på nätet", true),
                            new Referrer(2, "Via annons", true),
                            new Referrer(3, "Via Facebook", true),
                            new Referrer(4, "En vän tipsade", true),
                            new Referrer(5, "Anna Dingizian", true),
                            new Referrer(6, "Jeanette Vinberg", true),
                            new Referrer(7, "Ylva Vezzoli", false),
                            new Referrer(8, "Charlotte Aldin", true),
                            new Referrer(9, "Linda Fennhagen", true),
                            new Referrer(10, "Nina von Platen", true),
                            new Referrer(11, "Emelie Persson", true),
                            new Referrer(12, "Nisse Andersson", false),
                            new Referrer(13, "Annat", true)
                        };
            }
        }

        public IEnumerable<WantedKidWantedCount> WantedKidsWanted
        {
            get
            {
                return new List<WantedKidWantedCount>
                               {
                                   new WantedKidWantedCount(1, "Spelar ingen roll", true),
                                   new WantedKidWantedCount(2, "Nej", true),
                                   new WantedKidWantedCount(3, "Ja", true)
                               };
            }
        }

        public int DefaultDatePackageId { get { return 0; } }

        public int AnonSurveyId { get { return 0; } }

        public int SupportSurveyId { get { return 1; } }
        
        public int SupportAnonSurveyId { get { return 2; } }

        public int AdminSurveyId { get { return 3; } }
        
        public string AdminSurveyName { get { return "J&A"; } }

        public int DevAdminSurveyId { get { return 4; } }
        
        public string DevAdminSurveyName { get { return "nd"; } }

        public int BroadcastSurveyId { get { return 5; } }

        public IEnumerable<WhatSearchingForWhat> WhatSearchingForWhats
        {
            get
            {
                return new List<WhatSearchingForWhat>
                           {
                               new WhatSearchingForWhat(1, "Välj nedan", true),
                               new WhatSearchingForWhat(2, "Man söker kvinna", true),
                               new WhatSearchingForWhat(3, "Man söker man", true),
                               new WhatSearchingForWhat(4, "Kvinna söker man", true),
                               new WhatSearchingForWhat(5, "Kvinna söker kvinna", true)
                           };
            }
        }

        public class Origins
        {
            public const int Jam = 1;

            public const int JamDating = 2;
        }
    }
}