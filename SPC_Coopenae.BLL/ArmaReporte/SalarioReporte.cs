using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DAL.Metodos;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.BLL.ArmaReporte
{
    public class SalarioReporte
    {

        ISalarioRepositorio _repoBD = new MSalarioRepositorio();
        IEjectutivoRepositorio _ejecutivoBD = new MEjecutivoRepositorio();

        public Salario sal_correspondiente { get; set; }

        public void FijarSalario(int cedulaP)
        {
            this.sal_correspondiente = _repoBD.BuscarSalarioEjecutivo(cedulaP);
        }

        public int diferencia_Meses(int cedula, DateTime reporte)
        {
            DateTime contratacion = _ejecutivoBD.BuscarEjecutivo(cedula).FechaContratacion;
            return (reporte.Month - contratacion.Month) + 12 * (reporte.Year - contratacion.Year);
        }

    }

}
