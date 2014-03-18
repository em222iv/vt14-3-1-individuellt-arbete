using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GalleryProject.Model
{
    public class Category
    {    //För det objektet som inte har något värde så kastas errormessage.
        [Required(ErrorMessage="Choose a category")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Choose a category")]
        public string CategoryName { get; set; }
    }
}