using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

using JAM.Core.Logic;

namespace JAM.Core.Attributes
{
    /// <summary>
    /// Decorates any MVC route that needs to have client requests limited by time.
    /// </summary>
    /// <remarks>
    /// Uses the current System.Web.Caching.Cache to store each client request to the decorated route.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ThrottleAttribute : ActionFilterAttribute
    {
        private const int Seconds = Constants.DefaultWaitTime;

        /// <summary>
        /// Gets or sets a unique name for this Throttle.
        /// </summary>
        /// <remarks>
        /// We'll be inserting a Cache record based on this name and client IP, e.g. "Name-192.168.0.1"
        /// </remarks>
        public string Name { get; set; }

        public override void OnActionExecuting(ActionExecutingContext c)
        {
            var key = string.Concat(Name, "-", c.HttpContext.Request.UserHostAddress);
            var allowExecute = false;

            if (HttpRuntime.Cache[key] == null)
            {
                HttpRuntime.Cache.Add(key, true, null, DateTime.Now.AddSeconds(Seconds), Cache.NoSlidingExpiration, CacheItemPriority.Low, null);

                allowExecute = true;
            }

            if (!allowExecute)
            {
                c.Result = new RedirectResult("/Error/Throttled", false);
            }
        }
    }
}