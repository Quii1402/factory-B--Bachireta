using FactoryB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FactoryB.Controllers
{
    public class InStock : BaseController<InStock>
    {
        private readonly ILogger<InStock> _logger;

        public InStock(ILogger<InStock> logger)
        {
            _logger = logger;
        }
        InStockModel dt = new InStockModel();
        public IActionResult Index()
        {
            return View();
        }

//lấy dữ liệu InStock
        [HttpPost]
        public IActionResult GetInStock(InStockModel ínstock)
        {
            var result = dt.GetInStockModel(ínstock);
            return Json(result);
        }

 //Xoa du lieu  Instock
        [HttpDelete]
        public IActionResult DeleteInstock([FromForm] InStockModel ínstock)
        {
            var response = new InStockModel().DeleteInstockData(ínstock);
            return new ObjectResult(new { type = response.Code == 1 ? "success" : "error", message = response.Msg });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}