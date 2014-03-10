using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;
using GalleryProject.Model;


namespace GalleryProject.Pages.CustomerPages
{

    public partial class WebForm2 : System.Web.UI.Page
    {
        private bool IsUploadSuccess
        {
            set { Session["UploadSuccess"] = value; }
            get
            {
                var isUploadSuccess = Session["UploadSuccess"] as bool? ?? false;
                Session.Remove("UploadSuccess");
                return isUploadSuccess;
            }
        }

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

        public IEnumerable<Category> CategoryListView()
        {
            return Service.GetCategories();
        }


        public IEnumerable<Picture> PictureListView_GetData()
        {
            return Service.GetPictures();
        }

        public void PictureListView_InsertItem(Picture picture)
        {

            if (ModelState.IsValid)
            {
                //try
                //{
                    ImageUpload imgupload = new ImageUpload();
                    var file = fileBrowse.FileContent;
                    var filename = fileBrowse.FileName;
                    Service.SavePicture(picture, file, filename, picture.PictureName);
                    Session["insertSuccess"] = true;
                    Response.Redirect("~/Pages/CustomerPages/WebForm2.aspx");
                //}
                //catch (Exception)
                //{
                //    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då galleriet skulle läggas till.");
                //}
            }
        }
        public void PictureListView_UpdateItem(int pictureID) // Parameterns namn måste överrensstämma med värdet DataKeyNames har.
        {
            //try
            //{
            var picture = Service.GetPicture(pictureID);
            if (picture == null)
            {
                // Hittade inte kunden.
                ModelState.AddModelError(String.Empty, String.Format("Galleriet med nummer {0} hittades inte.", pictureID));
                return;
            }

            if (TryUpdateModel(picture))
            {
                ImageUpload imgupload = new ImageUpload();
                
                var file = fileBrowse.FileContent;
                var filename = fileBrowse.FileName;
                Service.SavePicture(picture, file, filename, picture.PictureName);

                IsUploadSuccess = true;
                Response.Redirect("~/Pages/CustomerPages/WebForm2.aspx");
            }
            

            //}
            //catch (Exception)
            //{
            //    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då galleriet skulle uppdateras.");
            //}
        }
        public void PictureListView_DeleteItem(int pictureID) // Parameterns namn måste överrensstämma med värdet DataKeyNames har.
        {
            try
            {
                var ImageQuery = Request.QueryString;
                Service.DeletePicture(pictureID);
                Session["deleteSuccess"] = true;
                Response.Redirect("~/Pages/CustomerPages/WebForm2.aspx");
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgiften skulle tas bort.");
            }
        }
    }
}