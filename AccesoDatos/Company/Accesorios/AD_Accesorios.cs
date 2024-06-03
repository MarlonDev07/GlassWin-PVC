using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Company.Accesorios
{
    public class AD_Accesorios
    {
        DataBase.ClsConnection Cnn = new DataBase.ClsConnection();

        public List<object> Articulo(int Id)
        {
            List<object> Accesorios = new List<object>();
            try
            {
                SqlDataReader Read;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SELECT Product.Description, Price.SalePrice FROM Product INNER JOIN Price ON Product.idProduct = Price.idProduct where Price.idPrice = @ID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", Id);
                Read = cmd.ExecuteReader();
                if (Read.Read())
                {
                    Accesorios.Add(Read.GetString(0));
                    Accesorios.Add(Read.GetDecimal(1));
                }
                Read.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Cnn.CloseConnection();
            }
            return Accesorios;
        }

    }
}
