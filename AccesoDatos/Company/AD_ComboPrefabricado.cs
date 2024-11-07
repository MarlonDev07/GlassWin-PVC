using AccesoDatos.DataBase;
using Dominio.Model.ClassComboArticulos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Company
{
    public class AD_ComboPrefabricado
    {
        public bool InsertarCombo(int idPrice, int IdWindows, decimal Metraje, decimal Cantidad) 
        {
            try
            {
                // Crear la conexión y el comando
                ClsConnection con = new ClsConnection();
                //Consulta sql no un sp
                SqlCommand cmd = new SqlCommand("INSERT INTO ArticulosCombo (idPrice, IdWindows, Metraje, Cantidad) VALUES (@idPrice, @IdWindows, @Metraje, @Cantidad)", con.OpenConecction());
                cmd.CommandType = CommandType.Text;

                // Agregar los parámetros de entrada
                cmd.Parameters.AddWithValue("@idPrice", idPrice);
                cmd.Parameters.AddWithValue("@IdWindows", IdWindows);
                cmd.Parameters.AddWithValue("@Metraje", Metraje);
                cmd.Parameters.AddWithValue("@Cantidad", Cantidad);

                // Ejecutar el comando
                cmd.ExecuteNonQuery();

                // Cerrar la conexión
                con.CloseConnection();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
