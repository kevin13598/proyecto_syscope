using SPC_Coopenae.DAL.Metodos.Reportes;
using SPC_Coopenae.DATA.ObjReportes;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.BLL.ArmaReporte
{
    public class ReporteCreditos
    {

        MReporteCreditosRepositorio _reporteCredsBD;

        public ReporteCreditos()
        {
            _reporteCredsBD = new MReporteCreditosRepositorio();
        }

        public MetaCredito metaCreditoCorrespondinte { get; set; }
        public decimal SumaColocaciones { get; set; }
        public List<RTipoCreditos> ComisionesPorTipoCreditos { get; set; }

        public void EstablecerMetaCorrespondiente(int cedula)
        {
            metaCreditoCorrespondinte = _reporteCredsBD.BuscaMetaCredito(cedula);
        }

        public void SumarMontosColocadosIDP(int cedulaP, DateTime fechaP)
        {
            this.SumaColocaciones = _reporteCredsBD.SumarMontosColocadosParaIDP(cedulaP, fechaP);
        }

        public void AsignarComisionesTipoCreditos(int cedulaP, DateTime fechaP, decimal PCTcomisionGanadaP)
        {
            var ConsultaTipoCreditosColocados = _reporteCredsBD.ConsultaTiposCreditosConVentas(cedulaP, fechaP);
            foreach (var x in ConsultaTipoCreditosColocados)
            {
                //Asigna  la comision que viene de parametro, si es null, si no lo es, usa la que esta asignada
                x.PCTComisionGanada = (x.PCTComisionGanada == null ? PCTcomisionGanadaP : Convert.ToDecimal(x.PCTComisionGanada));
                if (x.SumaColocaciones != 0)
                {
                    //Pasa la comision a porcentaje
                    decimal pctComision = (decimal) x.PCTComisionGanada / 100;
                    //Saca la comision ganada en colones y la asgna al objeto
                    decimal TotalComisionCls = Convert.ToDecimal(x.SumaColocaciones) * pctComision;
                    //Comprueba que la comision no sea mayor
                    if (x.MaxComision != null)
                    {
                        TotalComisionCls = (TotalComisionCls > x.MaxComision ? Convert.ToDecimal(x.MaxComision) : TotalComisionCls);
                    }

                    x.TotalComision = TotalComisionCls;
                }
                
            }
            this.ComisionesPorTipoCreditos = ConsultaTipoCreditosColocados;
        }

    }
}
