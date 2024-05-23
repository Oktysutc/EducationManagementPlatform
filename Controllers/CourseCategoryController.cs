using EducationManagementPlatform.Models;
using EducationManagementPlatform.Utility;
using Microsoft.AspNetCore.Mvc;

namespace EducationManagementPlatform.Controllers
{
    public class CourseCategoryController : Controller
    {    
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        public CourseCategoryController(ICourseCategoryRepository context)
        {
            _courseCategoryRepository = context;
        }
        
        public IActionResult Index()
        {
            List<CourseCategory> objCourseCategoryList = _courseCategoryRepository.GetAll().ToList();
            return View(objCourseCategoryList);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CourseCategory courseCategory)
        {
            if (ModelState.IsValid) 
            {
            _courseCategoryRepository.Add(courseCategory);
            _courseCategoryRepository.Save();
                TempData["okey"] = "okey";
            return RedirectToAction("Index","CourseCategory"); }
            return View();
        }






        public IActionResult Update(int? id)
        {
            if(id==0||id==null)
            {
                return NotFound();
            }
            CourseCategory? courseCategoryVt = _courseCategoryRepository.Get(u=>u.Id==id);
            if (courseCategoryVt==null) {
                return NotFound();
            }
            return View(courseCategoryVt);
        }
        [HttpPost]
        public IActionResult Update(CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                _courseCategoryRepository.Update(courseCategory);
                _courseCategoryRepository.Save();
                TempData["okey"] = "okey";

                return RedirectToAction("Index", "CourseCategory");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            CourseCategory? courseCategoryVt = _courseCategoryRepository.Get(u => u.Id == id);
            if (courseCategoryVt == null)
            {
                return NotFound();
            }
            return View(courseCategoryVt);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            CourseCategory? courseCategory = _courseCategoryRepository.Get(u => u.Id == id);
            if (courseCategory == null)
            {
                return NotFound();
            }
            _courseCategoryRepository.Delete(courseCategory);
            _courseCategoryRepository.Save();
            TempData["okey"] = "okey";

            return RedirectToAction("Index", "CourseCategory");
        }
    }
}
