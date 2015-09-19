using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using JAM.Logic;

using StackExchange.Profiling;

namespace JAM
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MapperConfig.CreateAutoMaps();
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var cooki = HttpContext.Current.Request.Cookies.Get(Constants.CultureCookieName);
            if (cooki != null)
            {
                var cultureCode = cooki.Value;

                if (cultureCode != null)
                {
                    try
                    {
                        var culture = CultureInfo.GetCultureInfo(cultureCode);

                        Thread.CurrentThread.CurrentCulture = culture;
                        Thread.CurrentThread.CurrentUICulture = culture;
                    }
                    catch (Exception)
                    {
                        HttpContext.Current.Request.Cookies.Remove(Constants.CultureCookieName);
                        HttpContext.Current.Response.Cookies.Remove(Constants.CultureCookieName);
                    }
                }
            }
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }
}