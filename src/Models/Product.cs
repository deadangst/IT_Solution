using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Product : BaseEntity
    {
        public Product()
        {
            this.thumbUrl = "/images/no-image-available.png";
            this.productCategory = Enum.ProductCategory.Other;
        }
        public Guid productId { get; set; }

        [Display(Name = "Nombre del Producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El {0} debe tener un máximo de {1} caracteres de longitud.")]
        public string productName { get; set; }

        [StringLength(200, ErrorMessage = "La {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "Descripción")]
        public string description { get; set; }

        [StringLength(255, ErrorMessage = "La {0} debe tener un máximo de {1} caracteres de longitud.")]
        [Display(Name = "URL de la miniatura")]
        public string thumbUrl { get; set; }

        [Display(Name = "Categoría del Producto")]
        public Enum.ProductCategory productCategory { get; set; }

        public Guid organizationId { get; set; }
        public Organization organization { get; set; }
    }
}
