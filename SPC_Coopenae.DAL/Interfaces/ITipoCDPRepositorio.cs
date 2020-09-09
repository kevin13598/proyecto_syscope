using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Interfaces
{
    public interface ITipoCDPRepositorio
    {
        List<TipoCDP> ListarTipoCDP();
        TipoCDP BuscarTipoCDP(int id);
        void InsertarTipoCDP(TipoCDP tipocdp);
        void ActualizarTipoCDP(TipoCDP tipocdp);
        void EliminarTipoCDP(int id);
    }
}
