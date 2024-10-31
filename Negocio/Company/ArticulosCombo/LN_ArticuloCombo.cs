using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Company.ArticulosCombo;

namespace Negocio.Company.ArticulosCombo
{
    public class LN_ArticuloCombo
    {
        private AD_ArticulosCombo AD_ArticulosCombo;

        public LN_ArticuloCombo()
        {
            AD_ArticulosCombo = new AD_ArticulosCombo();
        }

        public bool GuardarArticuloCombo(int idProduct, string Color, int IdQuote, int IdWindows)
        {
            return AD_ArticulosCombo.GuardarArticuloCombo(idProduct, Color, IdQuote, IdWindows);
        }
    }
}
