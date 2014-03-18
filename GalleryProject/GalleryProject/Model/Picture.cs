using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GalleryProject.Model
{
    public class Picture
    {   
        //För det objektet som inte har något värde så kastas errormessage.
        [Required(ErrorMessage = "Choose a picture.")]
        public int PictureID { get; set; }

        //är strängen längre än 30tecken så kastas ett errormessage
        [Required(ErrorMessage = "Name the picture.")]
        [StringLength(30, ErrorMessage = "The name can't be longer than 30 letters")]
        public string PictureName { get; set; }

        [Required(ErrorMessage = "Choose a category.")]
        public int CategoryID { get; set; }
    }
}