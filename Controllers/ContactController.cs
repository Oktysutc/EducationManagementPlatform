using EducationManagementPlatform.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationManagementPlatform.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Send(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                var emailMessage = new EmailMessage
                {
                    To = "oktaysutcu50@gmail.com",
                    Subject = "New contact form submission",
                    Body = $"Name: {model.Name}\nEmail: {model.Email}\nMessage: {model.Message}"
                };

                _emailService.SendEmail(emailMessage);

                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
    }
}
