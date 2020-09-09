using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DAL.Metodos
{
    public class MTipoCambioRepositorio : ITipoCambioRepositorio
    {

        public TipoCambio BuscarTipoCambioFecha(DateTime fechaP)
        {
            using (var dbc = new SPC_BD())
            {
                TipoCambio defaultTP = new TipoCambio()
                {
                    Estado = false,
                    Fecha = fechaP,
                    IdTipoCambio = -1,
                    Valor = -321
                };

                return dbc.TipoCambio.Where(x => (x.Fecha.Month == fechaP.Month &&
                                                  x.Fecha.Year == fechaP.Year &&
                                                  x.Estado == true)).FirstOrDefault() ?? defaultTP;
            }
        }

        public List<TipoCambio> ListarTipoCambio()
        {
            using (var dbc = new SPC_BD())
            {
                return dbc.TipoCambio.Where(x => x.Estado == true).OrderByDescending(x => x.Fecha).ToList();
            }
        }

        public void InsertarTipoCambio(TipoCambio tipoCambio)
        {
            using (var dbc = new SPC_BD())
            {
                var _tipoCambio = dbc.TipoCambio.Where(x => x.Fecha.Month == tipoCambio.Fecha.Month && 
                                                            x.Fecha.Year == tipoCambio.Fecha.Year)
                                                            .ToList();
                _tipoCambio.ForEach(x => x.Estado = false);

                dbc.TipoCambio.Add(tipoCambio);

                dbc.SaveChanges();

            }
        }
    }
}
