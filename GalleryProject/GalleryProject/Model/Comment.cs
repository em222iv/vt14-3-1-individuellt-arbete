using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalleryProject.Model
{
    public class Comment
    {
        public int CommentID { get; set;}
        public int PictureID { get; set; }
        public string CommentInput { get; set; }
        public string Commentator { get; set; }
    }
}