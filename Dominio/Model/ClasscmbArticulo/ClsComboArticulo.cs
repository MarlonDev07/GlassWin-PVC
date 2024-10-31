using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model.ClasscmbArticulo
{
    public static class ClsComboArticulo
    {
        public static bool Guardar { get; set; }
        public static int IdQuote { get; set; }
        public static decimal Precio { get; set; }
        //Crear un data table para guardar los articulos
        public static DataTable dtArticulo = new DataTable();



    }
}
