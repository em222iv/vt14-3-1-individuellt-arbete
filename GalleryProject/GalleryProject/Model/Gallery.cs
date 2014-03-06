using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GalleryProject.Model
{
    public class Gallery
    {
        public int GalleryID { get; set; }
        public string GalleryName { get; set; }
    }
}