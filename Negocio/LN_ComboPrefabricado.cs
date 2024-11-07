using AccesoDatos.DataBase;
using Dominio.Model.ClassComboArticulos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Company;
using Twilio.Rest.Insights.V1.Call;

namespace Negocio.Company
{
    public class LN_ComboPrefabricado
    {

        public bool InsertarCombo(int idPrice, int IdWindows, decimal Metraje, decimal Cantidad) 
        {
            AD_ComboPrefabricado adComboPrefabricado = new AD_ComboPrefabricado();
            return adComboPrefabricado.InsertarCombo(idPrice, IdWindows, Metraje, Cantidad);
        }
    }
}
