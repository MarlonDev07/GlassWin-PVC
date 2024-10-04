using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Negocio.Company.OrdenProduccion
{
    public class N_OrdenProduccion
    {
      #region 5020
        public decimal CalculoRebajos5020(string Diseño, string Ancho, string Alto, string Pieza) 
      {
            decimal Rebajo = 0;
            //Convertimos a decimal el ancho y alto
            decimal W = Convert.ToDecimal(Ancho);
            decimal H = Convert.ToDecimal(Alto);

            switch (Diseño)
            {
                case "FijoMovil":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Rebajo = W;
                            break;
                        case "Umbral":
                            Rebajo = W;
                            break;
                        case "Jamba":
                            Rebajo = H-0.016M;
                            break;
                        case "Superior":
                            Rebajo = Math.Round((W / 2) - 0.015M, 3);
                            break;
                        case "Inferior":
                            Rebajo = Math.Round((W / 2) - 0.013M, 3);
                            break;
                        case "Vertical":
                            Rebajo = H-0.03M;
                            break;
                        case "Vertical Centro":
                            Rebajo = H-0.03M;
                            break;
                        case "Vertical Centro fijo":
                            Rebajo = H - 0.02M;
                            break;
                        case "Vertical fijo":
                            Rebajo = H - 0.02M;
                            break;
                        case "Vidrio M ancho":
                            Rebajo = W / 2 - 0.054M;
                            break;
                        case "Vidrio F ancho":
                            Rebajo = W / 2 - 0.054M;
                            break;
                        case "Vidrio M alto":
                            Rebajo = H - 0.10M;
                            break;
                        case "Vidrio F alto":
                            Rebajo = H - 0.09M;
                            break;
                    }
                    break;

                case "MovilFijo":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Rebajo = W;
                            break;
                        case "Umbral":
                            Rebajo = W;
                            break;
                        case "Jamba":
                            Rebajo = H - 0.016M;
                            break;
                        case "Superior":
                            Rebajo = (W / 2) - 0.015M;
                            break;
                        case "Inferior":
                            Rebajo = (W / 2) - 0.013M;
                            break;
                        case "Vertical":
                            Rebajo = H - 0.03M;
                            break;
                        case "Vertical Centro":
                            Rebajo = H - 0.03M;
                            break;
                        case "Vertical Centro fijo":
                            Rebajo = H - 0.02M;
                            break;
                        case "Vertical fijo":
                            Rebajo = H - 0.02M;
                            break;
                        case "Vidrio M ancho":
                            Rebajo = W / 2 - 0.054M;
                            break;
                        case "Vidrio F ancho":
                            Rebajo = W / 2 - 0.054M;
                            break;
                        case "Vidrio M alto":
                            Rebajo = H - 0.10M;
                            break;
                        case "Vidrio F alto":
                            Rebajo = H - 0.09M;
                            break;

                    }
                    break;
                case "MovilMovil":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Rebajo = W;
                            break;
                        case "Umbral":
                            Rebajo = W;
                            break;
                        case "Jamba":
                            Rebajo = H - 0.016M;
                            break;
                        case "Superior":
                            Rebajo = (W / 2) - 0.015M;
                            break;
                        case "Inferior":
                            Rebajo = (W / 2) - 0.013M;
                            break;
                        case "Vertical":
                            Rebajo = H - 0.03M;
                            break;
                        case "Vertical Centro":
                            Rebajo = H - 0.03M;
                            break;
                        case "Vidrio M ancho":
                            Rebajo = W / 2 - 0.054M;
                            break;
                        case "Vidrio M alto":
                            Rebajo = H - 0.10M;
                            break;
                        
                    }
                    break;


                        case "MovilFijoMovil":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Rebajo = W;
                            break;
                        case "Umbral":
                            Rebajo = W;
                            break;
                        case "Jamba":
                            Rebajo = H - 0.016M;
                            break;
                        case "Superior":
                            Rebajo = (W / 4) - 0.007M;
                            break;
                        case "Superior F":
                            Rebajo = (W / 2) - 0.005M;
                            break;
                        case "Inferior":
                            Rebajo = (W / 4) - 0.005M;
                            break;
                        case "Vertical":
                            Rebajo = H - 0.03M;
                            break;
                        case "Vertical Centro":
                            Rebajo = H - 0.03M;
                            break;
                        case "Vertical Centro fijo":
                            Rebajo = H - 0.02M;
                            break;
                        case "Vidrio M ancho":
                            Rebajo = W / 4 - 0.044M;
                            break;
                        case "Vidrio M alto":
                            Rebajo = W / 2 - 0.010M;
                            break;
                        case "Vidrio F ancho":
                            Rebajo = W / 2 - 0.034M;
                            break;
                        case "Vidrio F alto":
                            Rebajo = H - 0.09M;
                            break;
                    }
                    break;
                case "FijoMovilFijo":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Rebajo = W;
                            break;
                        case "Umbral":
                            Rebajo = W;
                            break;
                        case "Jamba":
                            Rebajo = H - 0.016M;
                            break;
                        case "Superior":
                            Rebajo = (W / 3) - 0.022M;
                            break;
                        case "Inferior":
                            Rebajo = (W / 3) - 0.02M;
                            break;
                        case "Vertical":
                            Rebajo = H - 0.03M;
                            break;
                        case "Vertical Centro":
                            Rebajo = H - 0.03M;
                            break;
                        case "Vertical Centro fijo":
                            Rebajo = H - 0.02M;
                            break;
                        case "Vertical fijo":
                            Rebajo = H - 0.02M;
                            break;
                        case "Vidrio M ancho":
                            Rebajo = W / 3 - 0.058M;
                            break;
                        case "Vidrio F ancho":
                            Rebajo = W / 3 - 0.058M;
                            break;
                        case "Vidrio M alto":
                            Rebajo = H - 0.104M;
                            break;
                        case "Vidrio F alto":
                            Rebajo = H - 0.09M;
                            break;
                    }
                    break;
                case "FijoMovilMovilFijo":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Rebajo = W;
                            break;
                        case "Umbral":
                            Rebajo = W;
                            break;
                        case "Jamba":
                            Rebajo = H - 0.016M;
                            break;
                        case "Superior":
                            Rebajo = (W / 4) - 0.012M;
                            break;
                        case "Inferior":
                            Rebajo = (W / 4) - 0.010M;
                            break;
                        case "Vertical":
                            Rebajo = H - 0.03M;
                            break;
                        case "Vertical Centro":
                            Rebajo = H - 0.03M;
                            break;
                        case "Vertical Centro fijo":
                            Rebajo = H - 0.02M;
                            break;
                        case "Vertical fijo":
                            Rebajo = H - 0.02M;
                            break;
                        case "Vidrio M ancho":
                            Rebajo = W / 4 - 0.052M;
                            break;
                        case "Vidrio F ancho":
                            Rebajo = W / 4 - 0.052M;
                            break;
                        case "Vidrio M alto":
                            Rebajo = H - 0.104M;
                            break;
                        case "Vidrio F alto":
                            Rebajo = H - 0.09M;
                            break;
                    }
                    break;
            }

            return Rebajo;
        }

        public decimal CalculoCantidadPiezas(string Diseño, string Pieza) 
        {
            decimal Cantidad = 0;

            switch (Diseño)
            {
                case "FijoMovil":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Cantidad = 1;
                            break;
                        case "Umbral":
                            Cantidad = 1;
                            break;
                        case "Jamba":
                            Cantidad = 2;
                            break;
                        case "Superior":
                            Cantidad = 3;
                            break;
                        case "Inferior":
                            Cantidad = 1;
                            break;
                        case "Vertical":
                            Cantidad = 1;
                            break;
                        case "Vertical Centro":
                            Cantidad = 1;
                            break;
                        case "Vertical Centro fijo":
                            Cantidad = 1;
                            break;
                        case "Vertical fijo":
                            Cantidad = 1;
                            break;
                        case "Vidrio M ancho":
                            Cantidad = 1;
                            break;
                        case "Vidrio F ancho":
                            Cantidad = 1;
                            break;
                        case "Vidrio M alto":
                            Cantidad = 1;
                            break;
                        case "Vidrio F alto":
                            Cantidad = 1;
                            break;

                    }
                    break;
                case "MovilFijo":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Cantidad = 1;
                            break;
                        case "Umbral":
                            Cantidad = 1;
                            break;
                        case "Jamba":
                            Cantidad = 2;
                            break;
                        case "Superior":
                            Cantidad = 3;
                            break;
                        case "Inferior":
                            Cantidad = 1;
                            break;
                        case "Vertical":
                            Cantidad = 2;
                            break;
                        case "Vertical Centro":
                            Cantidad = 2;
                            break;
                        case "Vidrio M ancho":
                            Cantidad = 1;
                            break;
                        case "Vidrio F ancho":
                            Cantidad = 1;
                            break;
                        case "Vidrio M alto":
                            Cantidad = 1;
                            break;
                        case "Vidrio F alto":
                            Cantidad = 1;
                            break;
                    }
                    break;
                case "MovilMovil":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Cantidad = 1;
                            break;
                        case "Umbral":
                            Cantidad = 1;
                            break;
                        case "Jamba":
                            Cantidad = 2;
                            break;
                        case "Superior":
                            Cantidad = 2;
                            break;
                        case "Inferior":
                            Cantidad = 2;
                            break;
                        case "Vertical":
                            Cantidad = 2;
                            break;
                        case "Vertical Centro":
                            Cantidad = 2;
                            break;
                        case "Vidrio M ancho":
                            Cantidad = 2;
                            break;
                        case "Vidrio M alto":
                            Cantidad = 2;
                            break;
                        
                    }
                    break;

                        case "MovilFijoMovil":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Cantidad = 1;
                            break;
                        case "Umbral":
                            Cantidad = 1;
                            break;
                        case "Jamba":
                            Cantidad = 2;
                            break;
                        case "Superior":
                            Cantidad = 2;
                            break;
                        case "Superior F":
                            Cantidad = 1;
                            break;
                        case "Inferior":
                            Cantidad = 2;
                            break;
                        case "Vertical":
                            Cantidad = 2;
                            break;
                        case "Vertical Centro":
                            Cantidad = 2;
                            break;
                        case "Vertical Centro fijo":
                            Cantidad = 2;
                            break;
                        case "Vidrio M ancho":
                            Cantidad = 2;
                            break;
                        case "Vidrio F ancho":
                            Cantidad = 1;
                            break;
                        case "Vidrio M alto":
                            Cantidad = 2;
                            break;
                        case "Vidrio F alto":
                            Cantidad = 1;
                            break;
                    }
                    break;
                case "FijoMovilFijo":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Cantidad = 1;
                            break;
                        case "Umbral":
                            Cantidad = 1;
                            break;
                        case "Jamba":
                            Cantidad = 2;
                            break;
                        case "Superior":
                            Cantidad = 5;
                            break;
                        case "Inferior":
                            Cantidad = 1;
                            break;
                        case "Vertical":
                            Cantidad = 1;
                            break;
                        case "Vertical Centro":
                            Cantidad = 1;
                            break;
                        case "Vertical Centro fijo":
                            Cantidad = 1;
                            break;
                        case "Vertical fijo":
                            Cantidad = 3;
                            break;
                        case "Vidrio M ancho":
                            Cantidad = 1;
                            break;
                        case "Vidrio F ancho":
                            Cantidad = 2;
                            break;
                        case "Vidrio M alto":
                            Cantidad = 1;
                            break;
                        case "Vidrio F alto":
                            Cantidad = 2;
                            break;
                    }
                    break;
                case "FijoMovilMovilFijo":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Cantidad = 1;
                            break;
                        case "Umbral":
                            Cantidad = 1;
                            break;
                        case "Jamba":
                            Cantidad = 2;
                            break;
                        case "Superior":
                            Cantidad = 6;
                            break;
                        case "Inferior":
                            Cantidad = 2;
                            break;
                        case "Vertical":
                            Cantidad = 2;
                            break;
                        case "Vertical Centro":
                            Cantidad = 2;
                            break;
                        case "Vertical Centro fijo":
                            Cantidad = 2;
                            break;
                        case "Vertical fijo":
                            Cantidad = 2;
                            break;
                        case "Vidrio M ancho":
                            Cantidad = 2;
                            break;
                        case "Vidrio F ancho":
                            Cantidad = 2;
                            break;
                        case "Vidrio M alto":
                            Cantidad = 2;
                            break;
                        case "Vidrio F alto":
                            Cantidad = 2;
                            break;
                    }
                    break;

            }

            return Cantidad;
        }
        #endregion

      #region 8025
        public decimal CalculoRebajos8025(string Diseño, string Ancho, string Alto, string Pieza)
        {
            decimal Rebajo = 0;
            //Convertimos a decimal el ancho y alto
            decimal W = Convert.ToDecimal(Ancho);
            decimal H = Convert.ToDecimal(Alto);

            switch (Diseño)
            {
                case "FijoMovil":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Rebajo = W;
                            break;
                        case "Umbral":
                            Rebajo = W;
                            break;
                        case "Jamba":
                            Rebajo = H - 0.011M;
                            break;
                        case "PisaAlfombra":
                            Rebajo = W;
                            break;
                        case "Superior":
                            Rebajo = (W / 2) - 0.008M;
                            break;
                        case "Inferior":
                            Rebajo = (W / 2) - 0.008M;
                            break;
                        case "Vertical":
                            Rebajo = H - 0.028M;
                            break;
                        case "Vertical Centro":
                            Rebajo = H - 0.028M;
                            break;
                        case "Vertical Centro fijo":
                            Rebajo = H - 0.023M;
                            break;
                        case "Vertical fijo":
                            Rebajo = H - 0.023M;
                            break;
                        case "Vidrio M ancho":
                            Rebajo = W / 2 - 0.064M;
                            break;
                        case "Vidrio F ancho":
                            Rebajo = W / 2 - 0.064M;
                            break;
                        case "Vidrio M alto":
                            Rebajo = H - 0.108M;
                            break;
                        case "Vidrio F alto":
                            Rebajo = H - 0.103M;
                            break;
                    }
                    break;
                case "MovilMovil":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Rebajo = W;
                            break;
                        case "Umbral":
                            Rebajo = W;
                            break;
                        case "Jamba":
                            Rebajo = H - 0.011M;
                            break;
                        case "PisaAlfombra":
                            Rebajo = W;
                            break;
                        case "Superior":
                            Rebajo = (W / 2) - 0.008M;
                            break;
                        case "Inferior":
                            Rebajo = (W / 2) - 0.008M;
                            break;
                        case "Vertical":
                            Rebajo = H - 0.028M;
                            break;
                        case "Vertical Centro":
                            Rebajo = H - 0.028M;
                            break;
                        case "Vidrio M ancho":
                            Rebajo = W / 2 - 0.064M;
                            break;
                        case "Vidrio M alto":
                            Rebajo = H - 0.108M;
                            break;

                    }
                    break;
                case "MovilFijoMovil":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Rebajo = W;
                            break;
                        case "Umbral":
                            Rebajo = W;
                            break;
                        case "Jamba":
                            Rebajo = H - 0.016M;
                            break;
                        case "Superior":
                            Rebajo = (W / 4) - 0.003M;
                            break;
                        case "Inferior":
                            Rebajo = (W / 4) - 0.003M;
                            break;
                        case "Superior f":
                            Rebajo = (W / 2) + 0.02M;
                            break;
                        case "Inferior f":
                            Rebajo = (W / 2) + 0.02M;
                            break;
                        case "Vertical":
                            Rebajo = H - 0.03M;
                            break;
                        case "Vertical Centro":
                            Rebajo = H - 0.03M;
                            break;
                        case "PisaAlfombra":
                            Rebajo = H - 0.03M;
                            break;
                        case "Vertical Centro fijo":
                            Rebajo = H - 0.023M;
                            break;
                        case "Vertical fijo":
                            Rebajo = H - 0.023M;
                            break;
                        case "Vidrio M ancho":
                            Rebajo = W / 2 - 0.064M;
                            break;
                        case "Vidrio F ancho":
                            Rebajo = W / 2 - 0.064M;
                            break;
                        case "Vidrio M alto":
                            Rebajo = H - 0.108M;
                            break;
                        case "Vidrio F alto":
                            Rebajo = H - 0.103M;
                            break;
                    }
                    break;
                case "FijoMovilFijo":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Rebajo = W;
                            break;
                        case "Umbral":
                            Rebajo = W;
                            break;
                        case "Jamba":
                            Rebajo = H - 0.016M;
                            break;
                        case "Superior":
                            Rebajo = (W / 3) - 0.008M;
                            break;
                        case "Inferior":
                            Rebajo = (W / 3) - 0.008M;
                            break;
                        case "Vertical":
                            Rebajo = H - 0.028M;
                            break;
                        case "Vertical Centro":
                            Rebajo = H - 0.028M;
                            break;
                        case "PisaAlfombra":
                            Rebajo = W;
                            break;
                        case "Vertical Centro fijo":
                            Rebajo = H - 0.023M;
                            break;
                        case "Vertical fijo":
                            Rebajo = H - 0.023M;
                            break;
                        case "Vidrio M ancho":
                            Rebajo = W / 3 - 0.069M;
                            break;
                        case "Vidrio F ancho":
                            Rebajo = W / 3 - 0.069M;
                            break;
                        case "Vidrio M alto":
                            Rebajo = H - 0.108M;
                            break;
                        case "Vidrio F alto":
                            Rebajo = H - 0.103M;
                            break;
                    }
                    break;
                case "FijoMovilMovilFijo":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Rebajo = W;
                            break;
                        case "Umbral":
                            Rebajo = W;
                            break;
                        case "Jamba":
                            Rebajo = H - 0.016M;
                            break;
                        case "Superior":
                            Rebajo = (W / 4);
                            break;
                        case "Inferior":
                            Rebajo = (W / 4);
                            break;
                        case "Vertical":
                            Rebajo = H - 0.028M;
                            break;
                        case "Vertical Centro":
                            Rebajo = H - 0.028M;
                            break;
                        case "PisaAlfombra":
                            Rebajo = W;
                            break;
                        case "Vertical Centro fijo":
                            Rebajo = H - 0.023M;
                            break;
                        case "Vertical fijo":
                            Rebajo = H - 0.023M;
                            break;
                        case "Vidrio M ancho":
                            Rebajo = W / 4 - 0.189M;
                            break;
                        case "Vidrio F ancho":
                            Rebajo = W / 4 - 0.189M;
                            break;
                        case "Vidrio M alto":
                            Rebajo = H - 0.108M;
                            break;
                        case "Vidrio F alto":
                            Rebajo = H - 0.103M;
                            break;
                    }
                    break;
            }

            return Rebajo;
        }

        public decimal CalculoCantidadPiezas8025(string Diseño, string Pieza)
        {
            decimal Cantidad = 0;

            switch (Diseño)
            {
                case "FijoMovil":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Cantidad = 1;
                            break;
                        case "Umbral":
                            Cantidad = 1;
                            break;
                        case "Jamba":
                            Cantidad = 2;
                            break;
                        case "PisaAlfombra":
                            Cantidad = 1;
                            break;
                        case "Superior":
                            Cantidad = 2;
                            break;
                        case "Inferior":
                            Cantidad = 2;
                            break;
                        case "Vertical":
                            Cantidad = 1;
                            break;
                        case "Vertical Centro":
                            Cantidad = 1;
                            break;
                        case "Vertical Centro fijo":
                            Cantidad = 1;
                            break;
                        case "Vertical fijo":
                            Cantidad = 1;
                            break;
                        case "Vidrio M ancho":
                            Cantidad = 1;
                            break;
                        case "Vidrio F ancho":
                            Cantidad = 1;
                            break;
                        case "Vidrio M alto":
                            Cantidad = 1;
                            break;
                        case "Vidrio F alto":
                            Cantidad = 1;
                            break;
                    }
                    break;
                case "MovilMovil":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Cantidad = 1;
                            break;
                        case "Umbral":
                            Cantidad = 1;
                            break;
                        case "Jamba":
                            Cantidad = 2;
                            break;
                        case "PisaAlfombra":
                            Cantidad = 1;
                            break;
                        case "Superior":
                            Cantidad = 2;
                            break;
                        case "Inferior":
                            Cantidad = 2;
                            break;
                        case "Vertical":
                            Cantidad = 2;
                            break;
                        case "Vertical Centro":
                            Cantidad = 2;
                            break;
                        case "Vidrio M ancho":
                            Cantidad = 2;
                            break;
                        case "Vidrio M alto":
                            Cantidad = 2;
                            break;
                        

                    }
                    break;
                case "MovilFijoMovil":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Cantidad = 1;
                            break;
                        case "Umbral":
                            Cantidad = 1;
                            break;
                        case "Jamba":
                            Cantidad = 2;
                            break;
                        case "PisaAlfombra":
                            Cantidad = 1;
                            break;
                        case "Superior":
                            Cantidad = 2;
                            break;
                        case "Inferior":
                            Cantidad = 2;
                            break;
                        case "Superior f":
                            Cantidad = 1;
                            break;
                        case "Inferior f":
                            Cantidad = 1;
                            break;
                        case "Vertical":
                            Cantidad = 2;
                            break;
                        case "Vertical Centro":
                            Cantidad = 2;
                            break;
                        case "Vertical Centro fijo":
                            Cantidad = 2;
                            break;
                        case "Vidrio M ancho":
                            Cantidad = 2;
                            break;
                        case "Vidrio F ancho":
                            Cantidad = 1;
                            break;
                        case "Vidrio M alto":
                            Cantidad = 2;
                            break;
                        case "Vidrio F alto":
                            Cantidad = 1;
                            break;

                    }
                    break;
                case "FijoMovilFijo":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Cantidad = 1;
                            break;
                        case "Umbral":
                            Cantidad = 1;
                            break;
                        case "Jamba":
                            Cantidad = 2;
                            break;
                        case "Superior":
                            Cantidad = 3;
                            break;
                        case "Inferior":
                            Cantidad = 3;
                            break;
                        case "Vertical":
                            Cantidad = 1;
                            break;
                        case "Vertical Centro":
                            Cantidad = 1;
                            break;
                        case "PisaAlfombra":
                            Cantidad = 2;
                            break;
                        case "Vertical Centro fijo":
                            Cantidad = 2;
                            break;
                        case "Vertical fijo":
                            Cantidad = 2;
                            break;
                        case "Vidrio M ancho":
                            Cantidad = 1;
                            break;
                        case "Vidrio F ancho":
                            Cantidad = 2;
                            break;
                        case "Vidrio M alto":
                            Cantidad = 1;
                            break;
                        case "Vidrio F alto":
                            Cantidad = 2;
                            break;
                    }
                    break;
                case "FijoMovilMovilFijo":
                    switch (Pieza)
                    {
                        case "Cargador":
                            Cantidad = 1;
                            break;
                        case "Umbral":
                            Cantidad = 1;
                            break;
                        case "Jamba":
                            Cantidad = 2;
                            break;
                        case "Superior":
                            Cantidad = 4;
                            break;
                        case "Inferior":
                            Cantidad = 4;
                            break;
                        case "Vertical":
                            Cantidad = 2;
                            break;
                        case "Vertical Centro":
                            Cantidad = 2;
                            break;
                        case "PisaAlfombra":
                            Cantidad = 2;
                            break;
                        case "Vertical Centro fijo":
                            Cantidad = 2;
                            break;
                        case "Vertical fijo":
                            Cantidad = 2;
                            break;
                        case "Vidrio M ancho":
                            Cantidad = 2;
                            break;
                        case "Vidrio F ancho":
                            Cantidad = 2;
                            break;
                        case "Vidrio M alto":
                            Cantidad = 2;
                            break;
                        case "Vidrio F alto":
                            Cantidad = 2;
                            break;
                    }
                    break;

            }

            return Cantidad;
        }
        #endregion
    }
}
