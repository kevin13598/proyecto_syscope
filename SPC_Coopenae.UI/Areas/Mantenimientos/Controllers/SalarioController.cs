using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DAL.Metodos;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Controllers
{
    public class SalarioController : Controller
    {

        ISalarioRepositorio _repositorioSalario;

        public SalarioController()
        {
            _repositorioSalario = new MSalarioRepositorio();
        }

        public ActionResult Index()
        {
            try
            {
                var listadoSalarios = _repositorioSalario.ListarSalario();
                var SalariosMostrar = Mapper.Map<List<Models.Salario>>(listadoSalarios);
                return View(SalariosMostrar);
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
        public ActionResult Registrar(Models.Salario sal)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var SalarioRegistrar = Mapper.Map<DATA.Salario>(sal);
                _repositorioSalario.InsertarSalario(SalarioRegistrar);
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
                _repositorioSalario.EliminarSalario(id);
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
              
                var SalarioBuscar = _repositorioSalario.BuscarSalario(id);
                var SalarioDetallar = Mapper.Map<Models.Salario>(SalarioBuscar);
                return View(SalarioDetallar);
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
                var SalarioBuscar = _repositorioSalario.BuscarSalario(id);
                var SalarioEditar = Mapper.Map<Models.Salario>(SalarioBuscar);
                return View(SalarioEditar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Editar(Models.Salario sal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var SalarioEditarBD = Mapper.Map<DATA.Salario>(sal);
                _repositorioSalario.ActualizarSalario(SalarioEditarBD);
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