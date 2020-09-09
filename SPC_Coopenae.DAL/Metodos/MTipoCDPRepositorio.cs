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
    public class MTipoCDPRepositorio : ITipoCDPRepositorio
    {
        public void ActualizarTipoCDP(TipoCDP tipocdp)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.Entry(tipocdp).State = EntityState.Modified;

                dbc.SaveChanges();

            }
        }

        public TipoCDP BuscarTipoCDP(int id)
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.TipoCDP.Find(id);
            }
        }

        public void EliminarTipoCDP(int id)
        {
            using (var dbc = new SPC_BD())
            {
                var aEliminar = dbc.TipoCDP.Find(id);
                aEliminar.Estado = false;
                dbc.SaveChanges();
            }
        }

        public void InsertarTipoCDP(TipoCDP tipocdp)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.TipoCDP.Add(tipocdp);
                dbc.SaveChanges();
            }
        }

        public List<TipoCDP> ListarTipoCDP()
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.TipoCDP.Where(x => x.Estado == true).ToList();
            }
        }
    }
}
