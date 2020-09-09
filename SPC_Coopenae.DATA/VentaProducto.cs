namespace SPC_Coopenae.DATA
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VentaProducto
    {
        [Key]
        public int IdVentaProducto { get; set; }

        public int Ejecutivo { get; set; }

        public DateTime Fecha { get; set; }

        public int Cedula { get; set; }

        public string Nombre { get; set; }

        public string CentroTrabajo { get; set; }

        public int Producto { get; set; }

        public bool Estado { get; set; }

    }
}
