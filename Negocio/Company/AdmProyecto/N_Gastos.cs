using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AccesoDatos;
using AccesoDatos.Company.AdmProyecto;
namespace Negocio.Company.AdmProyecto
{
    public class N_Gastos
    {
        AD_Gastos adGastos = new AD_Gastos();

        public bool InsertarGastos(int idProyeto, DateTime Fecha, string Motivo, decimal Monto)
        {
            return adGastos.InsertarGastos(idProyeto, Fecha, Motivo, Monto);
        }

        public bool ActualizarGastos(int idGasto, DateTime Fecha, string Motivo, decimal Monto)
        {
            return adGastos.ActualizarGastos(idGasto, Fecha, Motivo, Monto);
        }

        public bool EliminarGastos(int idGasto)
        {
            return adGastos.EliminarGastos(idGasto);
        }

        public DataTable ListarGastos(int idProyecto)
        {
            return adGastos.ListarGastos(idProyecto);
        }
    }
}
