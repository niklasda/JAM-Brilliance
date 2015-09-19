using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using JAM.Core.Interfaces;
using JAM.Core.Interfaces.Admin;
using JAM.Core.Logic;
using JAM.Core.Models;
using JAM.Logic;
using JAM.Models;
using JAM.Models.ViewModels;
using PagedList;
using PagedList.Mvc;
using Constants = JAM.Logic.Constants;
using PageIds = JAM.Logic.PageIds;
using Picture = JAM.Models.Picture;
using WantedSurvey = JAM.Models.WantedSurvey;

namespace JAM.Controllers
{
    public class SurveyController : Controller
    {
       // private JamContext db = new JamContext();
       // private PagedListRenderOptions opts = new PagedListRenderOptions();

        private readonly IPictureDataService _pictureDataService;
        private readonly ISurveyDataService _surveyDataService;
        private readonly ISurveyAdminDataService _surveyAdminDataService;
        private readonly IAccountService _accountService;
        private readonly IVisitorDataService _historyService;
        private readonly IGeoService _geoService;
        private readonly IDataCache _dataCache;

        public SurveyController(ISurveyDataService surveyDataService, ISurveyAdminDataService surveyAdminDataService, IPictureDataService pictureDataService, IAccountService accountService, IVisitorDataService visitorDataService, IGeoService geoService, IDataCache dataCache)
        {
            _pictureDataService = pictureDataService;
            _surveyDataService = surveyDataService;
            _surveyAdminDataService = surveyAdminDataService;
            _accountService = accountService;
            _historyService = visitorDataService;
            _geoService = geoService;
            _dataCache = dataCache;

            //opts.LinkToFirstPageFormat = "&larr;&larr; Först";
            //opts.LinkToPreviousPageFormat = "&larr; Förra";
            //opts.LinkToNextPageFormat = "Nästa &rarr;";
            //opts.LinkToLastPageFormat = "Sista &rarr;&rarr;";

           // ViewData["PagedListRenderOptions"] = opts;
        }

        public ViewResult Index(string sortOrder, string searchString, int? page)
        {
            var surveys = _surveyAdminDataService.GetSurveys();
           // var surveys = db.Surveys.Where(s => !s.IsDisabled);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        public ViewResult IndexMen(string sortOrder, string searchString, int? page)
        {
            var surveys = _surveyAdminDataService.GetSurveys();
//            var surveys = db.Surveys.Where(s => s.IsMale && !s.IsDisabled && !s.IsInterviewed);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        public ViewResult IndexWomen(string sortOrder, string searchString, int? page)
        {
            var surveys = _surveyAdminDataService.GetSurveys();
//            var surveys = db.Surveys.Where(s => !s.IsMale && !s.IsDisabled && !s.IsInterviewed);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        public ViewResult IndexAll(string sortOrder, string searchString, int? page)
        {
            var surveys = _surveyAdminDataService.GetSurveys();
//            var surveys = db.Surveys.Select(s => s);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        public ActionResult IndexProspects(string sortOrder, string searchString, int? page)
        {
            var surveys = _surveyAdminDataService.GetSurveys();
//            var surveys = db.Surveys.Where(s => s.IsInterviewed && !s.IsDisabled);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        [Authorize]
        public ViewResult IndexFiltered(IEnumerable<Survey> surveys, string sortOrder, string searchString, int? page)
        {
            ViewBag.IdSortParm = string.IsNullOrEmpty(sortOrder) ? "IdDesc" : "Id";
            ViewBag.NameSortParm = sortOrder == "Name" ? "NameDesc" : "Name";
            ViewBag.CitySortParm = sortOrder == "City" ? "CityDesc" : "City";
            ViewBag.SearchString = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
                surveys = surveys.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                        || s.City.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Name":
                    surveys = surveys.OrderBy(s => s.Name);
                    break;

                case "NameDesc":
                    surveys = surveys.OrderByDescending(s => s.Name);
                    break;

                case "City":
                    surveys = surveys.OrderBy(s => s.City);
                    break;

                case "CityDesc":
                    surveys = surveys.OrderByDescending(s => s.City);
                    break;

                case "Id":
                    surveys = surveys.OrderBy(s => s.SurveyId);
                    break;

                case "IdDesc":
                    surveys = surveys.OrderByDescending(s => s.SurveyId);
                    break;

                default:
                    surveys = surveys.OrderByDescending(s => s.SurveyId);
                    break;
            }

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = surveys.ToPagedList(pageNumber, Constants.PageSize);

            ViewBag.OnePageOfProducts = onePageOfProducts;

            return View("Index", onePageOfProducts.ToList());
        }

        public ViewResult Confirm()
        {
            // Create Confirmation
            return View();
        }

        [Authorize]
        public ViewResult Details(int id)
        {
            var survey = _surveyDataService.GetSurvey(id);

 //           Survey survey = db.Surveys.Find(id);
            //ViewBag.KidsCountId = new SelectList(db.KidCounts, "KidCountId", "Name", survey.KidsCountId);
            //ViewBag.KidsWantedCountId = new SelectList(db.KidWantedCounts, "KidWantedCountId", "Name", survey.KidsWantedCountId);
            //ViewBag.WantedKidsWantedCountId = new SelectList(db.WantedKidWantedCounts, "WantedKidWantedCountId", "Name", survey.WantedKidsWantedCountId);
            //ViewBag.DatePackageId = new SelectList(db.DatePackages, "DatePackageId", "Name", survey.DatePackageId);
            //ViewBag.ReferrerId = new SelectList(db.Referrers, "ReferrerId", "Name", survey.ReferrerId);
            return View(survey);
        }

        public ActionResult Create()
        {
            ViewBag.CurrentPageId = PageIds.Survery;

            //ViewBag.KidsCountId = new SelectList(db.KidCounts, "KidCountId", "Name");
            //ViewBag.KidsWantedCountId = new SelectList(db.KidWantedCounts, "KidWantedCountId", "Name");
            //ViewBag.WantedKidsWantedCountId = new SelectList(db.WantedKidWantedCounts, "WantedKidWantedCountId", "Name");
            //ViewBag.DatePackageId = new SelectList(db.DatePackages, "DatePackageId", "Name");
            //ViewBag.ReferrerId = new SelectList(db.Referrers, "ReferrerId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Survey survey)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["OriginalLocation"];
                if (file != null)
                {
                    int length = file.ContentLength;

                    var tempImage = new byte[length];
                    file.InputStream.Read(tempImage, 0, length);

                    var p = new Picture();
                    p.ThePicture = tempImage;
                    p.ContentType = file.ContentType;
                    //survey.Picture = p;
                }

                survey.IsInterviewed = Request.IsAuthenticated;

                // hard code these since they might not be set in the form if not logged in
                //if (survey.DatePackageId == 0)
                //{
                //    survey.DatePackageId = 1;
                //}

                //if (survey.KidsCountId == 0)
                //{
                //    survey.KidsCountId = 2;
                //}

                //if (survey.KidsWantedCountId == 0)
                //{
                //    survey.KidsWantedCountId = 2;
                //}

                //if (survey.WantedKidsWantedCountId == 0)
                //{
                //    survey.WantedKidsWantedCountId = 2;
                //}

                //db.Surveys.Add(survey);
                //int c2 = db.SaveChanges();

                if (!Request.IsAuthenticated)
                {
                    // only send payment info if not submitted by logged-in user
                    //Mailer.SendMail(survey, @Server.MachineName);
                }

                return View("Confirm", survey);
            }

            //ViewBag.KidsCountId = new SelectList(db.KidCounts, "KidCountId", "Name", survey.KidsCountId);
            //ViewBag.KidsWantedCountId = new SelectList(db.KidWantedCounts, "KidWantedCountId", "Name", survey.KidsWantedCountId);
            //ViewBag.WantedKidsWantedCountId = new SelectList(db.WantedKidWantedCounts, "WantedKidWantedCountId", "Name", survey.WantedKidsWantedCountId);
            //ViewBag.DatePackageId = new SelectList(db.DatePackages, "DatePackageId", "Name", survey.DatePackageId);
            //ViewBag.ReferrerId = new SelectList(db.Referrers, "ReferrerId", "Name", survey.ReferrerId);
            return View(survey);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var survey = _surveyDataService.GetSurvey(id);
//            Survey survey = db.Surveys.Find(id);
            //ViewBag.KidsCountId = new SelectList(db.KidCounts, "KidCountId", "Name", survey.KidsCountId);
            //ViewBag.KidsWantedCountId = new SelectList(db.KidWantedCounts, "KidWantedCountId", "Name", survey.KidsWantedCountId);
            //ViewBag.WantedKidsWantedCountId = new SelectList(db.WantedKidWantedCounts, "WantedKidWantedCountId", "Name", survey.WantedKidsWantedCountId);
            //ViewBag.DatePackageId = new SelectList(db.DatePackages, "DatePackageId", "Name", survey.DatePackageId);
            //ViewBag.ReferrerId = new SelectList(db.Referrers, "ReferrerId", "Name", survey.ReferrerId);

            return View(survey);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Survey survey)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["OriginalLocation"];
                if (file != null)
                {
                    int length = file.ContentLength;
                    if (length > 0)
                    {
                        var tempImage = new byte[length];
                        file.InputStream.Read(tempImage, 0, length);

                        //survey.Picture.ThePicture = tempImage;
                        //survey.Picture.ContentType = file.ContentType;
                        //db.Entry(survey.Picture).State = EntityState.Modified;
                    }
                }

               // db.Entry(survey).State = EntityState.Modified;
               // db.Entry(survey.WantedSurvey).State = EntityState.Modified;
               // db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.KidsCountId = new SelectList(db.KidCounts, "KidCountId", "Name", survey.KidsCountId);
            //ViewBag.KidsWantedCountId = new SelectList(db.KidWantedCounts, "KidWantedCountId", "Name", survey.KidsWantedCountId);
            //ViewBag.WantedKidsWantedCountId = new SelectList(db.WantedKidWantedCounts, "WantedKidWantedCountId", "Name", survey.WantedKidsWantedCountId);
            //ViewBag.DatePackageId = new SelectList(db.DatePackages, "DatePackageId", "Name", survey.DatePackageId);
            //ViewBag.ReferrerId = new SelectList(db.Referrers, "ReferrerId", "Name", survey.ReferrerId);

            return View(survey);
        }

        [Authorize]
        public ActionResult Hide(int id)
        {
            var survey = _surveyDataService.GetSurvey(id);
//            Survey survey = db.Surveys.Find(id);
            survey.IsDisabled = true;
            //db.Entry(survey).State = EntityState.Modified;

            //db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult UnHide(int id)
        {
            var survey = _surveyDataService.GetSurvey(id);
//            Survey survey = db.Surveys.Find(id);
            survey.IsDisabled = false;
         //   db.Entry(survey).State = EntityState.Modified;
         //   db.SaveChanges();
            return RedirectToAction("IndexAll");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var survey = _surveyDataService.GetSurvey(id);
//            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            var survey = _surveyDataService.GetSurvey(id);
//            Survey survey = db.Surveys.Find(id);
            //if (survey.WantedSurvey.WantedSurveyId > 1)
            //{
            //    WantedSurvey wanted = db.WantedSurveys.Find(survey.WantedSurvey.WantedSurveyId);
            //    db.WantedSurveys.Remove(wanted);
            //}

            //if (survey.Picture.PictureId > 1)
            //{
            //    Picture picture = db.Pictures.Find(survey.Picture.PictureId);
            //    db.Pictures.Remove(picture);
            //}

           // db.Surveys.Remove(survey);
           // db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ShowPhoto(int id)
        {
            return View(id);
        }

        [Authorize]
        public ActionResult PhotoData(int id)
        {
           // var image = db.Pictures.Find(id);
            var image = _pictureDataService.GetPicture(1, id);
            var result = File(image.ThePicture, image.ContentType);

            return result;
        }

        public ViewResult Page1(int? surveyId)
        {
            ViewBag.CurrentPageId = PageIds.Survery;

            Survey survey = GetAllowedSurvey(surveyId);
            var sp1Vm = Mapper.Map<SurveyPage1ViewModel>(survey);

            sp1Vm.KidsCounts = _dataCache.Kids;
            sp1Vm.KidsWantedCounts = _dataCache.KidsWanted;
            sp1Vm.WhatSearchingForWhats = _dataCache.WhatSearchingForWhats;

            return View(sp1Vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Page1(SurveyPage1ViewModel sp1Vm, string direction)
        {
            if (ModelState.IsValid)
            {
                sp1Vm.PostalCode = sp1Vm.PostalCode.Replace(" ", "");
                var partSurvey = GetAllowedPartSurvey(sp1Vm);

                if (_surveyDataService.SavePage1(partSurvey))
                {
                    Task.Run(() => this._geoService.LookupAndStoreGeoCoordinates(partSurvey.SurveyId, partSurvey.PostalCode, partSurvey.City, partSurvey.Country));
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

        public ActionResult Page2(int? surveyId)
        {
            ViewBag.CurrentPageId = PageIds.Survery;

            Survey survey = GetAllowedSurvey(surveyId);
            if (survey.SurveyId == 0)
            {
                return RedirectToAction("Page1", new { surveyId = surveyId });
            }

            var sp2Vm = Mapper.Map<SurveyPage2ViewModel>(survey);

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
            ViewBag.CurrentPageId = PageIds.Survery;

            Survey survey = GetAllowedSurvey(surveyId);
            if (survey.SurveyId == 0)
            {
                return RedirectToAction("Page1", new { surveyId = surveyId });
            }

            var sp3Vm = Mapper.Map<SurveyPage3ViewModel>(survey);

            return View(sp3Vm);
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
            var partSurvey = Mapper.Map<Survey>(spxvm);

            if (!User.IsInRole(MemberRoles.Administrator))
            {
                int surveyId = _surveyDataService.GetCurrentUserSurveyId();
                string email = _accountService.GetCurrentUserEmail();

                partSurvey.SurveyId = surveyId;
                partSurvey.Email = email;
            }

            return partSurvey;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}