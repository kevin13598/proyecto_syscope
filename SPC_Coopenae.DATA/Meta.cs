namespace SPC_Coopenae.DATA
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Meta
    {
        [Key]
        public int IdMeta { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public int Escala { get; set; }

        public int Salario { get; set; }

    }
}
