using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Enum
{
    public enum TicketStatus
    {
        SinAsignar = 1,
        Abierto = 2,
        [Display(Name = "En Espera")]
        EnEspera = 3,
        Escalado = 4,
        Cerrado = 5
    }

}
