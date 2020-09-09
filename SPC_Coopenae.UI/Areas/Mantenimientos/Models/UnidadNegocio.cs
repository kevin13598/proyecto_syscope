using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Models
{
    public class UnidadNegocio
    {
        public int IdUnidad { get; set; }

        public string Nombre { get; set; }

        public bool Estado { get; set; } = true;

        [Display(Name = "Meta Asignada")]
        public int Meta { get; set; }

    }
}