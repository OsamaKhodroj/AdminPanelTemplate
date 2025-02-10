using Microsoft.AspNetCore.Mvc;

namespace AdminPanelTemplate.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
