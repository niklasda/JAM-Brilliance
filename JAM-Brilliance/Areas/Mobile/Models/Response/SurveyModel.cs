using JAM.Brilliance.Models.ViewModels;

namespace JAM.Brilliance.Areas.Mobile.Models.Response
{
    public class SurveyModel : ResponseBase
    {
        public ShortSurveyViewModel Survey { get; set; }
    }
}