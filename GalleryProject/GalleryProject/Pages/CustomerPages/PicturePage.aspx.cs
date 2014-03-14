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
        private bool IsUploadSuccess
        {//skapar en session för att visa ett meddelande om en bild blivit uppladdad eller inte
            set { Session["UploadSuccess"] = value; }
            get
            {//sparar ner den i en variabel för att kunna aktivera den på valfria ställen
                var isUploadSuccess = Session["UploadSuccess"] as bool? ?? false;
                Session.Remove("UploadSuccess");
                return isUploadSuccess;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["insertSuccess"] as bool? == true)
            //{
            //    insertSuccess.Visible = true;
            //    Session.Remove("insertSuccess");
            //}

            //if (Session["deleteSuccess"] as bool? == true)
            //{
            //    deleteSuccess.Visible = true;
            //    Session.Remove("deleteSuccess");
            //}
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
                    // skickar vidare dessa till serviceklassens savepiture
                    Service.SavePicture(picture, file, filename);
                    //aktiverer en session som säger att bilden har lagt upp till användaren
                    Session["insertSuccess"] = true;
                    //uppdaterar sidan för att se att den nya bilden sparats
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
            var ThumbImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images/thumbImg");

            try
            {
                //hämtar bildens nuvarande namn från databasen och sparar ner den i oldimagename. samma med thumbnail
                var picture = Service.GetPicture(pictureID);
                string oldImageName = Path.Combine(imagePath, picture.PictureName);
                string oldthumbImageName = Path.Combine(ThumbImagePath, picture.PictureName);

                if (picture == null)
                {
                    // Hittas inte bilden så kastas undantaget
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
                    Service.SavePicture(picture, file, filename);

                    if (oldImageName != null)
                    { // hämtar bilden nya namn från databasen och sparar undan den i en variabel. samma för thumbnailen
                        var newPictureName = Service.GetPicture(pictureID);
                        var UpdatedImageName = Path.Combine(imagePath, newPictureName.PictureName);
                        var UpdatedThumbImageName = Path.Combine(ThumbImagePath, newPictureName.PictureName);
                        //byter den gamla bildfilens namn mot det nya för att stämma överens med databasen namn
                        File.Move(oldImageName, UpdatedImageName);
                        File.Move(oldthumbImageName, UpdatedThumbImageName);

                    }
                    //visar med ett meddelande att bidlen blivit uppladdad för användaren
                    IsUploadSuccess = true;
                    //uppdaterar sidan
                    Response.Redirect("~/Pages/CustomerPages/PicturePage.aspx");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då galleriet skulle uppdateras.");
            }
        }
        public void PictureListView_DeleteItem(int pictureID)
        {
            try
            {   //sparar undan bilden i picture
                var picture = Service.GetPicture(pictureID);
                //skickar med pictureobjektet
                Service.DeletePicture(picture);
                Session["deleteSuccess"] = true;
                //uppdaterar sidan
                Response.Redirect("~/Pages/CustomerPages/PicturePage.aspx");
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgiften skulle tas bort.");
            }
        }
    }
}