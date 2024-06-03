using System;
using AccesoDatos.Company.Accounts;

namespace Negocio.Company.Account
{
    public class N_MoveBank
    {
        AD_MoveBank AD_MoveBank = new AD_MoveBank();

        public System.Data.DataTable ListMoveBank()
        {
            try
            {
                return AD_MoveBank.ListMoveBank();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool CreateMoveBank(Int64 IdAccount, string Description, string TypeMove, decimal Account, string Client)
        {
            try
            {
                return AD_MoveBank.CreateMoveBank(IdAccount, Description, TypeMove, Account, Client);
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdatMoveBank(Int32 IdMoveBank, Int64 IdAccount, string Description, string TypeMove, decimal Account, string Client)
        {
            try
            {
                return AD_MoveBank.UpdatMoveBank(IdMoveBank, IdAccount, Description, TypeMove, Account, Client);
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
