using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JAM.Logic;
using JAM.Models;
using PagedList;
using PagedList.Mvc;

namespace JAM.Controllers
{
    public class SurveyController : Controller
    {
        private JamContext db = new JamContext();
        private PagedListRenderOptions opts = new PagedListRenderOptions();

        public SurveyController()
        {
            opts.LinkToFirstPageFormat = "&larr;&larr; Först";
            opts.LinkToPreviousPageFormat = "&larr; Förra";
            opts.LinkToNextPageFormat = "Nästa &rarr;";
            opts.LinkToLastPageFormat = "Sista &rarr;&rarr;";

            ViewData["PagedListRenderOptions"] = opts;
        }

        public ViewResult Index(string sortOrder, string searchString, int? page)
        {
            var surveys = db.Surveys.Where(s => !s.IsDisabled);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        public ViewResult IndexMen(string sortOrder, string searchString, int? page)
        {
            var surveys = db.Surveys.Where(s => s.IsMale && !s.IsDisabled && !s.IsInterviewed);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        public ViewResult IndexWomen(string sortOrder, string searchString, int? page)
        {
            var surveys = db.Surveys.Where(s => !s.IsMale && !s.IsDisabled && !s.IsInterviewed);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        public ViewResult IndexAll(string sortOrder, string searchString, int? page)
        {
            var surveys = db.Surveys.Select(s => s);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        public ActionResult IndexProspects(string sortOrder, string searchString, int? page)
        {
            var surveys = db.Surveys.Where(s => s.IsInterviewed && !s.IsDisabled);

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
            Survey survey = db.Surveys.Find(id);
            ViewBag.KidsCountId = new SelectList(db.KidCounts, "KidCountId", "Name", survey.KidsCountId);
            ViewBag.KidsWantedCountId = new SelectList(db.KidWantedCounts, "KidWantedCountId", "Name", survey.KidsWantedCountId);
            ViewBag.WantedKidsWantedCountId = new SelectList(db.WantedKidWantedCounts, "WantedKidWantedCountId", "Name", survey.WantedKidsWantedCountId);
            ViewBag.DatePackageId = new SelectList(db.DatePackages, "DatePackageId", "Name", survey.DatePackageId);
            ViewBag.ReferrerId = new SelectList(db.Referrers, "ReferrerId", "Name", survey.ReferrerId);
            return View(survey);
        }

        public ActionResult Create()
        {
            ViewBag.CurrentPageId = PageIds.Survery;

            ViewBag.KidsCountId = new SelectList(db.KidCounts, "KidCountId", "Name");
            ViewBag.KidsWantedCountId = new SelectList(db.KidWantedCounts, "KidWantedCountId", "Name");
            ViewBag.WantedKidsWantedCountId = new SelectList(db.WantedKidWantedCounts, "WantedKidWantedCountId", "Name");
            ViewBag.DatePackageId = new SelectList(db.DatePackages, "DatePackageId", "Name");
            ViewBag.ReferrerId = new SelectList(db.Referrers, "ReferrerId", "Name");
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
                    survey.Picture = p;
                }

                survey.IsInterviewed = Request.IsAuthenticated;

                // hard code these since they might not be set in the form if not logged in
                if (survey.DatePackageId == 0)
                {
                    survey.DatePackageId = 1;
                }

                if (survey.KidsCountId == 0)
                {
                    survey.KidsCountId = 2;
                }

                if (survey.KidsWantedCountId == 0)
                {
                    survey.KidsWantedCountId = 2;
                }

                if (survey.WantedKidsWantedCountId == 0)
                {
                    survey.WantedKidsWantedCountId = 2;
                }

                db.Surveys.Add(survey);
                int c2 = db.SaveChanges();

                if (!Request.IsAuthenticated)
                {
                    // only send payment info if not submitted by logged-in user
                    Mailer.SendMail(survey, @Server.MachineName);
                }

                return View("Confirm", survey);
            }

            ViewBag.KidsCountId = new SelectList(db.KidCounts, "KidCountId", "Name", survey.KidsCountId);
            ViewBag.KidsWantedCountId = new SelectList(db.KidWantedCounts, "KidWantedCountId", "Name", survey.KidsWantedCountId);
            ViewBag.WantedKidsWantedCountId = new SelectList(db.WantedKidWantedCounts, "WantedKidWantedCountId", "Name", survey.WantedKidsWantedCountId);
            ViewBag.DatePackageId = new SelectList(db.DatePackages, "DatePackageId", "Name", survey.DatePackageId);
            ViewBag.ReferrerId = new SelectList(db.Referrers, "ReferrerId", "Name", survey.ReferrerId);
            return View(survey);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            Survey survey = db.Surveys.Find(id);
            ViewBag.KidsCountId = new SelectList(db.KidCounts, "KidCountId", "Name", survey.KidsCountId);
            ViewBag.KidsWantedCountId = new SelectList(db.KidWantedCounts, "KidWantedCountId", "Name", survey.KidsWantedCountId);
            ViewBag.WantedKidsWantedCountId = new SelectList(db.WantedKidWantedCounts, "WantedKidWantedCountId", "Name", survey.WantedKidsWantedCountId);
            ViewBag.DatePackageId = new SelectList(db.DatePackages, "DatePackageId", "Name", survey.DatePackageId);
            ViewBag.ReferrerId = new SelectList(db.Referrers, "ReferrerId", "Name", survey.ReferrerId);

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

                        survey.Picture.ThePicture = tempImage;
                        survey.Picture.ContentType = file.ContentType;
                        db.Entry(survey.Picture).State = EntityState.Modified;
                    }
                }

                db.Entry(survey).State = EntityState.Modified;
                db.Entry(survey.WantedSurvey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KidsCountId = new SelectList(db.KidCounts, "KidCountId", "Name", survey.KidsCountId);
            ViewBag.KidsWantedCountId = new SelectList(db.KidWantedCounts, "KidWantedCountId", "Name", survey.KidsWantedCountId);
            ViewBag.WantedKidsWantedCountId = new SelectList(db.WantedKidWantedCounts, "WantedKidWantedCountId", "Name", survey.WantedKidsWantedCountId);
            ViewBag.DatePackageId = new SelectList(db.DatePackages, "DatePackageId", "Name", survey.DatePackageId);
            ViewBag.ReferrerId = new SelectList(db.Referrers, "ReferrerId", "Name", survey.ReferrerId);

            return View(survey);
        }

        [Authorize]
        public ActionResult Hide(int id)
        {
            Survey survey = db.Surveys.Find(id);
            survey.IsDisabled = true;
            db.Entry(survey).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult UnHide(int id)
        {
            Survey survey = db.Surveys.Find(id);
            survey.IsDisabled = false;
            db.Entry(survey).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("IndexAll");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Survey survey = db.Surveys.Find(id);
            if (survey.WantedSurvey.WantedSurveyId > 1)
            {
                WantedSurvey wanted = db.WantedSurveys.Find(survey.WantedSurvey.WantedSurveyId);
                db.WantedSurveys.Remove(wanted);
            }

            if (survey.Picture.PictureId > 1)
            {
                Picture picture = db.Pictures.Find(survey.Picture.PictureId);
                db.Pictures.Remove(picture);
            }

            db.Surveys.Remove(survey);
            db.SaveChanges();
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
            var image = db.Pictures.Find(id);

            var result = File(image.ThePicture, image.ContentType);

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}