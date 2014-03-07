using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using GalleryProject.Model;

namespace GalleryProject.Model
{
    public class PictureDAL : DALBase
    {

        public Picture GetPicture(int pictureID)
        {

            using (SqlConnection conn = CreateConnection())
            {
                //try
                //{
                //starta ett sqlcommand som sendan sparas undan för att kunna exekveras
                SqlCommand cmd = new SqlCommand("AppSchema.usp_GetPicture", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //skjuter in @ContactID så att den lagrade proceduren hittar.
                cmd.Parameters.AddWithValue("@PictureID", pictureID);

                // öppnar conn strängen
                conn.Open();


                //får en referens till executeReader
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Om det finns en post att läsa returnerar Read true. Finns ingen post returnerar
                    // Read false.
                    if (reader.Read())
                    {
                        // getordinal hämtar index
                        var pictureIdIndex = reader.GetOrdinal("PictureID");
                        var pictureNameIndex = reader.GetOrdinal("PictureName");
                        var categoryNameIndex = reader.GetOrdinal("CategoryID");

                        return new Picture
                        {
                            PictureID = reader.GetInt32(pictureIdIndex),
                            PictureName = reader.GetString(pictureNameIndex),
                            CategoryID = reader.GetInt32(categoryNameIndex)

                        };
                    }
                }

                return null;
                //}
                //catch
                //{
                //    throw new ApplicationException("An error occured in the data access layer.");
                //}
            }
        }


        public IEnumerable<Picture> GetPictures()
        {
            using (var conn = CreateConnection())
            {
                //try
                //{
                    //Skapar det List-objekt som initialt har plats för 100 referenser till Customer-objekt.
                    var Pictures = new List<Picture>(10);

                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("AppSchema.usp_AllFromPictureTable", conn);
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
                            // Hämtar ut datat för en post.
                            // Du måste känna till SQL-satsen för att kunna välja rätt GetXxx-metod!!!!
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
                //}
                //catch
                //{
                //    throw new ApplicationException("An error occured while getting conacts from the database.");
                //}
            }
        }

        public void InsertPicture(Picture picture)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                //try
                //{

                    SqlCommand cmd = new SqlCommand("AppSchema.usp_InsertPicture", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PictureName", SqlDbType.VarChar, 30).Value = picture.PictureName;
                    cmd.Parameters.Add("@PictureID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@CatergoryID", SqlDbType.Int, 4).Direction = ParameterDirection.Input;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    picture.PictureID = (int)cmd.Parameters["@PictureID"].Value;
                //}
                //catch
                //{
                //    // Kastar ett eget undantag om ett undantag kastas.
                //    throw new ApplicationException("An error occured in the data access layer.");
                //}
            }
        }
        public void DeletePicture(int pictureID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.usp_DeletePicture", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PictureID", SqlDbType.Int).Value = pictureID;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
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

                    cmd.Parameters.Add("@PictureID", SqlDbType.Int).Value = picture.PictureID;
                    cmd.Parameters.Add("@PictureName", SqlDbType.VarChar, 30).Value = picture.PictureName;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en INSERT-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
    }
    
}