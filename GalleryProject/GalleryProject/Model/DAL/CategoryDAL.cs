using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using GalleryProject.Model;

namespace GalleryProject.Model
{
    public class CategoryDAL : DALBase
    {
        //tar emot ett ett id för att hämta dess info från databasen
        public Category GetCategory(int categoryID)
        {

            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                //starta ett sqlcommand som sendan sparas undan för att kunna exekveras
                SqlCommand cmd = new SqlCommand("AppSchema.GetCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //skjuter in @ContactID så att den lagrade proceduren hittar.
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);

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
                        var categoryIdIndex = reader.GetOrdinal("CategoryID");
                        var categoryNameIndex = reader.GetOrdinal("CategoryName");

                        return new Category
                        {   
                            CategoryID = reader.GetInt32(categoryIdIndex),
                            CategoryName = reader.GetString(categoryNameIndex),

                        };
                    }
                }

                return null;
                }
                catch
                {//kastar ut ett undantag från dataåtkomstlagret
                    throw new ApplicationException("An error occured in the data access layer when trying to get category");
                }
            }
        }


        public IEnumerable<Category> GetCategories()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Skapar det List-objekt som initialt har plats för 100 referenser till Customer-objekt.
                    var Categories = new List<Category>(10);

                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("AppSchema.usp_SelectALLFromCategoryTable", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {   //skjuter in categoryfältens värde till variablerna
                        var categoryIdIndex = reader.GetOrdinal("CategoryID");
                        var categoryNameIndex = reader.GetOrdinal("CategoryName");

                        while (reader.Read())
                        {
                            // Hämtar ut data och sparar ner
                            Categories.Add(new Category
                            {
                                CategoryID = reader.GetInt32(categoryIdIndex),
                                CategoryName = reader.GetString(categoryNameIndex),
                            });
                        }
                    }
                    //minimerar utrymme som listan tar
                    Categories.TrimExcess();

                    // Returnerar referensen till List-objektet
                    return Categories;
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer when trying to get categories");
                }
            }
        }

        public void InsertCategory(Category category)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    //hämtar lagrad procedur 
                    SqlCommand cmd = new SqlCommand("AppSchema.InsertCategory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // skickar in categoru name som parameter och tar emot cateogryID(output)
                    cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar, 30).Value = category.CategoryName;
                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    //öppnar sträng
                    conn.Open();
                    //kör den lagrade proceduren
                    cmd.ExecuteNonQuery();
                    //sparar ner outputvärdet till en int
                    category.CategoryID = (int)cmd.Parameters["@CategoryID"].Value;
                }
                catch
                {
                    throw new ApplicationException("AAn error occured in the data access layer when trying to set category");
                }
            }
        }
      
        public void UpdateCategory(Category category)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.usp_UpdateGallery", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = category.CategoryID;
                    cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar, 30).Value = category.CategoryName;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer when trying to update category");
                }
            }
        }
    }
}