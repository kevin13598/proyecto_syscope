using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Metodos
{
    public class MDetalleEscalaRepositorio : IDetalleEscalaRepositorio
    {

        public void InsertarDetalleEscala(DetalleEscala dEsc)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.DetalleEscala.Add(dEsc);
                dbc.SaveChanges();
            }
        }

        public List<DetalleEscala> ListarDetalleEscalas(int idEscala)
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.DetalleEscala.Where(x => x.Escala == idEscala).ToList();
            }
        }

        
    }
}
