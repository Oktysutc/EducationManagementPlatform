using Microsoft.AspNetCore.Mvc;
using EducationManagementPlatform.Services;
using EducationManagementPlatform.ViewModels;

public class ContactController : Controller
{
    private readonly IEmailService _emailService;

    public ContactController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet]
    public IActionResult SendEmail()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail(ContactViewModel model)
    {
        if (ModelState.IsValid)
        {
            string subject = "New Message from Contact Form";
            string message = $"{model.Name} ({model.Email}) sent you a message: {model.Message}";
            await _emailService.SendEmailAsync("oktaysutcu50@gmail.com", subject, message);

            ViewBag.Message = "mesajınız gönderildi!";
        }

        return View(model);
    }
}
