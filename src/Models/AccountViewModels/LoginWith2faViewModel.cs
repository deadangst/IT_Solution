using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(7, ErrorMessage = "El {0} debe tener al menos {2} y como máximo {1} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Código del autenticador")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Recordar este dispositivo")]
        public bool RememberMachine { get; set; }

        [Display(Name = "Recuérdame")]
        public bool RememberMe { get; set; }
    }
}
