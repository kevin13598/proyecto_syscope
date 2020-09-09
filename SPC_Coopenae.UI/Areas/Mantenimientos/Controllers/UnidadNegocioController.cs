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
    public class UnidadNegocioController : Controller
    {

        IUnidadNegocioRepositorio _repositorioUnidadNegocio;
        IMetaRepositorio _meta;

        public UnidadNegocioController()
        {
            _repositorioUnidadNegocio = new MUnidadNegocioRepositorio();
            _meta = new MMetaRepositorio();
        }

        public ActionResult Index()
        {
            try
            {
                ViewBag.Meta = new SelectList(_meta.ListarMetas(), "IdMeta", "Descripcion");
                var ListadoUnidadesBD = _repositorioUnidadNegocio.ListarUnidadNegocio();
                var UnidadNegocioesMostrar = Mapper.Map<List<Models.UnidadNegocio>>(ListadoUnidadesBD);
                return View(UnidadNegocioesMostrar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }

        }

        public ActionResult Registrar()
        {
            ViewBag.Meta = new SelectList(_meta.ListarMetas(), "IdMeta", "Descripcion");
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Models.UnidadNegocio unidadP)
        {
            try
            {
                ViewBag.Meta = new SelectList(_meta.ListarMetas(), "IdMeta", "Descripcion");

                if (!ModelState.IsValid)
                {
                    return View();
                }
                var UnidadNegocioRegistrar = Mapper.Map<DATA.UnidadNegocio>(unidadP);
                _repositorioUnidadNegocio.InsertarUnidadNegocio(UnidadNegocioRegistrar);
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
                _repositorioUnidadNegocio.EliminarUnidadNegocio(id);
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
                ViewBag.Meta = new SelectList(_meta.ListarMetas(), "IdMeta", "Descripcion");
                var UnidadNegocioBuscar = _repositorioUnidadNegocio.BuscarUnidadNegocio(id);
                var UnidadNegocioDetallar = Mapper.Map<Models.UnidadNegocio>(UnidadNegocioBuscar);

                return View(UnidadNegocioDetallar);
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
                ViewBag.Meta = new SelectList(_meta.ListarMetas(), "IdMeta", "Descripcion");
                var UnidadNegocioBuscar = _repositorioUnidadNegocio.BuscarUnidadNegocio(id);
                var UnidadNegocioEditar = Mapper.Map<Models.UnidadNegocio>(UnidadNegocioBuscar);
                return View(UnidadNegocioEditar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Editar(Models.UnidadNegocio unidadP)
        {
            try
            {
                ViewBag.Meta = new SelectList(_meta.ListarMetas(), "IdMeta", "Descripcion");

                if (!ModelState.IsValid)
                {
                    return View();
                }
                var UnidadNegocioEditarBD = Mapper.Map<DATA.UnidadNegocio>(unidadP);
                _repositorioUnidadNegocio.ActualizarUnidadNegocio(UnidadNegocioEditarBD);
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