using KDTVN_Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.ViewComponents
{
    [ViewComponent(Name = "Account")]
    public class AccountViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testMode = bool.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("IsTestMode").Value);
            var account = testMode ? new AccountExtend("tvn184786") : new AccountExtend(Guid.Parse(Request.Cookies["Guid"]), null);

            return View("Index", account);
        }
    }
}
