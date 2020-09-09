using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Interfaces
{
    public interface IEscalaRepositorio
    {
        List<Escala> ListarEscalas();

        Escala BuscarEscala(int id);

        int InsertarEscala(Escala escala);

        void EliminarEscala(int idEscala);

    }
}
