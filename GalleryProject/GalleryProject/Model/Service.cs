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
    {   //gallerihantering
        private static GalleryDAL _galleryDAL;
        private static GalleryDAL galleryDAL { get { return _galleryDAL ?? (_galleryDAL = new GalleryDAL()); } }

        public static Gallery GetGallery(int galleryID) { return galleryDAL.GetGallery(galleryID); }

        public static IEnumerable<Gallery> GetGalleries() { return galleryDAL.GetGalleries(); }

        public static void DeleteGallery(Gallery gallery) { DeleteGallery(gallery); }

        public static void DeleteGallery(int galleryID) { galleryDAL.DeleteGallery(galleryID); }

        public static void SaveGallery(Gallery gallery)
        {
            if (gallery.GalleryID == 0)
            {
                galleryDAL.InsertGallery(gallery);
            }
            else
            {
                galleryDAL.UpdateGallery(gallery);
            }

        }
        //bildhantering
        private static PictureDAL _pictureDAL;
        private static PictureDAL pictureDAL { get { return _pictureDAL ?? (_pictureDAL = new PictureDAL()); } }
        private static ImageUpload _imageDAL;
        private static ImageUpload imageDAL { get { return _imageDAL ?? (_imageDAL = new ImageUpload()); } }

        public static Picture GetPicture(int pictureID) { return pictureDAL.GetPicture(pictureID); }

        public static IEnumerable<Picture> GetPictures() { return pictureDAL.GetPictures(); }

        public static void DeletePicture(Picture picture) { DeletePicture(picture); }

        public static void DeletePicture(int pictureID) { pictureDAL.DeletePicture(pictureID); }

        public static void SavePicture(Picture picture, Stream file, string filename)
        {
            if (picture.PictureID == 0)
            {
                pictureDAL.InsertPicture(picture);
                imageDAL.SaveImage(file, filename);
            }
            else
            {
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
            if (category.CategoryID == 0)
            {
                categoryDAL.InsertCategory(category);
            }
            else
            {
                categoryDAL.UpdateCategory(category);
            }
        }
    }
}