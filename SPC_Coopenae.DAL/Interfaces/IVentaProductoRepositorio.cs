using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Interfaces
{
    public interface IVentaProductoRepositorio
    {

        List<VentaProducto> ListarVentasProducto();
        VentaProducto BuscarVentaProducto(int id);
        void InsertarVentaProducto(VentaProducto venta);
        void ActualizarVentaProducto(VentaProducto venta);
        void EliminarVentaProducto(int id);
        List<VentaProducto> BuscarListaVentaProductos(int cedula, DateTime fecha);

    }
}
