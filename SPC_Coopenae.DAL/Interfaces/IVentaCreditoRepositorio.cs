using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Interfaces
{
    public interface IVentaCreditoRepositorio
    {
        List<VentaCredito> ListarVentaCredito();
        VentaCredito BuscarVentaCredito(int id);
        void InsertarVentaCredito(VentaCredito venta);
        void ActualizarVentaCredito(VentaCredito venta);
        void EliminarVentaCredito(int id);
        List<VentaCredito> BuscarListaCreditos(int cedula, DateTime fecha);
    }
}
