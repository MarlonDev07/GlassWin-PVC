using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AccesoDatos.Products;
using Dominio.PriceProduct;
using Dominio.Product;

namespace Negocio.Products
{
    public class N_Products
    {
        #region Read
        public DataTable View() 
        {
            try
            {
                DataTable dt = new DataTable();
                CD_Products products = new CD_Products();
                dt = products.View();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public DataTable Find(string code, string System, string Category, string Color)
        {
            try
            {
                DataTable dt = new DataTable();
                CD_Products products = new CD_Products();
                StringBuilder Query = new StringBuilder("SELECT Product.*, Price.* FROM Product JOIN Price ON Product.idProduct = Price.idProduct WHERE");

                if (!string.IsNullOrEmpty(code))
                {
                    Query.Append($" Product.idProduct = '{code}'");
                }

                if (!string.IsNullOrEmpty(System))
                {
                    Query.Append($" Product.System = '{System}'");
                }

                if (!string.IsNullOrEmpty(Category))
                {
                    if (!string.IsNullOrEmpty(System))
                    {
                        Query.Append(" AND");
                    }
                    Query.Append($" Product.Category = '{Category}'");
                }

                if (!string.IsNullOrEmpty(Color) )
                {
                    if (!string.IsNullOrEmpty(System) || !string.IsNullOrEmpty(Category))
                    {
                        Query.Append(" AND");
                    }
                    Query.Append($" Price.Color = '{Color}'");
                }



                dt = products.Find(Query.ToString());
                return dt;
            }
            catch (Exception)
            {

                return null;
            }
        }
        #endregion

        #region Create
        public int LastCode() 
        {
            try
            {
                CD_Products Products = new CD_Products();
                int LastCode;
                LastCode = Products.LastCode();
                return LastCode;
            }
            catch (Exception)
            {

                return 0;
            }
            
        }

        public bool CreateProduct(string Id, string Description, string System, string Category) 
        {
            try
            {
                CD_Products Products = new CD_Products();
                return Products.CreateProduct(Convert.ToInt32(Id),Description, System, Category);
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
                CD_Products Products = new CD_Products();
                return Products.CreatePriceProduct(prices);
            }
            catch (Exception)
            {

                return false;
            }
           
        }
        #endregion

        #region Update
        public List<ProductClass> FindDataProductxID(string Code) 
        {
            try
            {
                CD_Products Products = new CD_Products();
                List<ProductClass> DataProduct;
                return DataProduct = Products.FindDataProductxID(Convert.ToInt32(Code));
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public List<PriceProductClass> FindColorxID(string Code)
        {
            try
             {
                CD_Products Products = new CD_Products();
                List<PriceProductClass> PriceProduct;
                return PriceProduct = Products.FindColorxID(Convert.ToInt32(Code));
            }
            catch (Exception)
            {

                return null;
            }
            


        }

        public bool UpdateProduct(string code, string Description, string System, string Category) 
        {
            try
            {
                CD_Products _Products = new CD_Products();
                return _Products.UpdateProduct(Convert.ToInt32(code), Description, System, Category);
            }
            catch (Exception)
            {

                return false;
            }
           
        }

        public bool UpdatePriceProduct(List<PriceProductClass> prices)
        {
            try
            {
                CD_Products Products = new CD_Products();
                return Products.UpdatePrice(prices);
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public DataTable ObtenerTamañoPieza(List<PriceProductClass> ListaPrecios) 
        {
            try
            {
                CD_Products Products = new CD_Products();
                DataTable dt = new DataTable();
                dt = Products.CargarTamañoPieza(ListaPrecios);
                return dt;
            }
            catch (Exception)
            {

                return null;
            }   
        }
        #endregion


    }
}
