using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GalleryProject.Model;
using System.IO;

namespace GalleryProject.Model
{
    public class Service
    {
        //bildhantering
        ///service pratar mellan codebohind och dataåtkomstlagert/affärslogiklagret
        //skapar instanser av klasserna och 
        private static PictureDAL _pictureDAL;
        private static PictureDAL pictureDAL { get { return _pictureDAL ?? (_pictureDAL = new PictureDAL()); } }
        private static ImageDAL _imageDAL;
        private static ImageDAL imageDAL { get { return _imageDAL ?? (_imageDAL = new ImageDAL()); } }
       
        public static Picture GetPicture(int pictureID) { return pictureDAL.GetPicture(pictureID); }

        public static IEnumerable<Picture> GetPictures() { return pictureDAL.GetPictures(); }

        public static void DeletePicture(Picture picture) { pictureDAL.DeletePicture(picture); }

        public static void SavePicture(Picture picture, Stream file, string filename, string oldImageName, string oldthumbImageName, int oldPictureID)
        {
            ICollection<ValidationResult> validationResults;
            if (!picture.Validate(out validationResults))
            {
                var ex = new ValidationException("The object didn't make the validation");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }
            if (picture.PictureID == 0)
            {
                imageDAL.SaveImage(file, filename, picture);
                pictureDAL.InsertPicture(picture);
            }
            else
            {
                imageDAL.UpdatePicture(picture, oldImageName, oldthumbImageName, oldPictureID);
                pictureDAL.UpdatePicture(picture);
            }

        }
        //category
        private static CategoryDAL _categoryDAL;
        private static CategoryDAL categoryDAL { get { return _categoryDAL ?? (_categoryDAL = new CategoryDAL()); } }

        public static Category GetCategory(int categoryID) { return categoryDAL.GetCategory(categoryID); }

        public static IEnumerable<Category> GetCategories() { return categoryDAL.GetCategories(); }

        public static void SaveCategory(Category category)
        {
            ICollection<ValidationResult> validationResults;
            if (!category.Validate(out validationResults))
            {
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }
            if (category.CategoryID == 0)
            {
                categoryDAL.InsertCategory(category);
            }
            else
            {
                categoryDAL.UpdateCategory(category);
            }
        }
        //kommentarhantering
        private static CommentDAL _commentDAL;
        private static CommentDAL commentDAL { get { return _commentDAL ?? (_commentDAL = new CommentDAL()); } }

        public static Comment GetComment(int commentID) { return commentDAL.GetComment(commentID); }

        public static IEnumerable<Comment> GetComments(int PictureID) { return commentDAL.GetComments(PictureID); }

        public static void DeleteComment(Comment comment) { DeleteComment(comment); }

        public static void DeleteComment(int commentID) { commentDAL.DeleteComment(commentID); }

        public static void SaveComment(Comment comment, int PictureID)
        {
            ICollection<ValidationResult> validationResults;
            if (!comment.Validate(out validationResults))
            {
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }
            if (comment.CommentID == 0)
            {
                commentDAL.InsertComment(comment, PictureID);
            }
            else
            {
                commentDAL.UpdateComment(comment);
            }
        }
    }
}