using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;


public partial class _Default : System.Web.UI.Page
{
    public partial class WebForm1 : System.Web.UI.Page
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
            /* var ImageQuery = Request.QueryString;
             Image.ImageUrl = "Content/Images/" + ImageQuery;
             Image.Visible = true;

             if (IsUploadSuccess)
             {
                 successLabel.Visible = true;
             }*/
        }


        //public IEnumerable<System.String> repeater_GetData()
        //{
        //    Gallery Gallery = new Gallery();
        //    return Gallery.GetImageName();

        //}

        protected void Button_Click(object sender, EventArgs e)
        {
            /*  if (IsValid)
               {
                   try
                   {
                       Gallery Gallery = new Gallery();
                       var file = fileBrowse.FileContent;
                       var filename = fileBrowse.FileName;
                       Gallery.SaveImage(file, filename);
                       IsUploadSuccess = true;
                       Response.Redirect("?" + filename);
                   }
                   catch (Exception ex)
                   {

                       ModelState.AddModelError(String.Empty, ex.Message);
                   }
               }*/
        }
        protected void deleteButton_Click(object sender, EventArgs e)
        {

            //    try
            //    {
            //        Gallery Gallery = new Gallery();
            //        var ImageQuery = Request.QueryString;

            //        Gallery.deleteImage(ImageQuery.ToString());
            //    }
            //    catch (Exception ex)
            //    {

            //        ModelState.AddModelError(String.Empty, ex.Message);
            //    }

            //    Image.ImageUrl = "";
            //}

        }
    }
}