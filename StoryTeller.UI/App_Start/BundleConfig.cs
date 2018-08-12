﻿using System.Web;
using System.Web.Optimization;

namespace StoryTeller
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom")
            .IncludeDirectory("~/Scripts/Custom", "*.js", true).Include("~/Scripts/Custom/jquery-confirm.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr")
            .IncludeDirectory("~/Scripts/SignalR", "*.js")
            );

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new StyleBundle("~/Content/css")
                      .Include("~/Content/bootstrap.min.css")
                      .Include("~/Content/jquery-ui.min.css")
                      .Include("~/Content/site.css")
                      .Include("~/Content/jquery-confirm.min.css")
                      .Include("~/Content/font-fix.css")
                      );
        }
    }
}
    