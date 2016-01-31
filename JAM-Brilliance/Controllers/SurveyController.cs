using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

using AutoMapper;
using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Core.Models;
using JAM.Brilliance.Models.ViewModels;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class SurveyController : Controller
    {
        private readonly IPictureDataService _pictureDataService;
        private readonly ISurveyDataService _surveyDataService;
        private readonly IAccountService _accountService;
        private readonly IVisitorDataService _historyService;
        private readonly IGeoService _geoService;
        private readonly IDataCache _dataCache;
        private readonly IMapper _mapper;
        public SurveyController(ISurveyDataService surveyDataService, IPictureDataService pictureDataService, IAccountService accountService, IVisitorDataService visitorDataService, IGeoService geoService, IDataCache dataCache, IMapper mapper)
        {
            _pictureDataService = pictureDataService;
            _surveyDataService = surveyDataService;
            _accountService = accountService;
            _historyService = visitorDataService;
            _geoService = geoService;
            _dataCache = dataCache;
            _mapper = mapper;
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult Details(int surveyId)
        {
            ViewBag.CurrentPageId = PageIds.DevPage;

            Survey survey = _surveyDataService.GetSurvey(surveyId);

            var svm = new SurveyViewModel();
            svm.Page1 =_mapper.Map<SurveyPage1ViewModel>(survey);
            svm.Page2 =_mapper.Map<SurveyPage2ViewModel>(survey);
            svm.Page3 =_mapper.Map<SurveyPage3ViewModel>(survey);
            svm.Page4 =_mapper.Map<SurveyPage4ViewModel>(survey);
            svm.Page5 =_mapper.Map<SurveyPage5ViewModel>(survey);
            svm.Page6 =_mapper.Map<SurveyPage6ViewModel>(survey);
            svm.SetAsReadOnly();

            svm.Page1.KidsCounts = _dataCache.Kids;
            svm.Page1.KidsWantedCounts = _dataCache.KidsWanted;
            svm.Page1.WhatSearchingForWhats = _dataCache.WhatSearchingForWhats;
            svm.Page6.WantedKidsWantedCounts = _dataCache.WantedKidsWanted;
            svm.Page6.Referrers = _dataCache.Referrers;

            return View(svm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult ShortDetails(int surveyId)
        {
            var selfSurveyId = _surveyDataService.GetCurrentUserSurveyId();

            Task.Run(() => _historyService.AddVisitor(new HistoryEntry { SelfSurveyId = selfSurveyId, OtherSurveyId = surveyId }));

            var survey = _surveyDataService.GetSurvey(surveyId);
            var ssvm = _mapper.Map<ShortSurveyViewModel>(survey);
            ssvm.LastActivityDate = _accountService.GetCurrentUserLastActivity(survey.Email);

            return View(ssvm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult MyShortDetails()
        {
            HttpContext.Response.Cookies.Set(new HttpCookie(Constants.CultureCookieName, _accountService.GetCurrentUserCommentCultureCode()));

            var survey = _surveyDataService.GetCurrentUserSurvey();
            var ssvm = _mapper.Map<ShortSurveyViewModel>(survey);
            ssvm.LastActivityDate = _accountService.GetCurrentUserLastActivity(survey.Email);
            
            return View(ssvm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult Page1(int? surveyId)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            Survey survey = GetAllowedSurvey(surveyId);
            var sp1Vm = _mapper.Map<SurveyPage1ViewModel>(survey);

            sp1Vm.KidsCounts = _dataCache.Kids;
            sp1Vm.KidsWantedCounts = _dataCache.KidsWanted;
            sp1Vm.WhatSearchingForWhats = _dataCache.WhatSearchingForWhats;

            return View(sp1Vm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        [ValidateAntiForgeryToken]
        public ActionResult Page1(SurveyPage1ViewModel sp1Vm, string direction)
        {
            if (ModelState.IsValid)
            {
                sp1Vm.PostalCode = sp1Vm.PostalCode.Replace(" ", "");
                var partSurvey = GetAllowedPartSurvey(sp1Vm);

                if (_surveyDataService.SavePage1(partSurvey))
                {
                    Task.Run(() => _geoService.LookupAndStoreGeoCoordinates(partSurvey.SurveyId, partSurvey.PostalCode, partSurvey.City, partSurvey.Country));
                }

                HttpPostedFileBase pictureFile = Request.Files["OriginalLocation"];

                if (pictureFile != null && pictureFile.ContentLength > 0)
                {
                    var pic = _pictureDataService.LoadBytesIntoPicture(pictureFile);
                    if (pic != null)
                    {
                        pic.SurveyId = partSurvey.SurveyId;
                        int pictureId = _pictureDataService.SaveMainPicture(pic);
                    }
                    else
                    {
                        int length = pictureFile.ContentLength;
                        string ftype = pictureFile.ContentType;
                        int kiloLength = length / 1000;
                        string ftypeEnd = ftype != null && ftype.IndexOf('/') > 0 ? ftype.Substring(ftype.IndexOf('/') + 1) : "-";

                        var fel = string.Format("Bilden får vara max 1500kb stor och måste vara jpeg eller png.{0}Din bild var {1}kb och {2}", Environment.NewLine, kiloLength, ftypeEnd);
                        ModelState.AddModelError("PictureDummyId", fel);
                        sp1Vm.KidsCounts = _dataCache.Kids;
                        sp1Vm.KidsWantedCounts = _dataCache.KidsWanted;
                        sp1Vm.WhatSearchingForWhats = _dataCache.WhatSearchingForWhats;

                        return View(sp1Vm);
                    }
                }

                if (direction.Equals(PageButtons.NextPage))
                {
                    return RedirectToAction("Page2", new { surveyId = sp1Vm.SurveyId });
                }

                return RedirectToAction("Page1", new { surveyId = sp1Vm.SurveyId });
            }

            sp1Vm.KidsCounts = _dataCache.Kids;
            sp1Vm.KidsWantedCounts = _dataCache.KidsWanted;
            sp1Vm.WhatSearchingForWhats = _dataCache.WhatSearchingForWhats;

            return View(sp1Vm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ActionResult Page2(int? surveyId)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            Survey survey = GetAllowedSurvey(surveyId);
            if (survey.SurveyId == 0)
            {
                return RedirectToAction("Page1", new { surveyId = surveyId });
            }

            var sp2Vm = _mapper.Map<SurveyPage2ViewModel>(survey);

            return View(sp2Vm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        [ValidateAntiForgeryToken]
        public ActionResult Page2(SurveyPage2ViewModel sp2Vm, string direction)
        {
            if (ModelState.IsValid)
            {
                var partSurvey = GetAllowedPartSurvey(sp2Vm);

                bool ok = _surveyDataService.SavePage2(partSurvey);

                if (direction.Equals(PageButtons.PreviousPage))
                {
                    return RedirectToAction("Page1", new { surveyId = sp2Vm.SurveyId });
                }

                return RedirectToAction("Page3", new { surveyId = sp2Vm.SurveyId });
            }

            return View(sp2Vm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ActionResult Page3(int? surveyId)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            Survey survey = GetAllowedSurvey(surveyId);
            if (survey.SurveyId == 0)
            {
                return RedirectToAction("Page1", new { surveyId = surveyId });
            }

            var sp3Vm = _mapper.Map<SurveyPage3ViewModel>(survey);

            return View(sp3Vm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        [ValidateAntiForgeryToken]
        public ActionResult Page3(SurveyPage3ViewModel sp3Vm, string direction)
        {
            if (ModelState.IsValid)
            {
                var partSurvey = GetAllowedPartSurvey(sp3Vm);

                bool ok = _surveyDataService.SavePage3(partSurvey);
                if (direction.Equals(PageButtons.PreviousPage))
                {
                    return RedirectToAction("Page2", new { surveyId = sp3Vm.SurveyId });
                }

                return RedirectToAction("Page4", new { surveyId = sp3Vm.SurveyId });
            }

            return View(sp3Vm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ActionResult Page4(int? surveyId)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            Survey survey = GetAllowedSurvey(surveyId);
            if (survey.SurveyId == 0)
            {
                return RedirectToAction("Page1", new { surveyId = surveyId });
            }

            var sp4Vm = _mapper.Map<SurveyPage4ViewModel>(survey);

            return View(sp4Vm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        [ValidateAntiForgeryToken]
        public ActionResult Page4(SurveyPage4ViewModel sp4Vm, string direction)
        {
            if (ModelState.IsValid)
            {
                var partSurvey = GetAllowedPartSurvey(sp4Vm);

                bool ok = _surveyDataService.SavePage4(partSurvey);
                if (direction.Equals(PageButtons.PreviousPage))
                {
                    return RedirectToAction("Page3", new { surveyId = sp4Vm.SurveyId });
                }

                return RedirectToAction("Page5", new { surveyId = sp4Vm.SurveyId });
            }

            return View(sp4Vm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ActionResult Page5(int? surveyId)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            Survey survey = GetAllowedSurvey(surveyId);
            if (survey.SurveyId == 0)
            {
                return RedirectToAction("Page1", new { surveyId = surveyId });
            }

            var sp5Vm = _mapper.Map<SurveyPage5ViewModel>(survey);

            return View(sp5Vm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        [ValidateAntiForgeryToken]
        public ActionResult Page5(SurveyPage5ViewModel sp5Vm, string direction)
        {
            if (ModelState.IsValid)
            {
                var partSurvey = GetAllowedPartSurvey(sp5Vm);

                bool ok = _surveyDataService.SavePage5(partSurvey);
                if (direction.Equals(PageButtons.PreviousPage))
                {
                    return RedirectToAction("Page4", new { surveyId = sp5Vm.SurveyId });
                }

                return RedirectToAction("Page6", new { surveyId = sp5Vm.SurveyId });
            }

            return View(sp5Vm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ActionResult Page6(int? surveyId)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            Survey survey = GetAllowedSurvey(surveyId);
            WantedSurvey wantedSurvey = GetAllowedWantedSurvey(surveyId);

            if (survey.SurveyId == 0)
            {
                return RedirectToAction("Page1", new { surveyId = surveyId });
            }

            var sp6Vm = _mapper.Map<SurveyPage6ViewModel>(survey);
            sp6Vm.WantedSurvey = _mapper.Map<WantedSurveyViewModel>(wantedSurvey);

            sp6Vm.WantedKidsWantedCounts = _dataCache.WantedKidsWanted;
            sp6Vm.Referrers = _dataCache.Referrers;

            return View(sp6Vm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        [ValidateAntiForgeryToken]
        public ActionResult Page6(SurveyPage6ViewModel sp6Vm, string direction)
        {
            if (ModelState.IsValid)
            {
                var partSurvey = GetAllowedPartSurvey(sp6Vm);
                var wantedSurvey = GetAllowedPartWantedSurvey(sp6Vm.WantedSurvey);

                bool ok = _surveyDataService.SavePage6(partSurvey, wantedSurvey);
                if (direction.Equals(PageButtons.PreviousPage))
                {
                    return RedirectToAction("Page5", new { surveyId = sp6Vm.SurveyId });
                }

                return RedirectToAction("Page6", new { surveyId = sp6Vm.SurveyId });
            }

            sp6Vm.WantedKidsWantedCounts = _dataCache.WantedKidsWanted;
            sp6Vm.Referrers = _dataCache.Referrers;

            return View(sp6Vm);
        }

        public RedirectToRouteResult DisableSurvey()
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();
            var hideSurvey = _surveyDataService.HideSurvey(surveyId);
            return RedirectToAction("MyShortDetails", "Survey");
        }

        public RedirectToRouteResult EnableSurvey()
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();
            var hideSurvey = _surveyDataService.UnhideSurvey(surveyId);
            return RedirectToAction("MyShortDetails", "Survey");
        }

        private Survey GetAllowedSurvey(int? surveyId)
        {
            Survey survey;
            if (surveyId.HasValue && surveyId.Value > 0 && User.IsInRole(MemberRoles.Administrator))
            {
                survey = _surveyDataService.GetSurvey(surveyId.Value);
            }
            else
            {
                survey = _surveyDataService.GetCurrentUserSurvey();
            }

            return survey;
        }

        private Survey GetAllowedPartSurvey(SurveyViewModelBase spxvm)
        {
            var partSurvey = _mapper.Map<Survey>(spxvm);

            if (!User.IsInRole(MemberRoles.Administrator))
            {
                int surveyId = _surveyDataService.GetCurrentUserSurveyId();
                string email = _accountService.GetCurrentUserEmail();

                partSurvey.SurveyId = surveyId;
                partSurvey.Email = email;
            }

            return partSurvey;
        }

        private WantedSurvey GetAllowedWantedSurvey(int? surveyId)
        {
            WantedSurvey wantedSurvey;
            if (surveyId.HasValue && User.IsInRole(MemberRoles.Administrator))
            {
                wantedSurvey = _surveyDataService.GetWantedSurvey(surveyId.Value);
            }
            else
            {
                wantedSurvey = _surveyDataService.GetCurrentUserWantedSurvey();
            }

            return wantedSurvey;
        }

        private WantedSurvey GetAllowedPartWantedSurvey(WantedSurveyViewModel wsvm)
        {
            var wantedSurvey = _mapper.Map<WantedSurvey>(wsvm);

            if (!User.IsInRole(MemberRoles.Administrator))
            {
                int surveyId = _surveyDataService.GetCurrentUserSurveyId();

                wantedSurvey.SurveyId = surveyId;
            }

            return wantedSurvey;
        }
    }
}