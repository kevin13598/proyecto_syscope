using SPC_Coopenae.DATA.ObjReportes;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Metodos.Reportes
{
    public class MReporteProductosRepositorio
    {

        public List<MetaTipoProducto> BuscarMetaProducto(int cedulaP)
        {
            using (var dbc = new SPC_BD())
            {
                return (from ejecutivo in dbc.Ejecutivo
                        join unidad in dbc.UnidadNegocio on ejecutivo.UnidadNegocio equals unidad.IdUnidad
                        join meta in dbc.Meta on unidad.Meta equals meta.IdMeta
                        join metaTipoProd in dbc.MetaTipoProducto on meta.IdMeta equals metaTipoProd.Meta
                        where unidad.Estado == true &&
                              meta.Estado == true &&
                              ejecutivo.Cedula == cedulaP
                        select metaTipoProd).ToList();
            }
        }

        //Metodo que devuelve los IDs de producto de cada meta de tipo de producto
        public List<MetaProductosParaIDP> ConsultaCantidadPorMetas(int[] metaProd, DateTime fecha, int cedula)
        {
            using (var dbc = new SPC_BD())
            {
                var query = from ventaProd in dbc.VentaProducto
                            join producto in dbc.Producto on ventaProd.Producto equals producto.IdProducto
                            join tipoProducto in dbc.TipoProducto on producto.TipoProducto equals tipoProducto.IdTipoProducto
                            join detalle in dbc.MetaTipoProductoDetalle on tipoProducto.IdTipoProducto equals detalle.TipoProducto
                            join meta in dbc.MetaTipoProducto on detalle.MetaTipoProducto equals meta.IdMetaTipoProducto
                            where ventaProd.Fecha.Month == fecha.Month && ventaProd.Fecha.Year == fecha.Year &&
                                  ventaProd.Estado == true && producto.Estado == true && tipoProducto.Estado == true &&
                                  metaProd.Contains(meta.IdMetaTipoProducto) &&
                                  ventaProd.Ejecutivo == cedula
                            group ventaProd by meta.IdMetaTipoProducto into x
                            select new
                            {
                                IdMeta = x.Key,
                                Cantidad = x.Count()
                            };


                List<MetaProductosParaIDP> listaDevolver = new List<MetaProductosParaIDP>();

                foreach (var x in query)
                {
                    listaDevolver.Add(
                        new MetaProductosParaIDP
                        {
                            IdMeta = x.IdMeta,
                            Cantidad = x.Cantidad
                        });
                }

                return listaDevolver;

            }
        }

        public List<RProductos> ConsultaProductosConVentas(int cedulaP, DateTime fechaP)
        {
            using (var dbc = new SPC_BD())
            {
                var query = (from tipoProducto in dbc.TipoProducto.Where(tp => tp.Estado == true)
                             join producto in dbc.Producto.Where(p => p.Estado == true)
                                           on tipoProducto.IdTipoProducto equals producto.TipoProducto
                             join ventaProducto in dbc.VentaProducto.Where(vp => (vp.Ejecutivo == cedulaP) &&
                                                                                 (vp.Fecha.Month == fechaP.Month) &&
                                                                                 (vp.Fecha.Year == fechaP.Year))
                                                on producto.IdProducto equals ventaProducto.Producto
                             into x
                             from z in x.DefaultIfEmpty()
                             group z by new
                             {
                                 tipoProducto.Descripcion,
                                 tipoProducto.IDPNecesario,
                                 tipoProducto.Moneda,
                                 producto.Nombre,
                                 producto.Comision
                             } into g
                             select new
                             {
                                 TipoProducto = g.Key.Descripcion,
                                 IDPNecesario = g.Key.IDPNecesario,
                                 Moneda = g.Key.Moneda,
                                 Producto = g.Key.Nombre,
                                 Comision = g.Key.Comision,
                                 Cantidad = g.Count(venProd => venProd.IdVentaProducto != null),
                             }).ToList();

                List<RProductos> listaReporteProductos = new List<RProductos>();

                foreach (var x in query)
                {
                    listaReporteProductos.Add(
                        new RProductos
                        {
                            TipoProducto = x.TipoProducto,
                            IDPNecesario = x.IDPNecesario,
                            Moneda = x.Moneda,
                            Nombre = x.Producto,
                            ComisionProducto = x.Comision,
                            CantidadVendida = x.Cantidad
                        });
                }
                return listaReporteProductos;

            }
        }

        public void TraeNombres(ref List<RTProducto_IDP> ListaRProdsIDP)
        {
            using (var dbc = new SPC_BD())
            {
                int[] idsMetas = ListaRProdsIDP.Select(x => x.Id).ToArray();

                var resultado = (from m in dbc.MetaTipoProducto
                                 join mtpd in dbc.MetaTipoProductoDetalle on m.IdMetaTipoProducto equals mtpd.MetaTipoProducto
                                 join tp in dbc.TipoProducto on mtpd.TipoProducto equals tp.IdTipoProducto
                                 where idsMetas.Contains(m.IdMetaTipoProducto)
                                 select new { m.IdMetaTipoProducto, tp.Descripcion }).ToList();

                foreach (var r in resultado)
                {
                    ListaRProdsIDP.Find(x => x.Id == r.IdMetaTipoProducto).TipoProductos.Add(r.Descripcion);
                }

            }
        }

    }
}
