using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GalleryProject.Model;


namespace GalleryProject
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["insertSuccess"] as bool? == true)
            {
                insertSuccess.Visible = true;
                Session.Remove("insertSuccess");
            }

            if (Session["deleteSuccess"] as bool? == true)
            {
                deleteSuccess.Visible = true;
                Session.Remove("deleteSuccess");
            }
        }
        public IEnumerable<Gallery> GalleryListView_GetData()
        {
            return Service.GetGalleries();
        }
        
        public void ContactListView_InsertItem(Gallery gallery)
        {
 
            if (ModelState.IsValid)
            {
                try
                {
                    Service.SaveGallery(gallery);
                    Session["insertSuccess"] = true;
                    Response.Redirect("~/Default.aspx");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då galleriet skulle läggas till.");
                }
            }
        }
        public void ContactListView_UpdateItem(int galleryID) // Parameterns namn måste överrensstämma med värdet DataKeyNames har.
        {
            //try
            //{
                var contact = Service.GetGallery(galleryID);
                if (contact == null)
                {
                    // Hittade inte kunden.
                    ModelState.AddModelError(String.Empty, String.Format("Galleriet med nummer {0} hittades inte.", galleryID));
                    return;
                }

                if (TryUpdateModel(contact))
                {
                    Service.SaveGallery(contact);
                }
                Response.Redirect("~/Default.aspx");

            //}
            //catch (Exception)
            //{
            //    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då galleriet skulle uppdateras.");
            //}
        }
        public void ContactListView_DeleteItem(int galleryID) // Parameterns namn måste överrensstämma med värdet DataKeyNames har.
        {
            try
            {
                Service.DeleteGallery(galleryID);
                Session["deleteSuccess"] = true;
                Response.Redirect("~/Default.aspx");
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgiften skulle tas bort.");
            }
        }
    }
}