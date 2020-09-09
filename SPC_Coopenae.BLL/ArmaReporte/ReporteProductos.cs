using SPC_Coopenae.DAL.Metodos.Reportes;
using SPC_Coopenae.DATA;
using SPC_Coopenae.DATA.ObjReportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.BLL.ArmaReporte
{
    public class ReporteProductos
    {

        MReporteProductosRepositorio _reporteProductosBD;

        public ReporteProductos()
        {
            _reporteProductosBD = new MReporteProductosRepositorio();
        }

        public List<MetaTipoProducto> metaTipoProductosCorrespondiente { get; set; }
        public List<MetaProductosParaIDP> metaYCantidadParaIDP { get; set; }
        public List<RProductos> ComisionesPorProductos { get; set; }
        public List<RTProducto_IDP> TProductosReporteIDP = new List<RTProducto_IDP>();

        public void EstablecerMetaCorrespondiente(int cedula)
        {
            metaTipoProductosCorrespondiente = _reporteProductosBD.BuscarMetaProducto(cedula);
        }

        public void TraerCantidadPorMeta(DateTime fecha, int cedula)
        {
            int [] idsMetas = metaTipoProductosCorrespondiente.Select(x => x.IdMetaTipoProducto).ToArray();
            metaYCantidadParaIDP = _reporteProductosBD.ConsultaCantidadPorMetas(idsMetas, fecha, cedula);
            foreach (var x in metaTipoProductosCorrespondiente)
            {
                int cantidadVendida;
                if ((metaYCantidadParaIDP.Count == 0) || (!(metaYCantidadParaIDP.Any(y => y.IdMeta == x.IdMetaTipoProducto))))
                {
                    cantidadVendida = 0;
                }
                else
                {
                    cantidadVendida = metaYCantidadParaIDP.Find(y => y.IdMeta == x.IdMetaTipoProducto).Cantidad;
                }

                TProductosReporteIDP.Add(
                    new RTProducto_IDP() {
                        Id = x.IdMetaTipoProducto,
                        CantVendida = cantidadVendida,
                        CantMeta = x.MetaCantidad,
                        IDPMeta = x.ValorIDP
                    });
            }
        }

        public void AsignarComisionesProductos(int cedulaP, DateTime fechaP, decimal IDPActual, decimal tipoCambioP)
        {
            var ConsultaProductosVendidos = _reporteProductosBD.ConsultaProductosConVentas(cedulaP, fechaP);

            foreach (var x in ConsultaProductosVendidos)
            {
                x.TotalComision = IDPActual >= x.IDPNecesario ? x.ComisionProducto * x.CantidadVendida : 0;
                x.TotalComision = x.Moneda == "d" ? x.TotalComision * tipoCambioP : x.TotalComision;
            }

            this.ComisionesPorProductos = ConsultaProductosVendidos.ToList();
            _reporteProductosBD.TraeNombres(ref TProductosReporteIDP);
        }

    }
}
