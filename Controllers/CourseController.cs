using EducationManagementPlatform.Models;
using EducationManagementPlatform.Utility;
using Microsoft.AspNetCore.Authorization;
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

        // [Authorize(Roles = "Admin,User")]
        public IActionResult Index(string category)
        {
            List<Course> objCourseList;

            if (string.IsNullOrEmpty(category))
            {
                objCourseList = _courseRepository.GetAll(includeProps: "CourseCategory").ToList();
            }
            else
            {
                objCourseList = _courseRepository.GetCoursesByCategory(category).ToList();
            }

            // Tüm kategori seçeneklerini dropdown listesi olarak al
            IEnumerable<SelectListItem> CourseCategoryList = _courseCategoryRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Name,
                    Value = k.Id.ToString()
                });
            ViewBag.CourseCategoryList = CourseCategoryList;
            ViewBag.SelectedCategory = category; // Seçilen kategoriyi ViewBag'de sakla

            return View(objCourseList);
        }




        [HttpGet]
        public IActionResult Category(string category)
        {
            // Kategoriye göre kursları getir
            var courses = _courseRepository.GetCoursesByCategory(category);

            // Kurs listesini view'e gönder
            return View("Category", courses);
        }

        //[Authorize(Roles = "Admin,User")]
        public IActionResult Detail(int id)
        {
            var course = _courseRepository.Get(c => c.Id == id, includeProps: "CourseCategory");
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpGet]
       // [Authorize(Roles = "Admin,User")]
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
                price = course.Price,
                file = course.File,
                videoUrl = course.VideoUrl
            });
        }

        [HttpPost]
        //[Authorize(Roles = UserRoles.Role_Admin)]
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

                // YouTube URL'sini embed URL'ye dönüştürme
                if (!string.IsNullOrEmpty(course.VideoUrl))
                {
                    var uri = new Uri(course.VideoUrl);
                    var videoId = System.Web.HttpUtility.ParseQueryString(uri.Query).Get("v");
                    course.VideoUrl = $"https://www.youtube.com/embed/{videoId}";
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
       // [Authorize(Roles = UserRoles.Role_Admin)]
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
