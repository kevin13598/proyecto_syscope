using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Models
{
    public class Salario
    {
        public int IdSalario { get; set; }

        [Display(Name = "Salario Base")]
        [Required(ErrorMessage ="Este campo es obligatorio")]
        public decimal Base { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public bool Estado { get; set; } = true;

        [Display(Name = "Meses para recibir el salario inicial")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int MesesInicio { get; set; }

        [Display(Name = "Salario Inicial")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal SalarioInicio { get; set; }

        [Display(Name = "Bono Inicial")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal BonoInicio { get; set; }
    }
}