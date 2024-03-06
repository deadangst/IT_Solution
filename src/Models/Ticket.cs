using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Ticket : BaseEntity
    {
        public Ticket()
        {
            this.ticketStatus = Enum.TicketStatus.SinAsignar; // Sin asignar
            this.ticketType = Enum.TicketType.Problema; // Problema
            this.ticketPriority = Enum.TicketPriority.Baja; // Baja
        }

        public Guid ticketId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Título del ticket")]
        public string ticketName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(200, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Descripción del problema")]
        public string description { get; set; }

        [Display(Name = "ID del Supervisor")]
        public Guid customerId { get; set; }

        [Display(Name = "Colaborador")]
        public Guid contactId { get; set; }

        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Correo electrónico")]
        public string email { get; set; }

        [StringLength(20, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Teléfono")]
        public string phone { get; set; }

        [Display(Name = "Estado")]
        public Enum.TicketStatus ticketStatus { get; set; }

        [Display(Name = "ID del propietario del ticket")]
        public Guid supportAgentId { get; set; }

        [Display(Name = "Ingeniero de soporte")]
        public Guid supportEngineerId { get; set; }

        [Display(Name = "Producto")]
        public Guid productId { get; set; }

        [Display(Name = "Tipo de ticket")]
        public Enum.TicketType ticketType { get; set; }

        [Display(Name = "Prioridad del ticket")]
        public Enum.TicketPriority ticketPriority { get; set; }

        [Display(Name = "Canal del ticket")]
        public Enum.TicketChannel ticketChannel { get; set; }

        public Guid organizationId { get; set; }
        public Organization organization { get; set; }

        public ICollection<TicketThread> ticketThreads { get; set; }
    }
}
