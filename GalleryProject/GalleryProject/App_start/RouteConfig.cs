using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace GalleryProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.MapPageRoute("GalleryPage", "", "~/Pages/CustomerPages/GalleryPage");

            routes.MapPageRoute("PicturePage", "", "~/Pages/CustomerPages/PicturePage");
        
        }
    }
}