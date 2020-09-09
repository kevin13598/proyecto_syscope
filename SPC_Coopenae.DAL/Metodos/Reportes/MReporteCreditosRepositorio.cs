using SPC_Coopenae.DATA.ObjReportes;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Metodos.Reportes
{
    public class MReporteCreditosRepositorio
    {

        //Trae la meta de credito correspondiente al ejecutivo, segun la unidad de negocio a la que pertenezca
        public MetaCredito BuscaMetaCredito(int cedulaP)
        {
            using (var dbc = new SPC_BD())
            {
                return (from ejecutivo in dbc.Ejecutivo
                        join unidad in dbc.UnidadNegocio on ejecutivo.UnidadNegocio equals unidad.IdUnidad
                        join meta in dbc.Meta on unidad.Meta equals meta.IdMeta
                        join metaCredito in dbc.MetaCredito on meta.IdMeta equals metaCredito.Meta
                        where unidad.Estado == true &&
                              meta.Estado == true &&
                              ejecutivo.Cedula == cedulaP
                        select metaCredito).FirstOrDefault();
            }
        }

        //Suma los montos de los creditos vendidos para sacar el IDP
        public decimal SumarMontosColocadosParaIDP(int cedulaP, DateTime fechaP)
        {
            using (var dbc = new SPC_BD())
            {
                return (from ventaCred in dbc.VentaCredito
                        join tipoCred in dbc.TipoCredito on ventaCred.TipoCredito equals tipoCred.IdTipoCredito
                        where tipoCred.SumaIDP == true && tipoCred.Estado == true &&
                              ventaCred.Ejecutivo == cedulaP &&
                              ventaCred.Fecha.Month == fechaP.Month &&
                              ventaCred.Fecha.Year == fechaP.Year
                        select ventaCred.Monto).DefaultIfEmpty(0).Sum();
            }
        }

        public List<RTipoCreditos> ConsultaTiposCreditosConVentas(int cedulaP, DateTime fechaP)
        {
            using (var dbc = new SPC_BD())
            {
                var resultado = (from tipoCredito in dbc.TipoCredito.Where(tc => tc.Estado == true)
                                 join ventaCredito in dbc.VentaCredito.Where(vc => (vc.Ejecutivo == cedulaP) &&
                                                                                   (vc.Fecha.Month == fechaP.Month) &&
                                                                                   (vc.Fecha.Year == fechaP.Year) &&
                                                                                   (vc.Estado == true))
                                                   on tipoCredito.IdTipoCredito equals ventaCredito.TipoCredito
                                 into x
                                 from z in x.DefaultIfEmpty()
                                 group z by new { tipoCredito.Nombre, tipoCredito.ComisionDistinta, tipoCredito.MaximoComision } into g
                                 select new
                                 {
                                     NombreTipo = g.Key.Nombre,
                                     PCTComisionGanada = g.Key.ComisionDistinta,
                                     MaxComision = g.Key.MaximoComision,
                                     SumaColocaciones = g.Sum(ventaCred => (decimal?)ventaCred.Monto) ?? 0
                                 }).ToList();

                List<RTipoCreditos> listaReporteTipoCreditos = new List<RTipoCreditos>();

                foreach (var x in resultado)
                {
                    listaReporteTipoCreditos.Add(
                        new RTipoCreditos
                        {
                            NombreTipo = x.NombreTipo,
                            PCTComisionGanada = x.PCTComisionGanada,
                            MaxComision = x.MaxComision,
                            SumaColocaciones = x.SumaColocaciones
                        });
                }
                return listaReporteTipoCreditos;

            }
        }

    }
}
