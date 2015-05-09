using System;
using System.Web.Mvc;

namespace JAM.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Start(string aspxerrorpath)
        {
            if (!string.IsNullOrEmpty(aspxerrorpath))
            {
                var hei = new HandleErrorInfo(new Exception(aspxerrorpath), "Error", "Start");
                return View("Error", hei);
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public void Test()
        {
            throw new Exception("Test");
        }
    }
}