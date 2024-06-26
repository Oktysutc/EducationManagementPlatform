using EducationManagementPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EducationManagementPlatform.Controllers
{
    public class CourseInfoController : Controller
    {
        private readonly ICourseInfoRepository _courseInfoRepository;
        private readonly ICourseRepository _courseRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public CourseInfoController(ICourseInfoRepository courseInfoRepository, ICourseRepository courseRepository, IWebHostEnvironment webHostEnvironment)
        {
            _courseInfoRepository = courseInfoRepository;
            _courseRepository = courseRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<CourseInfo> objCourseInfoList = _courseInfoRepository.GetAll().ToList();
            return View(objCourseInfoList);

        
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var courseInfo = _courseInfoRepository.Get(c => c.Id == id);
            if (courseInfo == null)
            {
                return NotFound();
            }
            return Json(new
            {
                id = courseInfo.Id,
                courseName = courseInfo.CourseName,
                courseCategoryId = courseInfo.CourseCategoryId,
                //  description = course.Description,
                price = courseInfo.Price,
                // duration = course.Duration,
                file = courseInfo.File
            });
        }

        

    }
}
