using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OverridingMVCViewEngin.Infrastructure
{
    
    public class CustomViewEngine : IViewEngine
    {

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return new ViewEngineResult(new string[] { "~/Views/{1}/{0}.cshtml",
                "~/Views/Partials/{1}/{0}.cshtml",
                "~/Views/Shared/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/{1}/{0}.vbhtml",
                "~/Views/Partials/{1}/{0}.vbhtml",
                "~/Views/Shared/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.vbhtml" });
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            


            var cntl = controllerContext.RouteData.Values["controller"];
            var act = controllerContext.RouteData.Values["action"];
            return new ViewEngineResult(new string[] { $"~/Views/{cntl}/{act}.cshtml",
                $"~/Views/Shared/{cntl}/{act}.cshtml",
                $"~/Views/Shared/{act}.cshtml",
                $"~/Views/{cntl}/{act}.vbhtml",
                $"~/Views/Shared/{cntl}/{act}.vbhtml",
                $"~/Views/Shared/{act}.vbhtml" });
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
            // do nothing as there is no specific state to be handled now
        }
    }
}