using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPC_Coopenae.UI.Models
{
    public class TipoCambio
    {
        public int IdTipoCambio { get; set; }

        [Required(ErrorMessage = "El campo código de vendedor es requerido")]
        public decimal Valor { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yyyy}")]
        [Required(ErrorMessage = "El campo código de vendedor es requerido")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo código de vendedor es requerido")]
        public bool Estado { get; set; } = true;
    }
}