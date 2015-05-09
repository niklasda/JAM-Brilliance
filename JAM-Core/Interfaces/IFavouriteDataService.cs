using System.Collections.Generic;
using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface IFavouriteDataService
    {
        IEnumerable<Favourite> GetFavourites(int surveyId);

        bool AddFavourite(Favourite favourite);

        bool RemoveFavourite(Favourite favourite);

        IEnumerable<Favourite> GetFans(int surveyId);

        int GetFansCount(int surveyId);
    }
}