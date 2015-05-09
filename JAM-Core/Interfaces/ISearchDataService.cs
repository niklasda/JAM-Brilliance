using System.Collections.Generic;
using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface ISearchDataService
    {
        IEnumerable<SearchResult> AdvancedSearch(SearchAdvCriteria advancedCriteria, Survey survey);

        IEnumerable<SearchResult> SimpleSearch(SearchCriteria criteria, Survey survey);

        IEnumerable<SearchResult> SimplerSearch(SearchCriteria criteria);
    }
}