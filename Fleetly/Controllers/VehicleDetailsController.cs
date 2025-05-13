using Microsoft.AspNetCore.Mvc;

namespace Fleetly.Controllers
{
    public class VehicleDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
