using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;
using src.Services;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Contact")]
    [Authorize]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ContactController(ApplicationDbContext context,
            IDotnetdesk dotnetdesk,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _context = context;
            _dotnetdesk = dotnetdesk;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: api/Contact
        [HttpGet("{customerId}")]
        public IActionResult GetContact([FromRoute]Guid customerId)
        {
            return Json(new { data = _context.Contact.Where(x => x.customerId.Equals(customerId)).ToList() });
        }

        // POST: api/Contact
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (contact.contactId == Guid.Empty)
                {
                    var user = new ApplicationUser { UserName = contact.email, Email = contact.email, FullName = contact.contactName };

                    user.IsCustomer = true;
                    var randomPassword = new Random().Next(0, 999999);
                    var result = await _userManager.CreateAsync(user, randomPassword.ToString());

                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);

                        try
                        {
                            await _emailSender.SendEmailAsync(contact.email, "Confirma tu correo y registro",
                            $"Tu correo ha sido registrado. Con el nombre de usuario:'{contact.email}' y la contraseña temporal:'{randomPassword.ToString()}'. Por favor confirma tu cuenta haciendo clic en este enlace: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>enlace</a>");

                            // Log the email sending action
                            // For example: _logger.LogInformation($"Confirmation email sent to {contact.email}.");
                        }
                        catch (Exception emailEx)
                        {
                            // Log the email error
                            // For example: _logger.LogError(emailEx, $"An error occurred while sending the confirmation email to {contact.email}.");
                            // You might choose to continue even if the email wasn't sent, or return an error.
                        }

                        contact.applicationUser = user;

                        contact.contactId = Guid.NewGuid();
                        _context.Contact.Add(contact);

                        await _context.SaveChangesAsync();

                        return Json(new { success = true, message = "Añadido nuevo dato exitosamente." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Fallo el CreateAsync del UserManager." });
                    }
                }
                else
                {
                    _context.Update(contact);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Datos editados exitosamente." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                // For example: _logger.LogError(ex, "An error occurred while processing the contact.");
                return Json(new { success = false, message = ex.Message });
            }
        }

        // DELETE: api/Contact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var contact = await _context.Contact.SingleOrDefaultAsync(m => m.contactId == id);
                if (contact == null)
                {
                    return NotFound();
                }

                _context.Contact.Remove(contact);
                await _context.SaveChangesAsync();
                

                return Json(new { success = true, message = "Eliminado con éxito." });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }

            
        }

        private bool ContactExists(Guid id)
        {
            return _context.Contact.Any(e => e.contactId == id);
        }
    }
}