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


namespace GalleryProject.Pages.CustomerPages
{

    public partial class PicturePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        public IEnumerable<Category> CategoryListView()
        {//skickar vidare mottagna värden till serviceklassens GetCategories
            return Service.GetCategories();
        }

        public IEnumerable<Picture> PictureListView_GetData()
        {//skickar vidare mottagna värden till serviceklassens GetPictures
            return Service.GetPictures();
        }

        public void PictureListView_InsertItem(Picture picture)
        {
            //Uppdaterar och binder värden som tas emot från formuläret till pictureobjektet
            if (TryUpdateModel(picture))
            {
                try
                {
                    //sparar undan filen och och filens namn i variablerlna file och filename
                    var file = fileBrowse.FileContent;
                    var filename = fileBrowse.FileName;
                    string oldImageName = "";
                    string oldthumbImageName = "";
                    // skickar vidare dessa till serviceklassens savepiture
                    Service.SavePicture(picture, file, filename, oldImageName, oldthumbImageName);
                    //aktiverer en session som säger att bilden har lagt upp till användaren
                    //uppdaterar sidan för att se att den nya bilden sparats
                    Page.SetTempData("Confirmation", "The picture has been added");
                    Response.Redirect("~/Pages/CustomerPages/PicturePage.aspx");
                }
                catch (Exception)
                {//kastar undandag ifall modelstaten inte går igenom
                    ModelState.AddModelError(String.Empty, "Error when trying to add picture");
                }
            }
        }
        public void PictureListView_UpdateItem(int pictureID)
        {//koden för att spara ner varbiabelnamn från databasen och för att byta namn på den existerande filen har jag tänkt att lägga
        //i en antingen en klass för sig eller till den existernande ImageDAL klassen som hanterar nedsparningen av bildfilerna.
        //Jag har tyvärr inte hunnit med detta
            //sparar den filmerna sökvägar
            var imagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images");
            var ThumbImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images\\thumbImg");

            //try
            //{
                //hämtar bildens nuvarande namn från databasen och sparar ner den i oldimagename. samma med thumbnail
                var picture = Service.GetPicture(pictureID);
                string oldImageName = Path.Combine(imagePath, picture.PictureName);
                string oldthumbImageName = Path.Combine(ThumbImagePath, picture.PictureName);
               

                if (picture == null)
                {
                    // Hittas inte bilden så kastas undantag
                    ModelState.AddModelError(String.Empty, String.Format("Picture with this id couldn't be found. ID{0}", pictureID));
                    return;
                }
                //Uppdaterar och binder värden som tas emot från formuläret till pictureobjektet
                if (TryUpdateModel(picture))
                {
                    //Skapar variablerna för att savepicture klassen i service vill ha dem när man väl vill spara filer. 
                    //de används inte till något just här utom undgå detta problem då jag inte hunnit titta närmare på det
                    var file = fileBrowse.FileContent;
                    var filename = fileBrowse.FileName;
                    //skickar vidare filväden och picureobjektes referensen till serviceklassen för att sparas tillsammans med fil och file
                    Service.SavePicture(picture, file, filename, oldImageName, oldthumbImageName);
                    //visar success meddelande, uppdaterar sidan
                    Page.SetTempData("Confirmation", "The picture has been edited");
                    Response.Redirect("~/Pages/CustomerPages/PicturePage.aspx");
                }
            //}
            //catch (Exception)
            //{
            //    ModelState.AddModelError(String.Empty, "An error occured when trying to edit picture");
            //}
        }
        public void PictureListView_DeleteItem(int pictureID)
        {
            try
            {   //sparar undan bilden i picture
                var picture = Service.GetPicture(pictureID);
                //skickar med pictureobjektet
                Service.DeletePicture(picture);
               
                //uppdaterar sidan
                Page.SetTempData("Confirmation", "The picture has been deleted");
                Response.Redirect("~/Pages/CustomerPages/PicturePage.aspx");
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "An error occurd when trying to delete picture");
            }
        }
    }
}