using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Company.ArticulosCombo
{
    public class AD_ArticulosCombo
    {
        private DataBase.ClsConnection Cnn;

        public AD_ArticulosCombo()
        {
            Cnn = new DataBase.ClsConnection();
        }

        public bool GuardarArticuloCombo(int idProduct, string Color, int IdQuote, int IdWindows)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = "insert into ArticulosCombo (idProduct,Color,IdQuote,IdWindows) values (@idProduct,@Color,@IdQuote,@IdWindows)";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@idProduct", idProduct);
                cmd.Parameters.AddWithValue("@Color", Color);
                cmd.Parameters.AddWithValue("@IdQuote", IdQuote);
                cmd.Parameters.AddWithValue("@IdWindows", IdWindows);
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
