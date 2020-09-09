using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPC_Coopenae.DATA;
using SPC_Coopenae.DAL.Metodos;
using SPC_Coopenae.DAL.Interfaces;

namespace SPC_Coopenae.BLL.ArmaReporte
{
    public class TipoCambioReporte
    {
        ITipoCambioRepositorio _tipoCambioBD = new MTipoCambioRepositorio();

        public TipoCambio tipoCambio { get; set; }

        public void TraeTipoCambio(DateTime fechaP)
        {
            this.tipoCambio = _tipoCambioBD.BuscarTipoCambioFecha(fechaP);
        }

    }
}
