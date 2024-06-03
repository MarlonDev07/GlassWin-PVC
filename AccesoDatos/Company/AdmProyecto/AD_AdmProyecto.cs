using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Company.AdmProyecto
{
    public class AD_AdmProyecto
    {
        private DataBase.ClsConnection Cnn;

        public AD_AdmProyecto()
        {
            Cnn = new DataBase.ClsConnection();
        }

        //Seleccionar el IdAdmProyecto por IdCxC
        public bool SeleccionarIdAdmProyectoyElimanar(int IdCxC)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = "select IdAdmProyecto from AdmProyecto where IdCxC = @IdCxC";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@IdCxC", IdCxC);
                int IdAdmProyecto = Convert.ToInt32(cmd.ExecuteScalar());
                Cnn.CloseConnection();
                if (IdAdmProyecto != 0)
                {
                    EliminarProyecto(IdAdmProyecto);
                    return true;
                }else
                {
                    return false;
                }
                
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool InsertarAdmProyecto (int IdCxC, string Proyecto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = "insert into AdmProyecto (IdCxC,Proyecto,IdCompany,Estado) values (@IdCxC,@Proyecto,@IdCompany,@Estado)";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@IdCxC", IdCxC);
                cmd.Parameters.AddWithValue("@IdCompany", CompanyCache.IdCompany);
                cmd.Parameters.AddWithValue("@Proyecto", Proyecto);
                cmd.Parameters.AddWithValue("@Estado", "Activo");
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }       
        }

        public bool ActualizarIdCxP(int IdProyecto,int IdCxP)
        {
            try { 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Cnn.OpenConecction();
            string query = "update AdmProyecto set IdCxP = @IdCxP ,Estado = @Estado where IdAdmProyecto = @IdProyecto";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@IdCxP", IdCxP);
            cmd.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            cmd.Parameters.AddWithValue("@Estado", "Activo");
            cmd.ExecuteNonQuery();
            Cnn.CloseConnection();
            return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool FinalzarProyecto(int IdProyecto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = "update AdmProyecto set Estado = @Estado where IdAdmProyecto = @IdProyecto";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@IdProyecto", IdProyecto);
                cmd.Parameters.AddWithValue("@Estado", "Finalizado");
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ActualizarMontos(int IdProyecto, decimal TotalGastos, decimal Utilidad)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = "update AdmProyecto set TotalGastos = @TotalGastos, Utilidad = @Utilidad where IdAdmProyecto = @IdProyecto";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@TotalGastos", TotalGastos);
                cmd.Parameters.AddWithValue("@Utilidad", Utilidad);
                cmd.Parameters.AddWithValue("@IdProyecto", IdProyecto);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public DataTable ListarNombresProyectos(string Option)
        {
            try
            {
                DataTable dataTable = new DataTable();
                string query = "select IdAdmProyecto,Proyecto from AdmProyecto where IdCompany = @Id and Estado = @Option";
                SqlCommand cmd = new SqlCommand(query, Cnn.OpenConecction());
                cmd.Parameters.AddWithValue("@Id", CompanyCache.IdCompany);
                cmd.Parameters.AddWithValue("@Option", Option);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                Cnn.CloseConnection();
                return dataTable;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public decimal MontoPagar(int IdProyecto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = "SELECT AP.AmountInitial FROM AdmProyecto adm INNER JOIN AccountPayable AP ON adm.IdCxP = AP.IdAccount WHERE adm.IdAdmProyecto = @IdProyecto";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@IdProyecto", IdProyecto);
                decimal Monto = Convert.ToDecimal(cmd.ExecuteScalar());
                Cnn.CloseConnection();
                return Monto;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public decimal CapitalProyecto(int IdProyecto) 
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = " SELECT Ac.InitialAmount FROM AdmProyecto adm INNER JOIN AccountReceivable Ac ON adm.IdCxC = Ac.IdAccount WHERE adm.IdAdmProyecto = @IdProyecto";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@IdProyecto", IdProyecto);
                decimal Monto = Convert.ToDecimal(cmd.ExecuteScalar());
                Cnn.CloseConnection();
                return Monto;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Elimanr Proyecto Utilizando un SP llamado SP_EliminarProyectoYGastos
        public bool EliminarProyecto(int IdProyecto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                cmd.CommandText = "SP_EliminarProyectoYGastos";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdAdmProyecto", IdProyecto);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
