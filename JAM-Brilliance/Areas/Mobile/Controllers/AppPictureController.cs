using System;
using System.Web.Mvc;
using JAM.Brilliance.Areas.Mobile.Attributes;
using JAM.Brilliance.Areas.Mobile.Models;
using JAM.Brilliance.Areas.Mobile.Models.Response;
using JAM.Core.Interfaces;
using JAM.Core.Interfaces.App;
using JAM.Core.Models;

namespace JAM.Brilliance.Areas.Mobile.Controllers
{
    public class AppPictureController : AppBaseController
    {
        private readonly IPictureDataService _pictureDataService;

        public AppPictureController(IAccountService accountService, IAccountTokenDataService accountTokenDataService, IPictureDataService pictureDataService)
            : base(accountService, accountTokenDataService)
        {
            _pictureDataService = pictureDataService;
        }

        [HttpPost, ValidateToken]
        public JsonResult UploadPictureData(PictureDataModel data)
        {
            var surveyId = AccountTokenDataService.GetSurveyIdForToken(Token);

            return Json(surveyId);
        }

        [HttpGet, ValidateToken(FromUrl = true)]
        public FileResult MainPictureDataFor(int id, Guid token)
        {
            var surveyId = AccountTokenDataService.GetSurveyIdForToken(Token);
            
            Picture picture = _pictureDataService.GetMainPictureFor(id);
            return CheckPicture(picture);
        }

        [HttpGet, ValidateToken(FromUrl = true)]
        public FileResult MainPictureData(Guid token)
        {
            var surveyId = AccountTokenDataService.GetSurveyIdForToken(Token);

            Picture picture = _pictureDataService.GetMainPictureFor(surveyId);
            return CheckPicture(picture);
        }

        private FileResult CheckPicture(Picture picture)
        {
            if (picture != null && !picture.IsApproved)
            {
                var path = "/Content/images/ja/missing_sv.jpg";
                return File(path, "image/jpeg");
            }
            else if (picture != null && picture.ThePicture != null)
            {
                var result = File(picture.ThePicture, picture.ContentType);

                return result;
            }
            else
            {
                return File("/Content/images/ja/missing_en.jpg", "image/png");
            }
        }
    }
}
