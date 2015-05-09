using System.Web.Mvc;

using JAM.Core.Attributes;
using JAM.Core.Exceptions;

using StackExchange.Profiling.Mvc;

namespace JAM.Brilliance
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RequreSecureConnectionFilter());
            filters.Add(new HandleErrorAttribute());
            filters.Add(
                new HandleErrorAttribute
                {
                    ExceptionType = typeof(SurveyIncompleteException),
                    View = "SurveyIncompleteError"
                });
            filters.Add(new ProfilingActionFilter());
        }
    }
}