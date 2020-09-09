using System;
using System.Collections.Generic;
using AutoMapper;
using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DAL.Metodos;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Controllers
{
    public class TipoProductoController : Controller
    {

        ITipoProductoRepositorio _tipoProductoRepositorio;
        // GET: Mantenimientos/TipoProducto
        public TipoProductoController() {
            _tipoProductoRepositorio = new MTipoProducto();
        }
        public ActionResult Index()
        {
            try {
                var TipoProductoBD = _tipoProductoRepositorio.ListarTipoProducto();
                var TipoProductoMostrar = Mapper.Map<List<Models.TipoProducto>>(TipoProductoBD);
                return View(TipoProductoMostrar);
            } catch (Exception ex) {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
            }
            return View();
        }

        public ActionResult Registrar() {
            return View();
        }
        [HttpPost]
        public ActionResult Registrar(Models.TipoProducto tipoProducto) {
            try
            {
                if (!ModelState.IsValid) {
                    return View();
                }
                var TipoProductoRegistrar = Mapper.Map<DATA.TipoProducto>(tipoProducto);
                _tipoProductoRepositorio.InsertarTipoProducto(TipoProductoRegistrar);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        public ActionResult Eliminar(int id) {
            try
            {
                _tipoProductoRepositorio.EliminarTipoProducto(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        public ActionResult Detalles(int id) {
            try
            {
                var TipoProductoBuscar = _tipoProductoRepositorio.BuscarTipoProducto(id);
                var TipoProductoMostrar = Mapper.Map<Models.TipoProducto>(TipoProductoBuscar);
                return View(TipoProductoMostrar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        public ActionResult Editar(int id) {
            try
            {
                var TipoProductoBuscar = _tipoProductoRepositorio.BuscarTipoProducto(id);
                var TipoProductoEditar = Mapper.Map<Models.TipoProducto>(TipoProductoBuscar);
                return View(TipoProductoEditar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Editar(Models.TipoProducto tipoProducto) {
            try
            {
                if (!ModelState.IsValid) {
                    return View();
                }
                var TipoProductoEditar = Mapper.Map<DATA.TipoProducto>(tipoProducto);
                _tipoProductoRepositorio.ActualizarTipoProducto(TipoProductoEditar);
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