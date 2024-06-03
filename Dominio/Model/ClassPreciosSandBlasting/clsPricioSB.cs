using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model.ClassPreciosSandBlasting
{
    public static class clsPricioSB
    {
        public static decimal ObtenerPrecio(string categoriaOServicio)
        {
            switch (categoriaOServicio)
            {
                case "CategoriaA":
                    return 3250;
                case "CategoriaB":
                    return 5350;
                case "CategoriaC":
                    return 6825;
                case "CategoriaD":
                    return 7875;
                case "CategoriaE":
                    return 8400;
                case "CategoriaF":
                    return 8925;
                case "CategoriaG":
                    return 10500;
                case "CategoriaH":
                    return 13125;
                case "CategoriaI":
                    return 15750;
                case "CategoriaJ":
                    return 18375;
                case "CategoriaK":
                    return 22050;
                case "CategoriaL":
                    return 26250;
                case "CategoriaM":
                    return 31500;
                case "CategoriaN":
                    return 36750;
                case "ServicioDiseñoArenado":
                    return 9505;
                case "ServicioDiseñoArenadoRapido":
                    return 13390;
                case "ServicioSello":
                    return 3200;
                default:
                    throw new ArgumentException("Categoría o servicio no válido.");
            }
        }
    }
}