using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Drawing;
using Microsoft.Win32;
using GalleryProject.Model;

namespace GalleryProject.Model
{
    public class PictureDAL : DALBase
    {
        public Picture GetPicture(int pictureID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                //starta ett sqlcommand som sendan sparas undan 
                SqlCommand cmd = new SqlCommand("AppSchema.usp_GetPicture", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //skjuter in @ContactID så att den lagrade proceduren hittar.
                cmd.Parameters.AddWithValue("@PictureID", pictureID);

                // öppnar conn strängen
                conn.Open();


                //får en referens till executeReader 
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // getordinal hämtar index
                        var pictureIdIndex = reader.GetOrdinal("PictureID");
                        var pictureNameIndex = reader.GetOrdinal("PictureName");
                        var categoryNameIndex = reader.GetOrdinal("CatergoryID");
                     
                        return new Picture
                        {//skjuter in indexvärdena till parametrarna
                            PictureID = reader.GetInt32(pictureIdIndex),
                            PictureName = reader.GetString(pictureNameIndex),
                            CategoryID = reader.GetInt32(categoryNameIndex)
                        };
                    }
                }

                return null;
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer when trying to get picture");
                }
            }
        }
        public IEnumerable<Picture> GetPictures()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var Pictures = new List<Picture>(10);

                    var cmd = new SqlCommand("AppSchema.usp_SelectALLFromPictureTable", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var galleryIdIndex = reader.GetOrdinal("PictureID");
                        var galleryNameIndex = reader.GetOrdinal("PictureName");
                        var categoryNameIndex = reader.GetOrdinal("CatergoryID");

                        while (reader.Read())
                        {
                            Pictures.Add(new Picture
                            {
                                PictureID = reader.GetInt32(galleryIdIndex),
                                PictureName = reader.GetString(galleryNameIndex),
                                CategoryID = reader.GetInt32(categoryNameIndex)
                            });
                        }
                    }

                    Pictures.TrimExcess();

                    // Returnerar referensen till List-objektet med referenser med Customer-objekt.
                    return Pictures;
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer when trying to get pictures");
                }
            }
        }

        public void InsertPicture(Picture picture)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.usp_InsertPicture", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //skjuter in picturename och categoryid medens jag tar emot pictureid(output)
                    cmd.Parameters.Add("@PictureName", SqlDbType.VarChar, 30).Value = picture.PictureName;
                    cmd.Parameters.Add("@PictureID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@CatergoryID", SqlDbType.Int, 4).Value = picture.CategoryID;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    picture.PictureID = (int)cmd.Parameters["@PictureID"].Value;
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer when trying to insert picture");
                }
            }
        }
        public void DeletePicture(Picture picture)
        {

            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.usp_DeletePicture", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PictureID", SqlDbType.Int).Value = picture.PictureID;
                    //kallar på deletemetoden i imageDAL för att ta bort de fysiska bilderna istället för att göra detta i pictureDAL klassen
                    Model.ImageDAL delete = new Model.ImageDAL();
                    delete.DeletePicture(picture);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer when trying to delete picture");
                }
            }
        }

        public void UpdatePicture(Picture picture)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.usp_UpdatePicture", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //picture.PictureName har ändrats till den uppdaterade namnet och skickar detta till databasen
                    //databsen tar fortfarande bara unika namn då jag valt att validera namn på detta sätt. allt får ett unikt namn.
                    cmd.Parameters.Add("@PictureID", SqlDbType.Int).Value = picture.PictureID;
                    cmd.Parameters.Add("@PictureName", SqlDbType.VarChar, 30).Value = picture.PictureName;
                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = picture.CategoryID;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer when trying to update picture");
                }
            }
        }
    }
}