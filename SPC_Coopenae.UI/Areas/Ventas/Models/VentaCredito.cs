using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SPC_Coopenae.UI.Areas.Ventas.Models
{
    public class VentaCredito
    {
        public int IdVentaCredito { get; set; }

        [Remote("ValidarEjecutivo", "ValidaEjecutivo", "", ErrorMessage = "Cédula no registrada")]
        public int Ejecutivo { get; set; }

        [Display(Name = "Fecha de Venta")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Cedula del Cliente")]
        public int Cedula { get; set; }

        [Display(Name = "Nombre del Cliente")]
        public string Nombre { get; set; }

        [Display(Name = "Centro de Trabajo")]
        public string CentroTrabajo { get; set; }

        [Display(Name = "Fecha de Afiliación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaAfiliacion { get; set; }

        [Display(Name = "Número de Operación")]
        public int NumeroOperacion { get; set; }

        [Display(Name = "Monto Colocado")]
        public decimal Monto { get; set; }

        [Display(Name = "Plazo (Meses)")]
        public int PlazoMeses { get; set; }

        public bool Estado { get; set; }

        [Display(Name = "Tipo de Crédito")]
        public int TipoCredito { get; set; }
    }
}