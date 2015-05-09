using System.Collections.Generic;
using System.Threading;
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
    public class PictureController : Controller
    {
        private readonly IPictureDataService _pictureDataService;
        private readonly ISurveyDataService _surveyDataService;
        private readonly IDiagnosticsService _diagnosticsService;

        public PictureController(ISurveyDataService surveyDataService, IPictureDataService pictureDataService, IDiagnosticsService diagnosticsService)
        {
            _pictureDataService = pictureDataService;
            _surveyDataService = surveyDataService;
            _diagnosticsService = diagnosticsService;
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult ShowMainPicture(int surveyid)
        {
            var spvm = new ShowPictureViewModel();
            spvm.OwnerSurveyId = surveyid;
            spvm.PictureId = _pictureDataService.GetPictureId(surveyid, 0);

            return View(spvm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        [OutputCache(VaryByParam = "surveyId", Duration = 30)]
        public FileResult MainPictureDataFor(int surveyId)
        {
            Picture picture = _pictureDataService.GetMainPictureFor(surveyId);

            return CheckPicture(picture);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult ShowMyPicture(int pictureId)
        {
            var spvm = new ShowPictureViewModel();
            spvm.PictureId = pictureId;

            return View(spvm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult ShowOtherPicture(int surveyId, int idx)
        {
            var pvm = new ShowPictureViewModel();
            pvm.OwnerSurveyId = surveyId;
            pvm.PictureId = _pictureDataService.GetPictureId(surveyId, idx);

            return View(pvm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        [OutputCache(VaryByParam = "pictureId", Duration = 30)]
        public FileResult OtherPictureData(int pictureId, int ownerSurveyId)
        {
            Picture picture = _pictureDataService.GetPicture(ownerSurveyId, pictureId);

            return CheckPicture(picture);
        }

        [Authorize(Roles = MemberRoles.Member)]
        [OutputCache(VaryByParam = "surveyId,idx", Duration = 30)]
        public FileResult OtherPictureDataFor(int surveyId, int idx)
        {
            int pictureId = _pictureDataService.GetPictureId(surveyId, idx);
            Picture picture = _pictureDataService.GetPicture(surveyId, pictureId);

            return CheckPicture(picture);
        }

        [Authorize(Roles = MemberRoles.Member)]
        [OutputCache(VaryByParam = "pictureId", Duration = 30, NoStore = true)]
        public FileResult MyPictureData(int pictureId)
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();
            Picture picture = _pictureDataService.GetPicture(surveyId, pictureId);

            if (picture != null && picture.ThePicture != null)
            {
                var result = File(picture.ThePicture, picture.ContentType);

                return result;
            }
            else
            {
                var countryCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                var path = string.Format("/Content/images/ja/missing_{0}.jpg", countryCode);

                if (System.IO.File.Exists(Server.MapPath(path)))
                {
                    return File(path, "image/jpeg");
                }

                path = "/Content/images/ja/missing_sv.jpg";
                return File(path, "image/jpeg");
            }
        }

        [Authorize(Roles = MemberRoles.Member)]
        public RedirectToRouteResult SetMainPicture(int pictureId)
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();
            _pictureDataService.SetMainPicture(surveyId, pictureId);

            return RedirectToAction("MyPictures");
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult MyPictures()
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            _diagnosticsService.ThrowUnlessSurveyComplete();

            var pms = _pictureDataService.GetPictures(surveyId);
            var pvms = Mapper.Map<IEnumerable<Picture>, IList<PictureViewModel>>(pms);

            var mpvm = new MyPicturesViewModel();
            mpvm.Pictures = pvms;

            return View(mpvm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult MyPictures(MyPicturesViewModel myp)
        {
            HttpPostedFileBase pictureFile = Request.Files["pictureFile"];
            var pic = _pictureDataService.LoadBytesIntoPicture(pictureFile);

            int surveyId = _surveyDataService.GetCurrentUserSurveyId();
            
            if (pic != null)
            {
                pic.SurveyId = surveyId;
                int pictureId = _pictureDataService.SaveNewPicture(pic);

                if (pictureId == 0)
                {
                    var fel = "Du får ha max 5 bilder.";
                    ModelState.AddModelError("PictureDummyId", fel);
                }
            }
            else
            {
                var fel = "En bild får vara max 500kb stor och måste vara jpeg eller png.";
                ModelState.AddModelError("PictureDummyId", fel);
            }

            var pms = _pictureDataService.GetPictures(surveyId);
            var pvms = Mapper.Map<IEnumerable<Picture>, IList<PictureViewModel>>(pms);

            var mpvm = new MyPicturesViewModel();
            mpvm.Pictures = pvms;

            return View(mpvm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ActionResult RemovePicture(int pictureId)
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();
            _pictureDataService.DeletePicture(surveyId, pictureId);

            return RedirectToAction("MyPictures");
        }

        private FileResult CheckPicture(Picture picture)
        {
            if (picture != null && !picture.IsApproved)
            {
                var countryCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                var path = string.Format("/Content/images/ja/missing_{0}.jpg", countryCode);

                if (System.IO.File.Exists(Server.MapPath(path)))
                {
                    return File(path, "image/jpeg");
                }

                path = "/Content/images/ja/missing_sv.jpg";
                return File(path, "image/jpeg");
            }
            else if (picture != null && picture.ThePicture != null)
            {
                var result = File(picture.ThePicture, picture.ContentType);

                return result;
            }
            else
            {
                return File("/Content/images/ja/t.png", "image/png");
            }
        }
    }
}