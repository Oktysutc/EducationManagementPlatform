using EducationManagementPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EducationManagementPlatform.Controllers
{
    public class RentController : Controller
    {
        private readonly IRentRepository _rentRepository;//  her nesne için tek tek sınıf oluşturmamak için dependency injection yapısı burada kullanıldı
        private readonly ICourseRepository _courseRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public RentController(IRentRepository rentRepository, ICourseRepository courseRepository, IWebHostEnvironment webHostEnvironment)// dependency burada enjekte edildi
        {
            _rentRepository = rentRepository;
            _courseRepository = courseRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            // List<Course> objCourseList = _courseRepository.GetAll().ToList();
            List<Rent> objRentList = _rentRepository.GetAll(includeProps: "Course").ToList();//burada veri tabanında ki kurs listesi çekildi


            return View(objRentList);// listeyi döndür
        }
        public IActionResult AddUpdate(int? id)// parametre alabilir null olabilir
        {
            IEnumerable<SelectListItem> CourseList = _courseRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.CourseName,
                    Value = k.Id.ToString()
                });
            ViewBag.CourseList = CourseList;// çanta görünümüyle listeyi getir
            if (id == null || id == 0)// id boş ie kayıt ekleme işlemleri
            {
                // Ekleme
                return View(new Rent());
            }
            else
            {
                // Güncelleme
                Rent? rentVt = _rentRepository.Get(u => u.Id == id);
                if (rentVt == null)
                {
                    return NotFound();
                }
                return View(rentVt);
            }
        }

        [HttpPost]
        public IActionResult AddUpdate(Rent rent)
        {
            if (ModelState.IsValid)
            {

                if (rent.Id == 0)//id 0 ise ekleme yapılacak
                {
                    _rentRepository.Add(rent);
                }
                else
                {
                    _rentRepository.Update(rent);// eğer id dolu ise güncelleme işlemi yapılacak
                    TempData["basarili"] = "okey";
                }

                _rentRepository.Save();//kaydetme methodu çağrıldı
                return RedirectToAction("Index", "Rent");
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
            IEnumerable<SelectListItem> CourseList = _courseRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.CourseName,
                    Value = k.Id.ToString()
                });
            ViewBag.CourseList = CourseList;

            if (id == 0 || id == null)//id boş ise hata döndür
            {
                return NotFound();
            }
            Rent? rentVt = _rentRepository.Get(u => u.Id == id);
            if (rentVt == null)
            {
                return NotFound();
            }
            return View(rentVt);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Rent? rent = _rentRepository.Get(u => u.Id == id);
            if (rent == null)
            {
               return NotFound();
            }
            _rentRepository.Delete(rent);
            _rentRepository.Save();
            TempData["okey"] = "okey";//işlemden sonra ekrana gelecek olan mesaj
            return RedirectToAction("Index", "Rent");
        }

    }
}
