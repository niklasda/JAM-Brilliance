using System.Web;
using System.Web.Mvc;
using JAM.Logic;

namespace JAM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Start()
        {
            var url = this.HttpContext.Request.Url;
            if (url != null && url.AbsoluteUri.Contains("dahlman.biz"))
            {
                return RedirectPermanent("http://www.jamatchmaking.com/");
            }
            else
            {
                ViewBag.CurrentPageId = PageIds.Start;
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.CurrentPageId = PageIds.About;
            return View();
        }

        [Authorize]
        public ActionResult DevPage()
        {
            return View();
        }

        public ActionResult Prices()
        {
            ViewBag.CurrentPageId = PageIds.Prices;
            return View();
        }

        public ActionResult Process()
        {
            ViewBag.CurrentPageId = PageIds.Process;
            return View();
        }

        public ActionResult Faq()
        {
            ViewBag.CurrentPageId = PageIds.Faq;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.CurrentPageId = PageIds.Contact;
            return View();
        }

        public ActionResult Reps()
        {
            ViewBag.CurrentPageId = PageIds.Reps;
            return View();
        }

        public ActionResult Pressrum()
        {
            ViewBag.CurrentPageId = PageIds.Press;
            return View();
        }

        public ActionResult References()
        {
            ViewBag.CurrentPageId = PageIds.References;
            return View();
        }

        public ActionResult Danish()
        {
            HttpContext.Response.Cookies.Set(new HttpCookie(Constants.CultureCookieName, "da-dk"));
            return RedirectToAction("Start");
        }

        public ActionResult Swedish()
        {
            HttpContext.Response.Cookies.Set(new HttpCookie(Constants.CultureCookieName, "sv-se"));
            return RedirectToAction("Start");
        }

        public ActionResult English()
        {
            HttpContext.Response.Cookies.Set(new HttpCookie(Constants.CultureCookieName, "en-us"));
            return RedirectToAction("Start");
        }
    }
}