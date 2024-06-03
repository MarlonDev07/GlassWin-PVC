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
    public class N_AdmProyecto
    {
        private AD_AdmProyecto adAdmProyecto = new AD_AdmProyecto();

        public bool InsertarAdmProyecto(int IdCxC, string Proyecto)
        {
            return adAdmProyecto.InsertarAdmProyecto(IdCxC, Proyecto);
        }

        public bool SeleccionarIdAdmProyectoyElimanar(int IdCxC)
        {
            return adAdmProyecto.SeleccionarIdAdmProyectoyElimanar(IdCxC);
        }

        public bool ActualizarIdCxP(int IdProyecto, int IdCxP)
        {
            return adAdmProyecto.ActualizarIdCxP(IdProyecto, IdCxP);
        }
        public bool FinalzarProyecto(int IdProyecto) 
        {
            return adAdmProyecto.FinalzarProyecto(IdProyecto);
        }
        public bool ActualizarMontos(int IdProyecto, decimal TotalGastos, decimal Utilidad)
        {
            return adAdmProyecto.ActualizarMontos(IdProyecto, TotalGastos, Utilidad);
        }
        public DataTable ListarNombresProyectos(string Option)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = adAdmProyecto.ListarNombresProyectos(Option);
                //Combinar las Columnas de IdAdmProyecto y Proyecto
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("ProyectoCompleto", typeof(string), "IdAdmProyecto + ' - ' + Proyecto");
                }
                return dt;
            }
            catch (Exception)
            {

                return null;
            }
            
            
            
        }
        public decimal MontoPagar(int IdProyecto) 
        {
            return adAdmProyecto.MontoPagar(IdProyecto);
        }
        public decimal CapitalProyecto(int IdProyecto) 
        { 
            return adAdmProyecto.CapitalProyecto(IdProyecto);
        }

        public bool EliminarProyecto(int IdProyecto)
        {
            return adAdmProyecto.EliminarProyecto(IdProyecto);
        }

    }
}
