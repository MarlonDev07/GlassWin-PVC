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

        public DataTable GetAllColors()
        {
            CD_Products data = new CD_Products();
            return data.GetAllColors();
        }


        public DataTable GetAllCategories()
        {
            CD_Products data = new CD_Products();
            return data.GetAllCategories();
        }

        public DataTable GetAllSystems()
        {
            CD_Products data = new CD_Products();
            return data.GetAllSystems();
        }



        public DataTable Find(string code, string system, string category, string color, string description)
        {
            try
            {
                DataTable dt = new DataTable();
                CD_Products products = new CD_Products();
                StringBuilder query = new StringBuilder("SELECT Product.*, Price.* FROM Product JOIN Price ON Product.idProduct = Price.idProduct WHERE 1=1");

                if (!string.IsNullOrEmpty(code))
                {
                    query.Append($" AND Product.idProduct = '{code}'");
                }

                if (!string.IsNullOrEmpty(system))
                {
                    query.Append($" AND Product.System = '{system}'");
                }

                if (!string.IsNullOrEmpty(category))
                {
                    query.Append($" AND Product.Category = '{category}'");
                }

                if (!string.IsNullOrEmpty(color))
                {
                    query.Append($" AND Price.Color = '{color}'");
                }

                if (!string.IsNullOrEmpty(description))
                {
                    query.Append($" AND Product.Description LIKE '%{description}%'");
                }

                dt = products.Find(query.ToString());
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

        public bool CreateProduct(string Id, string Description, string System, string Category, string user) 
        {
            try
            {
                CD_Products Products = new CD_Products();
                return Products.CreateProduct(Convert.ToInt32(Id),Description, System, Category, user);
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

        public bool UpdateProduct(string code, string Description, string System, string Category, string user) 
        {
            try
            {
                CD_Products _Products = new CD_Products();
                return _Products.UpdateProduct(Convert.ToInt32(code), Description, System, Category, user);
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
