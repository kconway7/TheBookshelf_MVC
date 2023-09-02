using Microsoft.AspNetCore.Identity.UI.Services;

namespace TheBookshelf.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Logic to send email, currently fake
            return Task.CompletedTask;
        }
    }
}
