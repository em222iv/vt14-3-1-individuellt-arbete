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
    public class ImageUpload
    {
        Picture picture = new Picture();

        private static readonly Regex ApprovedExenstions;
        private static string PhysicalUploadedImagePath;

        static ImageUpload()
        {
            ApprovedExenstions = new Regex(@"^.*\.(gif|jpg|png)$", RegexOptions.IgnoreCase);
            PhysicalUploadedImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images");
        }
        bool ImageExist(string name)
        {
            return File.Exists(Path.Combine(PhysicalUploadedImagePath, name));
        }

        //public void DeleteImage(string fileName, string PictureName)
        //{

        //    if (ImageExist(fileName))
        //    {
        //        File.Delete(Path.Combine(PhysicalUploadedImagePath, PictureName));
        //    }
        //    else
        //    {
        //        throw new ApplicationException("Choose a picture to delete");
        //    }


        //}  
      
        public string SaveImage(Stream stream, string fileName, string PictureName)
        {

            var image = System.Drawing.Image.FromStream(stream);
            //var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);

            if (ImageExist(fileName))
            {
                var noExtension = Path.GetFileNameWithoutExtension(fileName);
                var Extension = Path.GetExtension(fileName);

           

                while (ImageExist(fileName))
                {
                    File.Delete(Path.Combine(PhysicalUploadedImagePath, PictureName));
                }
            }
            if (IsValidImage(image))
            {
          
                var Extension = Path.GetExtension(fileName);
                fileName = string.Format("{0}{1}", PictureName, Extension);
                image.Save(Path.Combine(PhysicalUploadedImagePath, fileName));

            }

            if (!ApprovedExenstions.IsMatch(fileName))
            {
                throw new Exception("MIME does not match");
            }
            return fileName;

        }
        bool IsValidImage(Image image)
        {
            return image != null &&
                (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
        }
    }
}