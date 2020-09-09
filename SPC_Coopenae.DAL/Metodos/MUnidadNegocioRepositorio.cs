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
    public class MUnidadNegocioRepositorio : IUnidadNegocioRepositorio
    {
        public void ActualizarUnidadNegocio(UnidadNegocio sucursalP)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.Entry(sucursalP).State = EntityState.Modified;

                dbc.SaveChanges();
            }
        }

        public UnidadNegocio BuscarUnidadNegocio(int id)
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.UnidadNegocio.Find(id);
            }
        }

        public void EliminarUnidadNegocio(int id)
        {
            using (var dbc = new SPC_BD())
            {
                var aEliminar = dbc.UnidadNegocio.Find(id);
                aEliminar.Estado = false;
                dbc.SaveChanges();
            }
        }

        public void InsertarUnidadNegocio(UnidadNegocio sucursal)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.UnidadNegocio.Add(sucursal);
                dbc.SaveChanges();
            }
        }

        public List<UnidadNegocio> ListarUnidadNegocio()
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.UnidadNegocio.Where(x => x.Estado == true).ToList();
            }
        }
    }
}
