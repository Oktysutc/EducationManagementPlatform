using Microsoft.AspNetCore.Mvc;

namespace EducationManagementPlatform.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
