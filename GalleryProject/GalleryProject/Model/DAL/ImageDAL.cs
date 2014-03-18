using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Drawing;
using Microsoft.Win32;


namespace GalleryProject.Model
{
    public class ImageDAL
    {   // skapar variabler för att göra sökvägar och användande av regex
        //om tid finns så läggs dessa in i en global klass för att slippa uppresa sig
        private static readonly Regex ApprovedExenstions;
        private static string PhysicalUploadedImagePath;
        private static string PhysicalUploadedThumbnailPath;

        static ImageDAL()
        {//sätter värdet på sökväg och regex variablerna.
            ApprovedExenstions = new Regex(@"^.*\.(gif|jpg|png)$", RegexOptions.IgnoreCase);
            PhysicalUploadedImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images");
            PhysicalUploadedThumbnailPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images/thumbImg");
        }

        bool ImageExist(string PictureName)
        {//tittar om en fil existerar i sökvägen med samma namn som parametern inenhåller
            return File.Exists(Path.Combine(PhysicalUploadedImagePath, PictureName));
        }

        bool IsValidImage(Image image)
        {//jämför ifall bildens format stämmer överens med det de formaten gif,jpeg/jpg,png
            return image != null &&
                (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
        }
        public string SaveImage(Stream stream, string fileName, Picture picture)
        {   //sparar undan filnamnet utan sitt nämna format/ändelse
            var noExtension = Path.GetFileNameWithoutExtension(fileName);
            //sparar undan bildformatet
            var Extension = Path.GetExtension(fileName);
            
            //sparar udan bilden i imagevariablen
            var image = System.Drawing.Image.FromStream(stream);

            //sparar ner en thumbnail av bilden i en fast storlek
            var thumbnail = image.GetThumbnailImage(200, 140, null, System.IntPtr.Zero);
            //sparar undan filnamnet med använderns anvgina bildnamn
            var PictureFullName = picture.PictureName + Extension;

            try
            {//kollar om bilden redan finns
                if (ImageExist(PictureFullName))
                {
                    int imageExistCount = 0;
                    while (ImageExist(PictureFullName))
                    {//finns bilden så går en countar igång och lägger på en siffra i slutet av bildnamnet tills det att det har ett unikt namn
                        imageExistCount++;
                        PictureFullName = string.Format("{0}{1}{2}", picture.PictureName, imageExistCount, Extension);
                        //sparar undan
                        image.Save(Path.Combine(PhysicalUploadedImagePath, picture.PictureName));
                        thumbnail.Save(Path.Combine(PhysicalUploadedThumbnailPath, picture.PictureName));
                    }
                }
                //om inte filerna matchar formaten så kastat ett undantag
                if (!ApprovedExenstions.IsMatch(PictureFullName))
                {
                    throw new Exception("MIME does not match");
                }
                if (IsValidImage(image))
                {
                    //sparar ner den ursprungliga bilden och thumbnailbilden i sina bestäma mappar
                    image.Save(Path.Combine(PhysicalUploadedImagePath, PictureFullName));
                    thumbnail.Save(Path.Combine(PhysicalUploadedThumbnailPath, PictureFullName));
                }
            }
            catch
            {
                throw new ApplicationException("An error occured in the data access layer when trying to save picture");
            }
            //sätter parametern PictureNames värde till den inlangda bildens värde för att kunna spara undan det i databasen
            picture.PictureName = PictureFullName;


            return fileName;

        }
    }
}