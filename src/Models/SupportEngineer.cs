using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class SupportEngineer
    {
        public Guid supportEngineerId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Nombre completo")]
        public string supportEngineerName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public Guid organizationId { get; set; }
        public Organization organization { get; set; }
    }
}
