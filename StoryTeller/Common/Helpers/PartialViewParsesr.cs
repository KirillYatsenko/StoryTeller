using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoryTeller.Common.Helpers
{
    public static class PartialViewParsesr
    {
        public static string RenderPartialToString(string view, object model, ControllerContext Context)
        {
            if (string.IsNullOrEmpty(view))
            {
                view = Context.RouteData.GetRequiredString("action");
            }

            ViewDataDictionary ViewData = new ViewDataDictionary();

            TempDataDictionary TempData = new TempDataDictionary();

            ViewData.Model = model;

            using (StringWriter stringWritter = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(Context, view);
                ViewContext viewContext = new ViewContext(Context, viewResult.View, ViewData, TempData, stringWritter);
                viewResult.View.Render(viewContext, stringWritter);

                return stringWritter.GetStringBuilder().ToString();
            }
        }
    }
}