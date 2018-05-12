using System.Web;
using System.Web.Optimization;

namespace SerieWeb
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region script
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                      "~/Scripts/umd/popper.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",                      
                      "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Scripts/datatables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/BootstrapMultiselect").Include(
                      "~/Scripts/BootstrapMultiselect/BootstrapMultiselect.min.js"));
            #endregion

            //bundles.Add(new StyleBundle("~/Content/multiselect").Include(
            //                    "~/Content/Multiselect/Multiselect.min.css"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.min.css",
                      "~/Content/Datatables/Datatables.min.css"));            
        }
    }
}
