    using FactoryB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace FactoryB.Controllers
{
    public class OutstockController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetOutStock(OutStockModel input)
        {
            var result = new OutStockModel().Get(input);
            return Json(result);
        }

        
    }
}
