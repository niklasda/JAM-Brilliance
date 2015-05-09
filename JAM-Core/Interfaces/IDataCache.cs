using System.Collections.Generic;

using JAM.Core.Models.Hard;

namespace JAM.Core.Interfaces
{
    public interface IDataCache
    {
        IEnumerable<KidCount> Kids { get; }

        IEnumerable<KidWantedCount> KidsWanted { get; }

        IEnumerable<Referrer> Referrers { get; }

        IEnumerable<WantedKidWantedCount> WantedKidsWanted { get; }

        IEnumerable<DatePackage> DatePackages { get; }

        IEnumerable<WhatSearchingForWhat> WhatSearchingForWhats { get; }

        int DefaultDatePackageId { get; }

        int AnonSurveyId { get; }

        int SupportSurveyId { get; }

        int SupportAnonSurveyId { get; }

        int AdminSurveyId { get; }

        string AdminSurveyName { get; }

        int DevAdminSurveyId { get; }

        string DevAdminSurveyName { get; }

        int BroadcastSurveyId { get; }
    }
}