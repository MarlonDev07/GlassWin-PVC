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
    public class AD_MoveBank
    {
        ClsConnection cnn = new ClsConnection();

        public DataTable ListMoveBank()
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM MoveBank WHERE IdCompany = @IdCompany";
                SqlDataAdapter da = new SqlDataAdapter(sql, cnn.OpenConecction());
                da.SelectCommand.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                da.Fill(dt);
                cnn.CloseConnection();
                return dt;
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
                cnn.OpenConecction();
                string sql = "INSERT INTO MoveBank(IdAccount, Description, TypeMove, Ammount, Client, IdCompany) VALUES(@IdAccount, @Description, @TypeMove, @Account, @Client, @IdCompany)";
                SqlCommand cmd = new SqlCommand(sql, cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdAccount", IdAccount);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@TypeMove", TypeMove);
                cmd.Parameters.AddWithValue("@Account", Account);
                cmd.Parameters.AddWithValue("@Client", Client);
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                cmd.ExecuteNonQuery();
                return true;
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
                cnn.OpenConecction();
                string sql = "UPDATE MoveBank SET IdAccount = @IdAccount, Description = @Description, TypeMove = @TypeMove, Account = @Account, Client = @Client WHERE IdMoveBank = @IdMoveBank";
                SqlCommand cmd = new SqlCommand(sql, cnn.OpenConecction());
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IdMoveBank", IdMoveBank);
                cmd.Parameters.AddWithValue("@IdAccount", IdAccount);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@TypeMove", TypeMove);
                cmd.Parameters.AddWithValue("@Account", Account);
                cmd.Parameters.AddWithValue("@Client", Client);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
