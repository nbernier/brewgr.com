using System.Web.Optimization;

namespace Brewgr.Web
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/js")
				.Include(
					"~/js/jquery.tmpl.min.js",
					"~/js/jquery.colorbox.js",
					"~/js/superfish.js",
					"~/js/jquery.tipsy.js",
					"~/js/jquery.validate.js",
					"~/js/jquery.validate.unobtrusive.js",
					"~/js/plugins.js",
					"~/js/jquery.custom.js",
					"~/js/jquery.raty.js",
					"~/js/jquery.filter_input.js",
					"~/js/jquery.timeago.js",
					"~/js/t.js",
					"~/js/jquery.chosen.js",
					"~/js/jquery.autosize.js",
					"~/js/script.js",
					"~/js/utility.js",
					"~/js/layout.js",
					"~/js/builder.js",
					"~/js/session.js",
					"~/js/style-chart.js"));

			bundles.Add(new StyleBundle("~/bundles/css")
				.Include(
                    //"~/css/smoothness/jquery-ui-1.10.3.custom.css",
                    //"~/css/style.css",
                    //"~/css/custom.css",
                    //"~/css/builder.css",
                    //"~/css/colorbox.css",

                    "~/css/smoothness/jquery-ui-1.8.18.custom.css",
                    "~/css/structure.css",
                    "~/css/content.css",
                    "~/css/lib/colorbox.css"));
        }

        public static void RegisterBuilderBundlesNew(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css/builder_V2")
                .Include(
                    "~/css/builder_V2.css"));
        }

        public static void RegisterBuilderBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css/builder")
                .Include(
                    "~/css/smoothness/jquery-ui-1.10.3.custom.css",
                    "~/css/style.css",
                    "~/css/custom.css",
                    "~/css/builder.css"));
        }
    }
}