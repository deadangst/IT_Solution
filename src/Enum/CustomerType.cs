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
        [Display(Name = "Pequeña Empresa")]
        SmallBusiness = 1,
        Enterprise = 2,
        Gobierno = 3,
        ONG = 4,
        Interno = 5
    }

}
