using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Company.AdmProyecto
{
    public class AD_Gastos
    {
        private DataBase.ClsConnection Cnn;

        public AD_Gastos()
        {
            Cnn = new DataBase.ClsConnection();
        }

        public bool InsertarGastos(int idProyeto, DateTime Fecha, string Motivo,decimal Monto) 
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = "insert into Gastos (IdAdmProyecto,Fecha,Motivo,Monto) values (@IdAdmProyecto,@Fecha,@Motivo,@Monto)";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@IdAdmProyecto", idProyeto);
                cmd.Parameters.AddWithValue("@Fecha", Fecha);
                cmd.Parameters.AddWithValue("@Motivo", Motivo);
                cmd.Parameters.AddWithValue("@Monto", Monto);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ActualizarGastos(int idGasto, DateTime Fecha, string Motivo, decimal Monto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = "update Gastos set Fecha = @Fecha, Motivo = @Motivo, Monto = @Monto where NDoc = @IdGastos";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@IdGastos", idGasto);
                cmd.Parameters.AddWithValue("@Fecha", Fecha);
                cmd.Parameters.AddWithValue("@Motivo", Motivo);
                cmd.Parameters.AddWithValue("@Monto", Monto);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminarGastos(int idGasto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = "delete from Gastos where NDoc = @IdGastos";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@IdGastos", idGasto);
                cmd.ExecuteNonQuery();
                Cnn.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable ListarGastos(int idProyecto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Cnn.OpenConecction();
                string query = "select * from Gastos where IdAdmProyecto = @IdAdmProyecto";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@IdAdmProyecto", idProyecto);
                SqlDataReader dr = cmd.ExecuteReader();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(dr);
                Cnn.CloseConnection();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
