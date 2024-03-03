using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Nombre de usuario")]
        public string Username { get; set; }

        [Display(Name = "¿Está el correo confirmado?")]
        public bool IsEmailConfirmed { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ser una dirección de correo electrónico válida.")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Debe ser un número de teléfono válido.")]
        [Display(Name = "Número de teléfono")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Mensaje de estado")]
        public string StatusMessage { get; set; }
    }
}
