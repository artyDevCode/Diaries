using System.Web;
using System.Web.Optimization;

namespace Diaries
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery.timepicker.js",
                        "~/Scripts/bootstrap-colorpicker.js",
                        "~/Scripts/jquery.multiselect.js",
                        //"~/Scripts/jquery.validate.js",
                        //"~/Scripts/jquery.ui.datepicker.validation.js",
                        "~/Scripts/Custom.js",
                        "~/Scripts/CustomHome.js",
                        "~/Scripts/CustomDiaryMngt.js",
                        "~/Scripts/CustomListType.js",
                        "~/Scripts/DataTables-1.10.0/media/js/*.js",
                       "~/Scripts/DataTables-1.10.0/extras/TableTools/media/js/*.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/DataTables-1.10.0/media/css/*.css",
                      "~/Content/DataTables-1.10.0/extras/TableTools/media/css/*.css", "~/Content/site.css",
                      "~/Content/bootstrap.css",                   
                      "~/Content/jquery.timepicker.css",
                      "~/Content/html5.css",
                      "~/Content/bootstrap-colorpicker.css",
                      "~/Content/Custom.css",
                      "~/Content/jquery.multiselect.css",
                      //"~/Content/style.css",
                      //"~/Content/prettify.css",
                       "~/Content/jquery-ui.css"));
                       // <link rel="stylesheet" href="//code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css">

      
            //***************************************************************************************************

          //  BundleTable.EnableOptimizations = true; MAKE SURE THIS IS COMMENTED OUT OR YOU CANNOT DEBUG JAVASCRIPT
        }
    }
}
