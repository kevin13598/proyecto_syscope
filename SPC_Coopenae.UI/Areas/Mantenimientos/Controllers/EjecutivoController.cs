using AutoMapper;
using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DAL.Metodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Controllers
{
    public class EjecutivoController : Controller
    {

        IEjectutivoRepositorio _repositorioEjecutivo;
        IUnidadNegocioRepositorio _repositorioUnidadNegocio;

        public EjecutivoController()
        {
            _repositorioEjecutivo = new MEjecutivoRepositorio();
            _repositorioUnidadNegocio = new MUnidadNegocioRepositorio();
        }

        public ActionResult Index()
        {
            try
            {
                ViewBag.listadoUnidadNegocio = new SelectList(_repositorioUnidadNegocio.ListarUnidadNegocio(), "IdUnidad", "Nombre");
                var ListadoEjecutivosBD = _repositorioEjecutivo.ListarEjecutivos();
                var EjecutivosMostrar = Mapper.Map<List<Models.Ejecutivo>>(ListadoEjecutivosBD);
                return View(EjecutivosMostrar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        public ActionResult Registrar()
        {
            ViewBag.listaUnidadesNegocio = new SelectList(_repositorioUnidadNegocio.ListarUnidadNegocio(), "IdUnidad", "Nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Models.Ejecutivo ejecutivoP)
        {
            try
            {
                ViewBag.listaUnidadesNegocio = new SelectList(_repositorioUnidadNegocio.ListarUnidadNegocio(), "IdUnidad", "Nombre");
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var EjecutivoRegistrar = Mapper.Map<DATA.Ejecutivo>(ejecutivoP);
                _repositorioEjecutivo.InsertarEjecutivo(EjecutivoRegistrar);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                _repositorioEjecutivo.EliminarEjecutivo(id);
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
                ViewBag.listadoUnidadNegocio = new SelectList(_repositorioUnidadNegocio.ListarUnidadNegocio(), "IdUnidad", "Nombre");
                var EjecutivoBuscar = _repositorioEjecutivo.BuscarEjecutivo(id);
                var EjecutivoDetallar = Mapper.Map<Models.Ejecutivo>(EjecutivoBuscar);
                return View(EjecutivoDetallar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                ViewBag.listaUnidadesNegocio = new SelectList(_repositorioUnidadNegocio.ListarUnidadNegocio(), "IdUnidad", "Nombre");
                var EjecutivoBuscar = _repositorioEjecutivo.BuscarEjecutivo(id);
                var EjecutivoEditar = Mapper.Map<Models.Ejecutivo>(EjecutivoBuscar);
                return View(EjecutivoEditar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Editar(Models.Ejecutivo ejecutivoP)
        {
            try
            {
                ViewBag.listaUnidadesNegocio = new SelectList(_repositorioUnidadNegocio.ListarUnidadNegocio(), "IdUnidad", "Nombre");
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var EjecutivoEditarBD = Mapper.Map<DATA.Ejecutivo>(ejecutivoP);
                _repositorioEjecutivo.ActualizarEjecutivo(EjecutivoEditarBD);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

    }
}