using Microsoft.AspNetCore.Mvc;

namespace Fleetly.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
