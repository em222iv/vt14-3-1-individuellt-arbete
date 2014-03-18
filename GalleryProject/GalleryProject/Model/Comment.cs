using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GalleryProject.Model
{
    public class Comment
    { //För det objektet som inte har något värde så kastas errormessage.
        [Required(ErrorMessage = "Couldn't find comment")]
        public int CommentID { get; set;}

        [Required(ErrorMessage = "Couldn't find Picture")]
        public int PictureID { get; set; }
        //är strängen längre än 30tecken så kastas ett errormessage
        [Required(ErrorMessage = "Make a comment")]
        [StringLength(30, ErrorMessage = "The comment can't be longer than 300 letters")]
        public string CommentInput { get; set; }
        //är strängen längre än 30tecken så kastas ett errormessage
        [Required(ErrorMessage = "Fill in your commentator alias")]
        [StringLength(30, ErrorMessage = "The name can't be longer than 30 letters")]
        public string Commentator { get; set; }
    }
}