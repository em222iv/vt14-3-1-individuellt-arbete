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
    public class CommentDAL : DALBase
    {       //tar emot ett ett id för att hämta dess info från databasen
        public Comment GetComment(int commentID)
        {

            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                //hämtar lagrad procedur och sparar undan den
                SqlCommand cmd = new SqlCommand("AppSchema.usp_GetComment", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //skjuter in @CommentID så att den lagrade proceduren hittar.
                cmd.Parameters.AddWithValue("@CommentID", commentID);

                conn.Open();

                //får en referens till executeReader
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Om det finns en post att läsa returnerar Read true. Finns ingen post returnerar
                    // Read false.
                    if (reader.Read())
                    {
                        // getordinal hämtar index
                        var commentIdIndex = reader.GetOrdinal("CommentID");
                        var pictureIdIndex = reader.GetOrdinal("PictureID");
                        var commentIndex = reader.GetOrdinal("Comment");
                        var commentatorIndex = reader.GetOrdinal("Commentator");

                        return new Comment
                        {
                            CommentID = reader.GetInt32(commentIdIndex),
                            PictureID = reader.GetInt32(pictureIdIndex),
                            CommentInput = reader.GetString(commentIndex),
                            Commentator = reader.GetString(commentatorIndex)
                        };
                    }
                }

                return null;
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer when trying to get comment");
                }
            }
        }

        public IEnumerable<Comment> GetComments(int PictureID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                   //skapar listan
                    var Comments = new List<Comment>(100);

                    //checkar den lagrade proceduren och spara undan den
                    var cmd = new SqlCommand("AppSchema.usp_SelectCommentByID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //skickar med PictureID
                    cmd.Parameters.AddWithValue("@PictureID", PictureID);
                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var commentIDIndex = reader.GetOrdinal("CommentID");
                        var commentIndex = reader.GetOrdinal("Comment");
                        var commentatorIndex = reader.GetOrdinal("Commentator");
                    

                        while (reader.Read())
                        {
                            // Hämtar ut datat för en post.
                            Comments.Add(new Comment
                            {
                                CommentID = reader.GetInt32(commentIDIndex),
                                CommentInput = reader.GetString(commentIndex),
                                Commentator = reader.GetString(commentatorIndex)
                            });
                        }
                    }

                    Comments.TrimExcess();

                    // Returnerar referensen till List-objektet 
                    return Comments;
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer when trying to get comments");
                }
            }
        }

        public void InsertComment(Comment comment, int PictureID)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
               try
               {
                    //läser av proceduren och spara ner den
                    SqlCommand cmd = new SqlCommand("AppSchema.usp_InsertComment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //skickar med/tar emot 
                    cmd.Parameters.Add("@CommentID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@PictureID", SqlDbType.Int, 4).Value = PictureID;
                    cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 300).Value = comment.CommentInput;
                    cmd.Parameters.Add("@Commentator", SqlDbType.VarChar, 30).Value = comment.Commentator;

                    conn.Open();
                    //kör proceduren
                    cmd.ExecuteNonQuery();

                    comment.CommentID = (int)cmd.Parameters["@CommentID"].Value;

            }
            catch
            {
                 throw new ApplicationException("An error occured in the data access layer when trying to comment");
            }
            }
        }
        public void DeleteComment(int commentID)
        {    // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {   
                    SqlCommand cmd = new SqlCommand("AppSchema.usp_DeleteComment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CommentID", SqlDbType.Int).Value = commentID;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer when trying to delete comment");
                }
            }
        }

        public void UpdateComment(Comment comment)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.usp_UpdateComment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CommentID", SqlDbType.Int, 4).Value = comment.CommentID;
                    cmd.Parameters.Add("@PictureID", SqlDbType.Int, 4).Value = comment.PictureID;
                    cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 300).Value = comment.CommentInput;
                    cmd.Parameters.Add("@Commentator", SqlDbType.VarChar, 30).Value = comment.Commentator;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer when trying to update comment");
                }
            }
        }
    }
}