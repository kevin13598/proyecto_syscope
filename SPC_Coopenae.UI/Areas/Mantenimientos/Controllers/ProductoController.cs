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
    public class ProductoController : Controller
    {

        IProductoRepositorio _repositorioProducto;
        ITipoProductoRepositorio _tipoProducto;

        public ProductoController()
        {
            _repositorioProducto = new MProductoRepositorio();
            _tipoProducto = new MTipoProducto();
        }

        public ActionResult Index()
        {
            try
            {
                ViewBag.TipoProducto = new SelectList(_tipoProducto.ListarTipoProducto(), "IdTipoProducto", "Descripcion");
                ViewBag.Moneda = new SelectList(_tipoProducto.ListarTipoProducto(), "IdTipoProducto", "Moneda");
                var ListadoProductosBD = _repositorioProducto.ListarProducto();
                var ProductosMostrar = Mapper.Map<List<Models.Producto>>(ListadoProductosBD);
                return View(ProductosMostrar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }

        }

        public ActionResult Registrar()
        {
            ViewBag.TipoProducto = new SelectList((from s in _tipoProducto.ListarTipoProducto()
                                                   select new
                                                   {
                                                       Id = s.IdTipoProducto,
                                                       Descripcion = s.Descripcion + " - " + (s.Moneda == "d" ? "$" : (s.Moneda == "c") ? "₡" : "Error")
                                                   }), "Id", "Descripcion");
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Models.Producto productosP)
        {
            try
            {
                ViewBag.TipoProducto = new SelectList((from s in _tipoProducto.ListarTipoProducto()
                                                       select new
                                                       {
                                                           Id = s.IdTipoProducto,
                                                           Descripcion = s.Descripcion + " - " + (s.Moneda == "d" ? "$" : (s.Moneda == "c") ? "₡" : "Error")
                                                       }), "Id", "Descripcion");
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var ProductoRegistrar = Mapper.Map<DATA.Producto>(productosP);
                _repositorioProducto.InsertarProducto(ProductoRegistrar);
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
                _repositorioProducto.EliminarProducto(id);
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
                ViewBag.TipoProducto = new SelectList(_tipoProducto.ListarTipoProducto(), "IdTipoProducto", "Descripcion");
                ViewBag.Moneda = new SelectList(_tipoProducto.ListarTipoProducto(), "IdTipoProducto", "Moneda");
                var ProductoBuscar = _repositorioProducto.BuscarProducto(id);
                var ProductoDetallar = Mapper.Map<Models.Producto>(ProductoBuscar);
                return View(ProductoDetallar);
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
                ViewBag.TipoProducto = new SelectList((from s in _tipoProducto.ListarTipoProducto()
                                                       select new
                                                       {
                                                           Id = s.IdTipoProducto,
                                                           Descripcion = s.Descripcion + " - " + (s.Moneda == "d" ? "$" : (s.Moneda == "c") ? "₡" : "Error")
                                                       }), "Id", "Descripcion");
                var ProductoBuscar = _repositorioProducto.BuscarProducto(id);
                var ProductoEditar = Mapper.Map<Models.Producto>(ProductoBuscar);
                return View(ProductoEditar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Editar(Models.Producto productosP)
        {
            try
            {
                ViewBag.TipoProducto = new SelectList((from s in _tipoProducto.ListarTipoProducto()
                                                       select new
                                                       {
                                                           Id = s.IdTipoProducto,
                                                           Descripcion = s.Descripcion + " - " + (s.Moneda == "d" ? "$" : (s.Moneda == "c") ? "₡" : "Error")
                                                       }), "Id", "Descripcion");
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var ProductoEditarBD = Mapper.Map<DATA.Producto>(productosP);
                _repositorioProducto.ActualizarProducto(ProductoEditarBD);
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