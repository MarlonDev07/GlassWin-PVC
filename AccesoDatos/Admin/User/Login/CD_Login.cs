using AccesoDatos.DataBase;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;


namespace AccesoDatos.Login
{
    public class CD_Login
    {
        #region Login
        public bool Login(string user, string pass)
        {
            bool resp = false;
            try
            {
                ClsConnection Conn = new ClsConnection(user);
                using (var command = new SqlCommand())
                {
                    command.Connection = Conn.OpenConecction();
                    command.CommandText = "select * from Users where UserName=@user and PassWord=@pass";
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@pass", pass);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserCache.IdUser = reader.GetInt64(0);
                            UserCache.Name = reader.GetString(1);
                            UserCache.Phone = reader.GetString(2);
                            UserCache.Email = reader.GetString(3);
                            UserCache.User = reader.GetString(4);
                            UserCache.Roll = reader.GetString(6);
                            if (UserCache.Roll == "User")
                            {
                                UserCache.Expiration = reader.GetDateTime(7);
                                bool res = LoadDataCompany(UserCache.IdUser, user);
                                if (res)
                                {
                                    resp = true;
                                }
                                else
                                {
                                    resp = false;
                                }
                            }

                            UserCache.State = reader.GetString(8);
                            resp = true;
                        } 
                    }
                    else { return false; }

                    return resp;
                        
                }
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public bool LoadDataCompany(Int64 IDUser, string user) 
        {
            try
            {
                if (IDUser == 255505555)
                {
                    IDUser = 25550555;
                }
                ClsConnection Conn = new ClsConnection(user);

                using (var command = new SqlCommand())
                {
                    command.Connection = Conn.OpenConecction();
                    command.CommandText = "select * from Company where idUser = @ID";
                    command.Parameters.AddWithValue("@ID", IDUser);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CompanyCache.IdUser = reader.GetInt64(0);
                            CompanyCache.IdCompany = reader.GetInt64(1);
                            CompanyCache.Phone = reader.GetString(2);
                            CompanyCache.Address = reader.GetString(3);
                            CompanyCache.Name = reader.GetString(5);
                        }
                        return true;
                    }

                    else
                        return false;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        #endregion

    }
}
