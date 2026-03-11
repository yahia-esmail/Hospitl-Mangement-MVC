using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Hospitl_Mangement_MVC.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromEmail = "healthforusy@outlook.com";
            var fromPassword = "MohamedSaad500500%";

            var message = new MailMessage();
            message.From = new MailAddress(fromEmail);
            message.To.Add(email);
            message.Subject = subject;
            message.Body = $"<html><body>{htmlMessage}</body></html>";
            message.IsBodyHtml = true;
            var client = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
            };
            client.Send(message);
        }
    }
}
