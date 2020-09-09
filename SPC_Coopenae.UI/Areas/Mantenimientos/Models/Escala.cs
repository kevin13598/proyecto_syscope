using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Models
{
    public class Escala
    {
        
        public int IdEscala { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Descripcion { get; set; }

        public bool Estado { get; set; } = true;

    }
}