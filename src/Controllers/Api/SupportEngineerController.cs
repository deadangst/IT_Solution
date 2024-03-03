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
    [Route("api/SupportEngineer")]
    [Authorize]
    public class SupportEngineerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public SupportEngineerController(ApplicationDbContext context,
            IDotnetdesk dotnetdesk,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _context = context;
            _dotnetdesk = dotnetdesk;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: api/SupportEngineer
        [HttpGet("{organizationId}")]
        public IActionResult GetSupportEngineer([FromRoute]Guid organizationId)
        {
            return Json(new { data = _context.SupportEngineer.Where(x => x.organizationId.Equals(organizationId)).ToList() });
        }


        // POST: api/SupportEngineer
        [HttpPost]
        public async Task<IActionResult> PostSupportEngineer([FromBody] SupportEngineer supportEngineer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (supportEngineer.supportEngineerId == Guid.Empty)
            {
                try
                {
                    var user = new ApplicationUser { UserName = supportEngineer.Email, Email = supportEngineer.Email, FullName = supportEngineer.supportEngineerName };

                    user.IsSupportEngineer = true;
                    var randomPassword = new Random().Next(0, 999999);
                    var result = await _userManager.CreateAsync(user, randomPassword.ToString());
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);

                        try
                        {
                            await _emailSender.SendEmailAsync(supportEngineer.Email, "Confirme su correo electrónico y registro",
                            $"Su correo electrónico ha sido registrado con el nombre de usuario: '{supportEngineer.Email}' y una contraseña temporal: '{randomPassword.ToString()}'. Por favor, confirme su cuenta haciendo clic en este enlace: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>enlace</a>");
                        }
                        catch (Exception emailEx)
                        {
                            // Log the email error
                            // Example: _logger.LogError(emailEx, "An error occurred while sending the confirmation email to {Email}", supportEngineer.Email);
                        }

                        supportEngineer.applicationUser = user;
                        supportEngineer.supportEngineerId = Guid.NewGuid();
                        Organization org = _context.Organization.FirstOrDefault(x => x.organizationId.Equals(supportEngineer.organizationId));
                        supportEngineer.organization = org;

                        _context.SupportEngineer.Add(supportEngineer);
                        await _context.SaveChangesAsync();

                        return Json(new { success = true, message = "Se ha añadido un nuevo dato con éxito." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Fallo en UserManager CreateAsync." });
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    // Example: _logger.LogError(ex, "An error occurred while creating support engineer.");
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                try
                {
                    _context.Update(supportEngineer);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Datos actualizados con éxito." });
                }
                catch (Exception ex)
                {
                    // Log the exception
                    // Example: _logger.LogError(ex, "An error occurred while updating support engineer.");
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }


        // DELETE: api/SupportEngineer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportEngineer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var supportEngineer = await _context.SupportEngineer.SingleOrDefaultAsync(m => m.supportEngineerId == id);
                if (supportEngineer == null)
                {
                    return NotFound();
                }

                string applicationUserId = supportEngineer.applicationUserId;

                _context.SupportEngineer.Remove(supportEngineer);
                await _context.SaveChangesAsync();

                ApplicationUser appUser = await _userManager.FindByIdAsync(applicationUserId);
                await _userManager.DeleteAsync(appUser);

                return Json(new { success = true, message = "Eliminación exitosa." });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }

           
        }

        private bool SupportEngineerExists(Guid id)
        {
            return _context.SupportEngineer.Any(e => e.supportEngineerId == id);
        }
    }
}