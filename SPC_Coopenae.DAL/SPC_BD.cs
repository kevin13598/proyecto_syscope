using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL
{
    public class SPC_BD : DbContext
    {

        public SPC_BD() : base("name=conexion")
        {
        }

        //EF "pluraliza" los nombres de los campos, con esto lo deja de hacer, si lo hace, no encunetra las tablas de la BD
        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        //Agregar las tablas que tenga la BD
        public DbSet<Ejecutivo> Ejecutivo { get; set; }
        public DbSet<Meta> Meta { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<TipoCredito> TipoCredito { get; set; }
        public DbSet<TipoCDP> TipoCDP { get; set; }
        public DbSet<UnidadNegocio> UnidadNegocio { get; set; }
        public DbSet<VentaCDP> VentaCDP { get; set; }
        public DbSet<VentaCredito> VentaCredito { get; set; }
        public DbSet<VentaProducto> VentaProducto { get; set; }
        public DbSet<TipoCambio> TipoCambio { get; set; }
        public DbSet<MetaCDP> MetaCDP { get; set; }
        public DbSet<TipoProducto> TipoProducto { get; set; }
        public DbSet<MetaCredito> MetaCredito { get; set; }
        public DbSet<Escala> Escala { get; set; }
        public DbSet<DetalleEscala> DetalleEscala { get; set; }
        public DbSet<Salario> Salario { get; set; }
        public DbSet<MetaTipoProducto> MetaTipoProducto { get; set; }
        public DbSet<MetaTipoProductoDetalle> MetaTipoProductoDetalle { get; set; }

    }

}
