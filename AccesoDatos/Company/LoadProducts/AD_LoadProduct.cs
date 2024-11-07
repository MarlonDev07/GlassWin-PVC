using AccesoDatos.DataBase;
using Dominio.Model.ClassComboArticulos;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.Company.LoadProducts
{
	public class AD_LoadProduct
	{
		public DataTable loadAluminio(string Color, string System, string supplier)
		{
			try
			{
				DataTable dt = new DataTable();
				ClsConnection con = new ClsConnection();
				string sql = "";

                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "select P.Description,PP.SalePrice from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Color ='" + Color + "' AND PP.Supplier = '" + supplier + "' AND P.Category = 'Aluminio' and p.System = '" + System + "'";
                }
                else
                {
                    sql = "select P.Description,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Color ='" + Color + "' AND PP.Supplier = '" + supplier + "' AND P.Category = 'Aluminio' and p.System = '" + System + "'";
                }
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



  



        public DataTable loadAluminioDesglose(string Color, string System, string supplier)
        {
            try
            {
                DataTable dt = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "";

                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "select P.Description,PP.SalePrice,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Color ='" + Color + "' AND PP.Supplier = '" + supplier + "' AND P.Category = 'Aluminio' and p.System = '" + System + "'";
                }
                else
                {
                    sql = "select P.Description,PP.Cost,PP.SalePrice from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Color ='" + Color + "' AND PP.Supplier = '" + supplier + "' AND P.Category = 'Aluminio' and p.System = '" + System + "'";
                }
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

        public DataTable LoadPrefabricadoDesglose(int IdWindows) 
        {
            try
            {
                DataTable dt = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "";

                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "SELECT P.Description,  PP.SalePrice,  PP.Cost, AC.Metraje, AC.Cantidad  FROM   ArticulosCombo AC  INNER JOIN  Price PP ON AC.IdPrice = PP.IdPrice INNER JOIN  Product P ON PP.IdProduct = P.IdProduct WHERE  AC.IdWindows = '"+IdWindows+"'";
                }
                else
                {
                    sql = "SELECT P.Description,  PP.SalePrice,  PP.Cost FROM   ArticulosCombo AC  INNER JOIN  Price PP ON AC.IdPrice = PP.IdPrice INNER JOIN  Product P ON PP.IdProduct = P.IdProduct WHERE  AC.IdWindows = '" + IdWindows + "'";
                }
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



        public DataTable loadAluminioDesglose()
        {
            try
            {
                DataTable dt = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "";

               
               sql = "select P.Description from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE P.Category = 'Aluminio'";


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

        public DataTable loadAccesorioDesglose()
        {
            try
            {
                DataTable dt = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "";


                sql = "select P.Description from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE P.Category = 'Accesorios'";


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


        public DataTable loadVidrioDesglose()
        {
            try
            {
                DataTable dt = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "";


                sql = "select P.Description from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE P.Category = 'Vidrio'";


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



        //LoadAluminio para la utilidad
        public DataTable loadAluminioUtilidad(string Color, string System, string supplier)
        {
            try
            {
                DataTable dt = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "";

                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "select P.Description,PP.SalePrice,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Color ='" + Color + "' AND PP.Supplier = '" + supplier + "' AND P.Category = 'Aluminio' and p.System = '" + System + "'";
                }
                else
                {
                    sql = "select P.Description,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Color ='" + Color + "' AND PP.Supplier = '" + supplier + "' AND P.Category = 'Aluminio' and p.System = '" + System + "'";
                }
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
        public DataTable loadAccesorios(string System, string supplier)
		{
			try
			{
				DataTable dt = new DataTable();
				ClsConnection con = new ClsConnection();
				string sql = "";
                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "select P.Description,PP.SalePrice from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Color = 'Negro' AND PP.Supplier = '" + supplier + "' AND P.Category = 'Accesorios' and p.System = '" + System + "'";
                }
                else
                {
                    sql = "select P.Description,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Color = 'Negro' AND PP.Supplier = '" + supplier + "' AND P.Category = 'Accesorios' and p.System = '" + System + "'";
                }

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






        public DataTable loadAccesoriosDesglose(string System, string supplier)
        {
            try
            {
                DataTable dt = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "";
                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "select P.Description,PP.SalePrice,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Color = 'Negro' AND PP.Supplier = '" + supplier + "' AND P.Category = 'Accesorios' and p.System = '" + System + "'";
                }
                else
                {
                    sql = "select P.Description,PP.Cost,PP.SalePrice from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Color = 'Negro' AND PP.Supplier = '" + supplier + "' AND P.Category = 'Accesorios' and p.System = '" + System + "'";
                }

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






        //Load Accesorios Utilidad
        public DataTable loadAccesoriosUtilidad(string System, string supplier)
        {
            try
            {
                DataTable dt = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "";
                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "select P.Description,PP.SalePrice,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Color = 'Negro' AND PP.Supplier = '" + supplier + "' AND P.Category = 'Accesorios' and p.System = '" + System + "'";
                }
                else
                {
                    sql = "select P.Description,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Color = 'Negro' AND PP.Supplier = '" + supplier + "' AND P.Category = 'Accesorios' and p.System = '" + System + "'";
                }

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
        public DataTable loadOnlyGlass()
		{
			try
			{
                DataTable dtGlass = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "select Description from Product where Category = 'Vidrio'";
                SqlDataAdapter da = new SqlDataAdapter(sql, con.OpenConecction());
                da.Fill(dtGlass);
                con.CloseConnection();
                return dtGlass;
            }
			catch (Exception)
			{
				return null;
			}
			
        }
		public DataTable loadPricesGlass(string supplier, string Description) 
		{
			try
			{
                DataTable dataTable = new DataTable();
                ClsConnection con = new ClsConnection();
				string sql = "";
                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "select P.Description,PP.SalePrice from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Supplier = '" + supplier + "' AND P.Description = '" + Description + "'";
                }
                else
                {
                    sql = "select P.Description,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Supplier = '" + supplier + "' AND P.Description = '" + Description + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(sql, con.OpenConecction());
                da.Fill(dataTable);
                con.CloseConnection();
                return dataTable;
            }
			catch (Exception)
			{

				return null;
			}
			
        }


        public DataTable loadPricesGlassDesglose(string supplier, string Description)
        {
            try
            {
                DataTable dataTable = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "";
                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "select P.Description,PP.SalePrice,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Supplier = '" + supplier + "' AND P.Description = '" + Description + "'";
                }
                else
                {
                    sql = "select P.Description,PP.Cost,PP.SalePrice from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Supplier = '" + supplier + "' AND P.Description = '" + Description + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(sql, con.OpenConecction());
                da.Fill(dataTable);
                con.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {

                return null;
            }

        }


        //Load PriceGlass para la Utilidad
        public DataTable loadPricesGlassUtilidad(string supplier, string Description)
        {
            try
            {
                DataTable dataTable = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "";
                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "select P.Description,PP.SalePrice,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Supplier = '" + supplier + "' AND P.Description = '" + Description + "'";
                }
                else
                {
                    sql = "select P.Description,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Supplier = '" + supplier + "' AND P.Description = '" + Description + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(sql, con.OpenConecction());
                da.Fill(dataTable);
                con.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {

                return null;
            }

        }


        public DataTable LoadPricesLock(string supplier, string Description) 
		{
            try
            {
                DataTable dataTable = new DataTable();
                ClsConnection con = new ClsConnection();

				string sql = "";
                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "select P.Description,PP.SalePrice from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Supplier = '" + supplier + "' AND P.Description = '" + Description + "'";
                }
                else
                {
                    sql = "select P.Description,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Supplier = '" + supplier + "' AND P.Description = '" + Description + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(sql, con.OpenConecction());
                da.Fill(dataTable);
                con.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {

                return null;
            }
        }



        public DataTable LoadPricesLockDesglose(string supplier, string Description)
        {
            try
            {
                DataTable dataTable = new DataTable();
                ClsConnection con = new ClsConnection();

                string sql = "";
                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "select P.Description,PP.SalePrice,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Supplier = '" + supplier + "' AND P.Description = '" + Description + "'";
                }
                else
                {
                    sql = "select P.Description,PP.Cost,PP.SalePrice from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Supplier = '" + supplier + "' AND P.Description = '" + Description + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(sql, con.OpenConecction());
                da.Fill(dataTable);
                con.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {

                return null;
            }
        }


        //Load PriceLock Utilidad
        public DataTable LoadPricesLockUtilidad(string supplier, string Description)
        {
            try
            {
                DataTable dataTable = new DataTable();
                ClsConnection con = new ClsConnection();

                string sql = "";
                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "select P.Description,PP.SalePrice,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Supplier = '" + supplier + "' AND P.Description = '" + Description + "'";
                }
                else
                {
                    sql = "select P.Description,PP.Cost from Product P  INNER JOIN Price PP ON P.idProduct = PP.idProduct WHERE PP.Supplier = '" + supplier + "' AND P.Description = '" + Description + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(sql, con.OpenConecction());
                da.Fill(dataTable);
                con.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {

                return null;
            }
        }


        public decimal loadSettingPrice(string supplier, string Description)
		{
            try
			{
                DataTable dataTable = new DataTable();
                ClsConnection con = new ClsConnection();
                string sql = "select Percentage from SettingPrice WHERE Supplier = '" + supplier + "' AND Name = '" + Description + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, con.OpenConecction());
                da.Fill(dataTable);
                con.CloseConnection();
                return Convert.ToDecimal(dataTable.Rows[0]["Percentage"].ToString());
            }
            catch (Exception)
			{

                return 0;
            }

        }

        public DataTable CargarProductoxID(int Id) 
        {
            try
            {
                DataTable dataTable = new DataTable();
                ClsConnection con = new ClsConnection();

                string sql = "";
                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "Select PP.IdPrice, P.Description, PP.Color, PP.SalePrice from Product P inner join Price PP on P.idProduct = PP.idProduct where PP.IdPrice = @id";
                }
                else
                {
                    sql = "Select PP.IdPrice, P.Description, PP.Color, PP.Cost from Product P inner join Price PP on P.idProduct = PP.idProduct where PP.IdPrice = 2180";
                }
                SqlDataAdapter da = new SqlDataAdapter(sql, con.OpenConecction());
                da.SelectCommand.Parameters.AddWithValue("@id", Id);
                da.Fill(dataTable);
                con.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public DataTable ListaArticulosxColor2(string descripcion = "")
        {
            try
            {
                DataTable dataTable = new DataTable();
                ClsConnection con = new ClsConnection();

                string sql;
                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "Select PP.IdPrice, P.Description, PP.Color, PP.SalePrice from Product P inner join Price PP on P.idProduct = PP.idProduct";
                }
                else
                {
                    sql = "Select PP.IdPrice, P.Description, PP.Color, PP.Cost from Product P inner join Price PP on P.idProduct = PP.idProduct";
                }

                if (!string.IsNullOrEmpty(descripcion))
                {
                    sql += " WHERE P.Description LIKE @Descripcion";
                }

                SqlDataAdapter da = new SqlDataAdapter(sql, con.OpenConecction());
                da.SelectCommand.Parameters.AddWithValue("@Descripcion", "%" + descripcion + "%");
                da.Fill(dataTable);
                con.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public DataTable ListaArticulosxColor() 
		{
            try
            {
                DataTable dataTable = new DataTable();
                ClsConnection con = new ClsConnection();

                string sql = "";
                if (UserCache.Name != "InnovaGlass")
                {
                    sql = "Select PP.IdPrice, P.Description, PP.Color, PP.SalePrice from Product P inner join Price PP on P.idProduct = PP.idProduct";
                }
                else
                {
                    sql = "Select PP.IdPrice, P.Description, PP.Color, PP.Cost from Product P inner join Price PP on P.idProduct = PP.idProduct";
                }
                SqlDataAdapter da = new SqlDataAdapter(sql, con.OpenConecction());
                da.Fill(dataTable);
                con.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {

                return null;
            }
        }
        #region InserWindows
        public bool insertWindows(string Description, string URL, decimal Width, decimal Height, string Glass, string Color, string TypeLock, decimal Price, int IdQuote, string System, string Design, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
               
                // Crear la conexión y el comando
                ClsConnection con = new ClsConnection();
                SqlCommand cmd = new SqlCommand("sp_InsertarWindows", con.OpenConecction());
                cmd.CommandType = CommandType.StoredProcedure;

                // Agregar los parámetros de entrada
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@URL", URL);
                cmd.Parameters.AddWithValue("@Width", Width);
                cmd.Parameters.AddWithValue("@Height", Height);
                cmd.Parameters.AddWithValue("@Glass", Glass);
                cmd.Parameters.AddWithValue("@Color", Color);
                cmd.Parameters.AddWithValue("@TypeLock", TypeLock);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@IdQuote", IdQuote);
                cmd.Parameters.AddWithValue("@System", System);
                cmd.Parameters.AddWithValue("@Design", Design);

                // Agregar el parámetro de salida para obtener el ID insertado
                SqlParameter outputIdParam = new SqlParameter("@NewWindowID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);

                // Ejecutar el comando
                cmd.ExecuteNonQuery();

                // Cerrar la conexión
                con.CloseConnection();

                // Devolver el ID insertado
                cls_ComboArticulos.IdWindows = (int)outputIdParam.Value;

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }


        public bool EditWindows(int IdWindows, string Description, string URL, decimal Width, decimal Height, string Glass, string Color, string TypeLock, decimal Price, int IdQuote, string System, string Desing) 
		{
            try
            {
                //Insertar Windows mediante un sp llamado sp_InsertarWindows
                ClsConnection con = new ClsConnection();
                SqlCommand cmd = new SqlCommand("UpdateWindow", con.OpenConecction());
                cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@WindowId", IdWindows);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@URL", URL);
                cmd.Parameters.AddWithValue("@Width", Width);
                cmd.Parameters.AddWithValue("@Height", Height);
                cmd.Parameters.AddWithValue("@Glass", Glass);
                cmd.Parameters.AddWithValue("@Color", Color);
                cmd.Parameters.AddWithValue("@TypeLock", TypeLock);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@IdQuote", IdQuote);
                cmd.Parameters.AddWithValue("@System", System);
                cmd.Parameters.AddWithValue("@Design", Desing);

                cmd.ExecuteNonQuery();
                con.CloseConnection();
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
