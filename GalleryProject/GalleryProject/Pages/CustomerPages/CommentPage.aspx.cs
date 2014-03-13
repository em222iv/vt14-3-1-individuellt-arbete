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
using System.Web.ModelBinding;


namespace GalleryProject.Pages.CustomerPages
{

    public partial class CommentPage : System.Web.UI.Page
    {
        private bool IsUploadSuccess
        {//sessin för att visa lyckat meddelande
            set { Session["UploadSuccess"] = value; }
            get
            {
                var isUploadSuccess = Session["UploadSuccess"] as bool? ?? false;
                Session.Remove("UploadSuccess");
                return isUploadSuccess;
            }
        }

        protected void Page_Load(object sender, EventArgs e, [RouteData] int PictureID)
        {
        }
                                                            //hämtar den medskickade datan i URLn 
        public IEnumerable<Comment> CommentListView_GetData([RouteData] int PictureID)
        {   //hämtar in alla kommentarerna
            return Service.GetComments(PictureID);
        }

        public void CommentListView_InsertItem(Comment comment, [RouteData] int PictureID)
        {
            if (TryUpdateModel(comment))
            {
                try
                {
                //
                Service.SaveComment(comment, PictureID);
                Session["insertSuccess"] = true;
                Response.Redirect(PictureID.ToString());
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "An error occured when trying to add comment");
                }
            }
        }
        public void CommentListView_UpdateItem(int commentID, [RouteData] int pictureID) 
        {
            try
            {
                var comment = Service.GetComment(commentID);
                if (comment == null)
                {
                    // Hittade inte kunden.
                    ModelState.AddModelError(String.Empty, String.Format("Galleriet med nummer {0} hittades inte.", commentID));
                    return;
                }

                if (TryUpdateModel(comment))
                {//skickar vidare comment till savecomment i service
                    Service.SaveComment(comment, pictureID);
                }
                IsUploadSuccess = true;
                //uppdaterar sidan till det bild id som är valt
                Response.Redirect(pictureID.ToString());

            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "An error occured when trying to update comment");
            }
        }
        public void CommentListView_DeleteItem(int commentID, [RouteData] int pictureID) 
        {
            try
            {//skickar med det valda id'et för att tas bort
                Service.DeleteComment(commentID);
                Session["deleteSuccess"] = true;
                //uppdaterar sidan
                Response.Redirect(pictureID.ToString());
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "An error occured when trying to delete comment");
            }
        }
    }
}