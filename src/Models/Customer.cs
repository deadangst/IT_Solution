using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            this.thumbUrl = "/images/no-image-available.png";
            this.customerType = Enum.CustomerType.Interno;
        }

        public Guid customerId { get; set; }

        [Display(Name = "Nombre del Supervisor")]
        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string customerName { get; set; }

        [StringLength(200, ErrorMessage = "La {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Descripción")]
        public string description { get; set; }

        [StringLength(255, ErrorMessage = "La {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "URL de miniatura")]
        public string thumbUrl { get; set; }

        [Display(Name = "Tipo de Supervisor")]
        public Enum.CustomerType customerType { get; set; } // Considera la traducción de los tipos de Supervisores si es necesario

        // Dirección
        [Display(Name = "Dirección completa")]
        [StringLength(100, ErrorMessage = "La {0} debe tener un máximo de {1} caracteres de longitud.")]
        public string address { get; set; }

        [Display(Name = "Teléfono")]
        [StringLength(20, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        public string phone { get; set; }

        [Display(Name = "Correo electrónico")]
        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        public string email { get; set; }

        [Display(Name = "Sitio web")]
        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        public string website { get; set; }

        [Display(Name = "LinkedIn")]
        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        public string linkedin { get; set; }

        public Guid organizationId { get; set; }
        public Organization organization { get; set; }

        // Contactos
        public ICollection<Contact> contacts { get; set; }
    }
}
