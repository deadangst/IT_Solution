using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Contact : BaseEntity
    {
        public Contact()
        {
            this.thumbUrl = "/images/no-image-available.png";
        }

        public Guid contactId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Nombre completo")]
        public string contactName { get; set; }

        [StringLength(200, ErrorMessage = "La {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Descripción")]
        public string description { get; set; }

        [StringLength(255, ErrorMessage = "La {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "URL de imagen")]
        public string thumbUrl { get; set; }

        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Correo electrónico")]
        public string email { get; set; }

        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Correo electrónico secundario")]
        public string secondaryEmail { get; set; }

        [StringLength(20, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Teléfono")]
        public string phone { get; set; }

        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Sitio web")]
        public string website { get; set; }

        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "LinkedIn")]
        public string linkedin { get; set; }

        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public Guid customerId { get; set; }
        public Customer customer { get; set; }


    }
}
