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


            routes.MapPageRoute("PicturePage", "", "~/Pages/CustomerPages/WebForm2.aspx");

            routes.MapPageRoute("Comment", "Picture/{PictureID}", "~/Pages/CustomerPages/WebForm1.aspx");
        
        }
    }
}