using AutoMapper;
using SPC_Coopenae.BLL.Reportes;
using SPC_Coopenae.DAL.Interfaces;
using SPC_Coopenae.DAL.Metodos;
using SPC_Coopenae.UI.Areas.Mantenimientos.Models;
using SPC_Coopenae.UI.Models;
using SPC_Coopenae.UI.Models.ObjsReporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace SPC_Coopenae.UI.Controllers
{
    public class ReporteController : Controller
    {

        ReporteGeneral _reporteBLL;
        IEjectutivoRepositorio _repositorioEjecutivo;

        public ReporteController()
        {
            _repositorioEjecutivo = new MEjecutivoRepositorio();
        }

        public ActionResult Index()
        {
            if (TempData["MensajeError"] != null)
            {
                ViewBag.MensajeError = TempData["MensajeError"].ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(int cedula, string fecha)
        {
            return RedirectToAction("Mensual", new { id = cedula, fecha = fecha });
        }

        public ActionResult Mensual(int? id, string fecha)
        {
            try
            {
                if (id != null && fecha != null)
                {
                    int cedula = id.Value;
                    var EjecutivoReportar = _repositorioEjecutivo.BuscarEjecutivo(cedula);

                    if (EjecutivoReportar == null || EjecutivoReportar.Estado == false)
                    {
                        TempData["MensajeError"] = "No se encontró la cédula " + cedula;
                        return RedirectToAction("Index");
                    }

                    DateTime FechaReporte = Convert.ToDateTime(fecha);

                    Reporte reporteMostrar = GenerarReporte(cedula, FechaReporte);
                    reporteMostrar.Cedula = EjecutivoReportar.Cedula;
                    reporteMostrar.Nombre = EjecutivoReportar.Nombre + " " + EjecutivoReportar.Apellidos;
                    reporteMostrar.Fecha = FechaReporte;

                    if (reporteMostrar.TipoCambio == -321)
                    {
                        TempData["MensajeError"] = "No se ha definido el tipo de cambio para el mes de " + FechaReporte.ToString("MMM") + " del " + FechaReporte.Year;
                        return RedirectToAction("Index");
                    }

                    return View(reporteMostrar);
                }
                else
                {
                    TempData["MensajeError"] = "Debe ingresar los datos que se solicitan.";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Ocurrió un error. " + ex.Message;
                return RedirectToAction("Index");
            }

        }

        [NonAction]
        public bool ValidarEjecutivo(int cedula)
        {
            var EjecutivoReportar = _repositorioEjecutivo.BuscarEjecutivo(cedula);
            if (EjecutivoReportar == null)
            {
                return false;
            }
            return true;
        }

        [NonAction]
        private Reporte GenerarReporte(int cedulaReporte, DateTime fechaReporte)
        {
            //Inicio el reporte
            _reporteBLL = new ReporteGeneral(cedulaReporte, fechaReporte);
            _reporteBLL.IniciarReporte();

            //Inicio el reporte modelo de vista
            Reporte reporteVista = new Reporte();

            //Trae el tipo de cambio para mostrar y asigna datos
            reporteVista.TipoCambio = _reporteBLL.GetTipoCambio();
            reporteVista.UnidadNegocio = _reporteBLL.GetUnidadNegocio();
            reporteVista.FechaContratacion = _reporteBLL.GetFechaContratacion();

            //Asigna lo correspondiente a creditos
            var reporteTipoCreditosBLL = _reporteBLL.GetReporteTipoCreditos();
            reporteVista.listaTipoCreditos = Mapper.Map<List<RTipoCreditos>>(reporteTipoCreditosBLL);
            reporteVista.TotalComisionCreditos = reporteVista.listaTipoCreditos.Sum(x => x.TotalComision);

            //Asigna lo correspondiente a productos
            var reporteProductosBLL = _reporteBLL.GetReporteProductos();
            reporteVista.listaProductos = Mapper.Map<List<RProductos>>(reporteProductosBLL);
            reporteVista.TotalComisionProductos = reporteVista.listaProductos.Sum(x => x.TotalComision);

            //Asigna lo correspondiente a cdps
            var reporteCDPsBLL = _reporteBLL.GetReporteTipoCDPs();
            reporteVista.listaCDPs = Mapper.Map<List<RCDPs>>(reporteCDPsBLL);
            reporteVista.TotalComisionCDPs = reporteVista.listaCDPs.Sum(x => x.TotalComision);

            //Suma las comisiones
            reporteVista.TotalGenerado = reporteVista.TotalComisionCreditos.Value + reporteVista.TotalComisionProductos + reporteVista.TotalComisionCDPs.Value;

            //Asignacion del salario
            Salario salarioBLL = Mapper.Map<Salario>(_reporteBLL.GetSalario());
            int mesesTrabajados = _reporteBLL.GetMesesTrabajados();

            if (mesesTrabajados <= salarioBLL.MesesInicio)
            {
                reporteVista.Salario = salarioBLL.SalarioInicio;
                reporteVista.Bono = reporteVista.TotalGenerado < salarioBLL.BonoInicio ? salarioBLL.BonoInicio : 0;
                reporteVista.TotalGenerado = reporteVista.Bono != 0 ? reporteVista.Bono : reporteVista.TotalGenerado;
            }
            else
            {
                reporteVista.Salario = salarioBLL.Base;
                reporteVista.Bono = 0;
            }
            
            reporteVista.TotalGenerado += reporteVista.Salario;

            //Asigna los IDPs para mostrarlos en el reporte
            reporteVista.Estado_IDP.IDP_Creditos = _reporteBLL.GetIDPCredito();
            reporteVista.Estado_IDP.IDP_CDPs = _reporteBLL.GetIDPsCDP();
            reporteVista.Estado_IDP.TipoProductos = Mapper.Map<List<RTProducto_IDP>>(_reporteBLL.GetIDPsProductos());
            reporteVista.Estado_IDP.Metas_Creditos = _reporteBLL.GetMetaCredito();
            reporteVista.Estado_IDP.Metas_CDPs = _reporteBLL.GetMetaCDP();
            reporteVista.Estado_IDP.TotalIDP = _reporteBLL.GetTotalIDP();

            return reporteVista;
        }

    }
}