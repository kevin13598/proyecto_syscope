using SPC_Coopenae.UI.Models.ObjsReporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPC_Coopenae.UI.Models
{
    public class Reporte
    {
        //Parametros
        public int Cedula { get; set; }

        public string Nombre { get; set; }

        public DateTime Fecha { get; set; }

        public decimal TipoCambio { get; set; }

        public decimal Salario { get; set; }

        public decimal Bono { get; set; }

        //Listas Y comisiones

        public List<RTipoCreditos> listaTipoCreditos { get; set; }

        public decimal? TotalComisionCreditos { get; set; }

        public List<RProductos> listaProductos { get; set; }

        public decimal TotalComisionProductos { get; set; }

        public List<RCDPs> listaCDPs { get; set; }

        public decimal? TotalComisionCDPs { get; set; }

        public decimal TotalGenerado { get; set; }

        //Datos
        public DateTime FechaContratacion { get; set; }

        public string UnidadNegocio { get; set; }

        public R_IDP Estado_IDP { get; set; }

        //Constructor
        public Reporte()
        {
            Estado_IDP = new R_IDP();
        }

    }
}