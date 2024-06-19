using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Company.Proveedor;

namespace Negocio.Proveedor
{
    public class LN_Proveedor
    {
        public DataTable CargarProveedor()
        {
            try
            {
                AD_Proveedor adProveedor = new AD_Proveedor();
                return adProveedor.CargarProveedor();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
