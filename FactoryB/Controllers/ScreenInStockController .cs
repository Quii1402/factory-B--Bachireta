using FactoryB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FactoryB.Controllers
{
    public class ScreenInStockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        InStockModel dt = new InStockModel();

        //lấy dữ liệu InStock
        [HttpPost]
        public IActionResult GetScreenInStock(InStockModel ínstock)
        {
            var result = dt.GetInStockModel(ínstock);
            return Json(result);
        }
    }
}
