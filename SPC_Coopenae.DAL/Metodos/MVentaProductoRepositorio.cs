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
    public class MVentaProductoRepositorio : IVentaProductoRepositorio
    {
        public void ActualizarVentaProducto(VentaProducto venta)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.Entry(venta).State = EntityState.Modified;

                dbc.SaveChanges();

            }
        }

        public VentaProducto BuscarVentaProducto(int id)
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.VentaProducto.Find(id);
            }
        }

        public void EliminarVentaProducto(int id)
        {
            using (var dbc = new SPC_BD())
            {
                VentaProducto colocacion = dbc.VentaProducto.Find(id);
                dbc.VentaProducto.Remove(colocacion);
                dbc.SaveChanges();
            }
        }

        public void InsertarVentaProducto(VentaProducto venta)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.VentaProducto.Add(venta);
                dbc.SaveChanges();
            }
        }

        public List<VentaProducto> ListarVentasProducto()
        {
            using (var dbc = new SPC_BD())
            {
                return (from ventaProductos in dbc.VentaProducto
                        join prod in dbc.Producto on ventaProductos.Producto equals prod.IdProducto
                        join tipo in dbc.TipoProducto on prod.TipoProducto equals tipo.IdTipoProducto
                        join ejec in dbc.Ejecutivo on ventaProductos.Ejecutivo equals ejec.Cedula
                        where ventaProductos.Estado == true &&
                        ejec.Estado == true &&
                        tipo.Estado == true && prod.Estado == true
                        select ventaProductos).ToList();
            }
        }

        public List<VentaProducto> BuscarListaVentaProductos(int cedula, DateTime fecha)
        {
            using (var dbc = new SPC_BD())
            {
                return (from ventaProductos in dbc.VentaProducto
                        join prod in dbc.Producto on ventaProductos.Producto equals prod.IdProducto
                        join tipo in dbc.TipoProducto on prod.TipoProducto equals tipo.IdTipoProducto
                        join ejec in dbc.Ejecutivo on ventaProductos.Ejecutivo equals ejec.Cedula
                        where ventaProductos.Ejecutivo == cedula &&
                        ejec.Estado == true &&
                        ventaProductos.Fecha.Month == fecha.Month &&
                        ventaProductos.Fecha.Year == fecha.Year &&
                        ventaProductos.Estado == true &&
                        tipo.Estado == true && prod.Estado == true
                        select ventaProductos).ToList();
            }
            
        }

    }
}
