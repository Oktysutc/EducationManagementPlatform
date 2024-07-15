using EducationManagementPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EducationManagementPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseCategoryRepository _courseCategoryRepository;

        public HomeController(ILogger<HomeController> logger, ICourseRepository courseRepository, ICourseCategoryRepository courseCategoryRepository)
        {
            _logger = logger;
            _courseRepository = courseRepository;
            _courseCategoryRepository = courseCategoryRepository;
        }

        public IActionResult Index(string searchQuery)
        {
            List<Course> objCourseList;
            if (string.IsNullOrEmpty(searchQuery))
            {
                objCourseList = _courseRepository.GetAll(includeProps: "CourseCategory").ToList();
            }
            else
            {
                objCourseList = _courseRepository.SearchCourses(searchQuery).ToList();
            }

            IEnumerable<SelectListItem> CourseCategoryList = _courseCategoryRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Name,
                    Value = k.Id.ToString()
                });
            ViewBag.CourseCategoryList = CourseCategoryList;
            ViewBag.SearchQuery = searchQuery; // Arama sorgusunu ViewBag'de saklayın
            return View(objCourseList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
