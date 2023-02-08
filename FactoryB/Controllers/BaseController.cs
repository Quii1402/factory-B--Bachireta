using KDTVN_Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactoryB.Controllers
{
    public abstract class BaseController<T> : Controller where T : BaseController<T>
    {
        protected AccountExtend _account;

        bool testMode = bool.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("IsTestMode").Value);

        //protected AccountExtend SignInAccount => _account ?? new AccountExtend(Guid.Parse(Request.Cookies["Guid"]), null);

        protected AccountExtend SignInAccount => _account ?? (testMode ? new AccountExtend("tvn184786") : new AccountExtend(Guid.Parse(Request.Cookies["Guid"]), null));

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (SignInAccount == null || SignInAccount.username == null)
                {
                    context.Result = new RedirectResult("http://kdtvn-web:8000");
                }
            }
            catch (ArgumentNullException)
            {
                context.Result = new RedirectResult("http://kdtvn-web:8000");
            }

            base.OnActionExecuting(context);
        }
    }
}
