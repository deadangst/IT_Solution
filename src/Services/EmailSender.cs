using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        //dependency injection
        public SendGridOptions _sendGridOptions { get; }
        public IDotnetdesk _dotnetdesk { get; }
        public SmtpOptions _smtpOptions { get; }
        public SmtpLocalOptions _smtpLocalOptions { get; }

        public EmailSender(IOptions<SendGridOptions> sendGridOptions,
                           IDotnetdesk dotnetdesk,
                           IOptions<SmtpOptions> smtpOptions,
                           IOptions<SmtpLocalOptions> smtpLocalOptions)
        {
            _sendGridOptions = sendGridOptions.Value;
            _dotnetdesk = dotnetdesk;
            _smtpOptions = smtpOptions.Value;
            _smtpLocalOptions = smtpLocalOptions.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                // Enviar correo localmente
                await _dotnetdesk.SendEmailLocallyAsync(true, subject, message, email, _smtpLocalOptions.fromEmail, _smtpLocalOptions.localDestination);

                // Para usar SendGrid, descomenta la siguiente línea y asegúrate de que las configuraciones estén correctas
                // await _dotnetdesk.SendEmailBySendGridAsync(_sendGridOptions.SendGridKey, _sendGridOptions.FromEmail, _sendGridOptions.FromFullName, subject, message, email);

                // Enviar correo usando SMTP
                await _dotnetdesk.SendEmailByGmailAsync(_smtpOptions.fromEmail,
                                                        _smtpOptions.fromFullName,
                                                        subject,
                                                        message,
                                                        email,
                                                        _smtpOptions.smtpUserName,
                                                        _smtpOptions.smtpPassword,
                                                        _smtpOptions.smtpHost,
                                                        _smtpOptions.smtpPort,
                                                        _smtpOptions.smtpSSL);
            }
            catch (Exception ex)
            {
                // Considera utilizar un framework de logging para registrar los errores
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // O maneja el error de una manera que sea adecuada para tu aplicación
            }
        }
    }
}
