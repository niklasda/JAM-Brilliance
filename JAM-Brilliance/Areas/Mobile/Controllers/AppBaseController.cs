using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JAM.Brilliance.Areas.Mobile.Attributes;
using JAM.Core.Interfaces;
using JAM.Core.Interfaces.App;

namespace JAM.Brilliance.Areas.Mobile.Controllers
{
    public class AppBaseController : Controller
    {
        protected readonly IAccountService AccountService;
        protected readonly IAccountTokenDataService AccountTokenDataService;

        protected AppBaseController(IAccountService accountService, IAccountTokenDataService accountTokenDataService)
        {
            AccountService = accountService;
            AccountTokenDataService = accountTokenDataService;
        }

        protected Guid Token { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string tokenSent;
            var attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ValidateTokenAttribute), false);
            if (attrs.Count() == 1)
            {
                var prop = attrs[0] as ValidateTokenAttribute;
                if (prop != null && prop.FromUrl)
                {
                    tokenSent = filterContext.ActionParameters["token"].ToString();
                }
                else
                {
                    tokenSent = filterContext.HttpContext.Request.Headers["x-brilliance-token"];
                }

                Guid token;
                if (Guid.TryParse(tokenSent, out token))
                {
                    if (!token.Equals(Guid.Empty))
                    {
                        bool ok = AccountTokenDataService.IsTokenValid(token);
                        if (ok)
                        {
                            Token = token;
                            return;
                        }
                    }
                }

                Token = Guid.Empty;
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }
    }
}
