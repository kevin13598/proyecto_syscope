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
    public class DatosEjecutivo
    {
        IEjectutivoRepositorio _ejecutivoBD = new MEjecutivoRepositorio();
        IUnidadNegocioRepositorio _unidadBD = new MUnidadNegocioRepositorio();

        public DateTime Contratacion { get; set; }
        public string UnidadNegocio { get; set; }

        public DatosEjecutivo(int cedulaP)
        {
            Ejecutivo ejecutivo;
            ejecutivo = _ejecutivoBD.BuscarEjecutivo(cedulaP);

            this.Contratacion = ejecutivo.FechaContratacion;
            this.UnidadNegocio = _unidadBD.BuscarUnidadNegocio(ejecutivo.UnidadNegocio).Nombre;
        }

    }
}
