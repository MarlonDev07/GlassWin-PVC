using iTextSharp.text.xml.simpleparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using AccesoDatos.Company.Bill.Accounts;
using System.Data;

namespace Negocio.Company.Account
{
   
    public class N_CxC
    {
        AD_CxC _CxC = new AD_CxC();
        public bool InsertCxC(int IdBill, decimal InitialAmount, decimal OutstandingBalance, string Proyecto)
        {
            return _CxC.InsertCxC(IdBill, InitialAmount, OutstandingBalance,Proyecto);
        }

        public DataTable LoadCxC()
        {
            return _CxC.LoadCxC();
        }

        public bool UpdateCxC(int IdAccount, int IdBill, decimal InitialAmount, decimal OutstandingBalance)
        {
            return _CxC.UpdateCxC(IdAccount, IdBill, InitialAmount, OutstandingBalance);
        }

        public DataTable FindCxCforClient(int IdClient)
        {
            return _CxC.FindCxCforClient(IdClient);
        }
        public int LastIdCxC() 
        { 
            return _CxC.LastIdCxC();
        }

        public bool DeleteCxC(int IdAccount)
        {
            return _CxC.DeleteCxC(IdAccount);
        }
        public void ActualizarFechaVencimiento(int idCuenta, DateTime nuevaFecha)
        {
            _CxC.ActualizarFechaVencimiento(idCuenta, nuevaFecha);
        }
    }
}
