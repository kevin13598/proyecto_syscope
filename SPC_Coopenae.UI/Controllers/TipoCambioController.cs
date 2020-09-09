using AutoMapper;
using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DAL.Metodos;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPC_Coopenae.UI.Controllers
{
    public class TipoCambioController : Controller
    {
        ITipoCambioRepositorio _repositorioTipoCambio;

        public TipoCambioController()
        {
            _repositorioTipoCambio = new MTipoCambioRepositorio();
        }

        // GET: TipoCambio
        public ActionResult Index()
        {

            try
            {
                var listado = _repositorioTipoCambio.ListarTipoCambio();
                var listadoMostrar = Mapper.Map<List<Models.TipoCambio>>(listado);
                return View(listadoMostrar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }

        }

        [HttpPost]
        public ActionResult GuardarTipoCambio(decimal valor, DateTime fecha)
        {
            try
            {
                var tipoCambio = new Models.TipoCambio
                {
                    Valor = valor,
                    Fecha = fecha,
                    Estado = true
                };

                var tipoCambioMostrar = Mapper.Map<DATA.TipoCambio>(tipoCambio);
                _repositorioTipoCambio.InsertarTipoCambio(tipoCambioMostrar);
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