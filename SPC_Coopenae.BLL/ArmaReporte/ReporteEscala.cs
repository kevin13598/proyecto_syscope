using SPC_Coopenae.DAL.Metodos.Reportes;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.BLL.ArmaReporte
{
    public class ReporteEscala
    {
        //Trae datos de DAL
        MEscalaReporteRepositorio _reporteEscalaBD;

        public ReporteEscala()
        {
            _reporteEscalaBD = new MEscalaReporteRepositorio();
        }

        private Escala escalaCorrespondiente;
        private List<DetalleEscala> detallesEscalaCorrespondientes;
        public decimal PCTComision { get; set; }

        public void EstablecerEscalaCorrespondiente(int cedula)
        {
            this.escalaCorrespondiente = _reporteEscalaBD.BuscarEscala(cedula);
            EstablecerDetallesEscala();
        }

        private void EstablecerDetallesEscala()
        {
            this.detallesEscalaCorrespondientes = _reporteEscalaBD.BuscarDetalleEscalas(escalaCorrespondiente.IdEscala);
        }

        public void EstablecerPCTComisionSegunIDP(decimal IDPActual)
        {
            foreach(var det_esc in this.detallesEscalaCorrespondientes)
            {
                if ((IDPActual >= det_esc.PCTMinimo) && (IDPActual <= det_esc.PCTMaximo))
                {
                    this.PCTComision = det_esc.PCTComision;
                    return;
                }
            }
        }

    }
}
