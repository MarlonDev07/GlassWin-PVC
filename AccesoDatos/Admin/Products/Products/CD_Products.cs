using Dominio.PriceProduct;
using Dominio.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace AccesoDatos.Products
{
    public class CD_Products
    {
        DataBase.ClsConnection Cnn = new DataBase.ClsConnection();



        #region Create
        public bool CreateProduct(int IdProduct, string Description, string System, string Category)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_InsertProduct";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProduct", IdProduct);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@System", System);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool CreatePriceProduct(List<PriceProductClass> prices)
        {
            try
            {
                if (prices != null)
                {
                    foreach (var product in prices)
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_InsertPriceProduct", Cnn.OpenConecction()))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@idProduct", product.IdProduct);
                            cmd.Parameters.AddWithValue("@Color", product.Color);
                            cmd.Parameters.AddWithValue("@BasePrice", product.BasePrice);
                            cmd.Parameters.AddWithValue("@Discount", product.Discount);
                            cmd.Parameters.AddWithValue("@Cost", product.Cost);
                            cmd.Parameters.AddWithValue("@SalePrice1", product.SalePrice1);
                            cmd.Parameters.AddWithValue("@SalePrice2", product.SalePrice2);
                            cmd.Parameters.AddWithValue("@Supplier", product.Supplier);
                            cmd.Parameters.AddWithValue("@Tamaño", product.Tamaño);
                            cmd.ExecuteNonQuery();
                        }

                    }

                    Cnn.CloseConnection();
                    return true;


                }
                else { return false; }

            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion

        #region Read
        public DataTable GetAllColors()
        {
            try
            {
                DataTable table = new DataTable();
                string query = "SELECT DISTINCT Price.Color FROM Product INNER JOIN Price ON Product.idProduct = Price.idProduct";
                using (SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction()))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    table.Load(reader);
                    Cnn.CloseConnection();
                }
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public DataTable GetAllCategories()
        {
            try
            {
                DataTable table = new DataTable();
                string query = "SELECT DISTINCT Category FROM Product";
                using (SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction()))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    table.Load(reader);
                    Cnn.CloseConnection();
                }
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public DataTable GetAllSystems()
        {
            try
            {
                DataTable table = new DataTable();
                string query = "SELECT DISTINCT System FROM Product";
                using (SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction()))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    table.Load(reader);
                    Cnn.CloseConnection();
                }
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public DataTable Find(string query)
        {
            try
            {
                DataTable table = new DataTable();
                SqlDataReader read;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = query;
                read = cmd.ExecuteReader();
                table.Load(read);
                Cnn.CloseConnection();
                return table;
            }
            catch (Exception)
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
                cmd.CommandText = "SELECT Product.*, Price.* FROM Product JOIN Price ON Product.idProduct = Price.idProduct;";
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

        #region Update
        public List<ProductClass> FindDataProductxID(int Code)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                List<ProductClass> ProductList = new List<ProductClass>();
                ProductClass product = new ProductClass();
                string query = "SELECT Description, System, Category FROM Product where idProduct = @ID ";
                using (SqlCommand command = new SqlCommand(query, Cnn.OpenConecction()))
                {
                    command.Parameters.AddWithValue("@ID", Code.ToString());

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            product.Description = reader.GetString(reader.GetOrdinal("Description"));
                            product.System = reader.GetString(reader.GetOrdinal("System"));
                            product.Category = reader.GetString(reader.GetOrdinal("Category"));
                            ProductList.Add(product);
                        }

                    }
                }
                return ProductList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<PriceProductClass> FindColorxID(int Code)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                List<PriceProductClass> PriceList = new List<PriceProductClass>();

                string Query = "SELECT idPrice, Color, BasePrice, Discount, Cost, SalePrice, SalePrice2, Supplier, Tamaño FROM Price where idProduct = @ID ";
                using (SqlCommand command = new SqlCommand(Query, Cnn.OpenConecction()))
                {
                    command.Parameters.AddWithValue("@ID", Code.ToString());

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PriceProductClass price = new PriceProductClass();

                            price.IdPriceProduct = reader.GetInt32(reader.GetOrdinal("idPrice"));
                            price.Color = reader.GetString(reader.GetOrdinal("Color"));
                            price.BasePrice = reader.GetDecimal(reader.GetOrdinal("BasePrice"));
                            price.Discount = reader.GetDecimal(reader.GetOrdinal("Discount"));
                            price.Cost = reader.GetDecimal(reader.GetOrdinal("Cost"));
                            price.SalePrice1 = reader.GetDecimal(reader.GetOrdinal("SalePrice"));
                            price.SalePrice2 = reader.GetDecimal(reader.GetOrdinal("SalePrice2"));
                            price.Supplier = reader.GetString(reader.GetOrdinal("Supplier"));
                            price.Tamaño = reader.GetDecimal(reader.GetOrdinal("Tamaño"));
                            PriceList.Add(price);
                        }

                    }
                }

                return PriceList;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public bool UpdateProduct(int code, string Description, string System, string Category)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_UpdateProduct";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProduct", code);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@System", System);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdatePrice(List<PriceProductClass> prices)
        {
            try
            {
                if (prices != null)
                {
                    foreach (var product in prices)
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_UpdatePriceProduct", Cnn.OpenConecction()))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@idPriceProduct", product.IdPriceProduct);
                            cmd.Parameters.AddWithValue("@Color", product.Color);
                            cmd.Parameters.AddWithValue("@BasePrice", product.BasePrice);
                            cmd.Parameters.AddWithValue("@Discount", product.Discount);
                            cmd.Parameters.AddWithValue("@Cost", product.Cost);
                            cmd.Parameters.AddWithValue("@SalePrice1", product.SalePrice1);
                            cmd.Parameters.AddWithValue("@SalePrice2", product.SalePrice2);
                            cmd.Parameters.AddWithValue("@Supplier", product.Supplier);
                            cmd.Parameters.AddWithValue("@Tamaño", product.Tamaño);
                            cmd.ExecuteNonQuery();
                        }

                    }

                    Cnn.CloseConnection();
                    return true;


                }
                else { return false; }

            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion

        #region SoportFunction

        public int LastCode()
        {
            try
            {
                int LastCode = 0;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SELECT MAX(idProduct) FROM Product";
                object Read = cmd.ExecuteScalar();

                if (Read != null)
                {
                    LastCode = Convert.ToInt32(Read) + 1;
                }

                return LastCode;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        #endregion

        public DataTable CargarTamañoPieza(List<PriceProductClass> List)
        {
            try
            {
                DataTable Table = new DataTable();
                using (SqlConnection connection = Cnn.OpenConecction())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;

                    // Crear una consulta dinámica para la lista de nombres
                    StringBuilder queryBuilder = new StringBuilder();
                    queryBuilder.Append("SELECT pr.Description ,p.*, pr.Category FROM Price p JOIN Product pr ON p.idProduct = pr.idProduct WHERE (");

                    for (int i = 0; i < List.Count; i++)
                    {
                        if (i > 0)
                        {
                            queryBuilder.Append(" OR ");
                        }
                        queryBuilder.Append("(pr.Description = @Nombre" + i + " AND p.Color = @Color" + i + " AND p.Supplier = @Proveedor" + i + ")");

                        // Añadir parámetros para cada artículo en la lista
                        cmd.Parameters.AddWithValue("@Nombre" + i, List[i].Nombre);
                        cmd.Parameters.AddWithValue("@Color" + i, List[i].Color);
                        cmd.Parameters.AddWithValue("@Proveedor" + i, List[i].Supplier);
                    }

                    queryBuilder.Append(")");
                    cmd.CommandText = queryBuilder.ToString();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Table.Load(reader);
                    }
                }

                return Table;
            }
            catch (Exception ex)
            {
                // Manejo del error - puedes registrar el error o realizar alguna acción necesaria
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
