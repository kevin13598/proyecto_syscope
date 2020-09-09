using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Interfaces
{
    public interface IProductoRepositorio
    {
        List<Producto> ListarProducto();
        Producto BuscarProducto(int id);
        void InsertarProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        void EliminarProducto(int id);

    }
}
