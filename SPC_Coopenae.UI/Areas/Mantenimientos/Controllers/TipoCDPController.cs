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
    public class TipoCDPController : Controller
    {

        ITipoCDPRepositorio _repositorioTipoCDP;

        public TipoCDPController()
        {
            _repositorioTipoCDP = new MTipoCDPRepositorio();
        }

        public ActionResult Index()
        {
            try
            {
                var ListaTipoCDPsBD = _repositorioTipoCDP.ListarTipoCDP();
                var ListaMostrarTiposCDPs = Mapper.Map<List<Models.TipoCDP>>(ListaTipoCDPsBD);
                return View(ListaMostrarTiposCDPs);
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
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Ocurrió un error", ex);
                return View();
            }

        }


        [HttpPost]
        public ActionResult Registrar(Models.TipoCDP tipoCDPP)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var TipoCDPRegistrar = Mapper.Map<DATA.TipoCDP>(tipoCDPP);
                _repositorioTipoCDP.InsertarTipoCDP(TipoCDPRegistrar);
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
                _repositorioTipoCDP.EliminarTipoCDP(id);
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
                var tipoCDPBuscar = _repositorioTipoCDP.BuscarTipoCDP(id);
                var tipoCDPDetallar = Mapper.Map<Models.TipoCDP>(tipoCDPBuscar);
                return View(tipoCDPDetallar);
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
                var tipoCDPBuscar = _repositorioTipoCDP.BuscarTipoCDP(id);
                var tipoCDPEditar = Mapper.Map<Models.TipoCDP>(tipoCDPBuscar);
                return View(tipoCDPEditar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Ocurrió un error", ex);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Editar(Models.TipoCDP tipoCDPP)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var tipoCDPEditar = Mapper.Map<DATA.TipoCDP>(tipoCDPP);
                _repositorioTipoCDP.ActualizarTipoCDP(tipoCDPEditar);
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