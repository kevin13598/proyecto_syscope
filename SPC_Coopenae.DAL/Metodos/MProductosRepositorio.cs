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
    public class MProductoRepositorio : IProductoRepositorio
    {
        public void ActualizarProducto(Producto producto)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.Entry(producto).State = EntityState.Modified;
                dbc.SaveChanges();
            }
        }

        public Producto BuscarProducto(int id)
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.Producto.Find(id);
            }
        }

        public void EliminarProducto(int id)
        {
            using (var dbc = new SPC_BD())
            {
                var aEliminar = dbc.Producto.Find(id);
                aEliminar.Estado = false;
                dbc.SaveChanges();
            }
        }

        public void InsertarProducto(Producto producto)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.Producto.Add(producto);
                dbc.SaveChanges();
            }
        }

        public List<Producto> ListarProducto()
        {
            using (var dbc = new SPC_BD())
            {
                return (from prod in dbc.Producto
                        join tipo in dbc.TipoProducto on prod.TipoProducto equals tipo.IdTipoProducto
                        where tipo.Estado == true && prod.Estado == true
                        select prod).ToList();
            }
        }
    }
}
