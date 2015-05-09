using JAM.Core.Logic;

namespace JAM.Brilliance.Models.ViewModels
{
    public class MyPageViewModel
    {
        public bool IsSurveyComplete { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsMember { get; set; }
     
        public bool IsAdmin { get; set; }
        
        public bool IsSearchigForMale { get; set; }

        public PageIds CurrentPageId { get; set; }
    }
}