using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPC_Coopenae.BLL.ArmaReporte;
using SPC_Coopenae.DATA;
using SPC_Coopenae.DATA.ObjReportes;

namespace SPC_Coopenae.BLL.Reportes
{
    public class ReporteGeneral
    {

        #region Atributos
        private int cedula;
        public DateTime fecha { get;  }
        #endregion

        #region ObjetosExternos
        //Objeto al que segun los parametros que le lleguen sacara el IDP correspondiente
        CalculaIDP _calculaIDP;

        //Objeto que irá al IDP para verificar el idp correspondiente
        ReporteCreditos _reporteCreditos;
        ReporteProductos _reporteProductos;
        ReporteCDP _reporteCDPs;

        //Objeto que bucara la escala correspondiente y dira cuanto porcentaje comisiona
        ReporteEscala _reporteEscala;
        SalarioReporte _salarioReporte;
        TipoCambioReporte _reporteTipoCambio;

        DatosEjecutivo _datosEjecutivo;
        #endregion

        // Constructor, se inicializan los objetos necesarios
        public ReporteGeneral(int cedulaP, DateTime fechaP)
        {
            //Asignar variables a la clase
            this.cedula = cedulaP;
            this.fecha = fechaP;
            //Instanciacion de otras clases
            _calculaIDP = new CalculaIDP();
            _reporteCreditos = new ReporteCreditos();
            _reporteProductos = new ReporteProductos();
            _reporteCDPs = new ReporteCDP();
            _reporteEscala = new ReporteEscala();
            _reporteTipoCambio = new TipoCambioReporte();
            _salarioReporte = new SalarioReporte();
            _datosEjecutivo = new DatosEjecutivo(this.cedula);
        }

        #region Metodos
        //Metodo engargado de hacer llamar a los demas metodos para el reporte
        public void IniciarReporte()
        {
            EstablecerTipoCambio();
            EstablecerMetas();
            SumarVentas();
            FijarIDPs();
            CompararConEscala();
            AsignarComisiones();
        }

        public void EstablecerTipoCambio()
        {
            _reporteTipoCambio.TraeTipoCambio(this.fecha);
            _salarioReporte.FijarSalario(this.cedula);
        }

        //Asigna las metas correspondientes, segun la cedula de las clase
        private void EstablecerMetas()
        {
            _reporteCreditos.EstablecerMetaCorrespondiente(this.cedula);
            _reporteProductos.EstablecerMetaCorrespondiente(this.cedula);
            _reporteCDPs.EstablecerMetaCorrespondiente(this.cedula);
        }

        //Suma de las ventas segun fecha y cedula
        private void SumarVentas()
        {
            _reporteCreditos.SumarMontosColocadosIDP(this.cedula, this.fecha);
            _reporteProductos.TraerCantidadPorMeta(this.fecha, this.cedula);
            _reporteCDPs.SumarCDPsColocadosIDP(this.cedula, this.fecha, this._reporteTipoCambio.tipoCambio.Valor);
        }

        private void FijarIDPs()
        {
            _calculaIDP.metaCred = _reporteCreditos.metaCreditoCorrespondinte;
            _calculaIDP.FijarIDPCred(_reporteCreditos.SumaColocaciones);

            _calculaIDP.metaTipoProducto = _reporteProductos.metaTipoProductosCorrespondiente;
            _calculaIDP.FijarIDPProductos(_reporteProductos.metaYCantidadParaIDP, ref _reporteProductos.TProductosReporteIDP);

            _calculaIDP.metaCDP = _reporteCDPs.metaCDPCorrespondiente;
            _calculaIDP.FijarIDP_CPDs(_reporteCDPs.SumaColocacionesCDP);

            _calculaIDP.SumarIDps();
        }

        private void CompararConEscala()
        {
            _reporteEscala.EstablecerEscalaCorrespondiente(cedula);
            _reporteEscala.EstablecerPCTComisionSegunIDP(_calculaIDP.TotalIDP);
        }

        private void AsignarComisiones()
        {
            _reporteCreditos.AsignarComisionesTipoCreditos(this.cedula, this.fecha, _reporteEscala.PCTComision);
            _reporteProductos.AsignarComisionesProductos(this.cedula, this.fecha, _calculaIDP.TotalIDP, _reporteTipoCambio.tipoCambio.Valor);
            _reporteCDPs.AsignarComisionesTipoCDPs(this.cedula, this.fecha, _reporteTipoCambio.tipoCambio.Valor, _calculaIDP.TotalIDP);
        }

        #endregion

        #region Gets
        public List<RTipoCreditos> GetReporteTipoCreditos()
        {
            return _reporteCreditos.ComisionesPorTipoCreditos;
        }

        public List<RProductos> GetReporteProductos()
        {
            return _reporteProductos.ComisionesPorProductos;
        }

        public decimal GetTipoCambio()
        {
            return _reporteTipoCambio.tipoCambio.Valor;
        }

        public List<RCDPs> GetReporteTipoCDPs()
        {
            return _reporteCDPs.ComisionesPorTipoCDPs;
        }

        public Salario GetSalario()
        {
            return _salarioReporte.sal_correspondiente;
        }

        public int GetMesesTrabajados()
        {
            return _salarioReporte.diferencia_Meses(this.cedula, this.fecha);
        }

        public string GetUnidadNegocio()
        {
            return _datosEjecutivo.UnidadNegocio;
        }

        public DateTime GetFechaContratacion()
        {
            return _datosEjecutivo.Contratacion;
        }

        public decimal[] GetIDPCredito()
        {
            decimal[] IDPsDevolver = new decimal[2];
            //Agrega el valor del IDP
            IDPsDevolver[0] = _calculaIDP.metaCred.ValorIDP;
            //Agrega lo que lleva de IDP 
            IDPsDevolver[1] = _calculaIDP.CreditoIDP;

            return IDPsDevolver;
        }

        public decimal[] GetMetaCredito()
        {
            decimal[] metasDevolver = new decimal[2];
            //Agrega la meta
            metasDevolver[0] = _reporteCreditos.metaCreditoCorrespondinte.MetaColocacion;
            //Agrega la colocacion que lleva
            metasDevolver[1] = _reporteCreditos.SumaColocaciones;

            return metasDevolver;
        }

        public List<RTProducto_IDP> GetIDPsProductos()
        {
            return _reporteProductos.TProductosReporteIDP;
        }

        public decimal[] GetIDPsCDP()
        {
            decimal[] IDPsDevolver = new decimal[2];
            //Agrega el IDP
            IDPsDevolver[0] = _reporteCDPs.metaCDPCorrespondiente.ValorIDP;
            IDPsDevolver[1] = _calculaIDP.CDP_IDP;

            return IDPsDevolver;
        }

        public decimal[] GetMetaCDP()
        {
            decimal[] metasDevolver = new decimal[2];
            //Agrega la meta
            metasDevolver[0] = _reporteCDPs.metaCDPCorrespondiente.Metacdp;
            //Agrega la colocacion que lleva
            metasDevolver[1] = _reporteCDPs.SumaColocacionesCDP;

            return metasDevolver;
        }

        public decimal GetTotalIDP()
        {
            return _calculaIDP.TotalIDP;
        }
        #endregion

    }
}
