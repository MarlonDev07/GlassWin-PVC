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

        public bool DeleteBill(int idQuote)
        {
            try
            {
                return ADQuote.DeleteBill(idQuote);
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
        //Cargador 5020
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
        //Cargador 8025
        public DataTable GetProductSizes8025()
        {
            try
            {
                return ADQuote.GetProductSizes8025();
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Umbral 5020
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
        //Umbral 8025
        public DataTable GetProductSizesU8025( )
        {
            try
            {
                return ADQuote.GetProductSizesUmbral8025();
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Jamba 5020
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
        //Jamba 8025
        public DataTable GetProductSizesJ8025()
        {
            try
            {
                return ADQuote.GetProductSizesJamba8025();
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Superior 5020
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
        //Superior 8025
        public DataTable GetProductSizesS8025()
        {
            try
            {
                return ADQuote.GetProductSizesSuperior8025();
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Inferior 5020
        public DataTable GetProductSizesI(string productName)
        {
            try
            {
                return ADQuote.GetProductSizesInferior(productName);
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Inferior 8025
        public DataTable GetProductSizesI8025()
        {
            try
            {
                return ADQuote.GetProductSizesInferior8025();
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Vertical 5020
        public DataTable GetProductSizesV(string productName)
        {
            try
            {
                return ADQuote.GetProductSizesVertical(productName);
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Vertical 8025
        public DataTable GetProductSizesV8025()
        {
            try
            {
                return ADQuote.GetProductSizesVertical8025();
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Vertical Centro 5020
        public DataTable GetProductSizesVC(string productName)
        {
            try
            {
                return ADQuote.GetProductSizesVerticalC(productName);
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Vertical Centro 8025
        public DataTable GetProductSizesVC8025()
        {
            try
            {
                return ADQuote.GetProductSizesVerticalC8025();
            }
            catch (Exception)
            {
                return null;
            }
        }
        //PisaAlformbra 8025
        public DataTable GetProductSizesPisaAlformbra8025()
        {
            try
            {
                return ADQuote.GetProductSizesPisaAlfombra8025();
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

        public bool updateQuoteTotal(int idQuote, decimal total) {
            try {
                ADQuote.UpdateQuoteTotal(Convert.ToInt32(idQuote), Convert.ToDecimal(total));
                return true;
            }
            catch  { return false; }

        }

        public int InsertTotalDesglose(int IdQuote, decimal TotalPV, decimal MontoFacturacion, decimal MontoInstalacion, decimal Total)
        {
            try
            {
                return ADQuote.InsertTotalDesglose(IdQuote, TotalPV, MontoFacturacion, MontoInstalacion, Total);
            }
            catch
            {
                return 0;
            }

        }

        public bool EliminarRegistro(int IdTotal)
        {
            try
            {
                return ADQuote.EliminarRegistro(IdTotal);
            }
            catch
            {
                return false;
            }
        }

        // Lógica de negocio
        public bool ExisteIdQuote(int idQuote)
        {
            try
            {
                return ADQuote.ExisteIdQuote(idQuote); // Asumiendo que ADQuote es tu clase de acceso a datos
            }
            catch (Exception ex)
            {
                // Manejo de excepciones (podrías agregar un log, por ejemplo)
                return false;
            }
        }



        public DataTable GetTotalDesgloseByQuoteId(int idQuote) {
            try
            {
                return ADQuote.GetTotalDesgloseByQuoteId(idQuote);
            }
            catch
            {
                    return null ;
            }
        }

    }
}
