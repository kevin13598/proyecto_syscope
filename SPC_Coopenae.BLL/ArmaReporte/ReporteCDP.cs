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
    public class ReporteCDP
    {

        MReporteCDPRepositorio _reporteCDP_BD;

        public ReporteCDP()
        {
            _reporteCDP_BD = new MReporteCDPRepositorio();
        }

        public MetaCDP metaCDPCorrespondiente { get; set; }
        public decimal SumaColocacionesCDP { get; set; }
        public List<RCDPs> ComisionesPorTipoCDPs { get; set; }

        public void EstablecerMetaCorrespondiente(int cedula)
        {
            metaCDPCorrespondiente = _reporteCDP_BD.BuscaMetaCDP(cedula);
        }

        public void SumarCDPsColocadosIDP(int cedulaP, DateTime fechaP, decimal tipoCambioP)
        {
            SumaColocacionesCDP = (_reporteCDP_BD.SumarCDPParaIDPDolares(cedulaP, fechaP) * tipoCambioP) +
                                   _reporteCDP_BD.SumarCPDParaIDPColones(cedulaP, fechaP);
        }

        public void AsignarComisionesTipoCDPs(int cedulaP, DateTime fechaP, decimal tipoCambioP, decimal IDPActual)
        {
            //Trae la lista
            var ConsultaTipoCDPs = _reporteCDP_BD.ConsultaCDPsConVentas(cedulaP, fechaP);
            foreach (var x in ConsultaTipoCDPs)
            {
                //Si el idp que lleva es mayor al que ocupa, si no, el total es cero
                if (x.IDPNecesario <= IDPActual)
                {
                    //Si ha colocado CDPs
                    if (x.SumaColocaciones != 0)
                    {
                        //Pasa la comision a porcentaje
                        decimal pctComision = x.PCTComision / 100;
                        //Si esta en dolares lo convierte
                        if (x.Moneda == "d")
                        {
                            x.SumaColocaciones *= tipoCambioP;
                        }

                        //Saca el total
                        decimal TotalComision = Convert.ToDecimal(x.SumaColocaciones) * pctComision;

                        if (x.MaxComision != null)
                        {
                            TotalComision = (TotalComision > x.MaxComision ? Convert.ToDecimal(x.MaxComision) : TotalComision);
                        }

                        x.TotalComision = TotalComision;

                    }
                }
                
            }
            this.ComisionesPorTipoCDPs = ConsultaTipoCDPs;
        }

    }
}
