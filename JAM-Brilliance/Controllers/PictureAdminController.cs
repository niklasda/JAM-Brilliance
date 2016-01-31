using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

using AutoMapper;
using JAM.Core.Interfaces;
using JAM.Core.Interfaces.Admin;
using JAM.Core.Logic;
using JAM.Core.Models;
using JAM.Brilliance.Models.ViewModels;

using PagedList;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    [Authorize(Roles = MemberRoles.Administrator)]
    public class PictureAdminController : Controller
    {
        private readonly IPictureAdminDataService _pictureAdminDataService;
        private readonly IAbuseAdminDataService _abuseAdminDataService;
        private readonly IMessageDataService _messageDataService;
        private readonly IDataCache _dataCache;
        private readonly IMapper _mapper;

        public PictureAdminController(IPictureAdminDataService pictureAdminDataService, IAbuseAdminDataService abuseAdminDataService, IMessageDataService messageDataService, IDataCache dataCache, IMapper mapper)
        {
            _pictureAdminDataService = pictureAdminDataService;
            _abuseAdminDataService = abuseAdminDataService;
            _messageDataService = messageDataService;
            _dataCache = dataCache;
            _mapper = mapper;
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult Start(int? page)
        {
            ViewBag.CurrentPageId = PageIds.DevPage;

            IEnumerable<Picture> pics = _pictureAdminDataService.GetUnapprovedPictures();
            var pvms = _mapper.Map<IEnumerable<Picture>, IList<PictureViewModel>>(pics);
            var spvms = pvms.OrderBy(x => x.UploadDate);

            var pageNumber = page ?? 1;
            var onePageOfPictures = spvms.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfPictures = onePageOfPictures;

            return View(onePageOfPictures);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult ShowPicture(int pictureId)
        {
            return View(pictureId);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public FileResult PictureData(int pictureId)
        {
            Picture picture = _pictureAdminDataService.GetPicture(pictureId);

            if (picture != null && picture.ThePicture != null)
            {
                var result = File(picture.ThePicture, picture.ContentType);

                return result;
            }
            else
            {
                return File(new byte[] { 0 }, "file/test");
            }
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult Approve(int pictureId)
        {
            bool ok = _pictureAdminDataService.ApprovePicture(pictureId);

            return RedirectToAction("Start");
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Administrator)]
        public JsonResult GetAdminNumbersOf()
        {
            int supportMessagesCount = _messageDataService.GetUnreadMessagesCount(_dataCache.SupportSurveyId);
            int anonSupportMessagesCount = _messageDataService.GetUnreadMessagesCount(_dataCache.SupportAnonSurveyId);
            int picturesWaitingCount = _pictureAdminDataService.GetUnapprovedPictureCount();
            int abuseReportsCount = _abuseAdminDataService.GetAbuseReportsCount();

            var counts = new { supportMessagesCount, anonSupportMessagesCount, picturesWaitingCount, abuseReportsCount };
            return Json(counts);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult RemovePicture(int pictureId)
        {
            bool ok = _pictureAdminDataService.RemoveUnapprovedPicture(pictureId);
            return RedirectToAction("Start");
        }
    }
}