using EducationManagementPlatform.Models;
using EducationManagementPlatform.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EducationManagementPlatform.Controllers
{
    public class BuyController : Controller
    {    
        private readonly IBuyRepository _buyRepository;//  her nesne için tek tek sınıf oluşturmamak için dependency injection yapısı burada kullanıldı
        private readonly ICourseRepository _courseRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public BuyController(IBuyRepository buyRepository, ICourseRepository courseRepository, IWebHostEnvironment webHostEnvironment)// dependency burada enjekte edildi
        {
            _buyRepository = buyRepository;
            _courseRepository = courseRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            // List<Course> objCourseList = _courseRepository.GetAll().ToList();
            List<Buy> objBuyList = _buyRepository.GetAll(includeProps:"Course").ToList();//burada veri tabanında ki kurs listesi çekildi


            return View(objBuyList);// listeyi döndür
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
                return View(new Buy());
            }
            else
            {
                // Güncelleme
                Buy? buyVt = _buyRepository.Get(u => u.Id == id);
                if (buyVt == null)
                {
                    return NotFound();
                }
                return View(buyVt);
            }
        }

        [HttpPost]
        public IActionResult AddUpdate(Buy buy)
        {
            if (ModelState.IsValid)
            {

                if (buy.Id == 0)//id 0 ise ekleme yapılacak
                {
                    _buyRepository.Add(buy);
                }
                else
                {
                    _buyRepository.Update(buy);// eğer id dolu ise güncelleme işlemi yapılacak
                    TempData["basarili"] = "okey";
                }

                _buyRepository.Save();//kaydetme methodu çağrıldı
                return RedirectToAction("Index", "Buy");
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

            if (id == 0 || id == null)//id boş ise hata dödür
            {
                return NotFound();
            }
            Buy? buyVt = _buyRepository.Get(u => u.Id == id);
            if (buyVt == null)
            {
                return NotFound();
            }
            return View(buyVt);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Buy? buy = _buyRepository.Get(u => u.Id == id);
            if (buy == null)
            {
                return NotFound();
            }
            _buyRepository.Delete(buy);
            _buyRepository.Save();
            TempData["okey"] = "okey";//işlemden sonra ekrana gelecek olan mesaj

            return RedirectToAction("Index", "Buy");
        }
       
    }
}
