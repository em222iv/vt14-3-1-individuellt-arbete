using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for DALBase
/// </summary>
namespace GalleryProject
{
    public abstract class DALBase
    {
        private static string _connectionString;

        static DALBase()
        {

            _connectionString = WebConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString;

        }

        protected static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}