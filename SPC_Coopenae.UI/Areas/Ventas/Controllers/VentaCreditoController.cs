using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DAL.Metodos;
using SPC_Coopenae.DATA;

namespace SPC_Coopenae.UI.Areas.Ventas.Controllers
{
    public class VentaCreditoController : Controller
    {

        IVentaCreditoRepositorio _repositorioVentaCred;
        ITipoCreditoRepositorio _repositorioTipoCred;
        IEjectutivoRepositorio _repositorioEjecutivo;

        public VentaCreditoController()
        {
            _repositorioVentaCred = new MVentaCreditoRepositorio();
            _repositorioTipoCred = new MTipoCreditoRepositorio();
            _repositorioEjecutivo = new MEjecutivoRepositorio();
        }

        // GET: Mantenimientos/ColocacionCredito
        public ActionResult Index()
        {
            try
            {
                 ViewBag.Ejecutivo = new SelectList((from s in _repositorioEjecutivo.ListarEjecutivos()
                                                                        select new
                                                                        {
                                                                            Id = s.Cedula,
                                                                            CombinedFields = s.Nombre + " " + s.Apellidos
                                                                        }), "Id", "CombinedFields");

                ViewBag.TipoCredito = new SelectList(_repositorioTipoCred.ListarTipoCredito(), "IdTipoCredito", "Nombre");
                var listarVentaCred = _repositorioVentaCred.ListarVentaCredito();
                var VentaCredListado = Mapper.Map<List<Models.VentaCredito>>(listarVentaCred);
                return View(VentaCredListado);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Index(int cedula, DateTime fecha)
        {
            try
            {
                ViewBag.Ejecutivo = new SelectList((from s in _repositorioEjecutivo.ListarEjecutivos()
                                                    select new
                                                    {
                                                        Id = s.Cedula,
                                                        CombinedFields = s.Nombre + " " + s.Apellidos
                                                    }), "Id", "CombinedFields");

                ViewBag.TipoCredito = new SelectList(_repositorioTipoCred.ListarTipoCredito(), "IdTipoCredito", "Nombre");
                var listarVentaCred = _repositorioVentaCred.BuscarListaCreditos(cedula, fecha);

                if (listarVentaCred.Any())
                {
                    ViewBag.MensajeBusqueda = "<div class='alert alert-success'>Buscando con la cédula del ejecutivo: " + cedula + ". Y la fecha " + fecha.ToString("MMM") + " del " + fecha.Year + "</div>";
                }
                else
                {
                    ViewBag.MensajeBusqueda = "<div class='alert alert-danger'>No se encuentran ventas con la cédula: " + cedula + ". Y la fecha " + fecha.ToString("MMM") + " del " + fecha.Year + "</div>";
                }

                var VentaCredListado = Mapper.Map<List<Models.VentaCredito>>(listarVentaCred);
                return View(VentaCredListado);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }

        }



        public ActionResult Registrar()
        {
            try
            {
                ViewBag.listaTipos = new SelectList(_repositorioTipoCred.ListarTipoCredito(), "IdCredito", "NombreDeCredito");
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }

        }


        [HttpPost]
        public ActionResult Registrar(Models.VentaCredito ventaCred)
        {
            try
            {
                ViewBag.listaTipos = new SelectList(_repositorioTipoCred.ListarTipoCredito(), "IdCredito", "NombreDeCredito");
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var venta = Mapper.Map<DATA.VentaCredito>(ventaCred);
                _repositorioVentaCred.InsertarVentaCredito(venta);
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
                _repositorioVentaCred.EliminarVentaCredito(id);
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
                ViewBag.Ejecutivo = ViewBag.Ejecutivo = new SelectList((from s in _repositorioEjecutivo.ListarEjecutivos()
                                                                        select new
                                                                        {
                                                                            Id = s.Cedula,
                                                                            CombinedFields = s.Nombre + " " + s.Apellidos
                                                                        }), "Id", "CombinedFields");
                ViewBag.TipoCredito = new SelectList(_repositorioTipoCred.ListarTipoCredito(), "IdTipoCredito", "Nombre");
                var ventaCredBuscar = _repositorioVentaCred.BuscarVentaCredito(id);
                var ventaCredDetallar = Mapper.Map<Models.VentaCredito>(ventaCredBuscar);
                return View(ventaCredDetallar);
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
                ViewBag.listaTipos = new SelectList(_repositorioTipoCred.ListarTipoCredito(), "IdCredito", "NombreDeCredito");
                var VentaCredBuscar = _repositorioVentaCred.BuscarVentaCredito(id);
                var VentaCredEditar = Mapper.Map<Models.VentaCredito>(VentaCredBuscar);
                return View(VentaCredEditar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Editar(VentaCredito ventaCred)
        {
            try
            {
                ViewBag.listaTipos = new SelectList(_repositorioTipoCred.ListarTipoCredito(), "IdCredito", "NombreDeCredito");
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var VentaCredEditar = Mapper.Map<DATA.VentaCredito>(ventaCred);
                _repositorioVentaCred.ActualizarVentaCredito(VentaCredEditar);
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