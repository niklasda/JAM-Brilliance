using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

using AutoMapper;

using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Core.Models;
using JAM.Models.ViewModels;

namespace JAM.Controllers
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
        [OutputCache(VaryByParam = "surveyId", Duration = 30)]
        public FileResult MainPictureDataFor(int surveyId)
        {
            Picture picture = _pictureDataService.GetMainPictureFor(surveyId);

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