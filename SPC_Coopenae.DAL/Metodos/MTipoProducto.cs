using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DATA;
using System.Data.Entity;

namespace SPC_Coopenae.DAL.Metodos
{
    public class MTipoProducto : ITipoProductoRepositorio
    {
        public void ActualizarTipoProducto(TipoProducto tipoProducto)
        {
            using (var dbc = new SPC_BD()) {
                dbc.Entry(tipoProducto).State = EntityState.Modified;

                dbc.SaveChanges();
            }
        }

        public TipoProducto BuscarTipoProducto(int id)
        {
            using (var dbc = new SPC_BD()) {
                return dbc.TipoProducto.Find(id);
            }
        }

        public void EliminarTipoProducto(int id)
        {
            using (var dbc = new SPC_BD())
            {
                var aEliminar = dbc.TipoProducto.Find(id);
                aEliminar.Estado = false;
                dbc.SaveChanges();
            }
        }

        public void InsertarTipoProducto(TipoProducto tipoProducto)
        {
            using (var dbc = new SPC_BD()) {
                dbc.TipoProducto.Add(tipoProducto);
                dbc.SaveChanges();
            }
        }

        public List<TipoProducto> ListarTipoProducto()
        {
            using (var dbc = new SPC_BD()) {
                return dbc.TipoProducto.Where(x => x.Estado == true).ToList();
            }
        }
    }
}
