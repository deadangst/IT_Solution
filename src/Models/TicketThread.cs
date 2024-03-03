using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class TicketThread : BaseEntity
    {
        public Guid ticketThreadId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(250, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Comentario")]
        public string Comment { get; set; }

        public Guid ticketId { get; set; }
    }
}
