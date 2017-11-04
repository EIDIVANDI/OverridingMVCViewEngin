using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OverridingMVCViewEngin.Infrastructure
{

    public class CustomViewEngine : RazorViewEngine
    {
        private string[] _ViewLocationFormats = null;

        public CustomViewEngine()
            : base()
        {

        }
        private string[] GetViewLocations(ControllerContext cnt)
        {
            if (_ViewLocationFormats == null ||
                !_ViewLocationFormats.Any())
            {
                var cntl = cnt.RouteData.Values["controller"];
                var act = cnt.RouteData.Values["action"];


                _ViewLocationFormats = new string[] { $"~/Views/{cntl}/{act}.cshtml",
                $"~/Views/Shared/{cntl}/{act}.cshtml",
                $"~/Views/Shared/{act}.cshtml",
                $"~/Views/{cntl}/{act}.vbhtml",
                $"~/Views/Shared/{cntl}/{act}.vbhtml",
                $"~/Views/Shared/{act}.vbhtml" };
            }
            return _ViewLocationFormats;
        }

        private string[] GetDefaultPartialViewLocations()
        {
            return GetDefaultViewLocations();
        }

        private string[] GetDefaultViewLocations()
        {
            return new[]
            {
                string.Format("~/{3}/{2}/{1}/{0}.cshtml","{0}","{1}","{2}","Views"),
                string.Format("~/{3}/{2}/{1}/{0}.vbhtml","{0}","{1}","{2}","Views"),
                string.Format("~/{1}/{2}/{0}.cshtml","{0}","Views", "Shared"),
                string.Format("~/{1}/{2}/{0}.vbhtml","{0}","Views", "Shared"),
            };
        }

        private string[] GetDefaultMasterLocations()
        {
            return new[]
            {
                string.Format("~/{3}/{2}/{1}/{0}.cshtml","{0}","{1}","{2}","Views"),
                string.Format("~/{3}/{2}/{1}/{0}.vbhtml","{0}","{1}","{2}","Views"),
                string.Format("~/{1}/{2}/{0}.cshtml","{0}","Views", "Shared"),
                string.Format("~/{1}/{2}/{0}.vbhtml","{0}","Views", "Shared"),
            };
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            ViewLocationFormats = GetViewLocations(controllerContext);
            return base.FileExists(controllerContext, virtualPath);
        }
    }

}
