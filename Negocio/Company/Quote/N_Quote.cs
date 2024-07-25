using AccesoDatos.Company.Quotes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Company.Quote
{
    public class N_Quote
    {
        AD_Quote ADQuote = new AD_Quote();
        //Cargar los datos de las ventanas:
        public DataTable GetProductDetailsByIdQuote(int idQuote)
        {
            return ADQuote.LoadProductDetailsByIdQuote(idQuote);
        }

        //Metodo para cargar los proyectos del usuario en sesion
        public DataTable GetProjectsByCompanyId()
        {
            return ADQuote.LoadProjectsByCompanyId();
        }

        public DataTable LoadQuotes()
        {
            try
            {
                return ADQuote.LoadQuotes();
            }
            catch { return null; }
        }

        public DataTable LoadQuotesFacturas()
        {
            try
            {
                return ADQuote.LoadQuotesFacturadas();
            }
            catch { return null; }
        }
        public int InsertQuoteAndGetLastID(DateTime Date, string ProjetName, string Address, string Condition, decimal Discount, decimal Labour, decimal IVA, decimal SubTotal, decimal Total, int IdClient)
        {
            try
            {
                return ADQuote.InsertQuoteAndGetLastID(Date, ProjetName, Address, Condition, Discount, Labour, IVA, SubTotal, Total, IdClient);
            }
            catch { return 0; }
        }

        public bool EditQuote(int idQuote, DateTime Date, string ProjetName, string Address, string Condition, decimal Discount, decimal Labour, decimal IVA, decimal SubTotal, decimal Total, int IdClient)
        {
            try
            {
                return ADQuote.EditQuote(idQuote, Date, ProjetName, Address, Condition, Discount, Labour, IVA, SubTotal, Total, IdClient);
            }
            catch { return false; }
        }
        
        public bool DeleteQuote(int idQuote)
        {
            try
            {
                return ADQuote.DeleteQuote(idQuote);
            }
            catch { return false; }
        }

        public DataTable LoadWindows(int idQuote) 
        {
            try
            {
                return ADQuote.LoadWindows(idQuote);
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
                return ADQuote.WindowsData(idQuote);
            }
            catch (Exception)
            {

                return null;
            }
        }
        //Cargador
        public DataTable GetProductSizes(string productName)
        {
            try
            {
                return ADQuote.GetProductSizes(productName);
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Umbral
        public DataTable GetProductSizesU(string productName)
        {
            try
            {
                return ADQuote.GetProductSizesUmbral(productName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Jamba
        public DataTable GetProductSizesJ(string productName)
        {
            try
            {
                return ADQuote.GetProductSizesJamba(productName);
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Superior
        public DataTable GetProductSizesS(string productName)
        {
            try
            {
                return ADQuote.GetProductSizesSuperior(productName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Decimal CalcTotal(DataTable dtWindows)
        {
            try
            {
               Decimal total = 0;
                foreach (DataRow row in dtWindows.Rows)
                {
                    total += Convert.ToDecimal(row["Total"]);
                }
                return total;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public bool UpdatePriceWindows(int IdWindows, decimal Price)
        {
            try
            {
                return ADQuote.UpdatePriceWindows(Convert.ToInt32(IdWindows), Price);
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteWindows(int IdWindows)
        {
            try
            {
                return ADQuote.DeleteWindows(Convert.ToInt32(IdWindows));
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool FindWindows(int IdWindows)
        {
            try
            {
                return ADQuote.FindWindows(Convert.ToInt32(IdWindows));
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateQuoteStatus(int IdQuote, string Status)
        {
            try
            {
                return ADQuote.UpdateQuoteStatus(Convert.ToInt32(IdQuote), Status);
            }
            catch (Exception)
            {

                return false;
            }
        }

        public DataTable LoadQuoteById(int IdQuote)
        {
            try
            {
                return ADQuote.LoadQuoteById(Convert.ToInt32(IdQuote));
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
                return ADQuote.LoadDataQuote(Convert.ToInt32(IdQuote));
            }
            catch (Exception)
            {

                return null;
            }
        }
       
    }
}
