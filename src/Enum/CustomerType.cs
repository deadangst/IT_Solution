using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Enum
{
    public enum CustomerType
    {
        Other = 0,
        [Display(Name = "Desarrollo")]
        Desarrollo = 1,
        Arte = 2,
        Planillas = 3,
        HR = 4,
        Interno = 5
    }

}
