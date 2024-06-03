using System;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.User
{
    public  class CD_CRUD_Users
    {
        DataBase.ClsConnection Cnn = new DataBase.ClsConnection();
        SqlDataReader Read;
        DataTable Table = new DataTable();

        public DataTable View(DateTime date, Int16 Mode)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_ViewUsers";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Mode", Mode);
                cmd.Parameters.AddWithValue("@DateNow", "");
                Read = cmd.ExecuteReader();
                Table.Load(Read);
                Cnn.CloseConnection();
                return Table;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
