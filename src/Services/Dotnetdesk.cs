using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using src.Data;
using src.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace src.Services
{
    public class Dotnetdesk : IDotnetdesk
    {
        public async Task SendEmailBySendGridAsync(string apiKey, string fromEmail, string fromFullName, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromFullName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            await client.SendEmailAsync(msg);
        }

        public async Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string message,
            string email,
            string smtpUserName,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL)
        {
            // Verificar que los parámetros de correo electrónico no sean nulos o vacíos
            if (string.IsNullOrWhiteSpace(fromEmail))
                throw new ArgumentNullException(nameof(fromEmail), "Sender email cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "Recipient email cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(smtpUserName))
                throw new ArgumentNullException(nameof(smtpUserName), "SMTP username cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(smtpPassword))
                throw new ArgumentNullException(nameof(smtpPassword), "SMTP password cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(smtpHost))
                throw new ArgumentNullException(nameof(smtpHost), "SMTP host cannot be null or empty.");

            var mailMessage = new MailMessage()
            {
                From = new MailAddress(fromEmail, fromFullName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(new MailAddress(email));

            using (var smtp = new SmtpClient(smtpHost, smtpPort))
            {
                smtp.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                smtp.EnableSsl = smtpSSL;
                try
                {
                    await smtp.SendMailAsync(mailMessage);
                }
                catch (SmtpException ex)
                {
                    // Log or handle your SMTP exception here.
                    throw; // Optionally re-throw the exception if you cannot handle it here.
                }
            }
        }

        public async Task SendEmailLocallyAsync(bool isBodyHtml, string subject, string body, string toEmail, string fromEmail, string localDestination)
        {
            using (var smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtp.PickupDirectoryLocation = localDestination;

                var message = new MailMessage()
                {
                    Body = body,
                    IsBodyHtml = isBodyHtml,
                    Subject = subject,
                    From = new MailAddress(fromEmail)
                };
                message.To.Add(new MailAddress(toEmail));
                await smtp.SendMailAsync(message);
            }
        }

        public async Task<bool> IsAccountActivatedAsync(string email, UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.FindByNameAsync(email);
            return user != null && await userManager.IsEmailConfirmedAsync(user);
        }

        public Task CreateDefaultOrganization(string applicationUserId, ApplicationDbContext context)
        {
            var org = new Organization
            {
                // Ensure these property names match your Organization class's properties
                organizationName = "Default HQ",
                description = "Default Organization / Default Branch or HQ",
                organizationOwnerId = applicationUserId
            };
            context.Organization.Add(org);
            context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env)
        {
            var result = "";
            var webRoot = env.WebRootPath;
            var uploads = Path.Combine(webRoot, "uploads");

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var extension = Path.GetExtension(formFile.FileName);
                    var fileName = Guid.NewGuid().ToString() + extension;
                    var filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    result = fileName;
                }
            }
            return result;
        }
    }
}
