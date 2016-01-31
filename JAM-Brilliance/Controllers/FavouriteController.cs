using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

using AutoMapper;
using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Core.Models;
using JAM.Brilliance.Models.ViewModels;

using PagedList;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class FavouriteController : Controller
    {
        private readonly ISurveyDataService _surveyDataService;
        private readonly IFavouriteDataService _favouriteDataService;
        private readonly IDiagnosticsService _diagnosticsService;
        private readonly IMapper _mapper;

        public FavouriteController(ISurveyDataService surveyDataService, IFavouriteDataService favouriteDataService, IDiagnosticsService diagnosticsService, IMapper mapper)
        {
            _surveyDataService = surveyDataService;
            _favouriteDataService = favouriteDataService;
            _diagnosticsService = diagnosticsService;
            _mapper = mapper;
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult MyFavourites(int? page)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            _diagnosticsService.ThrowUnlessSurveyComplete();

            var fvms = GetMySortedFavourites();

            var pageNumber = page ?? 1;
            var onePageOfFavourites = fvms.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfFavourites = onePageOfFavourites;

            return View(onePageOfFavourites);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult MyFans(int? page)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            _diagnosticsService.ThrowUnlessSurveyComplete();

            var fvms = GetMySortedFans();

            var pageNumber = page ?? 1;
            var onePageOfFans = fvms.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfFans = onePageOfFans;

            return View(onePageOfFans);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public RedirectToRouteResult AddFavourite(int otherSurveyId)
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            var fav = new Favourite();
            fav.SelfSurveyId = surveyId;
            fav.OtherSurveyId = otherSurveyId;

            bool ok = _favouriteDataService.AddFavourite(fav);

            return RedirectToAction("MyFavourites");
        }

        [Authorize(Roles = MemberRoles.Member)]
        public RedirectToRouteResult RemoveFavourite(int favouriteId)
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            var fav = new Favourite();
            fav.SelfSurveyId = surveyId;
            fav.FavouriteId = favouriteId;

            bool ok = _favouriteDataService.RemoveFavourite(fav);

            return RedirectToAction("MyFavourites");
        }

        private IEnumerable<FavouriteViewModel> GetMySortedFavourites()
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            IEnumerable<Favourite> fms = _favouriteDataService.GetFavourites(surveyId);
            var fvms = _mapper.Map<IEnumerable<Favourite>, IList<FavouriteViewModel>>(fms);

            IEnumerable<FavouriteViewModel> smvms = fvms.OrderBy(x => x.OtherName);
            return smvms;
        }

        private IEnumerable<FavouriteViewModel> GetMySortedFans()
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            IEnumerable<Favourite> fms = _favouriteDataService.GetFans(surveyId);
            var fvms = _mapper.Map<IEnumerable<Favourite>, IList<FavouriteViewModel>>(fms);

            IEnumerable<FavouriteViewModel> smvms = fvms.OrderBy(x => x.SelfName);
            return smvms;
        }
    }
}