using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Interfaces
{
    public interface IMetaRepositorio
    {

        int InsertarMeta(Meta meta);
        void EliminarMeta(int id);
        Meta BuscarMeta(int id);
        List<Meta> ListarMetas();
        DetalleMeta DetallarMeta(int id);

        void InsertarMetaCredito(MetaCredito meta);
        void InsertarMetaCDP(MetaCDP meta);
        int InsertarMetaProducto(MetaTipoProducto meta);
        void InsertarMetaProductoDetalle(MetaTipoProductoDetalle meta);
    }
}
