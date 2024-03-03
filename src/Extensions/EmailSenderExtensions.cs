using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using src.Services;

namespace src.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirma tu correo electrónico",
                $"Por favor, confirma tu cuenta haciendo clic en este enlace: <a href='{HtmlEncoder.Default.Encode(link)}'>enlace</a>");
        }

    }
}
