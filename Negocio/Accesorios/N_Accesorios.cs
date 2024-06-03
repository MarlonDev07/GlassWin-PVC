using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Accesorios
{
    public class N_Accesorios
    {
        AccesoDatos.Company.Accesorios.AD_Accesorios AD_Accesorios = new AccesoDatos.Company.Accesorios.AD_Accesorios();
        public List<object> Articulo(int Id)
        {
            return AD_Accesorios.Articulo(Id);
        }
    }
}
