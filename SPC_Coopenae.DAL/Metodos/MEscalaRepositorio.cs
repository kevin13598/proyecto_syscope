using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Metodos
{
    public class MEscalaRepositorio : IEscalaRepositorio
    {
        public Escala BuscarEscala(int id)
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.Escala.Find(id);
            }
        }

        public void EliminarEscala(int idEscala)
        {
            using (var dbc = new SPC_BD())
            {
                var eEscala = dbc.Escala.Find(idEscala);
                eEscala.Estado = false;
                dbc.SaveChanges();
            }
        }

        public int InsertarEscala(Escala escala)
        {
            using (var dbc = new SPC_BD())
            {
                escala.Estado = true;
                dbc.Escala.Add(escala);
                dbc.SaveChanges();

                return escala.IdEscala;

            }
        }

        public List<Escala> ListarEscalas()
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.Escala.Where(x => x.Estado == true).ToList();
            }
        }
    }
}
