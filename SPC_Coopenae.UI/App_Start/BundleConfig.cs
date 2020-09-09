using System.Web;
using System.Web.Optimization;

namespace SPC_Coopenae.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //registra jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //registra los scripts encargados de guardar las escalas y metas
            bundles.Add(new ScriptBundle("~/bundles/escalas").Include(
                        "~/Scripts/Otros/GuardarEscala.js"));

            bundles.Add(new ScriptBundle("~/bundles/metas").Include(
                        "~/Scripts/Otros/GuardarMeta.js"));

            //registra los sripts de validacion de mvc
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Scripts/Otros/bootstrap-datepicker.min.js",
                        "~/Scripts/Otros/bootstrap-datepicker.es.min.js",
                        "~/Scripts/Otros/fechapicker.js"
                ));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));
        }
    }
}
