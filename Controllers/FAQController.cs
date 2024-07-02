using Microsoft.AspNetCore.Mvc;
using EducationManagementPlatform.Models;
using System.Collections.Generic;
using EducationManagementPlatform.Models;

namespace EducationManagementPlatform.Controllers
{
    public class FAQController : Controller
    {
        public IActionResult Index()
        {
            var faqs = new List<FAQ>
            {
                new FAQ { Id = 1, Question = "Platforma nasıl kayıt olurum?", Answer = "Kayıt sayfasına giderek bilgilerinizi doldurup kayıt olabilirsiniz." },
                new FAQ { Id = 2, Question = "Kurslara nasıl katılabilirim?", Answer = "Kurslar sayfasından istediğiniz kursa kayıt olup katılabilirsiniz." },
                new FAQ { Id = 3, Question = "Hangi ödeme yöntemleri kabul ediliyor?", Answer = "Kredi kartı, banka havalesi ve PayPal kabul edilmektedir." }
            };

            return View(faqs);
        }
    }
}
