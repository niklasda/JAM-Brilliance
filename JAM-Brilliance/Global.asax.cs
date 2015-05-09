using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using JAM.Core.Logic;
using StackExchange.Profiling;

namespace JAM.Brilliance
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

        protected void Session_Start(object sender, EventArgs e)
        {
            if (Context.Request.Url.Host.Contains("brilliance.azure") || Context.Request.Url.Host.Contains("brilliancedating"))
            {
                Response.Redirect("http://brilliance.se");
            }
        }

        //protected void Application_PreSendRequestHeaders()
        //{
        //    Response.Headers.Remove("Server");
        //}

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            bool allOk = false;

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

                        allOk = true;
                    }
                    catch (Exception)
                    {
                        HttpContext.Current.Request.Cookies.Remove(Constants.CultureCookieName);
                        HttpContext.Current.Response.Cookies.Remove(Constants.CultureCookieName);
                    }
                }
            }

            if (!allOk)
            {
                var culture = CultureInfo.GetCultureInfo(Constants.DefaultCountryCode);

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
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