﻿using System.Threading.Tasks;

namespace EducationManagementPlatform.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
