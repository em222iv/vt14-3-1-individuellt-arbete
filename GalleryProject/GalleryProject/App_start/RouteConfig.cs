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
            routes.MapPageRoute("PicturePage", "", "~/Pages/CustomerPages/PicturePage.aspx");
            routes.MapPageRoute("CommentPage", "Picture/{PictureID}", "~/Pages/CustomerPages/CommentPage.aspx");
        }
    }
}