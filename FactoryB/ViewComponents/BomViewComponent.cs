using FactoryB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace FactoryB.ViewComponents
{
    [ViewComponent(Name = "Bom")]
    public class BomViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var boms = new BomModel().GetAll();
            return View("Index", boms);
        }
    }
}
