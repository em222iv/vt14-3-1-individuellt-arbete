using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

/// <summary>
/// Summary description for GalleryDAL
/// </summary>
namespace GalleryProject
{
    public class GalleryDAL
    {
        public GalleryDAL()
        {
        }
        public Contact GetContact(int contactID)
        {

            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    //starta ett sqlcommand som sendan sparas undan för att kunna exekveras
                    SqlCommand cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //skjuter in @ContactID så att den lagrade proceduren hittar.
                    cmd.Parameters.AddWithValue("@ContactID", contactID);

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
                            var contactsIdIndex = reader.GetOrdinal("ContactID");
                            var firstNameIndex = reader.GetOrdinal("FirstName");
                            var lastNameIndex = reader.GetOrdinal("LastName");
                            var mailAdressIndex = reader.GetOrdinal("EmailAddress");

                            return new Contact
                            {
                                ContactID = reader.GetInt32(contactsIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAdress = reader.GetString(mailAdressIndex),
                            };
                        }
                    }


                    return null;
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }    
    }
}
    

    