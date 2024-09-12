using Dominio.ClassUser;
using Dominio.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.User
{
    public  class CD_Users
    {
        DataBase.ClsConnection Cnn = new DataBase.ClsConnection();
        SqlDataReader Read;
        DataTable Table = new DataTable();
       

        public DataTable View()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "select * from Users where Roll != 'Admin'";
                cmd.CommandType = CommandType.Text;
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

        public List<clsUser> FindxID(long ID) 
        {
            SqlCommand cmd = new SqlCommand();
            List<clsUser> UserList = new List<clsUser>();
            clsUser User = new clsUser();
            string query = "SELECT * FROM Users where idUser = @ID ";
            using (SqlCommand command = new SqlCommand(query, Cnn.OpenConecction()))
            {
                command.Parameters.AddWithValue("@ID", ID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User.Name = reader.GetString(1);
                        User.Phone = reader.GetString(2);
                        User.Email = reader.GetString(3);
                        User.User = reader.GetString(4);
                        User.Roll = reader.GetString(6);
                        User.State = reader.GetString(8);
                        UserList.Add(User);
                    }

                }
            }
            return UserList;

        }

        public bool Create(Int64 ID,string Name, string phone, string Email, string UserName, string PassWord, string Roll, DateTime Expiration, string State )
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_CreateUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@PassWord", PassWord);
                cmd.Parameters.AddWithValue("@Roll", Roll);
                cmd.Parameters.AddWithValue("@Expiration", Expiration);
                cmd.Parameters.AddWithValue("@State", State);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(long ID, string Name, string phone, string Email, string UserName, string Roll, string State)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_EditUser"; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUser", ID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Roll", Roll);
                cmd.Parameters.AddWithValue("@State", State);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
