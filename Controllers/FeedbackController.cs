using Microsoft.AspNetCore.Mvc;
using EducationManagementPlatform.Models; // Kullanacağınız model sınıflarınıza göre değiştirin
using EducationManagementPlatform.Models; // Kullanacağınız view model sınıflarınıza göre değiştirin

public class FeedbackController : Controller
{
    // GET: /Feedback/Form
    public IActionResult Form()
    {
        ViewData["Title"] = "Geri Bildirim Formu";
        return View();
    }

    // POST: /Feedback/Submit
    [HttpPost]
    public IActionResult Submit(FeedbackViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Burada model verilerini kullanarak geri bildirimi işleyebilirsiniz
            // Örneğin, model.Name, model.Email, model.Message gibi alanlara erişebilirsiniz
            // Veritabanına kaydetme, e-posta gönderme, vs. gibi işlemler yapılabilir

            // Başarılı bir şekilde işlem yapıldıktan sonra kullanıcıyı başka bir sayfaya yönlendirme gibi bir işlem yapılabilir
            return RedirectToAction("Success"); // Örnek bir yönlendirme
        }

        // ModelState.IsValid false ise, formda hata var demektir ve aynı view ile tekrar gösterilmesi gerekebilir
        return View("Form", model);
    }

    // GET: /Feedback/Success
    public IActionResult Success()
    {
        ViewData["Title"] = "Geri Bildirim Gönderildi";
        return View();
    }
}
