namespace SPC_Coopenae.DATA
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VentaCredito
    {
        [Key]
        public int IdVentaCredito { get; set; }

        public int Ejecutivo { get; set; }

        public DateTime Fecha { get; set; }

        public int Cedula { get; set; }

        public string Nombre { get; set; }

        public string CentroTrabajo { get; set; }

        public DateTime FechaAfiliacion { get; set; }

        public int NumeroOperacion { get; set; }

        public decimal Monto { get; set; }

        public int PlazoMeses { get; set; }

        public bool Estado { get; set; }

        public int TipoCredito { get; set; }

    }

}
