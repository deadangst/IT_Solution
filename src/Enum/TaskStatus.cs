using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Enum
{
    public enum TaskStatus
    {
        [Display(Name = "No Iniciada")]
        NoIniciada = 1,
        Diferida = 2,
        [Display(Name = "En Progreso")]
        EnProgreso = 3,
        Completada = 4
    }

}
