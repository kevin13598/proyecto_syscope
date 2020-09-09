using SPC_Coopenae.DATA;
using SPC_Coopenae.DATA.ObjReportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Metodos.Reportes
{
    public class MReporteCDPRepositorio
    {
        public MetaCDP BuscaMetaCDP(int cedulaP)
        {
            using (var dbc = new SPC_BD())
            {
                return (from ejecutivo in dbc.Ejecutivo
                        join unidad in dbc.UnidadNegocio on ejecutivo.UnidadNegocio equals unidad.IdUnidad
                        join meta in dbc.Meta on unidad.Meta equals meta.IdMeta
                        join metacdp in dbc.MetaCDP on meta.IdMeta equals metacdp.Meta
                        where unidad.Estado == true &&
                              meta.Estado == true &&
                              ejecutivo.Cedula == cedulaP
                        select metacdp).FirstOrDefault();
            }
        }

        public decimal SumarCPDParaIDPColones(int cedulaP, DateTime fechaP)
        {
            using (var dbc = new SPC_BD())
            {
                return (from ventaCDP in dbc.VentaCDP
                        join tipoCDP in dbc.TipoCDP on ventaCDP.TipoCDP equals tipoCDP.IdTipoCDP
                        where tipoCDP.Estado == true &&
                              tipoCDP.Moneda == "c" &&
                              ventaCDP.Ejecutivo == cedulaP &&
                              ventaCDP.Fecha.Month == fechaP.Month &&
                              ventaCDP.Fecha.Year == fechaP.Year
                        select ventaCDP.Monto).DefaultIfEmpty(0).Sum();
            }
        }

        public decimal SumarCDPParaIDPDolares(int cedulaP, DateTime fechaP)
        {
            using (var dbc = new SPC_BD())
            {
                return (from ventaCDP in dbc.VentaCDP
                        join tipoCDP in dbc.TipoCDP on ventaCDP.TipoCDP equals tipoCDP.IdTipoCDP
                        where tipoCDP.Estado == true &&
                              tipoCDP.Moneda == "d" &&
                              ventaCDP.Ejecutivo == cedulaP &&
                              ventaCDP.Fecha.Month == fechaP.Month &&
                              ventaCDP.Fecha.Year == fechaP.Year
                        select ventaCDP.Monto).DefaultIfEmpty(0).Sum();
            }
        }

        public List<RCDPs> ConsultaCDPsConVentas(int cedulaP, DateTime fechaP)
        {
            using (var dbc = new SPC_BD())
            {
                var resultado = (from tipoCDP in dbc.TipoCDP.Where(tc => tc.Estado == true)
                                 join venta in dbc.VentaCDP.Where(v => (v.Estado == true) &&
                                                                       (v.Ejecutivo == cedulaP) &&
                                                                       (v.Fecha.Month == fechaP.Month) &&
                                                                       (v.Fecha.Year == fechaP.Year))
                                            on tipoCDP.IdTipoCDP equals venta.TipoCDP
                                 into x
                                 from z in x.DefaultIfEmpty()
                                 group z by new { tipoCDP.Nombre, tipoCDP.Moneda, tipoCDP.ComisionMaxima, tipoCDP.PCTComision, tipoCDP.IDPNecesario } into g
                                 select new
                                 {
                                     g.Key.Nombre,
                                     g.Key.Moneda,
                                     g.Key.ComisionMaxima,
                                     g.Key.PCTComision,
                                     g.Key.IDPNecesario,
                                     suma = g.Sum(venta => (decimal?)venta.Monto) ?? 0
                                 }).ToList();

                List<RCDPs> listaReporteTipoCDPs = new List<RCDPs>();

                foreach (var x in resultado)
                {
                    listaReporteTipoCDPs.Add(
                        new RCDPs {
                            NombreTipo = x.Nombre,
                            Moneda = x.Moneda,
                            MaxComision = x.ComisionMaxima,
                            PCTComision = x.PCTComision,
                            IDPNecesario = x.IDPNecesario,
                            SumaColocaciones = x.suma
                        });
                }

                return listaReporteTipoCDPs;

            }
        }

    }
}
