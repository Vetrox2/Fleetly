using Microsoft.AspNetCore.Mvc;

namespace Fleetly.Controllers
{
    public class InspectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
