using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GalleryProject.Model;

namespace GalleryProject.Model
{
    public class Service
    {
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
    }
}