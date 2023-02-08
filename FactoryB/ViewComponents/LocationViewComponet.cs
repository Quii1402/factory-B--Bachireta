using FactoryB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CStockManagement.ViewComponents
{
    [ViewComponent(Name = "Location")]
    public class LocationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lines = new LocationModel().GetAll();
            return View("Index", lines);
        }
    }
}
