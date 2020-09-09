using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SPC_Coopenae.UI.Areas.Ventas.Models
{
    public class VentaProducto
    {
        public int IdVentaProducto { get; set; }

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

        [Display(Name = "Producto Vendido")]
        public int Producto { get; set; }

        public bool Estado { get; set; }

    }
}