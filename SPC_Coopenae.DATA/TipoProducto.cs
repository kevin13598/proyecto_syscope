using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPC_Coopenae.DATA
{
    public class TipoProducto
    {
        [Key]
        public int IdTipoProducto { get; set; }

        public string Descripcion { get; set; }

        public string Moneda { get; set; }

        public bool Estado { get; set; }

        public decimal IDPNecesario { get; set; }

    }
}
