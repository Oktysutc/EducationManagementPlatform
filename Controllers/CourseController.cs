using EducationManagementPlatform.Models;
using EducationManagementPlatform.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EducationManagementPlatform.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public CourseController(ICourseRepository courseRepository, ICourseCategoryRepository courseCategoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _courseRepository = courseRepository;
            _courseCategoryRepository = courseCategoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Course> objCourseList = _courseRepository.GetAll(includeProps: "CourseCategory").ToList();
            IEnumerable<SelectListItem> CourseCategoryList = _courseCategoryRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Name,
                    Value = k.Id.ToString()
                });
            ViewBag.CourseCategoryList = CourseCategoryList;
            return View(objCourseList);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var course = _courseRepository.Get(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return Json(new
            {
                id = course.Id,
                courseName = course.CourseName,
                courseCategoryId = course.CourseCategoryId,
              //  description = course.Description,
                price = course.Price,
               // duration = course.Duration,
                file = course.File
            });
        }

        [HttpPost]
        public IActionResult AddUpdate(Course course, IFormFile? File)
        {
            if (ModelState.IsValid)
            {
                string wwwrootPath = _webHostEnvironment.WebRootPath;
                string coursePath = Path.Combine(wwwrootPath, "img");

                if (File != null)
                {
                    string fileName = Path.GetFileName(File.FileName);
                    string fullPath = Path.Combine(coursePath, fileName);

                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        File.CopyTo(fileStream);
                    }
                    course.File = $"/img/{fileName}";
                }

                if (course.Id == 0)
                {
                    _courseRepository.Add(course);
                }
                else
                {
                    _courseRepository.Update(course);
                    TempData["basarili"] = "okey";
                }

                _courseRepository.Save();
                return Json(new { success = true });

               
                
            }

            return Json(new { success = false, message = "Model state is not valid." });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var course = _courseRepository.Get(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            _courseRepository.Delete(course);
            _courseRepository.Save();
            TempData["okey"] = "okey";

            return Json(new { success = true });
        }
    }
}
