namespace SPC_Coopenae.DATA
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Ejecutivo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Cedula { get; set; }

        public string CodigoVendedor { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public int Telefono { get; set; }

        public string Correo { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public DateTime FechaContratacion { get; set; }

        public bool Estado { get; set; }

        public int UnidadNegocio { get; set; }
    }
}
