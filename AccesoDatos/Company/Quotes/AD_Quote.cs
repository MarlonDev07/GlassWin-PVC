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
                string query = "select Q.IdClient, Q.IdQuote, Q.Date, C.Name, Q.ProjectName, Q.Address, Q.SubTotal, Q.IVA, Q.Total from Quote Q Inner Join Client C ON Q.IdClient = C.IdClient where C.IdCompany = @IdCompany and State = 'Pending' order by Q.IdQuote desc";
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
        public bool DeleteBill(int idQuote)
        {
            try
            {
                using (SqlConnection connection = Cnn.OpenConecction())
                {
                    // Abrir la conexión
                    // connection.Open();

                    // Verificar si el saldo pendiente es 0 en AccountReceivable
                    if (!IsOutstandingBalanceZero(idQuote, connection))
                    {
                        // Saldo pendiente no es 0, no se puede eliminar
                        return false;
                    }

                    // Crear una transacción para asegurar que todas las operaciones se completen correctamente
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Eliminar la relación en AdmProyecto
                            DeleteAdmProyecto(idQuote, connection, transaction);

                            // Eliminar la relación en AccountReceivable y PaymentsStatistics
                            DeleteAccountReceivableAndPaymentsStatistics(idQuote, connection, transaction);

                            // Eliminar las relaciones en la tabla Bill
                            string deleteBillQuery = "DELETE FROM Bill WHERE IdQuote = @IdQuote";
                            using (SqlCommand deleteBillCommand = new SqlCommand(deleteBillQuery, connection, transaction))
                            {
                                deleteBillCommand.Parameters.AddWithValue("@IdQuote", idQuote);
                                deleteBillCommand.ExecuteNonQuery();
                            }

                            // Eliminar las relaciones en la tabla Windows
                            string deleteWindowsQuery = "DELETE FROM Windows WHERE IdQuote = @IdQuote";
                            using (SqlCommand deleteWindowsCommand = new SqlCommand(deleteWindowsQuery, connection, transaction))
                            {
                                deleteWindowsCommand.Parameters.AddWithValue("@IdQuote", idQuote);
                                deleteWindowsCommand.ExecuteNonQuery();
                            }

                            // Eliminar el registro en la tabla Quote
                            string deleteQuoteQuery = "DELETE FROM Quote WHERE IdQuote = @IdQuote";
                            using (SqlCommand deleteQuoteCommand = new SqlCommand(deleteQuoteQuery, connection, transaction))
                            {
                                deleteQuoteCommand.Parameters.AddWithValue("@IdQuote", idQuote);
                                deleteQuoteCommand.ExecuteNonQuery();
                            }

                            // Confirmar la transacción si todo salió bien
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            // Revertir la transacción en caso de error
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Manejar errores de conexión u otros errores
                return false;
            }
        }

        private bool IsOutstandingBalanceZero(int idQuote, SqlConnection connection)
        {
            string query = @"
        SELECT COUNT(*) 
        FROM AccountReceivable AR
        INNER JOIN Bill B ON AR.IdBill = B.IdBill
        WHERE B.IdQuote = @IdQuote AND AR.OutstandingBalance <> 0";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdQuote", idQuote);
                int count = (int)command.ExecuteScalar();
                // Si el conteo es mayor a 0, significa que hay al menos una factura con saldo pendiente
                return count == 0;
            }
        }

        private void DeleteAdmProyecto(int idQuote, SqlConnection connection, SqlTransaction transaction)
        {
            // Obtener los IdAccount asociados con la IdQuote
            string getAccountIdsQuery = @"
        SELECT DISTINCT AR.IdAccount 
        FROM AccountReceivable AR
        INNER JOIN Bill B ON AR.IdBill = B.IdBill
        WHERE B.IdQuote = @IdQuote";

            List<int> accountIds = new List<int>();

            using (SqlCommand getAccountIdsCommand = new SqlCommand(getAccountIdsQuery, connection, transaction))
            {
                getAccountIdsCommand.Parameters.AddWithValue("@IdQuote", idQuote);
                using (SqlDataReader reader = getAccountIdsCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accountIds.Add(reader.GetInt32(0));
                    }
                }
            }

            // Eliminar los registros en AdmProyecto
            if (accountIds.Count > 0)
            {
                string deleteAdmProyectoQuery = "DELETE FROM AdmProyecto WHERE IdCxC IN (" + string.Join(",", accountIds) + ")";
                using (SqlCommand deleteAdmProyectoCommand = new SqlCommand(deleteAdmProyectoQuery, connection, transaction))
                {
                    deleteAdmProyectoCommand.ExecuteNonQuery();
                }
            }
        }

        private void DeleteAccountReceivableAndPaymentsStatistics(int idQuote, SqlConnection connection, SqlTransaction transaction)
        {
            // Obtener los IdBill asociados con la IdQuote
            string getBillIdsQuery = "SELECT IdBill FROM Bill WHERE IdQuote = @IdQuote";
            List<int> billIds = new List<int>();

            using (SqlCommand getBillIdsCommand = new SqlCommand(getBillIdsQuery, connection, transaction))
            {
                getBillIdsCommand.Parameters.AddWithValue("@IdQuote", idQuote);
                using (SqlDataReader reader = getBillIdsCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        billIds.Add(reader.GetInt32(0));
                    }
                }
            }

            // Eliminar los registros en AccountReceivable y PaymentsStatistics
            if (billIds.Count > 0)
            {
                // Eliminar los registros en PaymentsStatistics
                string deletePaymentsStatisticsQuery = "DELETE FROM PaymentsStatistics WHERE IdAccount IN (SELECT IdAccount FROM AccountReceivable WHERE IdBill IN (" + string.Join(",", billIds) + "))";
                using (SqlCommand deletePaymentsStatisticsCommand = new SqlCommand(deletePaymentsStatisticsQuery, connection, transaction))
                {
                    deletePaymentsStatisticsCommand.ExecuteNonQuery();
                }

                // Eliminar los registros en AccountReceivable
                string deleteAccountReceivableQuery = "DELETE FROM AccountReceivable WHERE IdBill IN (" + string.Join(",", billIds) + ")";
                using (SqlCommand deleteAccountReceivableCommand = new SqlCommand(deleteAccountReceivableQuery, connection, transaction))
                {
                    deleteAccountReceivableCommand.ExecuteNonQuery();
                }
            }
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

        //5020
        public DataTable GetProductSizes(string productName)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = @ProductName and Tamaño = 6.40", Cnn.OpenConecction());
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
        //8025
        public DataTable GetProductSizes8025()
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = 'Cargador 8025' and Tamaño = 6.40", Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
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
        //5020
        public DataTable GetProductSizesUmbral(string productName)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = @ProductName and Tamaño = 6.40", Cnn.OpenConecction());
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
        //8025
        public DataTable GetProductSizesUmbral8025()
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = 'Umbral 8025' and Tamaño = 6.40", Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
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
        //5020
        public DataTable GetProductSizesJamba(string productName)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = @ProductName and Tamaño = 6.40", Cnn.OpenConecction());
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
        //8025
        public DataTable GetProductSizesJamba8025()
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = 'Jamba 8025' and Tamaño = 4.60", Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
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

        //5020
        public DataTable GetProductSizesSuperior(string productName)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = @ProductName and Tamaño = 6.40", Cnn.OpenConecction());
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
        //8025
        public DataTable GetProductSizesSuperior8025()
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = 'Superior 8025' and Tamaño = 6.40", Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
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
        //5020
        public DataTable GetProductSizesInferior(string productName)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = @ProductName and Tamaño = 6.40", Cnn.OpenConecction());
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
        //8025
        public DataTable GetProductSizesInferior8025()
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = 'Inferior 8025' and Tamaño = 6.40", Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
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
        //5020
        public DataTable GetProductSizesVertical(string productName)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = @ProductName and Tamaño = 6.40", Cnn.OpenConecction());
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
        //8025
        public DataTable GetProductSizesVertical8025()
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = 'Vertical 8025' and Tamaño = 4.60", Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
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
        //5020
        public DataTable GetProductSizesVerticalC(string productName)
        {
            try
            {
                DataTable dataTable = new DataTable();
                string query;

                // Verificar si el nombre del producto es "VerticalCentro"
                if (productName == "VerticalCentro")
                {
                    // Usar "Vertical Centro" en la consulta
                    query = "SELECT DISTINCT Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = 'Vertical Centro 5020' AND Tamaño = 6.40";
                }
                else
                {
                    // Usar el nombre del producto tal como está
                    query = "SELECT DISTINCT Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = @ProductName AND Tamaño = 6.40";
                }

                using (SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction()))
                {
                    cmd.CommandType = CommandType.Text;
                    if (productName != "VerticalCentro")
                    {
                        cmd.Parameters.AddWithValue("@ProductName", productName + " 5020");
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dataTable);
                    }
                }

                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception ex)
            {
                // Manejar el error según sea necesario
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        //8025
        public DataTable GetProductSizesVerticalC8025()
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = 'Vertical Centro 8025' and Tamaño = 4.60", Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
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
        //8025
        public DataTable GetProductSizesPisaAlfombra8025()
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT  Tamaño FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE pr.Description = 'Pisalfombra 8025' and Tamaño = 6.40", Cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
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

        

        public bool UpdateQuoteTotal(int idQuote, decimal Total)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "UPDATE Quote SET Total = @Total WHERE idQuote = @idQuote;";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@idQuote", idQuote);
                cmd.Parameters.AddWithValue("@Total", Total);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;

            }
            catch { return false; }
        }



        public int InsertTotalDesglose(int IdQuote, decimal TotalPV, decimal MontoFacturacion, decimal MontoInstalacion, decimal Total)
        {
            using (SqlConnection connection = Cnn.OpenConecction()) // Supongo que 'Cnn.OpenConecction()' es tu método para abrir una conexión.
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO dbo.TotalDesglose (IdQuote, TotalPV, MontoFacturacion, MontoInstalacion, Total) VALUES (@IdQuote, @TotalPV, @MontoFacturacion, @MontoInstalacion, @Total); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    // Configurar los parámetros explícitamente
                    command.Parameters.Add("@IdQuote", SqlDbType.Int).Value = IdQuote;
                    command.Parameters.Add("@TotalPV", SqlDbType.Decimal).Value = TotalPV;
                    command.Parameters.Add("@MontoFacturacion", SqlDbType.Decimal).Value = MontoFacturacion;
                    command.Parameters.Add("@MontoInstalacion", SqlDbType.Decimal).Value = MontoInstalacion;
                    command.Parameters.Add("@Total", SqlDbType.Decimal).Value = Total;

                    // Ejecuta el comando y obtiene el último ID insertado
                    int ultimoIDInsertado = Convert.ToInt32(command.ExecuteScalar());

                    return ultimoIDInsertado;
                }
            }
        }

        // Acceso a datos
        public bool ExisteIdQuote(int idQuote)
        {
            using (SqlConnection connection = Cnn.OpenConecction()) // Asumiendo que tienes un método para abrir la conexión
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(1) FROM dbo.TotalDesglose WHERE IdQuote = @IdQuote", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@IdQuote", SqlDbType.Int).Value = idQuote;

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // Si el conteo es mayor a 0, significa que existe
                }
            }
        }


        public bool EliminarRegistro(int IdTotal)
        {
            using (SqlConnection connection = Cnn.OpenConecction())
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM dbo.TotalDesglose WHERE IdTotal = @IdTotal", connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add("@IdTotal", SqlDbType.Int).Value = IdTotal;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public DataTable GetTotalDesgloseByQuoteId(int idQuote)
        {
            DataTable dtTotalDesglose = new DataTable();

            using (SqlConnection connection = Cnn.OpenConecction()) // Abre la conexión a la base de datos.
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.TotalDesglose WHERE IdQuote = @IdQuote", connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    // Configurar el parámetro
                    command.Parameters.Add("@IdQuote", SqlDbType.Int).Value = idQuote;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // Llenar el DataTable con los datos obtenidos
                        adapter.Fill(dtTotalDesglose);
                    }
                }
            }

            return dtTotalDesglose;
        }



    }
}
