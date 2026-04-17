using Microsoft.AspNetCore.Mvc;

namespace INVENTORY_MANAGEMENT_SYSTEM.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
