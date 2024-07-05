using EducationManagementPlatform.Models;
using EducationManagementPlatform.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EducationManagementPlatform.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class BuyController : Controller
    {
        private readonly IBuyRepository _buyRepository; // Dependency injection
        private readonly ICourseRepository _courseRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public BuyController(IBuyRepository buyRepository, ICourseRepository courseRepository, IWebHostEnvironment webHostEnvironment)
        {
            _buyRepository = buyRepository;
            _courseRepository = courseRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Buy> objBuyList = _buyRepository.GetAll(includeProps: "Course").ToList();
            IEnumerable<SelectListItem> CourseList = _courseRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.CourseName,
                    Value = k.Id.ToString()
                });
            ViewBag.CourseList = CourseList;
            return View(objBuyList);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var buy = _buyRepository.Get(b => b.Id == id);
            if (buy == null)
            {
                return NotFound();
            }
            return Json(buy);
        }

        [HttpPost]
        public IActionResult AddUpdate(Buy buy)
        {
            if (ModelState.IsValid)
            {
                if (buy.Id == 0)
                {
                    _buyRepository.Add(buy);
                }
                else
                {
                    _buyRepository.Update(buy);
                }
                _buyRepository.Save();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var buy = _buyRepository.Get(b => b.Id == id);
            if (buy == null)
            {
                return NotFound();
            }
            _buyRepository.Delete(buy);
            _buyRepository.Save();
            return Json(new { success = true });
        }
    }
}
