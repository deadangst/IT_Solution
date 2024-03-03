using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace src.Models.ManageViewModels
{
    public class EnableAuthenticatorViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(7, ErrorMessage = "El {0} debe tener al menos {2} y como máximo {1} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Código de Verificación")]
        public string Code { get; set; }

        [BindNever]
        public string SharedKey { get; set; }

        [BindNever]
        public string AuthenticatorUri { get; set; }
    }
}
