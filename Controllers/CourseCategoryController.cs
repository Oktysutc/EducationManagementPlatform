using EducationManagementPlatform.Models;
using EducationManagementPlatform.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationManagementPlatform.Controllers
{
    // [Authorize(Roles =UserRoles.Role_Admin)]
    public class CourseCategoryController : Controller
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository; // Dependency injection
        public CourseCategoryController(ICourseCategoryRepository context)
        {
            _courseCategoryRepository = context;
        }

        public IActionResult Index()
        {
            List<CourseCategory> objCourseCategoryList = _courseCategoryRepository.GetAll().ToList();
            return View(objCourseCategoryList);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var courseCategory = _courseCategoryRepository.Get(c => c.Id == id);
            if (courseCategory == null)
            {
                return NotFound();
            }
            return Json(courseCategory);
        }

        [HttpPost]
        public IActionResult Add(CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                _courseCategoryRepository.Add(courseCategory);
                _courseCategoryRepository.Save();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Update(CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                _courseCategoryRepository.Update(courseCategory);
                _courseCategoryRepository.Save();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var courseCategory = _courseCategoryRepository.Get(c => c.Id == id);
            if (courseCategory == null)
            {
                return NotFound();
            }
            _courseCategoryRepository.Delete(courseCategory);
            _courseCategoryRepository.Save();
            return Json(new { success = true });
        }
    }
}
