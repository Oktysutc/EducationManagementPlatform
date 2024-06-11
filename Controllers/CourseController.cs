using EducationManagementPlatform.Models;
using EducationManagementPlatform.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EducationManagementPlatform.Controllers
{
    [Authorize(Roles=UserRoles.Role_Admin)]
    public class CourseController : Controller
    {    
        private readonly ICourseRepository _courseRepository;//dependency injection kullanıldı
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public CourseController(ICourseRepository courseRepository, ICourseCategoryRepository courseCategoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _courseRepository = courseRepository;//buarada enjecte edildi
            _courseCategoryRepository = courseCategoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            // List<Course> objCourseList = _courseRepository.GetAll().ToList();
            List<Course> objCourseList = _courseRepository.GetAll(includeProps:"CourseCategory").ToList();//liste burada döndürüldü


            return View(objCourseList);//listeyi döndür
        }
        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> CourseCategoryList = _courseCategoryRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Name,
                    Value = k.Id.ToString()
                });
            ViewBag.CourseCategoryList = CourseCategoryList;

            if (id == null || id == 0)
            {
                // Ekleme
                return View(new Course());
            }
            else
            {
                // Güncelleme
                Course? courseVt = _courseRepository.Get(u => u.Id == id);
                if (courseVt == null)
                {
                    return NotFound();
                }
                return View(courseVt);
            }
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
                    course.File = $"/img/{fileName}"; // Doğru URL formatı için
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
                return RedirectToAction("Index", "Course");
            }

            // ViewBag.CourseCategoryList'i tekrar doldur
            ViewBag.CourseCategoryList = _courseCategoryRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Name,
                    Value = k.Id.ToString()
                });

            return View(course);
        }





        /*

        public IActionResult Update(int? id)
        {
            if(id==0||id==null)
            {
                return NotFound();
            }
            Course? courseVt = _courseRepository.Get(u=>u.Id==id);
            if (courseVt==null) {
                return NotFound();
            }
            return View(courseVt);
        }
       
        [HttpPost]
        public IActionResult Update(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.Update(course);
                _courseRepository.Save();
                TempData["okey"] = "okey";

                return RedirectToAction("Index", "Course");
            }
            return View();
        }
        */

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Course? courseVt = _courseRepository.Get(u => u.Id == id);
            if (courseVt == null)
            {
                return NotFound();
            }
            return View(courseVt);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Course? course = _courseRepository.Get(u => u.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            _courseRepository.Delete(course);
            _courseRepository.Save();
            TempData["okey"] = "okey";

            return RedirectToAction("Index", "Course");
        }
       
    }
}
