using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DAL.Metodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPC_Coopenae.UI.Areas.Ventas.Controllers
{
    public class ValidaEjecutivoController : Controller
    {

        IEjectutivoRepositorio _repositorioEjecutivo;

        public ValidaEjecutivoController()
        {
            _repositorioEjecutivo = new MEjecutivoRepositorio();

        }

        public JsonResult ValidarEjecutivo(int Ejecutivo)
        {
            try
            {
                var EjecutivoBuscar = _repositorioEjecutivo.BuscarEjecutivo(Ejecutivo);
                if (EjecutivoBuscar == null)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}