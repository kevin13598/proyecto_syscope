using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPC_Coopenae.DATA
{
    public class MetaCredito
    {
        [Key]
        public int IdMetaCredito { get; set; }

        public decimal MetaColocacion { get; set; }

        public decimal ValorIDP { get; set; }

        public int Meta { get; set; }

    }
}
