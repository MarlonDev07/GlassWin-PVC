using AccesoDatos.DataBase;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;

namespace AccesoDatos.User
{
    public class CD_RestoreUser
    {
        public bool VerificationData(Int16 ID, string Email, string Phone)
        {
            try
            {
                ClsConnection Conn = new ClsConnection();
                using (var command = new SqlCommand())
                {
                    command.Connection = Conn.OpenConecction();
                    command.CommandText = "select * from Users where IDUser=@ID and Email=@Email and Phone = @Phone";
                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Phone", Phone);

                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        return true;
                    }

                    else
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ChangePassWord(int ID, string PassWord) 
        {
            try
            {
                ClsConnection Conn = new ClsConnection();
                using (var command = new SqlCommand())
                {
                    command.Connection = Conn.OpenConecction();
                    command.CommandText = "UPDATE Users SET PassWord = @PassWord WHERE IDUser = @ID";
                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@PassWord", PassWord);

                    command.CommandType = CommandType.Text;
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true;
                    }

                    else
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
