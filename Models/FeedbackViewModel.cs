using System.ComponentModel.DataAnnotations;

public class FeedbackViewModel
{
    [Required(ErrorMessage = "Ad alanı zorunludur.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "E-posta alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Geri bildirim alanı zorunludur.")]
    public string Message { get; set; }
}
