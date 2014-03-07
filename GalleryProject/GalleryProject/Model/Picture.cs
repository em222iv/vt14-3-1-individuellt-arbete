using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalleryProject.Model
{
    public class Picture
    {
        public int PictureID { get; set; }
        public string PictureName { get; set; }
        public int CategoryID { get; set; }
    }
}