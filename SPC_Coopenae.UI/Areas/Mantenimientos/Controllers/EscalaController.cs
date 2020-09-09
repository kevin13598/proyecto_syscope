using AutoMapper;
using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DAL.Metodos;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Controllers
{
    public class EscalaController : Controller
    {
        IEscalaRepositorio _repositorioEscala;
        IDetalleEscalaRepositorio _repositorioDetallesE;

        public EscalaController()
        {
            _repositorioEscala = new MEscalaRepositorio();
            _repositorioDetallesE = new MDetalleEscalaRepositorio();
        }


        public ActionResult Index()
        {
            try
            {
                var listadoEscala = _repositorioEscala.ListarEscalas();
                var escalaMostrar = Mapper.Map<List<Models.Escala>>(listadoEscala);
                return View(escalaMostrar);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }

        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(string descripcion, DetalleEscala[] detalles)
        {
            bool devolverError = false;
            string devolverMensaje = "Ocurrió un error";
            try
            {
                devolverError = ValidarEscala(detalles);
                if (!devolverError)
                {
                    devolverMensaje = "La escala ingresada no es válida";
                }
                else
                {
                    var escala = new Models.Escala
                    {
                        IdEscala = 0,
                        Descripcion = descripcion,
                        Estado = true
                    };
                    var EscalaIngresar = Mapper.Map<DATA.Escala>(escala);
                    int idEscala = _repositorioEscala.InsertarEscala(EscalaIngresar);

                    foreach(var det in detalles)
                    {
                        det.Escala = idEscala;
                        var detalleInsertar = Mapper.Map<DATA.DetalleEscala>(det);
                        _repositorioDetallesE.InsertarDetalleEscala(detalleInsertar);
                    }

                }
                return Json(new { success = devolverError, responseText = devolverMensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "Ocurrió un error: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                _repositorioEscala.EliminarEscala(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        public ActionResult Detalles(int id)
        {
            try
            {
                var EscalaBuscar = _repositorioEscala.BuscarEscala(id);
                var EscalaDetallar = Mapper.Map<Models.Escala>(EscalaBuscar);
                var listadoDetalles = _repositorioDetallesE.ListarDetalleEscalas(id);
                ViewBag.DetallesDeEscala = listadoDetalles;
                return View(EscalaDetallar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        [NonAction]
        private bool ValidarEscala(DetalleEscala[] detalles)
        {
            int tamanno = detalles.Length;
            int minimoAnterior = 0;
            int cuentaActual = 0;
            for (int i = 0; i < tamanno; i++)
            {
                if (i == 0)
                {
                    minimoAnterior = detalles[i].PCTMinimo;
                }
                else
                {
                    cuentaActual += detalles[i].PCTMinimo - minimoAnterior;
                    if (tamanno == (i + 1))
                    {

                        cuentaActual += detalles[i].PCTMaximo - detalles[i].PCTMinimo;
                    }
                    else
                    {
                        minimoAnterior = detalles[i].PCTMinimo;
                    }
                }
            }
            return cuentaActual == 100 ? true : false;
        }        

    }
}