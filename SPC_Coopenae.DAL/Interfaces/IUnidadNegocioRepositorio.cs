using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Interfaces
{
    public interface IUnidadNegocioRepositorio
    {
        List<UnidadNegocio> ListarUnidadNegocio();
        UnidadNegocio BuscarUnidadNegocio(int id);
        void InsertarUnidadNegocio(UnidadNegocio unidadNegocio);
        void ActualizarUnidadNegocio(UnidadNegocio unidadNegocio);
        void EliminarUnidadNegocio(int id);
    }
}
