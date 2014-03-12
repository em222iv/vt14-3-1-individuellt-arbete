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
    {
        private static readonly Regex ApprovedExenstions;
        private static string PhysicalUploadedImagePath;
        private static string PhysicalUploadedThumbnailPath;

        static ImageDAL()
        {
            ApprovedExenstions = new Regex(@"^.*\.(gif|jpg|png)$", RegexOptions.IgnoreCase);
            PhysicalUploadedImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images");
            PhysicalUploadedThumbnailPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images/thumbImg");
        }

        bool ImageExist(string PictureName)
        {
            return File.Exists(Path.Combine(PhysicalUploadedImagePath, PictureName));
        }

        bool IsValidImage(Image image)
        {
            return image != null &&
                (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
        }
        public string SaveImage(Stream stream, string fileName, string PictureName, Picture picture)
        {
            var noExtension = Path.GetFileNameWithoutExtension(fileName);
            var Extension = Path.GetExtension(fileName);

            var image = System.Drawing.Image.FromStream(stream);
            var thumbnail = image.GetThumbnailImage(200, 140, null, System.IntPtr.Zero);
            var PictureFullName = PictureName + Extension;

            try
            {
                if (ImageExist(PictureFullName))
                {
                    int imageExistCount = 0;
                    while (ImageExist(PictureFullName))
                    {
                        imageExistCount++;
                        PictureFullName = string.Format("{0}{1}{2}", PictureName, imageExistCount, Extension);
                        image.Save(Path.Combine(PhysicalUploadedImagePath, PictureName));
                        thumbnail.Save(Path.Combine(PhysicalUploadedThumbnailPath, PictureName));
                    }
                }
                if (IsValidImage(image))
                {

                    image.Save(Path.Combine(PhysicalUploadedImagePath, PictureFullName));
                    thumbnail.Save(Path.Combine(PhysicalUploadedThumbnailPath, PictureFullName));

                }
                if (!ApprovedExenstions.IsMatch(PictureFullName))
                {
                    throw new Exception("MIME does not match");
                }
            }
            catch
            {
                throw new ApplicationException("An error occured in the data access layer when trying to save picture");
            }
            picture.PictureName = PictureFullName;


            return fileName;

        }
    }
}