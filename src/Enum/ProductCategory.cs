﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Enum
{
    public enum ProductCategory
    {
        Other = 0,
        Monitor = 1,
        Teléfono = 2,
        Escritorio = 3,
        Portátil = 4,
        Impresora = 5,
        [Display(Name = "Otro Hardware")]
        OtroHardware = 6,
        Windows = 7,
        Word = 8,
        Excel = 9,
        Powerpoint = 10,
        [Display(Name = "Otro Software")]
        OtroSoftware = 11
    }

}
