using SPC_Coopenae.DATA;
using SPC_Coopenae.DATA.ObjReportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.BLL.ArmaReporte
{
    public class CalculaIDP
    {

        public decimal TotalIDP { get; set; }

        public MetaCredito metaCred { get; set; }
        public MetaCDP metaCDP { get; set; }
        public List<MetaTipoProducto> metaTipoProducto { get; set; }
        //hay que hacer que no se pase de la cantidad del idp, porque por ejemplo si es 70 puede que en la suma haya mas de eso
        public decimal CreditoIDP;
        public decimal ProductosIDP;
        public decimal CDP_IDP;

        public void FijarIDPCred(decimal montoColocado)
        {
            if (metaCred.MetaColocacion == 0)
            {
                CreditoIDP = 0;
                return;
            }
            if (metaCred != null)
            {
                decimal porcentajeObtenido = montoColocado / metaCred.MetaColocacion;
                CreditoIDP = porcentajeObtenido * metaCred.ValorIDP;
                CreditoIDP = CreditoIDP > metaCred.ValorIDP ? metaCred.ValorIDP : CreditoIDP;
            }
            else
            {
                return;
            }
        }

        public void FijarIDPProductos(List<MetaProductosParaIDP> metaYCantidad, ref List<RTProducto_IDP> reporteIDP)
        {
            ProductosIDP = 0;
            foreach (var meta in metaTipoProducto)
            {
                var correspondiente = metaYCantidad.Find(x => x.IdMeta == meta.IdMetaTipoProducto);
                decimal porcentajeObtenido;
                if (correspondiente == null)
                {
                    porcentajeObtenido = 0;
                }
                else
                {
                    porcentajeObtenido = (decimal)correspondiente.Cantidad / (decimal)meta.MetaCantidad;
                }
                
                decimal IDPProdGanado = porcentajeObtenido * meta.ValorIDP;
                IDPProdGanado = IDPProdGanado > meta.ValorIDP ? meta.ValorIDP : IDPProdGanado;
                reporteIDP.Find(x => x.Id == meta.IdMetaTipoProducto).IDPGanado = IDPProdGanado;
                ProductosIDP += IDPProdGanado;
            }
        }

        public void FijarIDP_CPDs(decimal montoColocado)
        {
            if (metaCDP.Metacdp == 0)
            {
                CDP_IDP = 0;
                return;
            }
            if (metaCDP != null)
            {
                decimal porcentajeObtenido = montoColocado / metaCDP.Metacdp;
                CDP_IDP = porcentajeObtenido * metaCDP.ValorIDP;
                CDP_IDP = CDP_IDP > metaCDP.ValorIDP ? metaCDP.ValorIDP : CDP_IDP;
            }
            else
            {
                return;
            }
        }

        public void SumarIDps()
        {
            TotalIDP = 0;
            TotalIDP += CreditoIDP + ProductosIDP + CDP_IDP;
        }


    }

}
