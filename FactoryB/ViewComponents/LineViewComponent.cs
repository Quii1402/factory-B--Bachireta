using FactoryB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CStockManagement.ViewComponents
{
    [ViewComponent(Name = "Line")]
    public class LineViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lines = new LineModel().GetAll();
            return View("Index", lines);
        }
    }

    [ViewComponent(Name = "LineMultiple")]
    public class LineMultipleViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lines = new LineModel().GetAll();
            return View("Index", lines);
        }
    }

    [ViewComponent(Name = "LineByProduct")]
    public class LineByProductViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lines = new LineModel().GetAll();
            return View("Index", lines);
        }
    }

}
