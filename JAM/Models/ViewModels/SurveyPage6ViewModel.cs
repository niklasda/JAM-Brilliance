using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using JAM.Core.Models.Hard;

namespace JAM.Models.ViewModels
{
    public class SurveyPage6ViewModel : SurveyViewModelBase
    {
        public WantedSurveyViewModel WantedSurvey { get; set; }

        [Display(Name = "Har barn")]
        public int KidsCountId { get; set; }

        [Display(Name = "Vill ha barn")]
        public int KidsWantedCountId { get; set; }

        [Display(Name = "Vill ha barn")]
        public int WantedKidsWantedCountId { get; set; }

        [Display(Name = "Välj typ av medlemskap")]
        public int DatePackageId { get; set; }

        [Display(Name = "Hur kom du i kontakt med J&A Dating?")]
        public int ReferrerId { get; set; }

        public IEnumerable<WantedKidWantedCount> WantedKidsWantedCounts { get; set; }

        public IEnumerable<Referrer> Referrers { get; set; }
    }
}