using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Models
{
    public class TipoProducto
    {
        public int IdTipoProducto { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo descripción es requerido")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "El campo moneda es requerido")]
        public string Moneda { get; set; }

        public bool Estado { get; set; } = true;

        [Display(Name = "IDP Necesario")]
        public decimal IDPNecesario { get; set; }
    }
}