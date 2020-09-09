namespace SPC_Coopenae.DATA
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UnidadNegocio
    {
        [Key]
        public int IdUnidad { get; set; }

        public string Nombre { get; set; }

        public bool Estado { get; set; }

        public int Meta { get; set; }

    }
}
