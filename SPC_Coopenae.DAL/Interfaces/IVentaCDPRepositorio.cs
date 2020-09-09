using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Interfaces
{
    public interface IVentaCDPRepositorio
    {
        List<VentaCDP> ListarCDP();
        VentaCDP BuscarCDP(int id);
        void InsertarCDP(VentaCDP cdp);
        void ActualizarCDP(VentaCDP cdp);
        void EliminarCDP(int id);
        List<VentaCDP> BuscarListarCDP(int ejecutivo, DateTime fecha);
    }
}
