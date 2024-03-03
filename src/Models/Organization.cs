using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Organization : BaseEntity
    {
        public Organization()
        {
            this.thumbUrl = "/images/blank-building.png";
        }
        public Guid organizationId { get; set; }

        [Display(Name = "Nombre de la organización")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        public string organizationName { get; set; }

        [StringLength(200, ErrorMessage = "La {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Descripción")]
        public string description { get; set; }

        [StringLength(255, ErrorMessage = "La {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "URL de la miniatura")]
        public string thumbUrl { get; set; }

        // Referencia al Usuario de la Aplicación
        public string organizationOwnerId { get; set; }

        // Productos
        public ICollection<Product> products { get; set; }
        // Clientes
        public ICollection<Customer> customers { get; set; }
        // Agentes de soporte
        public ICollection<SupportAgent> supportAgents { get; set; }
        // Ingenieros de soporte
        public ICollection<SupportEngineer> supportEngineers { get; set; }
        // Tickets
        public ICollection<Ticket> tickets { get; set; }
    }
}
