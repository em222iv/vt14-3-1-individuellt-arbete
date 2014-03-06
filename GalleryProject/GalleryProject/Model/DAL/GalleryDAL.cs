using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using GalleryProject.Model;

namespace GalleryProject.Model
{
    public class GalleryDAL : DALBase
    {

        public Gallery GetGallery(int galleryID)
        {

            using (SqlConnection conn = CreateConnection())
            {
                //try
                //{
                    //starta ett sqlcommand som sendan sparas undan för att kunna exekveras
                    SqlCommand cmd = new SqlCommand("AppSchema.GetGallery", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //skjuter in @ContactID så att den lagrade proceduren hittar.
                    cmd.Parameters.AddWithValue("@GalleryID", galleryID);

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
                            var galleryIdIndex = reader.GetOrdinal("GalleryID");
                            var galleryNameIndex = reader.GetOrdinal("GalleryName");

                            return new Gallery
                            {
                                GalleryID = reader.GetInt32(galleryIdIndex),
                                GalleryName = reader.GetString(galleryNameIndex),
                         
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


        public IEnumerable<Gallery> GetGalleries()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                //Skapar det List-objekt som initialt har plats för 100 referenser till Customer-objekt.
                var Galleries = new List<Gallery>(10);

                // Skapar och initierar ett SqlCommand-objekt som används till att 
                // exekveras specifierad lagrad procedur.
                var cmd = new SqlCommand("AppSchema.SelectALLFromGalleryTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Öppnar anslutningen till databasen.
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var galleryIdIndex = reader.GetOrdinal("GalleryID");
                    var galleryNameIndex = reader.GetOrdinal("GalleryName");

                    while (reader.Read())
                    {
                        // Hämtar ut datat för en post.
                        // Du måste känna till SQL-satsen för att kunna välja rätt GetXxx-metod!!!!
                        Galleries.Add(new Gallery
                        {
                            GalleryID = reader.GetInt32(galleryIdIndex),
                            GalleryName = reader.GetString(galleryNameIndex),
                        });
                    }
                }

                Galleries.TrimExcess();

                // Returnerar referensen till List-objektet med referenser med Customer-objekt.
                return Galleries;
                }
                catch
                {
                    throw new ApplicationException("An error occured while getting conacts from the database.");
                }
            }
        }

        public void InsertGallery(Gallery gallery)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("AppSchema.InsertGallery", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@GalleryName", SqlDbType.VarChar, 30).Value = gallery.GalleryName;
                    cmd.Parameters.Add("@GalleryID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    gallery.GalleryID = (int)cmd.Parameters["@GalleryID"].Value;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
        public void DeleteGallery(int galleryID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.DeleteGallery", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@GalleryID", SqlDbType.Int).Value = galleryID;

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

        public void UpdateGallery(Gallery gallery)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.usp_UpdateGallery", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@GalleryID", SqlDbType.Int).Value = gallery.GalleryID;
                    cmd.Parameters.Add("@GalleryName", SqlDbType.VarChar, 30).Value = gallery.GalleryName;

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