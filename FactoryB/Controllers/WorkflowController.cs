using FactoryB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FactoryB.Controllers
{
    public class WorkflowController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
