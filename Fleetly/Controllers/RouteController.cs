using Microsoft.AspNetCore.Mvc;

namespace Fleetly.Controllers
{
    public class RouteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
