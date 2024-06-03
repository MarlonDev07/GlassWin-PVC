using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AccesoDatos.DataBase
{
    public class ClsConnection
    {
        private SqlConnection Conexion;
        private string connectionString;

        public ClsConnection()
        {
            connectionString = ConfigurationManager.ConnectionStrings["GlassWinDB"].ConnectionString;
            Conexion = new SqlConnection(connectionString);
        }

        public SqlConnection OpenConecction()
        {
            try
            {
                if (Conexion.State == ConnectionState.Closed)
                {
                    Conexion.ConnectionString = connectionString;
                    Conexion.Open();
                }
                return Conexion;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SqlConnection CloseConnection()
        {
            try
            {
                if (Conexion.State == ConnectionState.Open)
                    Conexion.Close();
                return Conexion;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetConnectionString()
        {
            return connectionString;
        }
    }
}
