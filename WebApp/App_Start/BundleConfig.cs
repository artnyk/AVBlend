using System.Web;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //viewer bundle
            bundles.Add(new ScriptBundle("~/bundles/threejs").Include(
                    "~/Scripts/three.js",
                    "~/Scripts/OrbitControls.js",
                    "~/Scripts/dat.gui.min.js",
                    "~/Scripts/libs/stats.min.js"));

            //Css animations
            bundles.Add(new StyleBundle("~/Content/layout").Include(
                      "~/Content/animate.css",
                      "~/Content/style.blue.css",
                      "~/Content/custom.css"));
        }
    }
}
