using EducationManagementPlatform.Utility;
using Microsoft.AspNetCore.Mvc;

namespace EducationManagementPlatform.Controllers
{
    public class CourseCategoryController : Controller
    {    /// <summary>
    /// ////////////
    /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
