using EducationManagementPlatform.Models;
using EducationManagementPlatform.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EducationManagementPlatform.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class RentController : Controller
    {
        private readonly IRentRepository _rentRepository; // Dependency injection
        private readonly ICourseRepository _courseRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public RentController(IRentRepository rentRepository, ICourseRepository courseRepository, IWebHostEnvironment webHostEnvironment)
        {
            _rentRepository = rentRepository;
            _courseRepository = courseRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Rent> objRentList = _rentRepository.GetAll(includeProps: "Course").ToList();
            IEnumerable<SelectListItem> CourseList = _courseRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.CourseName,
                    Value = k.Id.ToString()
                });
            ViewBag.CourseList = CourseList;
            return View(objRentList);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var buy = _rentRepository.Get(b => b.Id == id);
            if (buy == null)
            {
                return NotFound();
            }
            return Json(buy);
        }

        [HttpPost]
        public IActionResult AddUpdate(Rent rent)
        {
            if (ModelState.IsValid)
            {
                if (rent.Id == 0)
                {
                    _rentRepository.Add(rent);
                }
                else
                {
                    _rentRepository.Update(rent);
                }
                _rentRepository.Save();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var rent = _rentRepository.Get(b => b.Id == id);
            if (rent == null)
            {
                return NotFound();
            }
            _rentRepository.Delete(rent);
            _rentRepository.Save();
            return Json(new { success = true });
        }
    }
}
