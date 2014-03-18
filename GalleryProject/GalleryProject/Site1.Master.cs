using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GalleryProject
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Confirmation.Text = Page.GetTempData("Confirmation") as string;
            Confirmation.Visible = !String.IsNullOrWhiteSpace(Confirmation.Text);
        }
    }
}