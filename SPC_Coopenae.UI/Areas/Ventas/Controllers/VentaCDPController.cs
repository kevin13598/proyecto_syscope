using AutoMapper;
using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DAL.Metodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPC_Coopenae.UI.Areas.Ventas.Controllers
{
    public class VentaCDPController : Controller
    {

        IVentaCDPRepositorio _repositorioCDP;
        ITipoCDPRepositorio _repositorioTipoCDP;
        IEjectutivoRepositorio _repositorioEjecutivo;

        public VentaCDPController()
        {
            _repositorioCDP = new MVentaCDPRepositorio();
            _repositorioTipoCDP = new MTipoCDPRepositorio();
            _repositorioEjecutivo = new MEjecutivoRepositorio();
        }

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
                ViewBag.TipoCDP = new SelectList(_repositorioTipoCDP.ListarTipoCDP(), "IdTipoCDP", "Nombre");
                ViewBag.Moneda = new SelectList(_repositorioTipoCDP.ListarTipoCDP(), "IdTipoCDP", "Moneda");
                var ListaCDPsBD = _repositorioCDP.ListarCDP();
                var ListaMostrarCDPs = Mapper.Map<List<Models.VentaCDP>>(ListaCDPsBD);
                return View(ListaMostrarCDPs);
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
                ViewBag.TipoCDP = new SelectList(_repositorioTipoCDP.ListarTipoCDP(), "IdTipoCDP", "Nombre");
                ViewBag.Moneda = new SelectList(_repositorioTipoCDP.ListarTipoCDP(), "IdTipoCDP", "Moneda");
                var ListaCDPsBD = _repositorioCDP.BuscarListarCDP(cedula, fecha);
                if (ListaCDPsBD.Any())
                {
                    ViewBag.MensajeBusqueda = "<div class='alert alert-success'>Buscando con la cedula del ejecutivo: " + cedula + ". Y la fecha " + fecha.ToString("MMM") + " del " + fecha.Year + "</div>";
                }
                else
                {
                    ViewBag.MensajeBusqueda = "<div class='alert alert-danger'>No se encuentran ventas con la cedula: " + cedula + ". Y la fecha " + fecha.ToString("MMM") + " del " + fecha.Year + "</div>";
                }
               
                var ListaMostrarCDPs = Mapper.Map<List<Models.VentaCDP>>(ListaCDPsBD);
                
                return View(ListaMostrarCDPs);
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
                ViewBag.listaTipos = new SelectList(_repositorioTipoCDP.ListarTipoCDP(), "IdTipoCDP", "Nombre");
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Ocurrió un error", ex);
                return View();
            }

        }


        [HttpPost]
        public ActionResult Registrar(Models.VentaCDP ventaCDP)
        {
            try
            {
                ViewBag.listaTipos = new SelectList(_repositorioTipoCDP.ListarTipoCDP(), "IdTipoCDP", "Nombre");
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var venta = Mapper.Map<DATA.VentaCDP>(ventaCDP);
                _repositorioCDP.InsertarCDP(venta);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Ocurrió un error", ex);
                return View();
            }
        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                _repositorioCDP.EliminarCDP(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Ocurrió un error", ex);
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
                ViewBag.TipoCDP = new SelectList(_repositorioTipoCDP.ListarTipoCDP(), "IdTipoCDP", "Nombre");
                ViewBag.Moneda = new SelectList(_repositorioTipoCDP.ListarTipoCDP(), "IdTipoCDP", "Moneda");
                var colCDPBuscar = _repositorioCDP.BuscarCDP(id);
                var colCDPDetallar = Mapper.Map<Models.VentaCDP>(colCDPBuscar);
                return View(colCDPDetallar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Ocurrió un error", ex);
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                ViewBag.listaTipos = new SelectList(_repositorioTipoCDP.ListarTipoCDP(), "IdTipoCDP", "Nombre");
                var VentaCDPBuscar = _repositorioCDP.BuscarCDP(id);
                var VentaCDPEditar = Mapper.Map<Models.VentaCDP>(VentaCDPBuscar);
                return View(VentaCDPEditar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Ocurrió un error", ex);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Editar(Models.VentaCDP colCDP)
        {
            try
            {
                ViewBag.listaTipos = new SelectList(_repositorioTipoCDP.ListarTipoCDP(), "IdTipoCDP", "Nombre");
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var VentaCDPEditar = Mapper.Map<DATA.VentaCDP>(colCDP);
                _repositorioCDP.ActualizarCDP(VentaCDPEditar);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Ocurrió un error", ex);
                return View();
            }
        }

    }
}