using Dominio.Model.ClassWindows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesoDatos.Company.Quotes
{
    public class AD_Quote
    {
        private DataBase.ClsConnection Cnn;

        public AD_Quote()
        {
            Cnn = new DataBase.ClsConnection();
        }
        //Cargar los datos de las ventanas en el dgv
        public DataTable LoadProductDetailsByIdQuote(int idQuote)
        {
            try
            {
                DataTable dataTable = new DataTable();
                string query = "SELECT DISTINCT p.idProduct, w.IdQuote, w.IdWindows, w.Glass, w.Width, w.Height, p.System, p.Description, pp.Tamaño " +
                               "FROM Product p " +
                               "INNER JOIN Price pp ON p.idProduct = pp.idProduct " +
                               "INNER JOIN Windows w ON p.System = w.System " +
                               "WHERE w.IdQuote = @IdQuote AND pp.Tamaño > 0" +
                               "ORDER BY w.IdWindows DESC";
                SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdQuote", idQuote);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles del producto: " + ex.Message);
                return null;
            }
        }

        //Metodo que devuelve los proyectos por IdCompany del cliente
        public DataTable LoadProjectsByCompanyId()
        {
            try
            {
                DataTable dataTable = new DataTable();
                string query = "select Q.ProjectName, Q.IdQuote, C.Name from Quote Q Inner Join Client C ON Q.IdClient = C.IdClient where C.IdCompany = @IdCompany ORDER BY Q.Date DESC";
                SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los proyectos: " + ex.Message);
                return null;
            }
        }
        public DataTable LoadQuotes()
        {
            try
            {
                DataTable dataTable = new DataTable();
                string query = "select Q.IdClient, Q.IdQuote, Q.Date, C.Name, Q.ProjectName, Q.Address, Q.SubTotal, Q.IVA, Q.Total from Quote Q Inner Join Client C ON Q.IdClient = C.IdClient where C.IdCompany = @IdCompany and State = 'Pending' order by Q.Date desc";
                SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public DataTable LoadQuotesFacturadas()
        {
            try
            {
                DataTable dataTable = new DataTable();
                string query = "select Q.IdClient,Q.IdQuote,Q.Date,C.Name,Q.ProjectName,Q.Address,Q.SubTotal,Q.IVA,Q.Total from Quote Q Inner Join Client C ON Q.IdClient = C.IdClient where C.IdCompany = @IdCompany and State = 'Factura' order by Q.Date desc";
                SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool EditQuote(int idQuote, DateTime Date, string ProjetName, string Address, string Condition, decimal Discount, decimal Labour, decimal IVA, decimal SubTotal, decimal Total, int IdClient)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_EditQuote";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QuoteID", idQuote);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@ProjectName", ProjetName);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Condition", Condition);
                cmd.Parameters.AddWithValue("@Discount", Discount);
                cmd.Parameters.AddWithValue("@Labour", Labour);
                cmd.Parameters.AddWithValue("@IVA", IVA);
                cmd.Parameters.AddWithValue("@SubTotal", SubTotal);
                cmd.Parameters.AddWithValue("@Total", Total);
                cmd.Parameters.AddWithValue("@IdClient", IdClient);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();


                return true;
            }
            catch { return false; }
        }

        public bool DeleteQuote(int idQuote)
        {
            //Eliminar Ventanas y la proforma
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_DeleteQuote";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdQuote", idQuote);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch { return false; }
        }

        public int InsertQuoteAndGetLastID(DateTime Date, string ProjectName, string Address, string Condition, decimal Discount, decimal Labour, decimal IVA, decimal SubTotal, decimal Total, int IdClient)
        {
            using (SqlConnection connection = Cnn.OpenConecction())
            {
                using (SqlCommand command = new SqlCommand("SP_InsertQuote", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Configurar los parámetros explícitamente
                    command.Parameters.Add("@Date", SqlDbType.Date).Value = Date;
                    command.Parameters.Add("@ProjectName", SqlDbType.VarChar, 50).Value = ProjectName;
                    command.Parameters.Add("@Address", SqlDbType.VarChar, 100).Value = Address;
                    command.Parameters.Add("@Condition", SqlDbType.VarChar, 1000).Value = Condition;
                    command.Parameters.Add("@Discount", SqlDbType.Decimal, 5).Value = Discount;
                    command.Parameters.Add("@Labour", SqlDbType.Decimal, 10).Value = Labour;
                    command.Parameters.Add("@IVA", SqlDbType.Decimal, 10).Value = IVA;
                    command.Parameters.Add("@SubTotal", SqlDbType.Decimal, 10).Value = SubTotal;
                    command.Parameters.Add("@Total", SqlDbType.Decimal, 10).Value = Total;
                    command.Parameters.Add("@IdClient", SqlDbType.Int).Value = IdClient;

                    // Parámetro de salida
                    SqlParameter outputParameter = new SqlParameter
                    {
                        ParameterName = "@UltimoID",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Ejecuta el Stored Procedure
                    command.ExecuteNonQuery();

                    // Obtiene el último ID insertado desde el parámetro de salida
                    int ultimoIDInsertado = Convert.ToInt32(outputParameter.Value);

                    return ultimoIDInsertado;
                }
            }
        }

        public DataTable LoadWindows(int idQuote) 
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("Select IdWindows,URL,Description,Price from Windows where IdQuote = @IdQuote", Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdQuote", idQuote);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public DataTable WindowsData(int idQuote)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("Select * from Windows where IdQuote = @IdQuote", Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdQuote", idQuote);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public DataTable GetProductSizes(string productName)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Nombre = @ProductName", Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductName", productName + " 5020");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {
                return null;
            }
        }



        public bool UpdatePriceWindows(int IdWindows, decimal Price)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "Update Windows set Price = @Price where IdWindows = @Id";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", IdWindows);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;

            }
            catch { return false; }
        }

        public bool DeleteWindows(int IdWindows)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "Delete from Windows where IdWindows = @Id";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", IdWindows);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;

            }
            catch { return false; }
        }

        public bool FindWindows( int ID) 
        {
            try
            {
                //Hacer una consulta a la base de datos para obtener los datos de la ventana guardarlos en ClsWindows
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "Select * from Windows where IdWindows = @Id";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", ID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ClsWindows.System = reader["System"].ToString();
                    ClsWindows.Desing = reader["Design"].ToString();
                    ClsWindows.Lock = "Update";
                    Cnn.CloseConnection();
                    return true;
                }
                else
                {
                    Cnn.CloseConnection();
                    return false;
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateQuoteStatus(int IdQuote, string State)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "Update Quote set State = @State where IdQuote = @Id";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", IdQuote);
                cmd.Parameters.AddWithValue("@State", State);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;

            }
            catch { return false; }
        }


        public DataTable LoadQuoteById(int IdQuote) 
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "select IdWindows, URL, Description, Width, Height from Windows where IdQuote = @Id";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", IdQuote);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Cnn.CloseConnection();
                return dt;

            }
            catch (Exception)
            {

               return null;
            }
            
        }

        public DataTable LoadDataQuote(int IdQuote) 
        {
            try
            {
               //Obtener los datos de la proforma
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "Select * from Quote where IdQuote = @Id";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", IdQuote);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Cnn.CloseConnection();
                return dt;
            }
            catch (Exception ex)
            {
             
                return null;
            }
        }
       

    }
}
