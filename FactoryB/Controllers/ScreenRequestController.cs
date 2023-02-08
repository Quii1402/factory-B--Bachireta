using FactoryB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FactoryB.Controllers
{
    public class ScreenRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetAllLine()
        {
            var result = new LineModel().GetAll();
            return Ok(result);
            
        }
        [HttpPost]
        public IActionResult GetRequest(string line,string floor)
        {
            var result = new ScreenRequestModel().Get(line, floor);
            return result == null ? Json(new EmptyResult()) : Json(result);
        }
    }
}
