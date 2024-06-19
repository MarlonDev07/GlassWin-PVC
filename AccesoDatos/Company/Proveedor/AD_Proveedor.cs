using AccesoDatos.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Company.Proveedor
{
    public class AD_Proveedor
    {
        public DataTable CargarProveedor()
        {
            try
            {
                DataTable dt = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "";
                sql = "Select * from Proveedor";
                SqlDataAdapter da = new SqlDataAdapter(sql, con.OpenConecction());
                da.Fill(dt);
                con.CloseConnection();
                return dt;
            }
            catch (Exception)
            {

                return null;

            }
        }
    }
}
