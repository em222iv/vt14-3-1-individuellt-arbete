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
        protected void Page_Load(object sender, EventArgs e, [RouteData] int PictureID)
        {
        }
                                                            //hämtar den medskickade datan i URLn 
        public IEnumerable<Comment> CommentListView_GetData([RouteData] int PictureID)
        {   //hämtar in alla kommentarerna från utvald bild
            return Service.GetComments(PictureID);
        }
                                                                //hämtar den medskickade datan i URLn 
        public void CommentListView_InsertItem(Comment comment, [RouteData] int PictureID)
        {
            if (TryUpdateModel(comment))
            {
                try
                {
                //skickar med commentobjektet och det bestämda pictureID 
                Service.SaveComment(comment, PictureID);
                //skickar meddelnade ifall det lyckats och redirectar sidan
                Page.SetTempData("Confirmation", "The comment has been added");
                Response.Redirect(PictureID.ToString());
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "An error occured when trying to add comment");
                }
            }
        }                                                       //hämtar den medskickade datan i URLn 
        public void CommentListView_UpdateItem(Comment Comment, [RouteData] int pictureID) 
        {
            try
            {
                var comment = Service.GetComment(Comment.CommentID);
                if (comment == null)
                {
                    // Hittade inte kunden.
                    ModelState.AddModelError(String.Empty, String.Format("Coudld not find the comment with this id. {0}", Comment.CommentID));
                    return;
                }

                if (TryUpdateModel(comment))
                {//skickar vidare comment till savecomment i service
                    Service.SaveComment(comment, pictureID);
                }

                // //skickar meddelnade ifall det lyckats och redirectar sidan till bildens picture id som man sen tidigare valt
                Page.SetTempData("Confirmation", "The comment has been edited");
                Response.Redirect(pictureID.ToString());

            }
            catch (Exception)
            {//kastar undantag om ett fel sker i uppdateringsprocessen
                ModelState.AddModelError(String.Empty, "An error occured when trying to update comment");
            }
        }                                                      //hämtar den medskickade datan i URLn 
        public void CommentListView_DeleteItem(int commentID, [RouteData] int pictureID) 
        {
            try
            {//skickar med det valda id'et för att tas bort
                Service.DeleteComment(commentID);
                // ger ett success meddelande, uppdaterar sidan
                Page.SetTempData("Confirmation", "The comment has been deleted");
                Response.Redirect(pictureID.ToString());
            }
            catch (Exception)
            {//Annars kastast ett undantag
                ModelState.AddModelError(String.Empty, "An error occured when trying to delete comment");
            }
        }
    }
}