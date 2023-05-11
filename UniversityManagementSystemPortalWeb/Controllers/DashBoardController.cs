using Microsoft.AspNetCore.Mvc;

namespace UniversityManagementSystemPortalWeb.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
