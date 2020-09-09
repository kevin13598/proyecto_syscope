namespace SPC_Coopenae.DATA
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TipoCredito
    {
        [Key]
        public int IdTipoCredito { get; set; }

        public string Nombre { get; set; }

        public decimal? ComisionDistinta { get; set; }

        public decimal? MaximoComision { get; set; }

        public bool SumaIDP { get; set; }

        public bool Estado { get; set; }

    }
}
