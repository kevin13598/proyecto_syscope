using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Metodos
{
    public class MVentaCreditoRepositorio : IVentaCreditoRepositorio
    {
        public void ActualizarVentaCredito(VentaCredito venta)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.Entry(venta).State = EntityState.Modified;

                dbc.SaveChanges();

            }
        }

        public VentaCredito BuscarVentaCredito(int id)
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.VentaCredito.Find(id);
            }
        }

        public void EliminarVentaCredito(int id)
        {
            using (var dbc = new SPC_BD())
            {
                var eColCred = dbc.VentaCredito.Find(id);
                dbc.VentaCredito.Remove(eColCred);

                dbc.SaveChanges();
            }
        }

        public void InsertarVentaCredito(VentaCredito venta)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.VentaCredito.Add(venta);

                dbc.SaveChanges();
            }
        }

        public List<VentaCredito> ListarVentaCredito()
        {
            using (var dbc = new SPC_BD())
            {
                return (from creditos in dbc.VentaCredito
                        join tipo in dbc.TipoCredito on creditos.TipoCredito equals tipo.IdTipoCredito
                        join ejec in dbc.Ejecutivo on creditos.Ejecutivo equals ejec.Cedula
                        where creditos.Estado == true &&
                        ejec.Estado == true &&
                        tipo.Estado == true
                        select creditos).ToList();
            }
        }

        public List<VentaCredito> BuscarListaCreditos(int ejecutivo, DateTime fecha)
        {
            using (var dbc = new SPC_BD())
            {
                return (from creditos in dbc.VentaCredito
                        join tipo in dbc.TipoCredito on creditos.TipoCredito equals tipo.IdTipoCredito
                        join ejec in dbc.Ejecutivo on creditos.Ejecutivo equals ejec.Cedula
                        where creditos.Ejecutivo == ejecutivo &&
                        ejec.Estado == true &&
                        creditos.Fecha.Month == fecha.Month &&
                        creditos.Fecha.Year == fecha.Year &&
                        creditos.Estado == true && 
                        tipo.Estado == true
                        select creditos).ToList();
            }
        }




    }
}
