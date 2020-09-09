namespace SPC_Coopenae.DATA
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }

        public string Nombre { get; set; }

        public decimal Comision { get; set; }

        public bool Estado { get; set; }

        public int TipoProducto { get; set; }

    }

}
