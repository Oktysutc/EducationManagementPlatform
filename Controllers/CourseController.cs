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
            // List<Course> objCourseList = _courseRepository.GetAll().ToList();
            List<Course> objCourseList = _courseRepository.GetAll(includeProps:"CourseCategory").ToList();


            return View(objCourseList);
        }
        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> CourseCategoryList = _courseCategoryRepository.GetAll().
                Select(k => new SelectListItem
                {
                    Text = k.Name,
                    Value = k.Id.ToString()
                });
            ViewBag.CourseCategoryList = CourseCategoryList;
            if (id == null || id == 0)
            {//ekleme
                return View();
            }
            else
            {
                //guncelleme
                Course? courseVt = _courseRepository.Get(u => u.Id == id);
                if (courseVt == null)
                {
                    return NotFound();
                }
                return View(courseVt);
            }
        }
        [HttpPost]
        public IActionResult AddUpdate(Course course,IFormFile? File)
        {
            if (ModelState.IsValid)
            {
               

                string wwwrootpath = _webHostEnvironment.WebRootPath;
                string coursePath = Path.Combine(wwwrootpath, @"img");
                if (File != null) {
                using (var fileStream = new FileStream(Path.Combine(coursePath, File.FileName),FileMode.Create))
                {
                    File.CopyTo(fileStream);
                }
                course.File = @"\img" + File.FileName; }

                if (course.Id == 0)
                {
                    _courseRepository.Add(course);
                }
                else
                {
                    _courseRepository.Update(course);
                    TempData["basarili"] = "okey";
                   
                }

            // _courseRepository.Add(course);
             _courseRepository.Save();
               
            return RedirectToAction("Index","Course"); 
            }
            return View();
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
