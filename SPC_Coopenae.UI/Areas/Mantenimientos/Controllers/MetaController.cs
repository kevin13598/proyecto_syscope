using AutoMapper;
using SPC_Coopenae.DAL;
using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DAL.Metodos;
using SPC_Coopenae.DATA;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Controllers
{
    public class MetaController : Controller
    {

        IMetaRepositorio _repositorio;
        IEscalaRepositorio _escala;
        ITipoProductoRepositorio _tipoProductos;
        ISalarioRepositorio _salario;
    

        public MetaController()
        {
            _repositorio = new MMetaRepositorio();
            _escala = new MEscalaRepositorio();
            _tipoProductos = new MTipoProducto();
            _salario = new MSalarioRepositorio();
        }

        public ActionResult Index()
        {
            try
            {
                ViewBag.ListaEscala = new SelectList(_escala.ListarEscalas(), "IdEscala", "Descripcion");
                var metas = _repositorio.ListarMetas();
                var metasMostrar = Mapper.Map<List<Models.Metas.Meta>>(metas);

                return View(metasMostrar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }

        }

        public ActionResult Registrar()
        {
            ViewBag.ListaEscala = new SelectList(_escala.ListarEscalas(), "IdEscala", "Descripcion");
            ViewBag.Salario = new SelectList(_salario.ListarSalario(), "IdSalario", "Descripcion");
            ViewBag.ListaTipoProductos = new SelectList(_tipoProductos.ListarTipoProducto(), "IdTipoProducto", "Descripcion");
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Models.Metas.Meta _meta,
                                      Models.Metas.MetaCredito _metaCredito,
                                      Models.Metas.MetaCDP _metaCDP,
                                      Models.Metas.MetaProducto[] _metaProducto)
        {

            string devolverMensaje = "Ocurrió un error";

            try
            {
                ViewBag.ListaEscala = new SelectList(_escala.ListarEscalas(), "IdEscala", "Descripcion");
                ViewBag.Salario = new SelectList(_salario.ListarSalario(), "IdSalario", "Descripcion");
                ViewBag.ListaTipoProductos = new SelectList(_tipoProductos.ListarTipoProducto(), "IdTipoProducto", "Descripcion");

                //Crear la meta con el parametro e insertar
                var meta = new Models.Metas.Meta
                {
                    IdMeta = 0,
                    Descripcion = _meta.Descripcion,
                    Estado = true,
                    Salario = _meta.Salario,
                    Escala = _meta.Escala
                };

                var MetaInsertar = Mapper.Map<DATA.Meta>(meta);
                int IdMeta = _repositorio.InsertarMeta(MetaInsertar);

                //Crear meta de creditos y de cdps

                var metaCredito = new Models.Metas.MetaCredito
                {
                    IdMetaCredito = 0,
                    MetaColocacion = _metaCredito.MetaColocacion,
                    ValorIDP = _metaCredito.ValorIDP,
                    Meta = IdMeta
                };

                var MetaCreditoInsertar = Mapper.Map<DATA.MetaCredito>(metaCredito);
                _repositorio.InsertarMetaCredito(MetaCreditoInsertar);

                var metaCDP = new Models.Metas.MetaCDP
                {
                    IdMetaCDP = 0,
                    Metacdp = _metaCDP.Metacdp,
                    ValorIDP = _metaCDP.ValorIDP,
                    Meta = IdMeta
                };

                var MetaCDPInsertar = Mapper.Map<DATA.MetaCDP>(metaCDP);
                _repositorio.InsertarMetaCDP(MetaCDPInsertar);

                //Insertar Meta de productos
                foreach (var item in _metaProducto)
                {
                    var metaTipoProducto = new DATA.MetaTipoProducto
                    {
                        IdMetaTipoProducto = 0,
                        MetaCantidad = item.MetaCantidad,
                        ValorIDP = item.ValorIDP,
                        Meta = IdMeta
                    };

                    int idMetaTP = _repositorio.InsertarMetaProducto(metaTipoProducto);

                    foreach (var x in item.TipoProductos)
                    {
                        var metaTipoProductoDetalle = new DATA.MetaTipoProductoDetalle
                        {
                            MetaTipoProducto = idMetaTP,
                            TipoProducto = x
                        };

                        _repositorio.InsertarMetaProductoDetalle(metaTipoProductoDetalle);
                    }

                }

                return Json(new { success = true, responseText = devolverMensaje }, JsonRequestBehavior.AllowGet);
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
                _repositorio.EliminarMeta(id);
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
                ViewBag.Escala = new SelectList(_escala.ListarEscalas(), "IdEscala", "Descripcion");
                ViewBag.Salario = new SelectList(_salario.ListarSalario(), "IdSalario", "Descripcion");

                DetalleMeta metaMostrar = _repositorio.DetallarMeta(id);

                return View(metaMostrar);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

    }
}