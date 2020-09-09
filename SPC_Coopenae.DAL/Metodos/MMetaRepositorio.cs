using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Metodos
{
    public class MMetaRepositorio : IMetaRepositorio
    {

        public int InsertarMeta(Meta meta)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.Meta.Add(meta);
                dbc.SaveChanges();

                return meta.IdMeta;
            }
        }

        public void EliminarMeta(int id)
        {
            using (var dbc = new SPC_BD())
            {
                var metaEliminar = dbc.Meta.Find(id);
                metaEliminar.Estado = false;
                dbc.SaveChanges();
            }
        }

        public Meta BuscarMeta(int id)
        {
            using (var dbc = new SPC_BD())
            {
                var metaB = dbc.Meta.Find(id);
                return metaB;
            }
        }

        public void InsertarMetaCredito(MetaCredito meta)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.MetaCredito.Add(meta);
                dbc.SaveChanges();
            }
        }

        public void InsertarMetaCDP(MetaCDP meta)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.MetaCDP.Add(meta);
                dbc.SaveChanges();
            }
        }

        public List<Meta> ListarMetas()
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.Meta.Where(x => x.Estado == true).ToList();
            }
        }

        public int InsertarMetaProducto(MetaTipoProducto meta)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.MetaTipoProducto.Add(meta);
                dbc.SaveChanges();

                return meta.IdMetaTipoProducto;
            }
        }

        public void InsertarMetaProductoDetalle(MetaTipoProductoDetalle meta)
        {
            using (var dbc = new SPC_BD())
            {
                dbc.MetaTipoProductoDetalle.Add(meta);
                dbc.SaveChanges();
            }
        }

        public DetalleMeta DetallarMeta(int id)
        {
            using (var dbc = new SPC_BD())
            {
                var resultado = (from metaDetallar in dbc.Meta
                                 join mCredito in dbc.MetaCredito on metaDetallar.IdMeta equals mCredito.Meta
                                 join mCDP in dbc.MetaCDP on metaDetallar.IdMeta equals mCDP.Meta
                                 where metaDetallar.IdMeta == id
                                 select new
                                 {
                                     metaDetallar.Descripcion,
                                     metaDetallar.Escala,
                                     metaDetallar.Salario,
                                     mCredito.MetaColocacion,
                                     idp_cred = mCredito.ValorIDP,
                                     mCDP.Metacdp,
                                     idp_cdp = mCDP.ValorIDP,
                                 }).First();

                var resultadoProds = (from metaDetallar in dbc.Meta
                                      join mProductos in dbc.MetaTipoProducto on metaDetallar.IdMeta equals mProductos.Meta
                                      join mProdDetalle in dbc.MetaTipoProductoDetalle on mProductos.IdMetaTipoProducto equals mProdDetalle.MetaTipoProducto
                                      join tipoProductos in dbc.TipoProducto on mProdDetalle.TipoProducto equals tipoProductos.IdTipoProducto
                                      where metaDetallar.IdMeta == id
                                      orderby mProductos.IdMetaTipoProducto
                                      select new
                                      {
                                          mProductos.IdMetaTipoProducto,
                                          mProductos.MetaCantidad,
                                          mProductos.ValorIDP,
                                          tipoProductos.Descripcion
                                      }).ToList();

                DetalleMeta metaEnviar = new DetalleMeta()
                {
                    Descripcion = resultado.Descripcion,
                    Escala = resultado.Escala,
                    Salario = resultado.Salario,
                    ColocacionCredito = resultado.MetaColocacion,
                    IDP_Credito = resultado.idp_cred,
                    ColocacionCDP = resultado.Metacdp,
                    IDP_CDP = resultado.idp_cdp
                };

                List<DetalleMetaTipoProducto> ListaMetaSTP = new List<DetalleMetaTipoProducto>();

                int idAnterior = -1;
                foreach (var x in resultadoProds)
                {
                    if (x.IdMetaTipoProducto != idAnterior)
                    {
                        DetalleMetaTipoProducto mtp_detalle = new DetalleMetaTipoProducto()
                        {
                            Id_MTP = x.IdMetaTipoProducto,
                            Cantidad = x.MetaCantidad,
                            IDP_TProductos = x.ValorIDP,
                        };
                        mtp_detalle.ListaTipoProductos = new List<string>();
                        ListaMetaSTP.Add(mtp_detalle);
                    }
                    idAnterior = x.IdMetaTipoProducto;
                }

                foreach (var res in resultadoProds)
                {
                    var dmtp = ListaMetaSTP.Find(x => x.Id_MTP == res.IdMetaTipoProducto);
                    dmtp.ListaTipoProductos.Add(res.Descripcion);
                }

                metaEnviar.TipoProductos = ListaMetaSTP;
                return metaEnviar;

            }
        }

    }
}