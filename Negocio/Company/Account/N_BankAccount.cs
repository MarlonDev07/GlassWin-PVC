using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using AccesoDatos.Company.Accounts;

namespace Negocio.Company.Account
{
    public class N_BankAccount
    {
        AD_AccountBank AD_AccountBank = new AD_AccountBank();

        public DataTable ListBankAccount() 
        {
            try
            {
                return AD_AccountBank.ListAccount();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool CreateAccountBank(int IdAccount, string Owner, string BankEmisor)
        {
            try
            {
                return AD_AccountBank.CreateAccountBank(IdAccount, Owner, BankEmisor);
            }
            catch (Exception)
            {
                throw;
            }
        } 
        
        public bool UpdateAccountBank(int IdAccount, string Owner, string BankEmisor)
        {
            try
            {
                return AD_AccountBank.UpdateAccountBank(IdAccount, Owner, BankEmisor);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
