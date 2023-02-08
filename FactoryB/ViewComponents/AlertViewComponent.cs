using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BaseProject.ViewComponents
{
    [ViewComponent(Name = "Alert")]
    public class AlertViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string title,string content)
        {
            ViewData["AlertTitle"] = title;
            ViewData["AlertContent"] = content;
            return View("Index");
        }
    }
}
