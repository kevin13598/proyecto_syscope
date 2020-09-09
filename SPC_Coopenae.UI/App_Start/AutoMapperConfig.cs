using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPC_Coopenae
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UI.Areas.Mantenimientos.Models.Ejecutivo, DATA.Ejecutivo>();
                cfg.CreateMap<DATA.Ejecutivo, UI.Areas.Mantenimientos.Models.Ejecutivo>();

                cfg.CreateMap<UI.Areas.Mantenimientos.Models.UnidadNegocio, DATA.UnidadNegocio>();
                cfg.CreateMap<DATA.UnidadNegocio, UI.Areas.Mantenimientos.Models.UnidadNegocio>();

                cfg.CreateMap<UI.Areas.Ventas.Models.VentaCredito, DATA.VentaCredito>();
                cfg.CreateMap<DATA.VentaCredito, UI.Areas.Ventas.Models.VentaCredito>();
              
                cfg.CreateMap<UI.Areas.Ventas.Models.VentaProducto, DATA.VentaProducto>();
                cfg.CreateMap<DATA.VentaProducto, UI.Areas.Ventas.Models.VentaProducto>();

                cfg.CreateMap<UI.Areas.Mantenimientos.Models.Producto, DATA.Producto>();
                cfg.CreateMap<DATA.Producto, UI.Areas.Mantenimientos.Models.Producto>();
                
                cfg.CreateMap<UI.Areas.Mantenimientos.Models.TipoCredito, DATA.TipoCredito>();
                cfg.CreateMap<DATA.TipoCredito, UI.Areas.Mantenimientos.Models.TipoCredito>();

                cfg.CreateMap<UI.Areas.Ventas.Models.VentaCDP, DATA.VentaCDP>();
                cfg.CreateMap<DATA.VentaCDP, UI.Areas.Ventas.Models.VentaCDP>();

                cfg.CreateMap<UI.Areas.Mantenimientos.Models.TipoProducto, DATA.TipoProducto>();
                cfg.CreateMap<DATA.TipoProducto, UI.Areas.Mantenimientos.Models.TipoProducto>();

                cfg.CreateMap<UI.Areas.Mantenimientos.Models.Escala, DATA.Escala>();
                cfg.CreateMap<DATA.Escala, UI.Areas.Mantenimientos.Models.Escala>();

                cfg.CreateMap<UI.Models.ObjsReporte.RTipoCreditos, DATA.ObjReportes.RTipoCreditos>();
                cfg.CreateMap<DATA.ObjReportes.RTipoCreditos, UI.Models.ObjsReporte.RTipoCreditos>();

                cfg.CreateMap<UI.Models.ObjsReporte.RTipoCreditos, DATA.ObjReportes.RCDPs>();
                cfg.CreateMap<DATA.ObjReportes.RTipoCreditos, UI.Models.ObjsReporte.RCDPs>();

                cfg.CreateMap<UI.Models.ObjsReporte.RProductos, DATA.ObjReportes.RProductos>();
                cfg.CreateMap<DATA.ObjReportes.RProductos, UI.Models.ObjsReporte.RProductos>();

                cfg.CreateMap<UI.Models.ObjsReporte.RTProducto_IDP, DATA.ObjReportes.RTProducto_IDP>();
                cfg.CreateMap<DATA.ObjReportes.RTProducto_IDP, UI.Models.ObjsReporte.RTProducto_IDP>();

                cfg.CreateMap<UI.Areas.Mantenimientos.Models.Salario, DATA.Salario>();
                cfg.CreateMap<DATA.Salario, UI.Areas.Mantenimientos.Models.Salario>();

                cfg.CreateMap<UI.Areas.Mantenimientos.Models.Metas.Meta, DATA.Meta>();
                cfg.CreateMap<DATA.Meta, UI.Areas.Mantenimientos.Models.Metas.Meta>();

                cfg.CreateMap<UI.Areas.Mantenimientos.Models.Metas.MetaCredito, DATA.MetaCredito>();
                cfg.CreateMap<DATA.MetaCredito, UI.Areas.Mantenimientos.Models.Metas.MetaCredito>();

                cfg.CreateMap<UI.Areas.Mantenimientos.Models.Metas.MetaCDP, DATA.MetaCDP>();
                cfg.CreateMap<DATA.MetaCDP, UI.Areas.Mantenimientos.Models.Metas.MetaCDP>();

            });
        }
    }
}