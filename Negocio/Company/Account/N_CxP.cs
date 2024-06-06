using AccesoDatos.Company.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Company.Account
{
    public class N_CxP
    {
        AD_CxP AD_CxP = new AD_CxP();

        //Insertar CxP
        public bool InsertCxP(string Supplyer, decimal AmountInitial, string Detail)
        {
            return AD_CxP.InsertCxP(Supplyer, AmountInitial,Detail);
        }

        //Actualizar CxP
        public bool UpdateCxP(int IdAccount, decimal AmountPending)
        {
            return AD_CxP.UpdateCxP(IdAccount, AmountPending);
        }

        //Seleccionar CxP
        public System.Data.DataTable SelectBySupplyer(string Supplyer)
        {
            return AD_CxP.SelectBySupplyer(Supplyer);
        }

       

       public DataTable GetSupplyers()
       {
            AD_CxP dal = new AD_CxP();
           return dal.GetSupplyers();
       }
        


        public DataTable SelectAll()
        {
            return AD_CxP.SelectAll();
        }
        public int LastIdCxP() 
        {
            return AD_CxP.LastIdCxP();
        }
    }
}
