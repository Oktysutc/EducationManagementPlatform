using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EducationManagementPlatform.Models; // ChangePasswordViewModel için

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private const string SecretKey = "6LeNDAkqAAAAAJq0iNpoC1nQPIm1gWMsNIdOoaOB"; // Google reCAPTCHA gizli anahtarınızı buraya ekleyin

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    [Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var recaptchaResponse = Request.Form["g-recaptcha-response"];
        var recaptchaResult = await ValidateCaptcha(recaptchaResponse);
        if (!recaptchaResult)
        {
            ModelState.AddModelError(string.Empty, "reCAPTCHA doğrulaması başarısız. Lütfen tekrar deneyin.");
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (result.Succeeded)
        {
            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    private async Task<bool> ValidateCaptcha(string response)
    {
        var client = new HttpClient();
        var values = new Dictionary<string, string>
        {
            { "secret", SecretKey },
            { "response", response }
        };
        var content = new FormUrlEncodedContent(values);
        var res = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
        var json = await res.Content.ReadAsStringAsync();
        dynamic obj = JObject.Parse(json);

        return obj.success == "true";
    }
}
