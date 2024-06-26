namespace EducationManagementPlatform.Models
{
    public interface IEmailService
    {
        void SendEmail(EmailMessage message);
    }
}
