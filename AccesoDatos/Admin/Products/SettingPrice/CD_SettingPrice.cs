using System.Data.SqlClient;
using System.Data;
using System;
using Dominio.SettingPrice;
using System.Collections.Generic;


namespace AccesoDatos.SettingPrice
{
    public class CD_SettingPrice
    {
        DataBase.ClsConnection Cnn = new DataBase.ClsConnection();

        #region Read
        public DataTable Find(string Name)
        {
            try
            {
                DataTable Table = new DataTable();
                SqlDataReader Read;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = Name;
                Read = cmd.ExecuteReader();
                Table.Load(Read);
                Cnn.CloseConnection();
                return Table;
            }
            catch (Exception )
            {
                return null;
            }

        }
        public DataTable View()
        {
            try
            {
                DataTable Table = new DataTable();
                SqlDataReader Read;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SELECT * FROM SettingPrice";
                cmd.CommandType = CommandType.Text;
                Read = cmd.ExecuteReader();
                Table.Load(Read);
                Cnn.CloseConnection();
                return Table;
            }
            catch (System.Exception)
            {
                return null;
            }

        }
        #endregion

        #region Create
        public bool CreateSettingPrice(string Name, decimal Percentage, string Supplier)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_InsertSettingPrice";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Percentage", Percentage);
                cmd.Parameters.AddWithValue("@Supplier", Supplier);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion

        #region Update
        public List<SettingPriceClass> DataSettingsPrice(int ID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                List<SettingPriceClass> SettingList = new List<SettingPriceClass>();

                string query = "SELECT Name, Percentage, Supplier FROM SettingPrice WHERE idSettingsPrice = @ID";
                using (SqlCommand command = new SqlCommand(query, Cnn.OpenConecction()))
                {
                    command.Parameters.AddWithValue("@ID", ID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SettingPriceClass SettingsPrice = new SettingPriceClass(); // Create a new instance for each record

                            SettingsPrice.Name = reader.GetString(reader.GetOrdinal("Name"));
                            SettingsPrice.Supplier = reader.GetString(reader.GetOrdinal("Supplier"));
                            SettingsPrice.Percentage = reader.GetDecimal(reader.GetOrdinal("Percentage"));

                            SettingList.Add(SettingsPrice);
                        }
                    }
                }

                return SettingList;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public bool UpdateSettingPrice(int ID,string Name, decimal Percentage, string Supplier)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_UpdateSettingPrice";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Percentage", Percentage);
                cmd.Parameters.AddWithValue("@Supplier", Supplier);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion
    }
}
