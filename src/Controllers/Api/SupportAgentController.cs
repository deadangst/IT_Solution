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
    [Route("api/SupportAgent")]
    [Authorize]
    public class SupportAgentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public SupportAgentController(ApplicationDbContext context,
            IDotnetdesk dotnetdesk,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _context = context;
            _dotnetdesk = dotnetdesk;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: api/SupportAgent
        [HttpGet("{organizationId}")]
        public IActionResult GetSupportAgent([FromRoute]Guid organizationId)
        {
            return Json(new { data = _context.SupportAgent.Where(x => x.organizationId.Equals(organizationId)).ToList() });
        }

        // POST: api/SupportAgent
        [HttpPost]
        public async Task<IActionResult> PostSupportAgent([FromBody] SupportAgent supportAgent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (supportAgent.supportAgentId == Guid.Empty)
            {
                var user = new ApplicationUser { UserName = supportAgent.Email, Email = supportAgent.Email, FullName = supportAgent.supportAgentName };
                user.IsSupportAgent = true;
                var randomPassword = new Random().Next(0, 999999);

                try
                {
                    var result = await _userManager.CreateAsync(user, randomPassword.ToString());
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);

                        try
                        {
                            await _emailSender.SendEmailAsync(supportAgent.Email, "Confirme su correo electrónico y registro",
                            $"Su correo electrónico ha sido registrado. Con nombre de usuario: '{supportAgent.Email}' y contraseña temporal: '{randomPassword}'. Por favor confirme su cuenta haciendo clic en este enlace: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>enlace</a>");
                        }
                        catch (Exception emailEx)
                        {
                            // Log the email error
                            // Consider: _logger.LogError(emailEx, "An error occurred while sending the confirmation email to {Email}", supportAgent.Email);
                        }

                        supportAgent.applicationUser = user;
                        supportAgent.supportAgentId = Guid.NewGuid();
                        Organization org = await _context.Organization.FirstOrDefaultAsync(x => x.organizationId.Equals(supportAgent.organizationId));
                        supportAgent.organization = org;

                        _context.SupportAgent.Add(supportAgent);
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
                    // Consider: _logger.LogError(ex, "An error occurred while creating support agent.");
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                try
                {
                    _context.Update(supportAgent);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Datos actualizados con éxito." });
                }
                catch (Exception ex)
                {
                    // Log the exception
                    // Consider: _logger.LogError(ex, "An error occurred while updating support agent.");
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }


        // DELETE: api/SupportAgent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportAgent([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var supportAgent = await _context.SupportAgent.SingleOrDefaultAsync(m => m.supportAgentId == id);
                if (supportAgent == null)
                {
                    return NotFound();
                }

                string applicationUserId = supportAgent.applicationUserId;

                _context.SupportAgent.Remove(supportAgent);
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

        private bool SupportAgentExists(Guid id)
        {
            return _context.SupportAgent.Any(e => e.supportAgentId == id);
        }
    }
}