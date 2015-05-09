using JAM.Brilliance.Models.ViewModels;
using JAM.Core.Models;

namespace JAM.Brilliance.Areas.Mobile.Models.Response
{
    public class SurveyModel : ResponseBase
    {
        public ShortSurveyViewModel Survey{ get; set; }
    }
}