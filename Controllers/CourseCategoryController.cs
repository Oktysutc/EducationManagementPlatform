using EducationManagementPlatform.Models;
using EducationManagementPlatform.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationManagementPlatform.Controllers
{
    [Authorize(Roles =UserRoles.Role_Admin)]
    public class CourseCategoryController : Controller
    {    
        private readonly ICourseCategoryRepository _courseCategoryRepository;//  her nesne için tek tek sınıf oluşturmamak için dependency injection yapısı burada kullanıldı
        public CourseCategoryController(ICourseCategoryRepository context)// dependency burada enjekte edildi
        {
            _courseCategoryRepository = context;
        }
        
        public IActionResult Index()
        {
            List<CourseCategory> objCourseCategoryList = _courseCategoryRepository.GetAll().ToList();//burada veri tabanında ki kurs listesi çekildi
            return View(objCourseCategoryList);
        }
        public IActionResult Add()//ekleme işlemi
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CourseCategory courseCategory)
        {
            if (ModelState.IsValid) // herşey dogru ise
            {
            _courseCategoryRepository.Add(courseCategory);//ekle
            _courseCategoryRepository.Save();//kaydet
                TempData["okey"] = "okey";//ekranda bunu yaz
            return RedirectToAction("Index","CourseCategory"); }//yönlendirilecek olan sayfa
            return View();
        }






        public IActionResult Update(int? id)//güncelleme işlemleri
        {
            if(id==0||id==null)// id boş ise
            {
                return NotFound();//hata
            }
            CourseCategory? courseCategoryVt = _courseCategoryRepository.Get(u=>u.Id==id);//id yi ara
            if (courseCategoryVt==null) {//boş ise
                return NotFound();//hata
            }
            return View(courseCategoryVt);//vt deki listeyi döndür
        }
        [HttpPost]
        public IActionResult Update(CourseCategory courseCategory)//güncelleme işlemleri
        {
            if (ModelState.IsValid)//herşey doğru ise
            {
                _courseCategoryRepository.Update(courseCategory);//methodu guncelle
                _courseCategoryRepository.Save();//kaydet
                TempData["okey"] = "okey";//ekrana yaz

                return RedirectToAction("Index", "CourseCategory");//yönlendirilecek olan sayfa
            }
            return View();
        }

        public IActionResult Delete(int? id)//silme işlemleri
        {
            if (id == 0 || id == null)//id boş ise
            {
                return NotFound();//hata
            }
            CourseCategory? courseCategoryVt = _courseCategoryRepository.Get(u => u.Id == id);//id yi ara
            if (courseCategoryVt == null)//liste boş ise
            {
                return NotFound();//hata
            }
            return View(courseCategoryVt);//listeyi döndür
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            CourseCategory? courseCategory = _courseCategoryRepository.Get(u => u.Id == id);//id yi ara
            if (courseCategory == null)//liste boş ise
            {
                return NotFound();// hata
            }
            _courseCategoryRepository.Delete(courseCategory);//sil
            _courseCategoryRepository.Save();//kaydet
            TempData["okey"] = "okey";//ekrana yaz

            return RedirectToAction("Index", "CourseCategory");//yönlendirme ssayfası
        }
    }
}
