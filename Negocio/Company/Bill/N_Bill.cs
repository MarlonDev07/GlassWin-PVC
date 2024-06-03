using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using AccesoDatos.Company.Bill;

namespace Negocio.Company.Bill
{
    public class N_Bill
    {
        AD_BILL _BILL = new AD_BILL();


        public bool InsertBill(int IdQuote, int IdClient, DateTime Date, DateTime DateExpiration)
        {
            return _BILL.InsertBill(IdQuote, IdClient, Date, DateExpiration);
        }
        public DataTable readBill()
        {
            return _BILL.readBill();
        }
        public int getLastID()
        {
            return _BILL.getLastID();
        }

        public int getIdClient(int IdBill)
        {
            return _BILL.getIdClient(IdBill);
        }
        public bool InsertSell(int idEmployer, decimal Amount) 
        { 
            return _BILL.InsertSell(idEmployer, Amount);
        }
        public DataTable SelectEmployersSeller()
        {
           return _BILL.SelectEmployersSeller();
        }
        public bool DeleteBill(int IdBill)
        {
            return _BILL.DeleteBill(IdBill);
        }
    }
}
