using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPC_Coopenae.UI.Areas.Ventas.Models
{
    public class VentaCDP
    {
        
        public int IdVentaCDP { get; set; }

        [Remote("ValidarEjecutivo", "ValidaEjecutivo", "", ErrorMessage = "Cédula no registrada")]
        public int Ejecutivo { get; set; }

        [Display(Name = "Fecha de Venta")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Cédula del Cliente")]
        public int Cedula { get; set; }

        [Display(Name = "Nombre del Cliente")]
        public string Nombre { get; set; }

        [Display(Name = "Centro de Trabajo")]
        public string CentroTrabajo { get; set; }

        [Display(Name = "Monto Colocado")]
        public decimal Monto { get; set; }

        [Display(Name = "Plazo (Meses)")]
        public int PlazoMeses { get; set; }

        public int Periocidad { get; set; }

        public decimal? Tasa { get; set; }

        [Display(Name = "Sobre Tasa")]
        public decimal? SobreTasa { get; set; }

        public bool Estado { get; set; } = true;

        [Display(Name = "Tipo de CDP")]
        public int TipoCDP { get; set; }

    }
}