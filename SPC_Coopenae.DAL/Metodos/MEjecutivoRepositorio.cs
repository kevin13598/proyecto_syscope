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
    public class MEjecutivoRepositorio : IEjectutivoRepositorio
    {
        public void ActualizarEjecutivo(Ejecutivo ejecutivoP)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.Entry(ejecutivoP).State = EntityState.Modified;

                dbc.SaveChanges();

            }
        }

        public Ejecutivo BuscarEjecutivo(int cedula)
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.Ejecutivo.Find(cedula);
            }
        }

        public void EliminarEjecutivo(int cedula)
        {
            using (var dbc = new SPC_BD())
            {
                var aEliminar = dbc.Ejecutivo.Find(cedula);
                aEliminar.Estado = false;
                dbc.SaveChanges();
            }
        }

        public void InsertarEjecutivo(Ejecutivo ejecutivo)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.Ejecutivo.Add(ejecutivo);
                dbc.SaveChanges();
            }
        }

        public List<Ejecutivo> ListarEjecutivos()
        {
            using (var dbc  = new SPC_BD())
            {
                return dbc.Ejecutivo.Where(x => x.Estado == true).ToList();
            }
        }
    }
}
