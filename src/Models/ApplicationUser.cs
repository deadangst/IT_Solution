using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace src.Models
{
    // Agrega datos de perfil para los usuarios de la aplicación añadiendo propiedades a la clase ApplicationUser
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Nombre completo")]
        public string FullName { get; set; }

        [StringLength(250, ErrorMessage = "La {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Imagen de perfil")]
        public string ProfilePictureUrl { get; set; } = "/images/empty-profile.png";

        [StringLength(250, ErrorMessage = "La {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Imagen de fondo")]
        public string WallpaperPictureUrl { get; set; } = "/images/wallpaper1.jpg";

        [Display(Name = "Es SuperAdmin")]
        public bool IsSuperAdmin { get; set; } = false;

        [Display(Name = "Es Supervisor")]
        public bool IsCustomer { get; set; } = false;

        [Display(Name = "Es Agente de Soporte")]
        public bool IsSupportAgent { get; set; } = false;

        [Display(Name = "Es Ingeniero de Soporte")]
        public bool IsSupportEngineer { get; set; } = false;
    }

}
