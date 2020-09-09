using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Models
{
    public class Producto
    {
        [Display(Name = "Id de prodcuto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Comisión")]
        public decimal Comision { get; set; }

        public bool Estado { get; set; } = true;

        [Display(Name = "Tipo de Prodcuto")]
        public int TipoProducto { get; set; }

    }
}