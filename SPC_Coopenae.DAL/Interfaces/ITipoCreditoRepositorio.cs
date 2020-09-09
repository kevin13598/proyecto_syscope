using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Interfaces
{
    public interface ITipoCreditoRepositorio
    {
        List<TipoCredito> ListarTipoCredito();
        TipoCredito BuscarTipoCredito(int id);
        void InsertarTipoCredito(TipoCredito tipoCredito);
        void ActualizarTipoCredito(TipoCredito tipoCredito);
        void EliminarTipoCredito(int id);

    }
}
