using System.Collections.Generic;
using JAM.Brilliance.Models.ViewModels;

namespace JAM.Brilliance.Areas.Mobile.Models.Response
{
    public class SearchResultModel : ResponseBase
    {
        public IEnumerable<SearchResultViewModel> SearchResults { get; set; }
    }
}