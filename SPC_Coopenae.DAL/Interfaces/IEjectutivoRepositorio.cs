using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Interfaces
{
    public interface IEjectutivoRepositorio
    {
        List<Ejecutivo> ListarEjecutivos();
        Ejecutivo BuscarEjecutivo(int cedula);
        void InsertarEjecutivo(Ejecutivo ejecutivo);
        void ActualizarEjecutivo(Ejecutivo ejecutivo);
        void EliminarEjecutivo(int cedula);

    }
}
