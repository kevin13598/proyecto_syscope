using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Metodos.Reportes
{
    public class MEscalaReporteRepositorio
    {
        public Escala BuscarEscala(int cedula)
        {
            using (var dbc = new SPC_BD())
            {
                return (from ejecutivo in dbc.Ejecutivo
                        join unidad in dbc.UnidadNegocio on ejecutivo.UnidadNegocio equals unidad.IdUnidad
                        join meta in dbc.Meta on unidad.Meta equals meta.IdMeta
                        join escala in dbc.Escala on meta.Escala equals escala.IdEscala
                        where unidad.Estado == true &&
                              meta.Estado == true &&
                              escala.Estado == true &&
                              ejecutivo.Cedula == cedula
                        select escala).FirstOrDefault();
            }
        }

        public List<DetalleEscala> BuscarDetalleEscalas(int IdEscala)
        {
            using (var dbc = new SPC_BD())
            {
                return (from detEscala in dbc.DetalleEscala
                        where detEscala.Escala == IdEscala
                        select detEscala).ToList();
            }
        }


    }
}
