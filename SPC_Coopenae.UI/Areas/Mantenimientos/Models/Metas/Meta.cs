using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Models.Metas
{
    public class Meta
    {
        [Display(Name = "Id de meta")]
        public int IdMeta { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public bool Estado { get; set; } = true;

        public int Escala { get; set; }

        public int Salario { get; set; }

    }
}