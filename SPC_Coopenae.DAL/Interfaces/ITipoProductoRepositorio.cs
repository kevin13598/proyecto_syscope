using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPC_Coopenae.DATA;

namespace SPC_Coopenae.DAL.Interfaces
{
    public interface ITipoProductoRepositorio
    {
        List<TipoProducto> ListarTipoProducto();
        TipoProducto BuscarTipoProducto(int id);
        void InsertarTipoProducto(TipoProducto tipoProducto);
        void ActualizarTipoProducto(TipoProducto tipoProducto);
        void EliminarTipoProducto(int id);
    }
}
