using AccesoDatos.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Company.Accounts
{
    public class AD_AccountBank
    {
        ClsConnection clsConnection = new ClsConnection();
        public DataTable ListAccount()
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM BankAccount WHERE IdCompany = @IdCompany", clsConnection.OpenConecction());
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                sqlDataAdapter.Fill(dataTable);
                clsConnection.CloseConnection();
                return dataTable;

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
                clsConnection.OpenConecction();
                string sql = "INSERT INTO BankAccount(IdAccount, Owner, BankEmisor,IdCompany) VALUES(@IdAccount, @Owner, @BankEmisor,@IdCompany)";
                SqlCommand cmd = new SqlCommand(sql, clsConnection.OpenConecction());
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@IdAccount", IdAccount);
                cmd.Parameters.AddWithValue("@Owner", Owner);
                cmd.Parameters.AddWithValue("@BankEmisor", BankEmisor);
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                clsConnection.CloseConnection();
            }
        }
        public bool UpdateAccountBank(int IdAccount, string Owner, string BankEmisor)
        {
            try
            {
                clsConnection.OpenConecction();
                string sql = "UPDATE BankAccount SET Owner = @Owner, BankEmisor = @BankEmisor WHERE IdAccount = @IdAccount AND IdCompany = @IdCompany";
                SqlCommand cmd = new SqlCommand(sql, clsConnection.OpenConecction());
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@IdAccount", IdAccount);
                cmd.Parameters.AddWithValue("@Owner", Owner);
                cmd.Parameters.AddWithValue("@BankEmisor", BankEmisor);
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                clsConnection.CloseConnection();
            }
        }
    }
}
