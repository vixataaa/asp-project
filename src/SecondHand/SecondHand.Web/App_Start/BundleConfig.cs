﻿using System.Web;
using System.Web.Optimization;

namespace SecondHand.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Kendo/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                        "~/Scripts/jquery.signalR-2.2.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-unobtrusive").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/custom/addAdvertisement").Include(
                "~/Scripts/Custom/addAdvertisement.js"));

            bundles.Add(new ScriptBundle("~/custom/sidenav").Include(
                "~/Scripts/Custom/sidenav.js"));

            bundles.Add(new ScriptBundle("~/custom/notificationManagerHome").Include(
                "~/Scripts/Custom/notificationManagerHome.js"));

            bundles.Add(new StyleBundle("~/Content/sidenav").Include(
                "~/Content/Custom/sidenav.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/Kendo/kendo.web.min.js",
                "~/Scripts/Kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo-css").Include(
                  "~/Content/Kendo/kendo.default.min.css",
                  "~/Content/Kendo/kendo.common.min.css"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
