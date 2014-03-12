﻿using System;
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
        {
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

        public IEnumerable<Comment> CommentListView_GetData([RouteData] int PictureID)
        {   
            return Service.GetComments(PictureID);
        }

        public void CommentListView_InsertItem(Comment comment, [RouteData] int PictureID)
        {
            if (TryUpdateModel(comment))
            {
                try
                {
                ImageDAL imgupload = new ImageDAL();
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
        public void CommentListView_UpdateItem(int commentID, [RouteData] int pictureID) // Parameterns namn måste överrensstämma med värdet DataKeyNames har.
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
                {
                    Service.SaveComment(comment, pictureID);
                }
                IsUploadSuccess = true;
                Response.Redirect(pictureID.ToString());

            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "An error occured when trying to update comment");
            }
        }
        public void CommentListView_DeleteItem(int commentID, [RouteData] int pictureID) // Parameterns namn måste överrensstämma med värdet DataKeyNames har.
        {
            try
            {
                var ImageQuery = Request.QueryString;
                Service.DeleteComment(commentID);
                Session["deleteSuccess"] = true;
                Response.Redirect(pictureID.ToString());
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "An error occured when trying to delete comment");
            }
        }
    }
}