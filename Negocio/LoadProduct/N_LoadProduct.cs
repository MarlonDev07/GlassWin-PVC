 using AccesoDatos.Company.LoadProducts;
using Dominio.Model.ClassWindows;
using Dominio.Model.PuertaBaño;
using System;
using System.Data;
using Twilio.TwiML.Voice;

namespace Negocio.LoadProduct
{
    public class N_LoadProduct

    {
        AD_LoadProduct loadProduct = new AD_LoadProduct();
        private bool Cedazo;


        #region Cargar de DataTables 
        public bool CedazoValor(bool Cedazo) {
            return this.Cedazo = Cedazo;
        }
        public DataTable loadAluminio(string Color, string System, string supplier)
        {

            try
            {
                DataTable dt = new DataTable();
                if (ClsWindows.System == "6030 2 Vias" || ClsWindows.System == "6030 3 Vias" || ClsWindows.System == "8040 2 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "CedazoAkari")
                {
                    System = "Akari";
                }
                if (ClsWindows.System == "8025 2 Vias" || ClsWindows.System == "8025 3 Vias")
                {
                    System = "8025";
                }
                if (ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias" || ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta")
                {
                    System = "EuCorredizo";
                }
                if (ClsWindows.System == "PuertaEuAbatible" || ClsWindows.System == "Ventila Euro")
                {
                    System = "EuAbatible";
                }
                if (ClsWindows.System == "5020 3 Vias" || ClsWindows.System == "Cedazo 1/2")
                {
                    System = "5020";
                }

                //Carga el DataTable con los datos de la base de datos
                dt = loadProduct.loadAluminio(Color, System, supplier);

                //Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }

                if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }
                foreach (DataRow item in dt.Rows)
                {
                    string Description = item[0].ToString();
                    decimal Metraje = CalcMetraje(Description);
                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de las columnas en la fila actual
                    item["Metraje"] = Metraje;
                    item["TotalPrice"] = Price;
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }






        public DataTable loadAluminioDesglose(string Color, string System, string supplier)
        {

            try
            {
                DataTable dt = new DataTable();
                if (ClsWindows.System == "6030 2 Vias" || ClsWindows.System == "6030 3 Vias" || ClsWindows.System == "8040 2 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "CedazoAkari")
                {
                    System = "Akari";
                }
                if (ClsWindows.System == "8025 2 Vias" || ClsWindows.System == "8025 3 Vias")
                {
                    System = "8025";
                }
                if (ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias" || ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta")
                {
                    System = "EuCorredizo";
                }
                if (ClsWindows.System == "PuertaEuAbatible" || ClsWindows.System == "Ventila Euro")
                {
                    System = "EuAbatible";
                }
                if (ClsWindows.System == "5020 3 Vias" || ClsWindows.System == "Cedazo 1/2")
                {
                    System = "5020";
                }

                //Carga el DataTable con los datos de la base de datos
                dt = loadProduct.loadAluminioDesglose(Color, System, supplier);

                //Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }

                /*if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }*/
                foreach (DataRow item in dt.Rows)
                {
                    string Description = item[0].ToString();
                    decimal Metraje = CalcMetraje(Description);
                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    //decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de las columnas en la fila actual
                    item["Metraje"] = Metraje;
                    //item["TotalPrice"] = Price;
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }





        //Load Aluminio utilidad
        public DataTable loadAluminioUtilidad(string Color, string System, string supplier)
        {
            try
            {
                DataTable dt = new DataTable();
                if (ClsWindows.System == "6030 2 Vias" || ClsWindows.System == "6030 3 Vias" || ClsWindows.System == "8040 2 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "CedazoAkari")
                {
                    System = "Akari";
                }
                if (ClsWindows.System == "8025 2 Vias" || ClsWindows.System == "8025 3 Vias")
                {
                    System = "8025";
                }
                if (ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias" || ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta")
                {
                    System = "EuCorredizo";
                }
                if (ClsWindows.System == "PuertaEuAbatible" || ClsWindows.System == "Ventila Euro")
                {
                    System = "EuAbatible";
                }
                if (ClsWindows.System == "5020 3 Vias" || ClsWindows.System == "Cedazo 1/2")
                {
                    System = "5020";
                }

                //Carga el DataTable con los datos de la base de datos
                dt = loadProduct.loadAluminioUtilidad(Color, System, supplier);

                //Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }

                if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }

                if (!dt.Columns.Contains("TotalCost"))
                {
                    dt.Columns.Add("TotalCost", typeof(decimal));
                }
                foreach (DataRow item in dt.Rows)
                {
                    string Description = item[0].ToString();
                    decimal Metraje = CalcMetraje(Description);
                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    decimal Cost = Convert.ToDecimal(item["Cost"]);
                    decimal Price = CalcPrice(Metraje, SalePrice);
                    decimal TotalCost = Cost * Metraje;

                    // Modifica directamente el valor de las columnas en la fila actual
                    item["Metraje"] = Metraje;
                    item["TotalPrice"] = Price;
                    item["TotalCost"] = TotalCost;
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable loadAluminioVentanaFija(string Color, string System, string supplier, string Material)
        {
            try
            {
                DataTable dt = new DataTable();
                if (ClsWindows.System == "PuertaEuAbatible")
                {
                    System = "EuAbatible";
                }

                dt = loadProduct.loadAluminio(Color, System, supplier);

                // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }
                if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }

                foreach (DataRow item in dt.Rows)
                {
                    decimal Metraje = 0;
                    string Description = item[0].ToString();
                    if (ClsWindows.System == "Vidrio Fijo")
                    {
                        if (ClsWindows.Desing == "FijoGeotrica")
                        {
                            Metraje = CalclMetrajeFijoAjusteAlto(Description);
                        }
                        else if (ClsWindows.Desing == "FijoGeotricaDivision")
                        {
                            Metraje = CalclMetrajeFijoAjusteAlto1Divicion(Description);
                        }
                        else if (ClsWindows.Desing == "FijoGeotrica2Division")
                        {
                            Metraje = CalclMetrajeFijoAjusteAlto2Divicion(Description);
                        }
                        else if (ClsWindows.Desing == "FijoGeotricaInvertido")
                        {
                            Metraje = CalclMetrajeFijoAjusteAltoinvertido(Description);
                        }
                        else if (ClsWindows.Desing == "FijoGeotricaInvertidoDivision")
                        {
                            Metraje = CalclMetrajeFijoAjusteAlto1DivicionInvertido(Description);
                        }
                        else if (ClsWindows.Desing == "FijoGeotricaInvertido2Division")
                        {
                            Metraje = CalclMetrajeFijoAjusteAlto2DivicionInvertido(Description);
                        }
                        else if (ClsWindows.Desing == "FijoEscaleno")
                        {
                            Metraje = CalclMetrajeFijoEscaleno(Description);
                        }
                        else if (ClsWindows.Desing == "FijoEscalenoDivision")
                        {
                            Metraje = CalclMetrajeFijoEscaleno1Divicion(Description);
                        }
                        else if (ClsWindows.Desing == "FijoEscaleno2Division")
                        {
                            Metraje = CalclMetrajeFijoEscaleno2Divicion(Description);
                        }
                        else if (ClsWindows.Desing == "Fijo1")
                        {
                            Metraje = CalclMetrajeVidrioFijo1(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                        else if (ClsWindows.Desing == "Fijo2")
                        {
                            Metraje = CalclMetrajeVidrioFijo2(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                        else if (ClsWindows.Desing == "Fijo3")
                        {
                            Metraje = CalclMetrajeVidrioFijo3(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                        else if (ClsWindows.Desing == "FijoCelocia1")
                        {
                            Metraje = CalclMetrajeVidrioFijoCelocia1(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                        else if (ClsWindows.Desing == "FijoCelocia2")
                        {
                            Metraje = CalclMetrajeVidrioFijoCelocia2(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                        else if (ClsWindows.Desing == "FijoCelocia3")
                        {
                            Metraje = CalclMetrajeVidrioFijoCelocia3(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                        else
                        {
                            Metraje = CalculoMetrajesVentanasFijas(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                    }
                    else
                    {
                        Metraje = CalculoMetrajesVentanasFijas(Description, ClsWindows.Weight, ClsWindows.heigt, "");
                    }


                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                    item["Metraje"] = Metraje;
                    item["TotalPrice"] = Price;



                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public DataTable loadAluminioVentanaFijaDesglose(string Color, string System, string supplier, string Material)
        {
            try
            {
                DataTable dt = new DataTable();
                if (ClsWindows.System == "PuertaEuAbatible")
                {
                    System = "EuAbatible";
                }

                dt = loadProduct.loadAluminioDesglose(Color, System, supplier);

                // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }
                /*if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }*/

                foreach (DataRow item in dt.Rows)
                {
                    decimal Metraje = 0;
                    string Description = item[0].ToString();
                    if (ClsWindows.System == "Vidrio Fijo")
                    {
                        if (ClsWindows.Desing == "FijoGeotrica")
                        {
                            Metraje = CalclMetrajeFijoAjusteAlto(Description);
                        }
                        else if (ClsWindows.Desing == "FijoGeotricaDivision")
                        {
                            Metraje = CalclMetrajeFijoAjusteAlto1Divicion(Description);
                        }
                        else if (ClsWindows.Desing == "FijoGeotrica2Division")
                        {
                            Metraje = CalclMetrajeFijoAjusteAlto2Divicion(Description);
                        }
                        else if (ClsWindows.Desing == "FijoGeotricaInvertido")
                        {
                            Metraje = CalclMetrajeFijoAjusteAltoinvertido(Description);
                        }
                        else if (ClsWindows.Desing == "FijoGeotricaInvertidoDivision")
                        {
                            Metraje = CalclMetrajeFijoAjusteAlto1DivicionInvertido(Description);
                        }
                        else if (ClsWindows.Desing == "FijoGeotricaInvertido2Division")
                        {
                            Metraje = CalclMetrajeFijoAjusteAlto2DivicionInvertido(Description);
                        }
                        else if (ClsWindows.Desing == "FijoEscaleno")
                        {
                            Metraje = CalclMetrajeFijoEscaleno(Description);
                        }
                        else if (ClsWindows.Desing == "FijoEscalenoDivision")
                        {
                            Metraje = CalclMetrajeFijoEscaleno1Divicion(Description);
                        }
                        else if (ClsWindows.Desing == "FijoEscaleno2Division")
                        {
                            Metraje = CalclMetrajeFijoEscaleno2Divicion(Description);
                        }
                        else if (ClsWindows.Desing == "Fijo1")
                        {
                            Metraje = CalclMetrajeVidrioFijo1(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                        else if (ClsWindows.Desing == "Fijo2")
                        {
                            Metraje = CalclMetrajeVidrioFijo2(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                        else if (ClsWindows.Desing == "Fijo3")
                        {
                            Metraje = CalclMetrajeVidrioFijo3(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                        else if (ClsWindows.Desing == "FijoCelocia1")
                        {
                            Metraje = CalclMetrajeVidrioFijoCelocia1(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                        else if (ClsWindows.Desing == "FijoCelocia2")
                        {
                            Metraje = CalclMetrajeVidrioFijoCelocia2(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                        else if (ClsWindows.Desing == "FijoCelocia3")
                        {
                            Metraje = CalclMetrajeVidrioFijoCelocia3(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                        else
                        {
                            Metraje = CalculoMetrajesVentanasFijas(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                        }
                    }
                    else
                    {
                        Metraje = CalculoMetrajesVentanasFijas(Description, ClsWindows.Weight, ClsWindows.heigt, "");
                    }


                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    // decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                    item["Metraje"] = Metraje;
                    // item["TotalPrice"] = Price;



                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }






        public DataTable loadAluminioDesglose()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = loadProduct.loadAluminioDesglose();

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public DataTable loadAccesorioDesglose()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = loadProduct.loadAccesorioDesglose();

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public DataTable loadVidrioDesglose()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = loadProduct.loadVidrioDesglose();

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }







        public DataTable loadAccesorios(string System, string supplier)
        {
            try
            {
                DataTable dt = new DataTable();
                if (ClsWindows.System == "6030 2 Vias" || ClsWindows.System == "6030 3 Vias" || ClsWindows.System == "8040 2 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "CedazoAkari")
                {
                    System = "Akari";
                }
                if (ClsWindows.System == "8025 2 Vias" || ClsWindows.System == "8025 3 Vias")
                {
                    System = "8025";
                }
                if (ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias" || ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta")
                {
                    System = "EuCorredizo";
                }
                if (ClsWindows.System == "PuertaEuAbatible" || ClsWindows.System == "Ventila Euro")
                {
                    System = "EuAbatible";
                }
                if (ClsWindows.System == "5020 3 Vias" || ClsWindows.System == "Cedazo 1/2")
                {
                    System = "5020";
                }
                dt = loadProduct.loadAccesorios(System, supplier);

                // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }
                if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }

                if (ClsWindows.System == "EuAbatible")
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string Description = item[0].ToString();
                        decimal Metraje = CalculoMetrajesVentanasFijas(Description, ClsWindows.Weight, ClsWindows.heigt, "");
                        decimal SalePrice = Convert.ToDecimal(item[1]);
                        decimal Price = CalcPrice(Metraje, SalePrice);

                        // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                        item["Metraje"] = Metraje;
                        item["TotalPrice"] = Price;
                    }
                }
                else
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string Description = item[0].ToString();
                        decimal Metraje = CalcMetraje(Description);
                        decimal SalePrice = Convert.ToDecimal(item[1]);
                        decimal Price = CalcPrice(Metraje, SalePrice);

                        // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                        item["Metraje"] = Metraje;
                        item["TotalPrice"] = Price;
                    }
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }





        public DataTable loadAccesoriosDesglose(string System, string supplier)
        {
            try
            {
                DataTable dt = new DataTable();
                if (ClsWindows.System == "6030 2 Vias" || ClsWindows.System == "6030 3 Vias" || ClsWindows.System == "8040 2 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "CedazoAkari")
                {
                    System = "Akari";
                }
                if (ClsWindows.System == "8025 2 Vias" || ClsWindows.System == "8025 3 Vias")
                {
                    System = "8025";
                }
                if (ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias" || ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta")
                {
                    System = "EuCorredizo";
                }
                if (ClsWindows.System == "PuertaEuAbatible" || ClsWindows.System == "Ventila Euro")
                {
                    System = "EuAbatible";
                }
                if (ClsWindows.System == "5020 3 Vias" || ClsWindows.System == "Cedazo 1/2")
                {
                    System = "5020";
                }
                dt = loadProduct.loadAccesoriosDesglose(System, supplier);

                // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }
                /*if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }*/

                if (ClsWindows.System == "EuAbatible")
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string Description = item[0].ToString();
                        decimal Metraje = CalculoMetrajesVentanasFijas(Description, ClsWindows.Weight, ClsWindows.heigt, "");
                        decimal SalePrice = Convert.ToDecimal(item[1]);
                        //decimal Price = CalcPrice(Metraje, SalePrice);

                        // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                        item["Metraje"] = Metraje;
                        //item["TotalPrice"] = Price;
                    }
                }
                else
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string Description = item[0].ToString();
                        decimal Metraje = CalcMetraje(Description);
                        decimal SalePrice = Convert.ToDecimal(item[1]);
                        // decimal Price = CalcPrice(Metraje, SalePrice);

                        // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                        item["Metraje"] = Metraje;
                        //item["TotalPrice"] = Price;
                    }
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }



        //Load Accesorios Utilidad
        public DataTable loadAccesoriosUtilidad(string System, string supplier)
        {
            try
            {
                DataTable dt = new DataTable();
                if (ClsWindows.System == "6030 2 Vias" || ClsWindows.System == "6030 3 Vias" || ClsWindows.System == "8040 2 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "CedazoAkari")
                {
                    System = "Akari";
                }
                if (ClsWindows.System == "8025 2 Vias" || ClsWindows.System == "8025 3 Vias")
                {
                    System = "8025";
                }
                if (ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias" || ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta")
                {
                    System = "EuCorredizo";
                }
                if (ClsWindows.System == "PuertaEuAbatible" || ClsWindows.System == "Ventila Euro")
                {
                    System = "EuAbatible";
                }
                if (ClsWindows.System == "5020 3 Vias" || ClsWindows.System == "Cedazo 1/2")
                {
                    System = "5020";
                }
                dt = loadProduct.loadAccesoriosUtilidad(System, supplier);

                // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }
                if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }
                if (!dt.Columns.Contains("TotalCost"))
                {
                    dt.Columns.Add("TotalCost", typeof(decimal));
                }

                if (ClsWindows.System == "EuAbatible")
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string Description = item[0].ToString();
                        decimal Metraje = CalculoMetrajesVentanasFijas(Description, ClsWindows.Weight, ClsWindows.heigt, "");
                        decimal SalePrice = Convert.ToDecimal(item[1]);
                        decimal Cost = Convert.ToDecimal(item["Cost"]);
                        decimal Price = CalcPrice(Metraje, SalePrice);
                        decimal TotalCost = Cost * Metraje;

                        // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                        item["Metraje"] = Metraje;
                        item["TotalPrice"] = Price;
                        item["TotalCost"] = TotalCost;
                    }
                }
                else
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        string Description = item[0].ToString();
                        decimal Metraje = CalcMetraje(Description);
                        decimal SalePrice = Convert.ToDecimal(item[1]);
                        decimal Cost = Convert.ToDecimal(item["Cost"]);
                        decimal Price = CalcPrice(Metraje, SalePrice);
                        decimal TotalCost = Cost * Metraje;

                        // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                        item["Metraje"] = Metraje;
                        item["TotalPrice"] = Price;
                        item["TotalCost"] = TotalCost;
                    }
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable loadOnlyGlass()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.loadOnlyGlass();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataTable loadPricesGlass(string supplier, string Description)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.loadPricesGlass(supplier, Description);

                // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }
                if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }

                foreach (DataRow item in dt.Rows)
                {
                    decimal Metraje;
                    if (ClsWindows.Desing == "FijoCelocia1" || ClsWindows.Desing == "FijoCelocia2")
                    {
                        Metraje = (ClsWindows.Weight * ClsWindows.heigt) / 2;

                    } else if (ClsWindows.Desing == "FijoCelocia3")
                    {
                        Metraje = (ClsWindows.Weight / 2) * ClsWindows.heigt;
                    }
                    else
                    {
                        Metraje = ClsWindows.Weight * ClsWindows.heigt;
                    }

                    if (Metraje == 0)
                    {
                        Metraje = clsPuertaBaño.WeightTotal * clsPuertaBaño.heigt;
                    }

                    decimal SalePrice = Convert.ToDecimal(item[1]);// Asumiendo que la columna "Cost" existe y tiene valores
                    decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                    item["Metraje"] = Metraje;
                    item["TotalPrice"] = Price;
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }






        public DataTable loadPricesGlassDesglose(string supplier, string Description)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.loadPricesGlassDesglose(supplier, Description);

                // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }
                /*if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }*/

                foreach (DataRow item in dt.Rows)
                {
                    decimal Metraje = ClsWindows.Weight * ClsWindows.heigt;
                    if (Metraje == 0)
                    {
                        Metraje = clsPuertaBaño.WeightTotal * clsPuertaBaño.heigt;
                    }

                    decimal SalePrice = Convert.ToDecimal(item[1]);// Asumiendo que la columna "Cost" existe y tiene valores
                    //decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                    item["Metraje"] = Metraje;
                    //item["TotalPrice"] = Price;
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }






        //Load PriceGlass para Utilidad
        public DataTable loadPricesGlassUtilidad(string supplier, string Description)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.loadPricesGlassUtilidad(supplier, Description);

                // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }
                if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }
                if (!dt.Columns.Contains("TotalCost"))
                {
                    dt.Columns.Add("TotalCost", typeof(decimal));
                }


                foreach (DataRow item in dt.Rows)
                {
                    decimal Metraje = ClsWindows.Weight * ClsWindows.heigt;
                    if (Metraje == 0)
                    {
                        Metraje = clsPuertaBaño.WeightTotal * clsPuertaBaño.heigt;
                    }

                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    decimal Cost = Convert.ToDecimal(item["Cost"]);
                    decimal Price = CalcPrice(Metraje, SalePrice);
                    decimal TotalCost = Cost * Metraje;

                    // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                    item["Metraje"] = Metraje;
                    item["TotalPrice"] = Price;
                    item["TotalCost"] = TotalCost;
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable LoadPricesLock(string supplier, string Description)
        {
            try
            {
                DataTable dt = new DataTable();

                if (ClsWindows.System == "8025 2 Vias" || ClsWindows.System == "8025 3 Vias" || ClsWindows.System == "8040 2 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta" || ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias")
                {
                    dt = loadProduct.LoadPricesLock(supplier, Description);

                    // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                    if (!dt.Columns.Contains("Metraje"))
                    {
                        dt.Columns.Add("Metraje", typeof(decimal));
                    }
                    if (!dt.Columns.Contains("TotalPrice"))
                    {
                        dt.Columns.Add("TotalPrice", typeof(decimal));
                    }

                    foreach (DataRow item in dt.Rows)
                    {
                        decimal Metraje = CalcMetrajeLock(Description);
                        decimal SalePrice = Convert.ToDecimal(item[1]);
                        decimal Price = CalcPrice(Metraje, SalePrice);

                        // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                        item["Metraje"] = Metraje;
                        item["TotalPrice"] = Price;
                    }
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable LoadPricesLockDesglose(string supplier, string Description)
        {
            try
            {
                DataTable dt = new DataTable();

                if (ClsWindows.System == "8025 2 Vias" || ClsWindows.System == "8025 3 Vias" || ClsWindows.System == "8040 2 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta" || ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias")
                {
                    dt = loadProduct.LoadPricesLockDesglose(supplier, Description);

                    // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                    if (!dt.Columns.Contains("Metraje"))
                    {
                        dt.Columns.Add("Metraje", typeof(decimal));
                    }
                    if (!dt.Columns.Contains("TotalPrice"))
                    {
                        dt.Columns.Add("TotalPrice", typeof(decimal));
                    }

                    foreach (DataRow item in dt.Rows)
                    {
                        decimal Metraje = CalcMetrajeLock(Description);
                        decimal SalePrice = Convert.ToDecimal(item[1]);
                        decimal Price = CalcPrice(Metraje, SalePrice);

                        // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                        item["Metraje"] = Metraje;
                        item["TotalPrice"] = Price;
                    }
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }


        //Load PriceLocks Utilidades
        public DataTable LoadPricesLockUtilidades(string supplier, string Description)
        {
            try
            {
                DataTable dt = new DataTable();

                if (ClsWindows.System == "8025 2 Vias" || ClsWindows.System == "8025 3 Vias" || ClsWindows.System == "8040 2 Vias" || ClsWindows.System == "8040 3 Vias" || ClsWindows.System == "Europa 2 Vias Puerta" || ClsWindows.System == "Europa 3 Vias Puerta" || ClsWindows.System == "Europa 2 Vias" || ClsWindows.System == "Europa 3 Vias")
                {
                    dt = loadProduct.LoadPricesLockUtilidad(supplier, Description);

                    // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                    if (!dt.Columns.Contains("Metraje"))
                    {
                        dt.Columns.Add("Metraje", typeof(decimal));
                    }
                    if (!dt.Columns.Contains("TotalPrice"))
                    {
                        dt.Columns.Add("TotalPrice", typeof(decimal));
                    }
                    if (!dt.Columns.Contains("TotalCost"))
                    {
                        dt.Columns.Add("TotalCost", typeof(decimal));
                    }

                    foreach (DataRow item in dt.Rows)
                    {
                        decimal Metraje = CalcMetrajeLock(Description);
                        decimal SalePrice = Convert.ToDecimal(item[1]);
                        decimal Price = CalcPrice(Metraje, SalePrice);
                        decimal Cost = Convert.ToDecimal(item["Cost"]);
                        decimal TotalCost = Cost * Metraje;

                        // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                        item["Metraje"] = Metraje;
                        item["TotalPrice"] = Price;
                        item["TotalCost"] = TotalCost;
                    }
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public DataTable LoadPriceNewGlass(string supplier, string Description, decimal Weight, decimal Heigt)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.loadPricesGlass(supplier, Description);
                if (!dt.Columns.Contains("Metraje") || !dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }
                foreach (DataRow item in dt.Rows)
                {
                    decimal Metraje = Weight * Heigt;
                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de la columna "Metraje" en la fila actual
                    item["Metraje"] = Metraje;
                    item["TotalPrice"] = Price;
                    Console.WriteLine(item[0].ToString() + " " + Metraje.ToString() + " " + Price.ToString());
                }
                return dt;
            }
            catch (Exception)
            {

                return null;
            }
        }


        public DataTable LoadPriceNewGlassDesglose(string supplier, string Description, decimal Weight, decimal Heigt)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.loadPricesGlassDesglose(supplier, Description);
                if (!dt.Columns.Contains("Metraje")/* || !dt.Columns.Contains("TotalPrice")*/)
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                    //dt.Columns.Add("TotalPrice", typeof(decimal));
                }
                foreach (DataRow item in dt.Rows)
                {
                    decimal Metraje = Weight * Heigt;
                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    //decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de la columna "Metraje" en la fila actual
                    item["Metraje"] = Metraje;
                    //item["TotalPrice"] = Price;
                    //Console.WriteLine(item[0].ToString() + " " + Metraje.ToString() + " " + Price.ToString());
                }
                return dt;
            }
            catch (Exception)
            {

                return null;
            }
        }

        //Load PriceNweGlass Utilidad
        public DataTable LoadPriceNewGlassUtilidad(string supplier, string Description, decimal Weight, decimal Heigt)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.loadPricesGlassUtilidad(supplier, Description);
                if (!dt.Columns.Contains("Metraje") || !dt.Columns.Contains("TotalPrice") || !dt.Columns.Contains("TotalCost"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                    dt.Columns.Add("TotalCost", typeof(decimal));
                }
                foreach (DataRow item in dt.Rows)
                {
                    decimal Metraje = Weight * Heigt;
                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    decimal Cost = Convert.ToDecimal(item["Cost"]);
                    decimal Price = CalcPrice(Metraje, SalePrice);
                    decimal TotalCost = Cost * Metraje;

                    // Modifica directamente el valor de la columna "Metraje" en la fila actual
                    item["Metraje"] = Metraje;
                    item["TotalPrice"] = Price;
                    item["TotalCost"] = TotalCost;
                    Console.WriteLine(item[0].ToString() + " " + Metraje.ToString() + " " + Price.ToString());
                }
                return dt;
            }
            catch (Exception)
            {

                return null;
            }
        }


        public DataTable LoadAluminioFijo(string Color, string System, string supplier, decimal Weight, decimal Heigt, string Material, int Diviciones)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.loadAluminio(Color, System, supplier);
                if (!dt.Columns.Contains("Metraje") || !dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }
                foreach (DataRow item in dt.Rows)
                {
                    string Description = item[0].ToString();
                    decimal Metraje = CalclMetrajeVidrioFijo(Description, Weight, Heigt, Material, Diviciones);
                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de la columna "Metraje" en la fila actual
                    item["Metraje"] = Metraje;
                    item["TotalPrice"] = Price;
                    Console.WriteLine(item[0].ToString() + " " + Metraje.ToString() + " " + Price.ToString());
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }




        public DataTable LoadAluminioFijoDesglose(string Color, string System, string supplier, decimal Weight, decimal Heigt, string Material, int Diviciones)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.loadAluminioDesglose(Color, System, supplier);
                if (!dt.Columns.Contains("Metraje")/* || !dt.Columns.Contains("TotalPrice")*/)
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                    //dt.Columns.Add("TotalPrice", typeof(decimal));
                }
                foreach (DataRow item in dt.Rows)
                {
                    string Description = item[0].ToString();
                    decimal Metraje = CalclMetrajeVidrioFijo(Description, Weight, Heigt, Material, Diviciones);
                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    //decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de la columna "Metraje" en la fila actual
                    item["Metraje"] = Metraje;
                    //item["TotalPrice"] = Price;
                    //Console.WriteLine(item[0].ToString() + " " + Metraje.ToString() + " " + Price.ToString());
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }



        //Load Aluminio Fijo Utilidad
        public DataTable LoadAluminioFijoUtilidad(string Color, string System, string supplier, decimal Weight, decimal Heigt, string Material, int Diviciones)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.loadAluminioUtilidad(Color, System, supplier);
                if (!dt.Columns.Contains("Metraje") || !dt.Columns.Contains("TotalPrice") || !dt.Columns.Contains("TotalCost"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                    dt.Columns.Add("TotalPrice", typeof(decimal));

                    // Verifica y agrega las columnas si no existen
                    if (!dt.Columns.Contains("Metraje"))
                    {
                        dt.Columns.Add("Metraje", typeof(decimal));
                    }

                    if (!dt.Columns.Contains("TotalPrice"))
                    {
                        dt.Columns.Add("TotalPrice", typeof(decimal));
                    }

                    if (!dt.Columns.Contains("TotalCost"))
                    {
                        dt.Columns.Add("TotalCost", typeof(decimal));
                    }

                    // Itera sobre cada fila y calcula el valor de TotalCosts
                    foreach (DataRow row in dt.Rows)
                    {
                        // Verifica que las columnas Metraje y Cost existan y que no sean nulas
                        if (dt.Columns.Contains("Metraje") && dt.Columns.Contains("Cost") && row["Metraje"] != DBNull.Value && row["Cost"] != DBNull.Value)
                        {
                            decimal metraje = Convert.ToDecimal(row["Metraje"]);
                            decimal cost = Convert.ToDecimal(row["Cost"]);
                            row["TotalCost"] = metraje * cost;
                        }
                    }
                }


                foreach (DataRow item in dt.Rows)
                {
                    string Description = item[0].ToString();
                    decimal Metraje = CalclMetrajeVidrioFijo(Description, Weight, Heigt, Material, Diviciones);
                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    decimal Cost = Convert.ToDecimal(item["Cost"]);
                    decimal Price = CalcPrice(Metraje, SalePrice);
                    decimal TotalCost = Cost * Metraje;

                    // Modifica directamente el valor de la columna "Metraje" en la fila actual
                    item["Metraje"] = Metraje;
                    item["TotalPrice"] = Price;
                    item["TotalCost"] = TotalCost;
                    Console.WriteLine(item[0].ToString() + " " + Metraje.ToString() + " " + Price.ToString());
                }
                return dt;
            }

            catch (Exception)
            {
                return null;
            }
        }


        public DataTable LoadAluminioCedazo(string Color, string System, string supplier)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.loadAluminio(Color, System, supplier);

                // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }
                if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }

                foreach (DataRow item in dt.Rows)
                {
                    string Description = item[0].ToString();
                    decimal Metraje = CalclMetrajeCedazo(Description);
                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                    item["Metraje"] = Metraje;
                    item["TotalPrice"] = Price;
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable loadAccesoriosCedazo(string System, string supplier)
        {
            try
            {
                DataTable dt = new DataTable();

                dt = loadProduct.loadAccesorios(System, supplier);

                // Agrega las columnas "Metraje", "TotalPrice" y "TotalCost" solo si no existen
                if (!dt.Columns.Contains("Metraje"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                }
                if (!dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }

                foreach (DataRow item in dt.Rows)
                {
                    string Description = item[0].ToString();
                    decimal Metraje = CalclMetrajeCedazo(Description);
                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de las columnas "Metraje", "TotalPrice" y "TotalCost" en la fila actual
                    item["Metraje"] = Metraje;
                    item["TotalPrice"] = Price;
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable LoadAluminioVentanaFija(string Color, string System, string supplier, decimal Weight, decimal Heigt, string Material)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.loadAluminio(Color, System, supplier);
                if (!dt.Columns.Contains("Metraje") || !dt.Columns.Contains("TotalPrice"))
                {
                    dt.Columns.Add("Metraje", typeof(decimal));
                    dt.Columns.Add("TotalPrice", typeof(decimal));
                }
                foreach (DataRow item in dt.Rows)
                {
                    string Description = item[0].ToString();
                    decimal Metraje = CalculoMetrajesVentanasFijas(Description, Weight, Heigt, Material);
                    decimal SalePrice = Convert.ToDecimal(item[1]);
                    decimal Price = CalcPrice(Metraje, SalePrice);

                    // Modifica directamente el valor de la columna "Metraje" en la fila actual
                    item["Metraje"] = Metraje;
                    item["TotalPrice"] = Price;
                    Console.WriteLine(item[0].ToString() + " " + Metraje.ToString() + " " + Price.ToString());
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataTable BuscarArticulosPorDescripcion(string descripcion)
        {
            try
            {
                return loadProduct.ListaArticulosxColor2(descripcion);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable ListaArticulosxColor()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.ListaArticulosxColor();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable ListaArticulosxID(int Id)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = loadProduct.CargarProductoxID(Id);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Seleccion del Calculo
        private decimal CalculoMetrajesVentanasFijas(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal metraje = 0;
            switch (ClsWindows.Desing)
            {
                case "1Fijo":
                    metraje = CalclMetrajeFijo(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                    break;
                case "2Fijo":
                    metraje = CalclMetrajeFijoFijo(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                    break;
                case "3Fijo":
                    metraje = CalclMetrajeFijoFijoFijo(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                    break;
                case "4Fijo":
                    metraje = CalclMetrajeFijoFijoFijoFijo(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                    break;
                case "5Fijo":
                    metraje = CalclMetrajeFijoFijoFijoFijoFijo(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                    break;
                case "6Fijo":
                    metraje = CalclMetrajeFijoFijoFijoFijoFijoFijo(Description, ClsWindows.Weight, ClsWindows.heigt, Material);
                    break;
            }
            return metraje;

        }

        private decimal CalcMetraje(string Description)
        {
            decimal metraje = 0;
            switch (ClsWindows.System)
            {
                case "5020":
                    switch (ClsWindows.Desing)
                    {
                        case "FijoMovil":
                            metraje = Calc5020FijoMovil(Description);
                            break;
                        case "MovilMovil":
                            metraje = Calc5020MovilMovil(Description);
                            break;
                        case "FijoMovilFijo":
                            metraje = Calc5020FijoMovilFijo(Description);
                            break;
                        case "MovilFijoMovil":
                            metraje = Calc5020MovilFijoMovil(Description);
                            break;
                        case "FijoMovilMovilFijo":
                            metraje = Calc5020FijoMovilMovilFijo(Description);
                            break;
                    }
                    break;
                case "5020 3 Vias":
                    switch (ClsWindows.Desing)
                    {
                        case "FijoMovil":
                            metraje = Calc5020_3Vias_FijoMovil(Description);
                            break;
                        case "MovilMovil":
                            metraje = Calc5020_3Vias_MovilMovil(Description);
                            break;
                        case "FijoMovilFijo":
                            metraje = Calc5020_3Vias_FijoMovilFijo(Description);
                            break;
                        case "MovilFijoMovil":
                            metraje = Calc5020_3Vias_MovilFijoMovil(Description);
                            break;
                        case "FijoMovilMovilFijo":
                            metraje = Calc5020_3Vias_FijoMovilMovilFijo(Description);
                            break;
                    }
                    break;
                case "8025 2 Vias":
                    switch (ClsWindows.Desing)
                    {
                        case "FijoMovil":
                            metraje = Calc8025_2Vias_FijoMovil(Description);
                            break;
                        case "MovilMovil":
                            metraje = Calc8025_2Vias_MovilMovil(Description);
                            break;
                        case "FijoMovilFijo":
                            metraje = Calc8025_2Vias_FijoMovilFijo(Description);
                            break;
                        case "MovilFijoMovil":
                            metraje = Calc8025_2Vias_MovilFijoMovil(Description);
                            break;

                        case "FijoMovilMovilFijo":
                            metraje = Calc8025_2Vias_FijoMovilMovilFijo(Description);
                            break;
                        case "2ViasMovilMovilMovil":
                            metraje = Calc8025_3ViasMovilMoviMovil(Description);
                            break;
                    }
                    break;
                case "8025 3 Vias":
                    switch (ClsWindows.Desing)
                    {
                        case "FijoMovil":
                            metraje = Calc8025_3Vias_FijoMovil(Description);
                            break;
                        case "MovilMovil":
                            metraje = Calc8025_3Vias_MovilMovil(Description);
                            break;
                        case "FijoMovilFijo":
                            metraje = Calc8025_3Vias_FijoMovilFijo(Description);
                            break;
                        case "MovilFijoMovil":
                            metraje = Calc8025_3Vias_MovilFijoMovil(Description);
                            break;

                        case "FijoMovilMovilFijo":
                            metraje = Calc8025_3Vias_FijoMovilMovilFijo(Description);
                            break;

                        case "FijoMovilMovil":
                            metraje = Calc8025_3Vias_FijoMovilMovil(Description);
                            break;
                        /*case "3ViasMovilMovilMovil":
                            metraje = Calc8025_3ViasMovilMoviMovil(Description);
                            break;*/
                    }
                    break;
                case "8040 2 Vias":
                    switch (ClsWindows.Desing)
                    {
                        case "FijoMovil":
                            metraje = Calc8040_2Vias_FijoMovil(Description);
                            break;
                        case "MovilMovil":
                            metraje = Calc8040_2Vias_MovilMovil(Description);
                            break;
                        case "FijoMovilFijo":
                            metraje = Calc8040_2Vias_FijoMovilFijo(Description);
                            break;
                        case "MovilFijoMovil":
                            metraje = Calc8040_2Vias_MovilFijoMovil(Description);
                            break;
                        case "FijoMovilMovilFijo":
                            metraje = Calc8040_2Vias_FijoMovilMovilFijo(Description);
                            break;
                        case "FijoMovilMovil":
                            metraje = Calc8040_2Vias_FijoMovilMovil(Description);
                            break;
                        case "2ViasMovilMovilMovil":
                            metraje = Calc8040_2Vias_MovilMovilMovil(Description);
                            break;
                        case "2ViasFijoMovilMovilMovil":
                            metraje = Calc8040_2Vias_FijoMovilMovilMovil(Description);
                            break;
                    }
                    break;
                case "8040 3 Vias":
                    switch (ClsWindows.Desing)
                    {
                        case "FijoMovil":
                            metraje = Calc8040_3Vias_FijoMovil(Description);
                            break;
                        case "MovilMovil":
                            metraje = Calc8040_3Vias_MovilMovil(Description);
                            break;
                        case "FijoMovilFijo":
                            metraje = Calc8040_3Vias_FijoMovilFijo(Description);
                            break;
                        case "MovilFijoMovil":
                            metraje = Calc8040_3Vias_MovilFijoMovil(Description);
                            break;
                        case "FijoMovilMovilFijo":
                            metraje = Calc8040_3Vias_FijoMovilMovilFijo(Description);
                            break;
                        case "FijoMovilMovil":
                            metraje = Calc8040_3Vias_FijoMovilMovil(Description);
                            break;
                        case "FijoMovilMovilMovil":
                            metraje = Calc8040_3Vias_FijoMovilMovilMovil(Description);
                            break;
                    }
                    break;
                case "6030 2 Vias":
                    switch (ClsWindows.Desing)
                    {
                        case "FijoMovil":
                            metraje = Calc6030_2Vias_FijoMovil(Description);
                            break;
                        case "MovilMovil":
                            metraje = Calc6030_2Vias_MovilMovil(Description);
                            break;
                        case "FijoMovilFijo":
                            metraje = Calc6030_2Vias_FijoMovilFijo(Description);
                            break;
                        case "MovilFijoMovil":
                            metraje = Calc6030_2Vias_MovilFijoMovil(Description);
                            break;

                        case "FijoMovilMovilFijo":
                            metraje = Calc6030_2Vias_FijoMovilMovilFijo(Description);
                            break;
                        case "2ViasMovilMovilMovil":
                            metraje = Calc6030_2Vias_MovilMovilMovil(Description);
                            break;
                        case "FijoMovilMovilMovilMovilFijo":
                            metraje = Calc6030_2Vias_MovilMovilMovilMovilMovilMovil(Description);
                            break;
                        case "Guillotina":
                            metraje = Calc6030_2Vias_Guillotina(Description);
                            break;
                    }
                    break;
                case "6030 3 Vias":
                    switch (ClsWindows.Desing)
                    {
                        case "FijoMovil":
                            metraje = Calc6030_3Vias_FijoMovil(Description);
                            break;
                        case "MovilMovil":
                            metraje = Calc6030_3Vias_MovilMovil(Description);
                            break;
                        case "FijoMovilFijo":
                            metraje = Calc6030_3Vias_FijoMovilFijo(Description);
                            break;
                        case "MovilFijoMovil":
                            metraje = Calc6030_3Vias_MovilFijoMovil(Description);
                            break;

                        case "FijoMovilMovilFijo":
                            metraje = Calc6030_3Vias_FijoMovilMovilFijo(Description);
                            break;
                    }
                    break;
                case "Europa 2 Vias":
                    switch (ClsWindows.Desing)
                    {
                        case "FijoMovil":
                            metraje = CalcEUCorredizo_2Vias_FijoMovil(Description);
                            break;
                        case "MovilMovil":
                            metraje = CalcEUCorredizo_2Vias_MovilMovil(Description);
                            break;
                        case "FijoMovilFijo":
                            metraje = CalcEUCorredizo_2Vias_FijoMovilFijo(Description);
                            break;
                        case "MovilFijoMovil":
                            metraje = CalcEUCorredizo_2Vias_MovilFijoMovil(Description);
                            break;
                        case "FijoMovilMovilFijo":
                            metraje = CalcEUCorredizo_2Vias_FijoMovilMovilFijo(Description);
                            break;
                        case "FijoMovilMovil":
                            metraje = CalcEUCorredizo_2Vias_FijoMovilMovil(Description);
                            break;
                        case "FijoMovilMovilMovilMovilFijo":
                            metraje = CalcEUCorredizo_2Vias_FijoMovilMovilMovilMovilFijo(Description);
                            break;
                    }
                    break;
                case "Europa 3 Vias":
                    switch (ClsWindows.Desing)
                    {
                        case "FijoMovil":
                            metraje = CalcEUCorredizo_3Vias_FijoMovil(Description);
                            break;
                        case "MovilMovil":
                            metraje = CalcEUCorredizo_3Vias_MovilMovil(Description);
                            break;
                        case "FijoMovilFijo":
                            metraje = CalcEUCorredizo_3Vias_FijoMovilFijo(Description);
                            break;
                        case "MovilFijoMovil":
                            metraje = CalcEUCorredizo_3Vias_MovilFijoMovil(Description);
                            break;
                        case "FijoMovilMovilFijo":
                            metraje = CalcEUCorredizo_3Vias_FijoMovilMovilFijo(Description);
                            break;
                        case "FijoMovilMovil":
                            metraje = CalcEUCorredizo_3Vias_FijoMovilMovil(Description);
                            break;
                    }
                    break;
                case "Europa 2 Vias Puerta":
                    switch (ClsWindows.Desing)
                    {
                        case "FijoMovil":
                            metraje = CalcEUCorredizoPuerta_2Vias_FijoMovil(Description);
                            break;
                        case "MovilMovil":
                            metraje = CalcEUCorredizoPuerta_2Vias_MovilMovil(Description);
                            break;
                        case "FijoMovilFijo":
                            metraje = CalcEUCorredizoPuerta_2Vias_FijoMovilFijo(Description);
                            break;
                        case "MovilFijoMovil":
                            metraje = CalcEUCorredizoPuerta_2Vias_MovilFijoMovil(Description);
                            break;
                        case "FijoMovilMovilFijo":
                            metraje = CalcEUCorredizoPuerta_2Vias_FijoMovilMovilFijo(Description);
                            break;
                        case "FijoMovilMovil":
                            metraje = CalcEUCorredizoPuerta_2Vias_FijoMovilMovil(Description);
                            break;
                        case "FijoMovilMovilMovilMovilFijo":
                            metraje = CalcEUCorredizoPuerta_2Vias_FijoMovilMovilMovilMovilFijo(Description);
                            break;
                    }
                    break;
                case "Europa 3 Vias Puerta":
                    switch (ClsWindows.Desing)
                    {
                        case "FijoMovil":
                            metraje = CalcEUCorredizoPuerta_3Vias_FijoMovil(Description);
                            break;
                        case "MovilMovil":
                            metraje = CalcEUCorredizoPuerta_3Vias_MovilMovil(Description);
                            break;
                        case "FijoMovilFijo":
                            metraje = CalcEUCorredizoPuerta_3Vias_FijoMovilFijo(Description);
                            break;
                        case "MovilFijoMovil":
                            metraje = CalcEUCorredizoPuerta_3Vias_MovilFijoMovil(Description);
                            break;
                        case "FijoMovilMovilFijo":
                            metraje = CalcEUCorredizoPuerta_3Vias_FijoMovilMovilFijo(Description);
                            break;
                        case "FijoMovilMovil":
                            metraje = CalcEUCorredizoPuerta_3Vias_FijoMovilMovil(Description);
                            break;

                    }
                    break;
                case "Ventila":
                    switch (ClsWindows.Desing)
                    {
                        case "1 Hoja Horizontal":
                            metraje = CalcVentila1HojaHorizontal(Description);
                            break;
                        case "2 Hoja Horizontal":
                            metraje = CalcVentila2HojasHorizontal(Description);
                            break;
                        case "3 Hoja Horizontal":
                            metraje = CalcVentila3HojaHorizontal(Description);
                            break;
                        case "4 Hoja Horizontal":
                            metraje = CalcVentila4HojaHorizontal(Description);
                            break;
                        case "5 Hoja Horizontal":
                            metraje = CalcVentila5HojaHorizontal(Description);
                            break;
                        case "6 Hoja Horizontal":
                            metraje = CalcVentila6HojaHorizontal(Description);
                            break;
                        case "1 Hoja Horizontal 1Fijo":
                            metraje = CalcVentila1HojaHorizontal1Fijo(Description);
                            break;
                        case "2 Hoja Vertical":
                            metraje = CalcVentila2HojasVertical(Description);
                            break;
                        case "1 Hoja 1 Fijo Vertical":
                            metraje = CalcVentila1Hojas1FijoVertical(Description);
                            break;
                        case "3 Hoja Vertical":
                            metraje = CalcVentila3HojasVertical(Description);
                            break;
                        case "Ventila1Fijo":
                            metraje = CalcVentila1Fijo(Description);
                            break;
                        case "VerticalFijoMovilFijo":
                            metraje = CalcVentilaVerticalFijoMovilFijo(Description);
                            break;
                       
                    }
                    break;
                case "Puerta Lujo":
                    switch (ClsWindows.Desing)
                    {
                        case "1 Hoja PL":
                            metraje = CalcPuertaLujo1Hoja(Description);
                            break;
                        case "2 Hoja PL":
                            metraje = CalcPuertaLujo2Hojas(Description);
                            break;
                        case "1 Hoja Con Divicion PL":
                            metraje = CalcPuertaLujo1HojasDivicion(Description);
                            break;
                        case "2 Hoja Con Divicion PL":
                            metraje = CalcPuertaLujo2HojasDivicion(Description);
                            break;
                        case "PCorredizo 1Hoja":
                            metraje = CalcPortonCorredizoLujo1Hoja(Description);
                            break;
                        case "PCorredizo 2Hoja":
                            metraje = CalcPortonCorredizoLujo2Hoja(Description);
                            break;

                    }
                    break;
                case "CedazoAkari":
                    switch (ClsWindows.Desing)
                    {
                        case "CedazoAkariFijoMovil":
                            metraje = CalcCedazoFijoMovil(Description);
                            break;
                        case "CedazoAkariFijoMovilMovilFijo":
                            metraje = CalcCedazoFijoMovilMovilFijo(Description);
                            break;
                    }
                    break;
                case "Puerta Liviana":
                    switch (ClsWindows.Desing)
                    {
                        case "1 Hoja PL":
                            metraje = CalcPuertaLiviana1Hoja(Description);
                            break;
                        case "1 Hoja PLv":
                            metraje = CalcPuertaLiviana1Hoja(Description);
                            break;
                        case "1 Hoja Con Divicion PL":
                            metraje = CalcPuertaLiviana1Hojacondivicion(Description);
                            break;
                        case "1 Hoja Con Divicion PLv":
                            metraje = CalcPuertaLiviana1Hojacondivicion(Description);
                            break;
                    }
                    break;
                case "PuertaEuAbatible":
                    switch (ClsWindows.Desing)
                    {
                        case "1 Hoja PL":
                            metraje = CalcPuertaEuAbatible1Hoja(Description);
                            break;
                        case "1 Hoja PE":
                            metraje = CalcPuertaEuAbatible1Hoja(Description);
                            break;
                        case "1 Hoja PLv":
                            metraje = CalcPuertaEuAbatible1Hoja(Description);
                            break;
                        case "2 Hoja PL":
                            metraje = CalcPuertaEuAbatible2Hojas(Description);
                            break;
                        case "2 Hoja PE":
                            metraje = CalcPuertaEuAbatible2Hojas(Description);
                            break;
                        case "1 Hoja Con Divicion PL":
                            metraje = CalcPuertaEuAbatible1HojasConDivicion(Description);
                            break;
                        case "1 Hoja Con Divicion PE":
                            metraje = CalcPuertaEuAbatible1HojasConDivicion(Description);
                            break;
                        case "1 Hoja Con Divicion PLv":
                            metraje = CalcPuertaEuAbatible1HojasConDivicion(Description);
                            break;
                        case "2 Hoja Con Divicion PL":
                            metraje = CalcPuertaEuAbatible2HojasConDivicion(Description);
                            break;
                        case "2 Hoja Con Divicion PE":
                            metraje = CalcPuertaEuAbatible2HojasConDivicion(Description);
                            break;
                    }
                    break;
                case "Ventila Euro":
                    switch (ClsWindows.Desing)
                    {
                        case "1 Hoja Horizontal":
                            metraje = CalcVentilaEuro1HojaHorizontal(Description);
                            break;
                        case "2 Hoja Horizontal":
                            metraje = CalcVentilaEuro2HojasHorizontal(Description);
                            break;
                        case "3 Hoja Horizontal":
                            metraje = CalcVentilaEuro3HojasHorizontal(Description);
                            break;
                        case "4 Hoja Horizontal":
                            metraje = CalcVentilaEuro4HojasHorizontal(Description);
                            break;
                        case "5 Hoja Horizontal":
                            metraje = CalcVentilaEuro5HojasHorizontal(Description);
                            break;
                        case "6 Hoja Horizontal":
                            metraje = CalcVentilaEuro6HojasHorizontal(Description);
                            break;
                        case "VentilaEuroEspecialV1":
                            metraje = CalcVentilaEuroEspecialV1(Description);
                            break;

                    }
                    break;
                case "Cedazo 1/2":
                    switch (ClsWindows.Desing)
                    {
                        case "Cedazo 12":
                            metraje = MetrajeCedazoMedia(Description);
                            break;
                        case "Cedazo 1":
                            metraje = MetrajeCedazo1(Description);
                            break;
                        case "Cedazo 2":
                            metraje = MetrajeCedazo2(Description);
                            break;


                    }
                    break;

            }
            if (ClsWindows.System != null) {
                clsPuertaBaño.System = null;
            }


            switch (clsPuertaBaño.System)
            {
                case "Puerta Baño":
                    switch (clsPuertaBaño.Desing)
                    {
                        case "MovilMovil":
                            metraje = PuertaBañoMovilMovil(Description);
                            break;
                        case "PB MovilMovil":
                            metraje = PuertaBañoMovilMovil(Description);
                            break;
                        case "FijoMovilMovil":
                            metraje = PuertaBañoFijoMovilMovil(Description);
                            break;
                        case "PB FijoMovilMovil":
                            metraje = PuertaBañoFijoMovilMovil(Description);
                            break;
                    }
                    break;
            }
            return metraje;
        }

        private decimal CalcMetrajeLock(string Description)
        {
            decimal metraje = 0;
            switch (ClsWindows.Desing)
            {
                case "FijoMovil":
                    metraje = 1;
                    break;
                case "MovilMovil":
                    metraje = 2;
                    break;
                case "FijoMovilFijo":
                    metraje = 1;
                    break;
                case "MovilFijoMovil":
                    metraje = 2;
                    break;
                case "FijoMovilMovil":
                    metraje = 1;
                    break;
                case "FijoMovilMovilFijo":
                    metraje = 1;
                    break;
                case "2ViasMovilMovilMovil":
                    metraje = 2;
                    break;
            }



            return metraje;
        }

        #endregion

        #region Calculos de Metrajes de Vidrio Fijo
        private decimal CalclMetrajeVidrioFijo(string Description, decimal Weigth, decimal Heigth, string Material, int Diviciones)
        {
            decimal Metraje = 0;
            if (Material == "1x2")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 1X2":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Venilla 1/2":
                        Metraje = (Weigth * 2 + Heigth * 2) + (1 * Diviciones);
                        break;

                    case "Tubo 2 Aleta 1X2":
                        if (Diviciones > 0)
                        {
                            Metraje = Heigth * Diviciones;
                        }
                        break;
                }
            }
            if (Material == "1 3/4x3")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x3":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Venilla 1/2":
                        Metraje = (Weigth * 2 + Heigth * 2) + (1 * Diviciones);
                        break;
                    case "Tubo 2 Aleta 13/4x3":
                        if (Diviciones > 0)
                        {
                            Metraje = Heigth * Diviciones;
                        }
                        break;
                }
            }
            if (Material == "1 3/4x4")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x4":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Venilla 1/2":
                        Metraje = (Weigth * 2 + Heigth * 2) + (1 * Diviciones);
                        break;
                    case "Tubo 2 Aleta 13/4x4":
                        if (Diviciones > 0)
                        {
                            Metraje = Heigth * Diviciones;
                        }
                        break;
                }
            }
            if (Material == "Curva tapa")
            {
                switch (Description)
                {
                    case "Tapa 1 aleta vidrio fijo x11":
                        Metraje = Weigth * 1 + Heigth * 2;
                        break;
                    case "Tubo 1 Aleta 1X2":
                        Metraje = Weigth * 1;
                        break;
                    case "Venilla 1/2":
                        Metraje = (Weigth * 2 + Heigth * 2) + (1 * Diviciones);
                        break;
                    case "Tubo 2 Aleta 1X2":
                        if (Diviciones > 0)
                        {
                            Metraje = Heigth * Diviciones;
                        }
                        break;


                }
            }
            if (Material == "Curva x12")
            {
                switch (Description)
                {
                    case "Canal X12":
                        Metraje = Weigth * 1 + Heigth * 2;
                        break;
                    case "Tubo 1 Aleta 1X2":
                        Metraje = Weigth * 1;
                        break;
                    case "Venilla 1/2":
                        Metraje = (Weigth * 2 + Heigth * 2) + (1 * Diviciones);
                        break;
                    case "Tubo 2 Aleta 1X2":
                        if (Diviciones > 0)
                        {
                            Metraje = Heigth * Diviciones;
                        }
                        break;
                }

            }
            return Metraje;
        }
        #endregion

        #region Calculos de Metrajes de Ventanas Fijas
        private decimal CalclMetrajeFijo(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal Metraje = 0;
            if (ClsWindows.System == "Vidrio Fijo")
            {
                if (Material == "X12")
                {
                    switch (Description)
                    {
                        case "Tubo 1 Aleta 1X2":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Venilla 1/2":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                    }
                }
                if (ClsWindows.System == "Vidrio Fijo")

                    if (Material == "1x2")
                    {
                        switch (Description)
                        {
                            case "Tubo 1 Aleta 1X2":
                                Metraje = Weigth * 2 + Heigth * 2;
                                break;
                            case "Venilla 1/2":
                                Metraje = Weigth * 2 + Heigth * 2;
                                break;
                        }
                    }
                if (Material == "1 3/4x3")
                {
                    switch (Description)
                    {
                        case "Tubo 1 Aleta 13/4x3":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Venilla 1/2":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                    }

                }
                if (Material == "1 3/4x4")
                {
                    switch (Description)
                    {
                        case "Tubo 1 Aleta 13/4x4":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Venilla 1/2":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                    }


                }
            }
            if (ClsWindows.System == "EuAbatible")
            {
                if (Material == "")
                {
                    switch (Description)
                    {
                        //*******************Aluminio********************//
                        case "Contramarco Recto EU D102":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Envidriador Curvo 6-8mm D111":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        //*******************Accesorios********************//
                        case "Escuadra 35,9x13,9mm CJ0421F EU":
                            Metraje = 4;
                            break;
                        case "Empaque EU V-94":
                            Metraje = Weigth * 4 + Heigth * 4;
                            break;
                    }
                }
            }

            return Metraje;
        }

        private decimal CalclMetrajeFijoFijo(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal Metraje = 0;
            if (ClsWindows.System == "EuAbatible")
            {
                if (Material == "")
                {
                    switch (Description)
                    {
                        //*******************Aluminio********************//
                        case "Contramarco Recto EU D102":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Marco Division EU 26mm D10":
                            Metraje = Heigth * 1;
                            break;
                        case "Envidriador Curvo 6-8mm D111":
                            Metraje = Weigth * 2 + Heigth * 4;
                            break;
                        //*******************Accesorios********************//
                        case "Union Tope Zanca CJ4018 EU":
                            Metraje = 2;
                            break;
                        case "Escuadra 35,9x13,9mm CJ0421F EU":
                            Metraje = 4;
                            break;
                        case "Empaque EU V-94":
                            Metraje = Weigth * 4 + Heigth * 8;
                            break;
                    }
                }
            }
            if (ClsWindows.System == "Vidrio Fijo" || ClsWindows.System != "EuAbatible")
            {
                if (Material == "1x2")
                {
                    switch (Description)
                    {
                        case "Tubo 1 Aleta 1X2":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Venilla 1/2":
                            Metraje = Weigth * 2 + Heigth * 4;
                            break;
                        case "Tubo 2 Aleta 1X2":
                            Metraje = Heigth * 1;
                            break;
                    }
                }

                if (Material == "1 3/4x3")
                {
                    switch (Description)
                    {
                        case "Tubo 1 Aleta 13/4x3":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Tubo 2 Aleta 13/4x3":
                            Metraje = Heigth * 1;
                            break;
                        case "Venilla 1/2":
                            Metraje = Weigth * 2 + Heigth * 4;
                            break;
                    }

                }
                if (Material == "1 3/4x4")
                {
                    switch (Description)
                    {
                        case "Tubo 1 Aleta 13/4x4":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Tubo 2 Aleta 13/4x4":
                            Metraje = Heigth * 1;
                            break;
                        case "Venilla 1/2":
                            Metraje = Weigth * 2 + Heigth * 4;
                            break;
                    }
                }
            }



            return Metraje;
        }

        private decimal CalclMetrajeFijoFijoFijo(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal Metraje = 0;
            if (ClsWindows.System == "EuAbatible")
            {
                if (Material == "")
                {
                    switch (Description)
                    {
                        //*******************Aluminio********************//
                        case "Contramarco Recto EU D102":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Marco Division EU 26mm D10":
                            Metraje = Heigth * 2;
                            break;
                        case "Envidriador Curvo 6-8mm D111":
                            Metraje = Weigth * 2 + Heigth * 6;
                            break;
                        //*******************Accesorios********************//
                        case "Union Tope Zanca CJ4018 EU":
                            Metraje = 4;
                            break;
                        case "Escuadra 35,9x13,9mm CJ0421F EU":
                            Metraje = 4;
                            break;
                        case "Empaque EU V-94":
                            Metraje = Weigth * 4 + Heigth * 12;
                            break;
                    }
                }
            }
            if (ClsWindows.System == "Vidrio Fijo")
            {
                if (Material == "1x2")
                {
                    switch (Description)
                    {
                        case "Tubo 1 Aleta 1X2":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Venilla 1/2":
                            Metraje = Weigth * 2 + Heigth * 6;
                            break;
                        case "Tubo 2 Aleta 1X2":
                            Metraje = Heigth * 2;
                            break;
                    }
                }

                if (Material == "1 3/4x3")
                {
                    switch (Description)
                    {
                        case "Tubo 1 Aleta 13/4x3":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Tubo 2 Aleta 13/4x3":
                            Metraje = Heigth * 2;
                            break;
                        case "Venilla 1/2":
                            Metraje = Weigth * 2 + Heigth * 6;
                            break;
                    }

                }

                if (Material == "1 3/4x4")
                {
                    switch (Description)
                    {
                        case "Tubo 1 Aleta 13/4x4":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Tubo 2 Aleta 13/4x4":
                            Metraje = Heigth * 2;
                            break;
                        case "Venilla 1/2":
                            Metraje = Weigth * 2 + Heigth * 6;
                            break;
                    }
                }
            }
            return Metraje;
        }

        private decimal CalclMetrajeFijoFijoFijoFijo(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal Metraje = 0;
            if (ClsWindows.System == "EuAbatible")
            {
                if (Material == "")
                {
                    switch (Description)
                    {
                        //*******************Aluminio********************//
                        case "Contramarco Recto EU D102":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Marco Division EU 26mm D10":
                            Metraje = Heigth * 3;
                            break;
                        case "Envidriador Curvo 6-8mm D111":
                            Metraje = Weigth * 2 + Heigth * 8;
                            break;
                        //*******************Accesorios********************//
                        case "Union Tope Zanca CJ4018 EU":
                            Metraje = 6;
                            break;
                        case "Escuadra 35,9x13,9mm CJ0421F EU":
                            Metraje = 4;
                            break;
                        case "Empaque EU V-94":
                            Metraje = Weigth * 4 + Heigth * 16;
                            break;
                    }
                }
            }
            if (ClsWindows.System == "Vidrio Fijo")
            {
                if (Material == "1x2")
                {
                    switch (Description)
                    {
                        case "Tubo 1 Aleta 1X2":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Venilla 1/2":
                            Metraje = Weigth * 2 + Heigth * 8;
                            break;
                        case "Tubo 2 Aleta 1X2":
                            Metraje = Heigth * 3;
                            break;
                    }
                }

                if (Material == "1 3/4x3")
                {
                    switch (Description)
                    {
                        case "Tubo 1 Aleta 13/4x3":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Tubo 2 Aleta 13/4x3":
                            Metraje = Heigth * 3;
                            break;
                        case "Venilla 1/2":
                            Metraje = Weigth * 2 + Heigth * 8;
                            break;
                    }

                }

                if (Material == "1 3/4x4")
                {
                    switch (Description)
                    {
                        case "Tubo 1 Aleta 13/4x4":
                            Metraje = Weigth * 2 + Heigth * 2;
                            break;
                        case "Tubo 2 Aleta 13/4x4":
                            Metraje = Heigth * 3;
                            break;
                        case "Venilla 1/2":
                            Metraje = Weigth * 2 + Heigth * 8;
                            break;
                    }
                }
            }
            return Metraje;
        }

        private decimal CalclMetrajeFijoFijoFijoFijoFijo(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal Metraje = 0;
            if (Material == "1x2")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 1X2":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth * 10;
                        break;
                    case "Tubo 2 Aleta 1X2":
                        Metraje = Heigth * 4;
                        break;
                }
            }

            if (Material == "1 3/4x3")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x3":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x3":
                        Metraje = Heigth * 4;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth * 10;
                        break;
                }
            }

            if (Material == "1 3/4x4")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x4":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x4":
                        Metraje = Heigth * 4;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth * 10;
                        break;
                }
            }

            return Metraje;
        }

        private decimal CalclMetrajeFijoFijoFijoFijoFijoFijo(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal Metraje = 0;

            if (Material == "1x2")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 1X2":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth * 12;
                        break;
                    case "Tubo 2 Aleta 1X2":
                        Metraje = Heigth * 5;
                        break;
                }
            }
            if (Material == "1 3/4x3")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x3":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x3":
                        Metraje = Heigth * 5;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth * 12;
                        break;
                }
            }
            if (Material == "1 3/4x4")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x4":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x4":
                        Metraje = Heigth * 5;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth * 12;
                        break;
                }
            }

            return Metraje;

        }

        // VIDRIO FIJO (NOMBRE DE LA IMAGEN VIDRIO FIJO 1) // 
        private decimal CalclMetrajeVidrioFijo1(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal Metraje = 0;

            if (Material == "1x2")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 1X2":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 4 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 1X2":
                        Metraje = Weigth * 1;
                        break;
                }
            }
            if (Material == "1 3/4x3")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x3":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x3":
                        Metraje = Weigth * 1;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 4 + Heigth * 2;
                        break;
                }
            }
            if (Material == "1 3/4x4")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x4":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x4":
                        Metraje = Weigth * 1;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 4 + Heigth * 2;
                        break;
                }
            }

            return Metraje;
        }
        // VIDRIO FIJO (NOMBRE DE LA IMAGEN VIDRIO FIJO 2) // 
        private decimal CalclMetrajeVidrioFijo2(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal Metraje = 0;

            if (Material == "1x2")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 1X2":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 4 + Heigth * 8;
                        break;
                    case "Tubo 2 Aleta 1X2":
                        Metraje = Weigth * 1 + Heigth * 3;
                        break;
                }
            }
            if (Material == "1 3/4x3")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x3":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x3":
                        Metraje = Weigth * 1 + Heigth * 3;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 4 + Heigth * 8;
                        break;
                }
            }
            if (Material == "1 3/4x4")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x4":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x4":
                        Metraje = Weigth * 1 + Heigth * 3;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 4 + Heigth * 8;
                        break;
                }
            }

            return Metraje;
        }
        // VIDRIO FIJO (NOMBRE DE LA IMAGEN VIDRIO FIJO 3) //
        private decimal CalclMetrajeVidrioFijo3(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal Metraje = 0;

            if (Material == "1x2")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 1X2":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 4 + Heigth * 4;
                        break;
                    case "Tubo 2 Aleta 1X2":
                        Metraje = Weigth * 1 + Heigth * 1;
                        break;
                }
            }
            if (Material == "1 3/4x3")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x3":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x3":
                        Metraje = Weigth * 1 + Heigth * 1;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 4 + Heigth * 4;
                        break;
                }
            }
            if (Material == "1 3/4x4")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x4":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x4":
                        Metraje = Weigth * 1 + Heigth * 1;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 4 + Heigth * 4;
                        break;
                }
            }

            return Metraje;

        }
        // VENTANA CON CELOCIA (NOMBRE DE LA IMAGEN VIDRIO FIJO CELOCIA 1) //
        private decimal CalclMetrajeVidrioFijoCelocia1(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal Metraje = 0;

            if (Material == "1x2")
            {
                switch (Description)
                {
                    case "Canal X12":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth / 2 * 4;
                        break;
                    case "Tubo 2 Aleta 1X2":
                        Metraje = Weigth * 1 + Heigth * 1;
                        break;
                    case "Paleta 5mm claro":
                        Metraje = Math.Ceiling((Heigth / 2 / 9 * 100)) * Math.Ceiling((Weigth / Convert.ToDecimal(2.54) * 100));
                        break;
                    case "Herraje L Natural 3":
                        if (ClsWindows.heigt / 2 >= 0.00m && ClsWindows.heigt / 2 <= 0.27m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 4":
                        if (ClsWindows.heigt / 2 >= 0.28m && ClsWindows.heigt / 2 <= 0.36m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 5":
                        if (ClsWindows.heigt / 2 >= 0.37m && ClsWindows.heigt / 2 <= 0.45m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 6":
                        if (ClsWindows.heigt / 2 >= 0.46m && ClsWindows.heigt / 2 <= 0.54m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 7":
                        if (ClsWindows.heigt / 2 >= 0.55m && ClsWindows.heigt / 2 <= 0.63m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 8":
                        if (ClsWindows.heigt / 2 >= 0.64m && ClsWindows.heigt / 2 <= 0.72m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 9":
                        if (ClsWindows.heigt / 2 >= 0.73m && ClsWindows.heigt / 2 <= 0.81m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 10":
                        if (ClsWindows.heigt / 2 >= 0.82m && ClsWindows.heigt / 2 <= 0.90m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 11":
                        if (ClsWindows.heigt / 2 >= 0.91m && ClsWindows.heigt / 2 <= 0.99m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 12":
                        if (ClsWindows.heigt / 2 >= 1.00m && ClsWindows.heigt / 2 <= 1.08m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 13":
                        if (ClsWindows.heigt / 2 >= 1.08m && ClsWindows.heigt / 2 <= 1.17m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 14":
                        if (ClsWindows.heigt / 2 >= 1.18m && ClsWindows.heigt / 2 <= 1.26m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 15":
                        if (ClsWindows.heigt / 2 >= 1.27m && ClsWindows.heigt / 2 <= 1.35m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 16":
                        if (ClsWindows.heigt / 2 >= 1.36m && ClsWindows.heigt / 2 <= 1.44m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 17":
                        if (ClsWindows.heigt / 2 >= 1.45m && ClsWindows.heigt / 2 <= 1.53m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 18":
                        if (ClsWindows.heigt / 2 >= 1.54m && ClsWindows.heigt / 2 <= 1.62m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 19":
                        if (ClsWindows.heigt / 2 >= 1.63m && ClsWindows.heigt / 2 <= 1.71m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 20":
                        if (ClsWindows.heigt / 2 >= 1.72m && ClsWindows.heigt / 2 <= 1.80m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                }
            }
            if (Material == "1 3/4x3")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x3":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x3":
                        Metraje = Weigth * 1 + Heigth * 1;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth / 2 * 4;
                        break;
                }
            }
            if (Material == "1 3/4x4")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x4":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x4":
                        Metraje = Weigth * 1 + Heigth * 1;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth / 2 * 4;
                        break;
                }
            }

            return Metraje;

        }
        // VENTANA CON CELOCIA (NOMBRE DE LA IMAGEN VIDRIO FIJO CELOCIA 2) //
        private decimal CalclMetrajeVidrioFijoCelocia2(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal Metraje = 0;

            if (Material == "1x2")
            {
                switch (Description)
                {
                    case "Canal X12":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth / 2 * 2;
                        break;
                    case "Tubo 2 Aleta 1X2":
                        Metraje = Weigth * 1 + Heigth / 2 * 1;
                        break;
                    case "Paleta 5mm claro":
                        Metraje = Math.Ceiling((Heigth / 2 / 9 * 100)) * Math.Ceiling((Weigth / Convert.ToDecimal(2.54) * 100));
                        break;
                    case "Herraje L Natural 3":
                        if (ClsWindows.heigt / 2 >= 0.00m && ClsWindows.heigt / 2 <= 0.27m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 4":
                        if (ClsWindows.heigt / 2 >= 0.28m && ClsWindows.heigt / 2 <= 0.36m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 5":
                        if (ClsWindows.heigt / 2 >= 0.37m && ClsWindows.heigt / 2 <= 0.45m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 6":
                        if (ClsWindows.heigt / 2 >= 0.46m && ClsWindows.heigt / 2 <= 0.54m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 7":
                        if (ClsWindows.heigt / 2 >= 0.55m && ClsWindows.heigt / 2 <= 0.63m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 8":
                        if (ClsWindows.heigt / 2 >= 0.64m && ClsWindows.heigt / 2 <= 0.72m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 9":
                        if (ClsWindows.heigt / 2 >= 0.73m && ClsWindows.heigt / 2 <= 0.81m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 10":
                        if (ClsWindows.heigt / 2 >= 0.82m && ClsWindows.heigt / 2 <= 0.90m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 11":
                        if (ClsWindows.heigt / 2 >= 0.91m && ClsWindows.heigt / 2 <= 0.99m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 12":
                        if (ClsWindows.heigt / 2 >= 1.00m && ClsWindows.heigt / 2 <= 1.08m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 13":
                        if (ClsWindows.heigt / 2 >= 1.08m && ClsWindows.heigt / 2 <= 1.17m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 14":
                        if (ClsWindows.heigt / 2 >= 1.18m && ClsWindows.heigt / 2 <= 1.26m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 15":
                        if (ClsWindows.heigt / 2 >= 1.27m && ClsWindows.heigt / 2 <= 1.35m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 16":
                        if (ClsWindows.heigt / 2 >= 1.36m && ClsWindows.heigt / 2 <= 1.44m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 17":
                        if (ClsWindows.heigt / 2 >= 1.45m && ClsWindows.heigt / 2 <= 1.53m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 18":
                        if (ClsWindows.heigt / 2 >= 1.54m && ClsWindows.heigt / 2 <= 1.62m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 19":
                        if (ClsWindows.heigt / 2 >= 1.63m && ClsWindows.heigt / 2 <= 1.71m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 20":
                        if (ClsWindows.heigt / 2 >= 1.72m && ClsWindows.heigt / 2 <= 1.80m)
                        {
                            Metraje = 2;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                }
            }
            if (Material == "1 3/4x3")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x3":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x3":
                        Metraje = Weigth * 1 + Heigth * 1;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth / 2 * 4;
                        break;
                }
            }
            if (Material == "1 3/4x4")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x4":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x4":
                        Metraje = Weigth * 1 + Heigth * 1;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth / 2 * 4;
                        break;
                }
            }

            return Metraje;
        }
        // VENTANA CON CELOCIA (NOMBRE DE LA IMAGEN VIDRIO FIJO CELOCIA 3) //
        private decimal CalclMetrajeVidrioFijoCelocia3(string Description, decimal Weigth, decimal Heigth, string Material)
        {
            decimal Metraje = 0;

            if (Material == "1x2")
            {
                switch (Description)
                {
                    case "Canal X12":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth / 2 * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 1X2":
                        Metraje = Heigth * 1;
                        break;
                    case "Paleta 5mm claro":
                        Metraje = Math.Ceiling((Heigth / 9 * 100)) * Math.Ceiling((Weigth / Convert.ToDecimal(2.54) * 100));
                        break;
                    case "Herraje L Natural 3":
                        if (ClsWindows.heigt >= 0.00m && ClsWindows.heigt <= 0.27m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 4":
                        if (ClsWindows.heigt >= 0.28m && ClsWindows.heigt <= 0.36m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 5":
                        if (ClsWindows.heigt >= 0.37m && ClsWindows.heigt <= 0.45m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 6":
                        if (ClsWindows.heigt >= 0.46m && ClsWindows.heigt <= 0.54m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 7":
                        if (ClsWindows.heigt >= 0.55m && ClsWindows.heigt <= 0.63m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 8":
                        if (ClsWindows.heigt >= 0.64m && ClsWindows.heigt <= 0.72m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 9":
                        if (ClsWindows.heigt >= 0.73m && ClsWindows.heigt <= 0.81m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 10":
                        if (ClsWindows.heigt >= 0.82m && ClsWindows.heigt <= 0.90m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 11":
                        if (ClsWindows.heigt >= 0.91m && ClsWindows.heigt <= 0.99m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 12":
                        if (ClsWindows.heigt >= 1.00m && ClsWindows.heigt <= 1.08m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 13":
                        if (ClsWindows.heigt >= 1.08m && ClsWindows.heigt <= 1.17m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 14":
                        if (ClsWindows.heigt >= 1.18m && ClsWindows.heigt <= 1.26m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 15":
                        if (ClsWindows.heigt >= 1.27m && ClsWindows.heigt <= 1.35m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 16":
                        if (ClsWindows.heigt >= 1.36m && ClsWindows.heigt <= 1.44m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 17":
                        if (ClsWindows.heigt >= 1.45m && ClsWindows.heigt <= 1.53m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 18":
                        if (ClsWindows.heigt >= 1.54m && ClsWindows.heigt <= 1.62m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 19":
                        if (ClsWindows.heigt >= 1.63m && ClsWindows.heigt <= 1.71m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                    case "Herraje L Natural 20":
                        if (ClsWindows.heigt >= 1.72m && ClsWindows.heigt <= 1.80m)
                        {
                            Metraje = 1;
                        }
                        else
                        {
                            Metraje = 0;
                        }
                        break;
                }
            }
            if (Material == "1 3/4x3")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x3":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x3":
                        Metraje = Weigth * 1 + Heigth * 1;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth / 2 * 4;
                        break;
                }
            }
            if (Material == "1 3/4x4")
            {
                switch (Description)
                {
                    case "Tubo 1 Aleta 13/4x4":
                        Metraje = Weigth * 2 + Heigth * 2;
                        break;
                    case "Tubo 2 Aleta 13/4x4":
                        Metraje = Weigth * 1 + Heigth * 1;
                        break;
                    case "Venilla 1/2":
                        Metraje = Weigth * 2 + Heigth / 2 * 4;
                        break;
                }
            }

            return Metraje;

        }

        #endregion

        #region Calculos de Metrajes de Cedazo
        private decimal CalclMetrajeCedazo(string Descripcion)
        {
            decimal Metraje = 0;
            switch (Descripcion)
            {
                case "Marco Cedazo 1/2 Fijo":
                    Metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Empaque Cedazo 1/2":
                    Metraje = Convert.ToDecimal(((ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Escuadra Cedazo 1/2":
                    Metraje = 4;
                    break;
            }
            return Metraje;
        }

        private decimal CalcCedazoFijoMovil(string Description)
        {
            decimal Metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior - Lateral 1 Via Akari":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Contramarco Inferior 1 Via Akari":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Marco Cedazo Akari":
                    Metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    Metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                //*******************Accesorios********************//
                case "Escuadra Union Universal":
                    Metraje = 4;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    Metraje = 4;
                    break;
                case "Rodin 6030":
                    Metraje = 2;
                    break;
                case "Tapa Rodin":
                    Metraje = 2;
                    break;
                case "Fibra Cedazo Akari":
                    Metraje = (Convert.ToDecimal(ClsWindows.heigt) * 1);
                    break;
                case "Jaladera Sencilla":
                    Metraje = 2;
                    break;
                case "Empaque Cedazo Akari":
                    Metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Felpa Akari ":
                    Metraje = (Convert.ToDecimal(ClsWindows.Weight) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 5);
                    break;


            }
            return Metraje;
        }

        private decimal CalcCedazoFijoMovilMovilFijo(string Description)
        {
            decimal Metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Contramarco Inferior Akari":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Marco Cedazo Akari":
                    Metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    Metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                //*******************Accesorios********************//
                case "Escuadra Union Universal":
                    Metraje = 8;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    Metraje = 4;
                    break;
                case "Rodin 6030":
                    Metraje = 4;
                    break;
                case "Tapa Rodin":
                    Metraje = 4;
                    break;
                case "Fibra Cedazo Akari":
                    Metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Jaladera Sencilla":
                    Metraje = 4;
                    break;
                case "Empaque Cedazo Akari":
                    Metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Akari ":
                    Metraje = (Convert.ToDecimal(ClsWindows.Weight) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;

            }
            return Metraje;
        }
        #endregion

        #region 5020
        private decimal Calc5020FijoMovil(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Cargador 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Umbral 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Jamba 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical Centro 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Superior 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2) * 3;
                        break;
                    case "Inferior 5020":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) / 2;
                        break;
                    case "Marco Cedazo 1/2 Fijo":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;



                    //*******************Accesorios********************////
                    case string s when s.Contains("Cerradura"):
                        metraje = 1;
                        break;
                    case "Rodin 5020 ":
                        metraje = 2;
                        break;
                    case "Guia 5020 ":
                        metraje = 4;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Empaque U 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Felpa Universal":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Fibra Cedazo 5020":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;
                }
            }
            else {
                switch (Description)
                {
                    case "Cargador 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Umbral 5020 "://Umbral 5020
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Umbral 5020"://Umbral 5020
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Jamba 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical Centro 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Superior 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2) * 3;
                        break;
                    case "Inferior 5020":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) / 2;
                        break;



                    //*******************Accesorios********************////
                    case string s when s.Contains("Cerradura"):
                        metraje = 1;
                        break;
                    case "Rodin 5020 ":
                        metraje = 2;
                        break;
                    case "Guia 5020 ":
                        metraje = 4;
                        break;
                    case "Empaque U 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Felpa Universal":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                }
            }

            return metraje;
        }
        private decimal Calc5020MovilMovil(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {

                    case "Cargador 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Umbral 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Jamba 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical Centro 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Superior 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight)) * 1;
                        break;
                    case "Inferior 5020":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                        break;
                    case "Marco Cedazo 1/2 Fijo":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;

                    //*******************Accesorios********************//

                    case string s when s.Contains("Cerradura"):
                        metraje = 2;
                        break;
                    case "Rodin 5020 ":
                        metraje = 4;
                        break;
                    case "Guia 5020 ":
                        metraje = 4;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Empaque U 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Felpa Universal":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Fibra Cedazo 5020":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;

                }

            }
            else
            {
                switch (Description)
                {
                    case "Cargador 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Umbral 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Jamba 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical Centro 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Superior 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                        break;
                    case "Inferior 5020":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                        break;



                    //*******************Accesorios********************////
                    case string s when s.Contains("Cerradura"):
                        metraje = 2;
                        break;
                    case "Rodin 5020 ":
                        metraje = 4;
                        break;
                    case "Guia 5020 ":
                        metraje = 4;
                        break;
                    case "Empaque U 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Felpa Universal":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                }
            }



            return metraje;
        }
        private decimal Calc5020FijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {


                    case "Cargador 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Umbral 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Jamba 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Vertical Centro 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Superior 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3) * 5;
                        break;
                    case "Inferior 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3) * 1;
                        break;
                    case "Adaptador 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.heigt)) * 1;
                        break;
                    case "Marco Cedazo 1/2 Fijo":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;


                    //*******************Accesorios********************//

                    case string s when s.Contains("Cerradura"):
                        metraje = 1;
                        break;
                    case "Rodin 5020 ":
                        metraje = 2;
                        break;
                    case "Guia 5020 ":
                        metraje = 10;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Empaque U 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Felpa Universal":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Fibra Cedazo 5020":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;

                }


            }
            else
            {
                switch (Description)
                {
                    case "Cargador 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Umbral 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Jamba 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Vertical Centro 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Superior 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3) * 5;
                        break;
                    case "Inferior 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3) * 1;
                        break;
                    case "Adaptador 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.heigt)) * 1;
                        break;

                    //*******************Accesorios********************//

                    case string s when s.Contains("Cerradura"):
                        metraje = 1;
                        break;
                    case "Rodin 5020 ":
                        metraje = 2;
                        break;
                    case "Guia 5020 ":
                        metraje = 10;
                        break;
                    case "Tornillo Ensamble ":
                        metraje = 12;
                        break;
                    case "Empaque U 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Felpa Universal":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                }
            }


            return metraje;
        }
        private decimal Calc5020MovilFijoMovil(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Cargador 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Umbral 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Jamba 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical Centro 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Superior 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 6;
                        break;
                    case "Inferior 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 2;
                        break;
                    case "Marco Cedazo 1/2 Fijo":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4 * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;

                    //*******************Accesorios********************//
                    case string s when s.Contains("Cerradura"):
                        metraje = 2;
                        break;
                    case "Rodin 5020 ":
                        metraje = 4;
                        break;
                    case "Guia 5020 ":
                        metraje = 8;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 8;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4 * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Empaque U 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Felpa Universal":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Fibra Cedazo 5020":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                }


            }
            else
            {
                switch (Description)
                {
                    case "Cargador 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Umbral 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Jamba 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical Centro 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Superior 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 6;
                        break;
                    case "Inferior 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 2;
                        break;

                    //*******************Accesorios********************//
                    case string s when s.Contains("Cerradura"):
                        metraje = 2;
                        break;
                    case "Rodin 5020 ":
                        metraje = 4;
                        break;
                    case "Guia 5020 ":
                        metraje = 8;
                        break;
                    case "Tornillo Ensamble ":
                        metraje = 12;
                        break;
                    case "Empaque U 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Felpa Universal":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                }

            }

            return metraje;
        }
        private decimal Calc5020FijoMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {

                    case "Cargador 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Umbral 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Jamba 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Vertical Centro 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Superior 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 6;
                        break;
                    case "Inferior 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 2;
                        break;
                    case "Adaptador 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.heigt)) * 1;
                        break;
                    case "Marco Cedazo 1/2 Fijo":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4 * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;

                    //*******************Accesorios********************//
                    case string s when s.Contains("Cerradura"):
                        metraje = 2;
                        break;
                    case "Rodin 5020 ":
                        metraje = 4;
                        break;
                    case "Guia 5020 ":
                        metraje = 12;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 8;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4 * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Empaque U 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Felpa Universal":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Fibra Cedazo 5020":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;


                }


            }
            else
            {

                switch (Description)
                {
                    case "Cargador 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Umbral 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Jamba 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Vertical 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Vertical Centro 5020 ":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Superior 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 6;
                        break;
                    case "Inferior 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 2;
                        break;
                    case "Adaptador 5020 ":
                        metraje = (Convert.ToDecimal(ClsWindows.heigt)) * 1;
                        break;

                    //*******************Accesorios********************//
                    case string s when s.Contains("Cerradura"):
                        metraje = 2;
                        break;
                    case "Rodin 5020 ":
                        metraje = 4;
                        break;
                    case "Guia 5020 ":
                        metraje = 12;
                        break;
                    case "Tornillo de Ensamble ":
                        metraje = 16;
                        break;
                    case "Empaque U 5020":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Felpa Universal":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                }
            }


            return metraje;
        }
        #endregion

        #region 5020 3 Vias
        private decimal Calc5020_3Vias_FijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical Centro 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Superior 5020 ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2) * 3;
                    break;
                case "Inferior 5020":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) / 2;
                    break;



                //*******************Accesorios********************//
                case string s when s.Contains("Cerradura"):
                    metraje = 1;
                    break;
                case "Rodin 5020 ":
                    metraje = 2;
                    break;
                case "Guia 5020 ":
                    metraje = 4;
                    break;
                case "Tornillo Ensamble ":
                    metraje = 8;
                    break;
                case "Empaque U 5020":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Universal":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
            }
            return metraje;
        }
        private decimal Calc5020_3Vias_MovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical Centro 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Superior 5020 ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) * 1;
                    break;
                case "Inferior 5020":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;

                //*******************Accesorios********************//

                case string s when s.Contains("Cerradura"):
                    metraje = 2;
                    break;
                case "Rodin 5020 ":
                    metraje = 4;
                    break;
                case "Guia 5020 ":
                    metraje = 4;
                    break;
                case "Tornillo Ensamble":
                    metraje = 8;
                    break;
                case "Empaque U 5020":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Universal":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

            }
            return metraje;
        }

        private decimal Calc5020_3Vias_FijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Vertical Centro 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Superior 5020 ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3) * 5;
                    break;
                case "Inferior 5020":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3) * 1;
                    break;
                case "Adaptador 5020 ":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt)) * 1;
                    break;

                //*******************Accesorios********************//
                case string s when s.Contains("Cerradura"):
                    metraje = 1;
                    break;
                case "Rodin 5020 ":
                    metraje = 2;
                    break;
                case "Guia 5020 ":
                    metraje = 10;
                    break;
                case "Tornillo Ensamble ":
                    metraje = 12;
                    break;
                case "Empaque U 5020":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Universal":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

            }
            return metraje;
        }

        private decimal Calc5020_3Vias_MovilFijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Vertical Centro 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Superior 5020 ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 6;
                    break;
                case "Inferior 5020":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 2;
                    break;

                //*******************Accesorios********************//
                case string s when s.Contains("Cerradura"):
                    metraje = 2;
                    break;
                case "Rodin 5020 ":
                    metraje = 4;
                    break;
                case "Guia 5020 ":
                    metraje = 8;
                    break;
                case "Tornillo de Ensamble ":
                    metraje = 12;
                    break;
                case "Empaque U 5020":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Universal":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;

            }
            return metraje;
        }

        private decimal Calc5020_3Vias_FijoMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Vertical Centro 5020 ":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Superior 5020 ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 6;
                    break;
                case "Inferior 5020":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 2;
                    break;
                case "Adaptador 5020 ":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt)) * 1;
                    break;

                //*******************Accesorios********************//
                case string s when s.Contains("Cerradura"):
                    metraje = 2;
                    break;
                case "Rodin 5020 ":
                    metraje = 4;
                    break;
                case "Guia 5020 ":
                    metraje = 12;
                    break;
                case "Tornillo de Ensamble ":
                    metraje = 16;
                    break;
                case "Empaque U 5020":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Felpa Universal":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;

            }
            return metraje;
        }

        #endregion

        #region 8025 2 Vias
        private decimal Calc8025_2Vias_FijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical Centro 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Superior 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Inferior 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Pisalfombra 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8025":
                    metraje = 2;
                    break;
                case "Guia Inferior 8025":
                    metraje = 4;
                    break;
                case "Guia Superior 8025":
                    metraje = 4;
                    break;
                case "Jaladera Doble 8025":
                    metraje = 0;
                    break;
                case "Empaque U 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Delgada 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

            }
            return metraje;
        }
        private decimal Calc8025_2Vias_MovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical Centro 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                    ;
                case "Superior 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Inferior 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Pisalfombra 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//

                case "Rodin 8025":
                    metraje = 4;
                    break;
                case "Guia Inferior 8025":
                    metraje = 4;
                    break;
                case "Guia Superior 8025":
                    metraje = 4;
                    break;
                case "Tornillo Ensamble":
                    metraje = 8;
                    break;
                case "Jaladera Doble 8025":
                    metraje = 0;
                    break;
                case "Empaque U 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Delgada 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

            }
            return metraje;
        }
        private decimal Calc8025_2Vias_FijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Vertical Centro 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Superior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight));
                    break;
                case "Inferior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight));
                    break;
                case "Adaptador 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt));
                    break;
                case "Pisalfombra 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;


                //*******************Accesorios********************//
                case "Rodin 8025":
                    metraje = 2;
                    break;
                case "Guia Inferior 8025":
                    metraje = 6;
                    break;
                case "Guia Superior 8025":
                    metraje = 6;
                    break;
                case "Jaladera Doble 8025":
                    metraje = 0;
                    break;
                case "Tornillo Ensamble":
                    metraje = 12;
                    break;
                case "Empaque U 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Delgada 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

            }
            return metraje;
        }
        private decimal Calc8025_2Vias_MovilFijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical Centro 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Superior 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Inferior 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Pisalfombra 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                //*******************Accesorios********************//


                case "Rodin 8025":
                    metraje = 4;
                    break;
                case "Guia Inferior 8025":
                    metraje = 6;
                    break;
                case "Guia Superior 8025":
                    metraje = 6;
                    break;
                case "Jaladera Doble 8025":
                    metraje = 0;
                    break;
                case "Tornillo Ensamble":
                    metraje = 12;
                    break;
                case "Empaque U 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Delgada 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;

            }
            return metraje;
        }
        private decimal Calc8025_2Vias_FijoMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Vertical Centro 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Superior 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Inferior 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Adaptador 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt);
                    break;
                case "Pisalfombra 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8025":
                    metraje = 4;
                    break;
                case "Guia Inferior 8025":
                    metraje = 8;
                    break;
                case "Guia Superior 8025":
                    metraje = 8;
                    break;
                case "Jaladera Doble 8025":
                    metraje = 1;
                    break;
                case "Tornillo Ensamble":
                    metraje = 16;
                    break;
                case "Empaque U 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Felpa Delgada 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;

            }
            return metraje;
        }
        private decimal Calc8025_3ViasMovilMoviMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical Centro 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Superior 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Inferior 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Pisalfombra 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8025":
                    metraje = 6;
                    break;
                case "Guia Inferior 8025":
                    metraje = 6;
                    break;
                case "Guia Superior 8025":
                    metraje = 6;
                    break;
                case "Jaladera Doble 8025":
                    metraje = 1;
                    break;
                case "Tornillo Ensamble":
                    metraje = 16;
                    break;
                case "Empaque U 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Delgada 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;

            }
            return metraje;

        }

    

        #endregion

        #region 8025 3 Vias
        private decimal Calc8025_3Vias_FijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 3;
                    break;
                case "Vertical Centro 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 3;
                    break;
                case "Superior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2) * 3;
                    break;
                case "Inferior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2) * 3;
                    break;
                case "Marco Cedazo 1/2 Fijo":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Pisalfombra 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8025":
                    metraje = 4;
                    break;
                case "Guia Inferior 8025":
                    metraje = 6;
                    break;
                case "Guia Superior 8025":
                    metraje = 6;
                    break;
                case "Jaladera Doble 8025":
                    metraje = 1;
                    break;
                case "Empaque U 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Delgada 8025":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 6) + (Convert.ToDecimal(ClsWindows.heigt) * 11);
                    break;
                case "Empaque Cedazo 1/2":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Escuadra Cedazo 1/2":
                    metraje = Convert.ToDecimal(4);
                    break;
                case "Fibra Cedazo 180":
                    metraje = Convert.ToDecimal(ClsWindows.heigt);
                    break;

            }
            return metraje;
        }
        private decimal Calc8025_3Vias_MovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 3;
                    break;
                case "Vertical Centro 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 3;
                    break;
                case "Superior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2) * 3;
                    break;
                case "Inferior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2) * 3;
                    break;
                case "Marco Cedazo 1/2 Fijo":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Pisalfombra 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8025":
                    metraje = 6;
                    break;
                case "Guia Inferior 8025":
                    metraje = 6;
                    break;
                case "Guia Superior 8025":
                    metraje = 6;
                    break;
                case "Jaladera Doble 8025":
                    metraje = 1;
                    break;
                case "Felpa Delgada 8025":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 6) + (Convert.ToDecimal(ClsWindows.heigt) * 11);
                    break;
                case "Escuadra Cedazo 1/2":
                    metraje = Convert.ToDecimal(4);
                    break;
                case "Fibra Cedazo 180":
                    metraje = Convert.ToDecimal(ClsWindows.heigt);
                    break;
                case "Empaque Cedazo 1/2":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Empaque U 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
            }
            return metraje;
        }
        private decimal Calc8025_3Vias_FijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 5;
                    break;
                case "Vertical Centro 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 3;
                    break;
                case "Superior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3) * 4;
                    break;
                case "Inferior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3) * 4;
                    break;
                case "Marco Cedazo 1/2":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Pisalfombra 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Adaptador 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                    break;

                //*******************Accesorios********************//
                case "Rodin 8025":
                    metraje = 4;
                    break;
                case "Guia Inferior 8025":
                    metraje = 8;
                    break;
                case "Guia Superior 8025":
                    metraje = 8;
                    break;
                case "Jaladera Doble 8025":
                    metraje = 1;
                    break;
                case "Felpa Delgada 8025":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 11);
                    break;
                case "Empaque Cedazo 1/2":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Escuadra Cedazo 1/2":
                    metraje = Convert.ToDecimal(4);
                    break;
                case "Fibra Cedazo 180":
                    metraje = Convert.ToDecimal(ClsWindows.heigt);
                    break;
                case "Empaque U 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
            }

            return metraje;
        }
        private decimal Calc8025_3Vias_MovilFijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Vertical Centro 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 6;
                    break;
                case "Superior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 6;
                    break;
                case "Inferior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 6;
                    break;
                case "Marco Cedazo 1/2 Fijo":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Pisalfombra 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8025":
                    metraje = 8;
                    break;
                case "Guia Inferior 8025":
                    metraje = 10;
                    break;
                case "Guia Superior 8025":
                    metraje = 10;
                    break;
                case "Jaladera Doble 8025":
                    metraje = 2;
                    break;
                case "Felpa Delgada 8025":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 12) + (Convert.ToDecimal(ClsWindows.heigt) * 14);
                    break;
                case "Empaque Cedazo 1/2":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Escuadra Cedazo 1/2":
                    metraje = Convert.ToDecimal(8);
                    break;
                case "Fibra Cedazo 180":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Empaque U 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
            }

            return metraje;
        }
        private decimal Calc8025_3Vias_FijoMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 6;
                    break;
                case "Vertical Centro 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 6;
                    break;
                case "Superior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 6;
                    break;
                case "Inferior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 4) * 6;
                    break;
                case "Marco Cedazo 1/2 Fijo":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Pisalfombra 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Adaptador 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;

                //*******************Accesorios********************//
                case "Rodin 8025":
                    metraje = 8;
                    break;
                case "Guia Inferior 8025":
                    metraje = 12;
                    break;
                case "Guia Superior 8025":
                    metraje = 12;
                    break;
                case "Jaladera Doble 8025":
                    metraje = 3;
                    break;
                case "Felpa Delgada 8025":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 12) + (Convert.ToDecimal(ClsWindows.heigt) * 14);
                    break;
                case "Empaque Cedazo 1/2":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Escuadra Cedazo 1/2":
                    metraje = Convert.ToDecimal(8);
                    break;
                case "Fibra Cedazo 180":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Empaque U 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
            }

            return metraje;
        }
        private decimal Calc8025_3Vias_FijoMovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Cargador 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Cargador 8025 2 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Umbral 8025 2 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Jamba 8025 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Jamba 8025 2 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Vertical 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 3;
                    break;
                case "Vertical Centro 8025":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 7;
                    break;
                case "Superior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3) * 5;
                    break;
                case "Inferior 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3) * 5;
                    break;
                case "Marco Cedazo 1/2 Fijo":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Pisalfombra 8025":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8025":
                    metraje = 8;
                    break;
                case "Guia Inferior 8025":
                    metraje = 10;
                    break;
                case "Guia Superior 8025":
                    metraje = 10;
                    break;
                case "Jaladera Doble 8025":
                    metraje = 1;
                    break;
                case "Felpa Delgada 8025":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 15) + (Convert.ToDecimal(ClsWindows.heigt) * 19);
                    break;
                case "Empaque Cedazo 1/2":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Escuadra Cedazo 1/2":
                    metraje = Convert.ToDecimal(8);
                    break;
                case "Fibra Cedazo 180":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Empaque U 8025":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
            }

            return metraje;

        }
        #endregion

        #region 8040 2 Vias
        private decimal Calc8040_2Vias_FijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 2;
                    break;
                case "Escuadra Union Universal":
                    metraje = 8;
                    break;
                case "Escuadra Contramarco 2 Vias Akari":
                    metraje = 4;
                    break;
                case "Tapa Rodin":
                    metraje = 2;
                    break;
                case "Jaladera Sencilla":
                    metraje = 0;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                    break;
                case "Botaguas Akari":
                    metraje = 3;
                    break;
                case "Corta Vientos":
                    metraje = 4;
                    break;
            }
            return metraje;
        }
        private decimal Calc8040_2Vias_MovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 4;
                    break;
                case "Escuadra Union Universal":
                    metraje = 8;
                    break;
                case "Escuadra Contramarco 2 Vias Akari":
                    metraje = 4;
                    break;
                case "Botaguas Akari":
                    metraje = 3;
                    break;
                case "Corta Vientos":
                    metraje = 4;
                    break;
                case "Tapa Rodin":
                    metraje = 4;
                    break;
                case "Jaladera Sencilla":
                    metraje = 0;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                    break;
            }
            return metraje;
        }
        private decimal Calc8040_2Vias_FijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Adaptador Marco Akari D74":
                    metraje = Convert.ToDecimal(ClsWindows.heigt);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 2;
                    break;
                case "Escuadra Union Universal":
                    metraje = 12;
                    break;
                case "Escuadra Contramarco 2 Vias Akari":
                    metraje = 4;
                    break;
                case "Botaguas Akari":
                    metraje = 4;
                    break;
                case "Corta Vientos":
                    metraje = 4;
                    break;
                case "Tapa Rodin":
                    metraje = 2;
                    break;
                case "Jaladera Sencilla":
                    metraje = 0;
                    break;
                case "Tornillo Ensamble":
                    metraje = 8;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                    break;

            }
            return metraje;
        }
        private decimal Calc8040_2Vias_MovilFijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 4;
                    break;
                case "Escuadra Union Universal":
                    metraje = 12;
                    break;
                case "Escuadra Contramarco 2 Vias Akari":
                    metraje = 4;
                    break;
                case "Botaguas Akari":
                    metraje = 4;
                    break;
                case "Corta Vientos":
                    metraje = 8;
                    break;
                case "Tapa Rodin":
                    metraje = 4;
                    break;
                case "Jaladera Sencilla":
                    metraje = 0;
                    break;
                case "Tornillo Ensamble":
                    metraje = 8;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;

            }
            return metraje;
        }
        private decimal Calc8040_2Vias_FijoMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Adaptador Marco Akari D74":
                    metraje = Convert.ToDecimal(ClsWindows.heigt);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 4;
                    break;
                case "Escuadra Union Universal":
                    metraje = 16;
                    break;
                case "Escuadra Contramarco 2 Vias Akari":
                    metraje = 4;
                    break;
                case "Botaguas Akari":
                    metraje = 4;
                    break;
                case "Corta Vientos":
                    metraje = 8;
                    break;
                case "Tapa Rodin":
                    metraje = 4;
                    break;
                case "Jaladera Sencilla":
                    metraje = 0;
                    break;
                case "Tornillo Ensamble":
                    metraje = 8;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                    break;

            }
            return metraje;
        }
        private decimal Calc8040_2Vias_FijoMovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 4;
                    break;
                case "Escuadra Union Universal":
                    metraje = 12;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 4;
                    break;
                case "Corta Vientos":
                    metraje = 8;
                    break;
                case "Tapa Rodin":
                    metraje = 4;
                    break;
                case "Jaladera Sencilla":
                    metraje = 0;
                    break;
                case "Tornillo Ensamble":
                    metraje = 8;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 12) + (Convert.ToDecimal(ClsWindows.heigt) * 16);
                    break;

            }
            return metraje;
        }
        private decimal Calc8040_2Vias_MovilMovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 6;
                    break;
                case "Escuadra Union Universal":
                    metraje = 12;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 4;
                    break;
                case "Corta Vientos":
                    metraje = 8;
                    break;
                case "Tapa Rodin":
                    metraje = 6;
                    break;
                case "Jaladera Sencilla":
                    metraje = 0;
                    break;
                case "Tornillo Ensamble":
                    metraje = 8;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 12) + (Convert.ToDecimal(ClsWindows.heigt) * 16);
                    break;

            }
            return metraje;

        }
        private decimal Calc8040_2Vias_FijoMovilMovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 6;
                    break;
                case "Escuadra Union Universal":
                    metraje = 12;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 4;
                    break;
                case "Corta Vientos":
                    metraje = 8;
                    break;
                case "Tapa Rodin":
                    metraje = 6;
                    break;
                case "Jaladera Sencilla":
                    metraje = 0;
                    break;
                case "Tornillo Ensamble":
                    metraje = 8;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 12) + (Convert.ToDecimal(ClsWindows.heigt) * 16);
                    break;

            }
            return metraje;

        }
        #endregion

        #region 8040 3 Vias
        private decimal Calc8040_3Vias_FijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 3);
                    break;


                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 2;
                    break;
                case "Rodin 6030":
                    metraje = 2;
                    break;
                case "Escuadra Union Universal":
                    metraje = 12;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 3;
                    break;
                case "Corta Vientos":
                    metraje = 8;
                    break;
                case "Tapa Rodin":
                    metraje = 4;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 12) + (Convert.ToDecimal(ClsWindows.heigt) * 15);
                    break;
                case "Fibra Cedazo Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2) * Convert.ToDecimal(ClsWindows.heigt);
                    break;
                case "Empaque Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Jaladera Sencilla":
                    metraje = 2;
                    break;



            }
            return metraje;
        }
        private decimal Calc8040_3Vias_MovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 3);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 4;
                    break;
                case "Rodin 6030":
                    metraje = 2;
                    break;
                case "Escuadra Union Universal":
                    metraje = 12;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 3;
                    break;
                case "Corta Vientos":
                    metraje = 8;
                    break;
                case "Tapa Rodin":
                    metraje = 4;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 12) + (Convert.ToDecimal(ClsWindows.heigt) * 15);
                    break;
                case "Fibra Cedazo Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2) * Convert.ToDecimal(ClsWindows.heigt);
                    break;
                case "Empaque Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Jaladera Sencilla":
                    metraje = 2;
                    break;




            }
            return metraje;
        }
        private decimal Calc8040_3Vias_FijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Adaptador Marco Akari D74":
                    metraje = Convert.ToDecimal(ClsWindows.heigt);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 3);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 2;
                    break;
                case "Rodin 6030":
                    metraje = 2;
                    break;
                case "Escuadra Union Universal":
                    metraje = 16;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 3;
                    break;
                case "Corta Vientos":
                    metraje = 8;
                    break;
                case "Tapa Rodin":
                    metraje = 4;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
                case "Fibra Cedazo Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 1);
                    break;
                case "Empaque Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Jaladera Sencilla":
                    metraje = 2;
                    break;


            }
            return metraje;
        }
        private decimal Calc8040_3Vias_MovilFijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 4;
                    break;
                case "Rodin 6030":
                    metraje = 4;
                    break;
                case "Escuadra Union Universal":
                    metraje = 20;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 3;
                    break;
                case "Corta Vientos":
                    metraje = 12;
                    break;
                case "Tapa Rodin":
                    metraje = 8;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
                case "Fibra Cedazo Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Empaque Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Jaladera Sencilla":
                    metraje = 4;
                    break;


            }
            return metraje;
        }
        private decimal Calc8040_3Vias_FijoMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Adaptador Marco Akari D74":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;

                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 4;
                    break;
                case "Rodin 6030":
                    metraje = 4;
                    break;
                case "Escuadra Union Universal":
                    metraje = 24;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 3;
                    break;
                case "Corta Vientos":
                    metraje = 12;
                    break;
                case "Tapa Rodin":
                    metraje = 8;
                    break;
                case "Jaladera Sencilla":
                    metraje = 6;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Felpa Akari ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 8 + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
                case "Fibra Cedazo Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Empaque Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;


            }
            return metraje;
        }
        private decimal Calc8040_3Vias_FijoMovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Contramarco Superior-Lateral Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Marco Cedazo Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3 * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;


                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 4;
                    break;
                case "Rodin 6030":
                    metraje = 4;
                    break;
                case "Escuadra Union Universal":
                    metraje = 20;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Escuadra Contramarco 2 Vias Akari":
                    metraje = 4;
                    break;
                case "Botaguas Akari":
                    metraje = 4;
                    break;
                case "Corta Vientos":
                    metraje = 12;
                    break;
                case "Tapa Rodin":
                    metraje = 8;
                    break;
                case "Jaladera Sencilla":
                    metraje = 2;
                    break;
                case "Tornillo Ensamble":
                    metraje = 8;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 20) + (Convert.ToDecimal(ClsWindows.heigt) * 26);
                    break;
                case "Fibra Cedazo Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Empaque Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;


            }
            return metraje;
        }

        private decimal Calc8040_3Vias_FijoMovilMovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Contramarco Superior-Lateral Akari 2 Vias":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight)) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 2 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 8040":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Rampa Akari 8040":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;


                //*******************Accesorios********************//
                case "Rodin 8040":
                    metraje = 6;
                    break;
                case "Escuadra Union Universal":
                    metraje = 16;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Escuadra Contramarco 2 Vias Akari":
                    metraje = 4;
                    break;
                case "Botaguas Akari":
                    metraje = 4;
                    break;
                case "Corta Vientos":
                    metraje = 12;
                    break;
                case "Tapa Rodin":
                    metraje = 6;
                    break;
                case "Jaladera Sencilla":
                    metraje = 2;
                    break;
                case "Tornillo Ensamble":
                    metraje = 8;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 20) + (Convert.ToDecimal(ClsWindows.heigt) * 26);
                    break;



            }
            return metraje;
        }
        #endregion

        #region 6030 2 Vias
        private decimal Calc6030_2Vias_FijoMovil(string Description)
        {
            decimal metraje = 0;

            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Marco Cedazo 1/2":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) / 2 * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;


                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 2;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 8;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 3;
                        break;
                    case "Corta Vientos":
                        metraje = 4;
                        break;
                    case "Tapa Rodin":
                        metraje = 2;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 8 + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 1;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 4;
                         break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) /2 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo 180":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;


                }
                return metraje;
            }
            else 
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    


                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 2;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 8;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 3;
                        break;
                    case "Corta Vientos":
                        metraje = 4;
                        break;
                    case "Tapa Rodin":
                        metraje = 2;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 8 + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 1;
                        break;

                }
                return metraje;
            }

            
        }
        private decimal Calc6030_2Vias_MovilMovil(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Marco Cedazo 1/2":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) / 2 * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;

                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 4;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 8;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 3;
                        break;
                    case "Corta Vientos":
                        metraje = 4;
                        break;
                    case "Tapa Rodin":
                        metraje = 4;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 8 + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 2;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo 180":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;

                }
                return metraje;
            }
            else 
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 4;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 8;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 3;
                        break;
                    case "Corta Vientos":
                        metraje = 4;
                        break;
                    case "Tapa Rodin":
                        metraje = 4;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 8 + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 2;
                        break;

                }
                return metraje;
            }
           
        }
        private decimal Calc6030_2Vias_FijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Adaptador Hoja Akari":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Adaptador Marco Akari D74":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;
                    case "Marco Cedazo 1/2":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) / 2 * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;


                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 2;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 12;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 3;
                        break;
                    case "Corta Vientos":
                        metraje = 4;
                        break;
                    case "Tapa Rodin":
                        metraje = 2;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 8 + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 1;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo 180":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;

                }
                return metraje;
            }
            else
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Adaptador Hoja Akari":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Adaptador Marco Akari D74":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;


                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 2;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 12;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 3;
                        break;
                    case "Corta Vientos":
                        metraje = 4;
                        break;
                    case "Tapa Rodin":
                        metraje = 2;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 8 + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 1;
                        break;

                }
                return metraje;
            }
           
        }
        private decimal Calc6030_2Vias_MovilFijoMovil(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Marco Cedazo 1/2":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) / 2 * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 4;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 12;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 3;
                        break;
                    case "Corta Vientos":
                        metraje = 8;
                        break;
                    case "Tapa Rodin":
                        metraje = 4;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 8 + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 2;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo 180":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;

                }
                return metraje;
            }
            else
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 4;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 12;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 3;
                        break;
                    case "Corta Vientos":
                        metraje = 8;
                        break;
                    case "Tapa Rodin":
                        metraje = 4;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 8 + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 2;
                        break;

                }
                return metraje;
            }
           
        }
        private decimal Calc6030_2Vias_FijoMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Adaptador Marco Akari D74":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;
                    case "Marco Cedazo 1/2":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) / 2 * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 4;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 16;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 4;
                        break;
                    case "Corta Vientos":
                        metraje = 8;
                        break;
                    case "Tapa Rodin":
                        metraje = 4;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 8 + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 2;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo 180":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;

                }
                return metraje;
            }
            else
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Adaptador Marco Akari D74":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;
                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 4;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 16;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 4;
                        break;
                    case "Corta Vientos":
                        metraje = 8;
                        break;
                    case "Tapa Rodin":
                        metraje = 4;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 8 + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 2;
                        break;

                }
                return metraje;
            }
           
        }
        private decimal Calc6030_2Vias_MovilMovilMovil(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari 3 Vias":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari 3 Vias":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Marco Cedazo 1/2":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) / 2 * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;


                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 6;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 12;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 3;
                        break;
                    case "Corta Vientos":
                        metraje = 4;
                        break;
                    case "Tapa Rodin":
                        metraje = 6;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 12 + (Convert.ToDecimal(ClsWindows.heigt) * 16);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 2;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo 180":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;

                }
                return metraje;
            }
            else
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari 3 Vias":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari 3 Vias":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;



                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 6;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 12;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 3;
                        break;
                    case "Corta Vientos":
                        metraje = 4;
                        break;
                    case "Tapa Rodin":
                        metraje = 6;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 12 + (Convert.ToDecimal(ClsWindows.heigt) * 16);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 2;
                        break;

                }
                return metraje;
            }
           
        }
        private decimal Calc6030_2Vias_MovilMovilMovilMovilMovilMovil(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari 3 Vias":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari 3 Vias":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 8;
                        break;
                    case "Marco Cedazo 1/2":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) / 2 * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;


                    //*******************Accesorios********************//
                    case "Rodin 6030":
                        metraje = 12;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 24;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 8;
                        break;
                    case "Botaguas Akari":
                        metraje = 6;
                        break;
                    case "Corta Vientos":
                        metraje = 16;
                        break;
                    case "Tapa Rodin":
                        metraje = 12;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 12 + (Convert.ToDecimal(ClsWindows.heigt) * 20);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 2;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) / 2 * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo 180":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;

                }
                return metraje;
            }
            else
            {
                switch (Description)
                {
                    case "Contramarco Superior-Lateral Akari 3 Vias":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Contramarco Inferior Akari 3 Vias":
                        metraje = Convert.ToDecimal(ClsWindows.Weight);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 8;
                        break;



                    //*******************Accesorios********************//
                    case "Balansin":
                        metraje = 12;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 24;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 8;
                        break;
                    case "Botaguas Akari":
                        metraje = 6;
                        break;
                    case "Corta Vientos":
                        metraje = 16;
                        break;
                    case "Tapa Rodin":
                        metraje = 12;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 12 + (Convert.ToDecimal(ClsWindows.heigt) * 20);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 2;
                        break;

                }
                return metraje;
            }
           

        }
                // VENTANA GUILLOTINA FM //
        private decimal Calc6030_2Vias_Guillotina(string Description)
        {
            decimal metraje = 0;

            if (Cedazo)
            {
                switch (Description)
                {
                    
                    case "Contramarco Inferior Akari":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Marco Cedazo 1/2":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) / 2 * 2);
                        break;


                    //*******************Accesorios********************//
                    case "Balansin":
                        metraje = 2;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 8;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 2;
                        break;
                    case "Corta Vientos":
                        metraje = 4;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 10 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 1;
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) /2 * 2);
                        break;
                    case "Fibra Cedazo 180":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;


                }
                return metraje;
            }
            else
            {
                switch (Description)
                {
                    
                    case "Contramarco Inferior Akari":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                    case "Marco Hoja 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Marco Hoja Enganche 6030":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;



                    //*******************Accesorios********************//
                    case "Balansin":
                        metraje = 2;
                        break;
                    case "Escuadra Union Universal":
                        metraje = 8;
                        break;
                    case "Escuadra Contramarco 2 Vias Akari":
                        metraje = 4;
                        break;
                    case "Botaguas Akari":
                        metraje = 2;
                        break;
                    case "Corta Vientos":
                        metraje = 4;
                        break;
                    case "Empaque Akari":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Felpa Akari ":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 10 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Cerradura Impacto Akari":
                        metraje = 1;
                        break;

                }
                return metraje;
            }


        }


        #endregion

        #region 6030 3 Vias
        private decimal Calc6030_3Vias_FijoMovil(string Description)
            {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 6030":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Enganche 6030":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Marco Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;

                //*******************Accesorios********************//
                case "Rodin 6030":
                    metraje = 4;
                    break;
                case "Escuadra Union Universal":
                    metraje = 12;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 3;
                    break;
                case "Corta Vientos":
                    metraje = 4;
                    break;
                case "Tapa Rodin":
                    metraje = 4;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Akari ":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 12) + (Convert.ToDecimal(ClsWindows.heigt) * 16);
                    break;
                case "Cerradura Impacto Akari":
                    metraje = 2;
                    break;
                case "Fibra Cedazo Akari":
                    metraje = Convert.ToDecimal(ClsWindows.heigt);
                    break;
                case "Empaque Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;

            }
            return metraje;
        }
        private decimal Calc6030_3Vias_MovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 6030":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Enganche 6030":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Marco Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;

                //*******************Accesorios********************//
                case "Rodin 6030":
                    metraje = 6;
                    break;
                case "Escuadra Union Universal":
                    metraje = 12;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 3;
                    break;
                case "Corta Vientos":
                    metraje = 4;
                    break;
                case "Tapa Rodin":
                    metraje = 6;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Akari ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 12 + (Convert.ToDecimal(ClsWindows.heigt) * 15);
                    break;
                case "Cerradura Impacto Akari":
                    metraje = 3;
                    break;
                case "Fibra Cedazo Akari":
                    metraje = Convert.ToDecimal(ClsWindows.heigt);
                    break;
                case "Empaque Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
            }
            return metraje;
        }
        private decimal Calc6030_3Vias_FijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 6030":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Marco Hoja Enganche 6030":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Adaptador Marco Akari D74":
                    metraje = Convert.ToDecimal(ClsWindows.heigt);
                    break;
                case "Marco Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;

                //*******************Accesorios********************//
                case "Rodin 6030":
                    metraje = 4;
                    break;
                case "Escuadra Union Universal":
                    metraje = 16;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 3;
                    break;
                case "Corta Vientos":
                    metraje = 4;
                    break;
                case "Tapa Rodin":
                    metraje = 4;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Akari ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 12 + (Convert.ToDecimal(ClsWindows.heigt) * 15);
                    break;
                case "Cerradura Impacto Akari":
                    metraje = 2;
                    break;
                case "Fibra Cedazo Akari":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                case "Empaque Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
            }
            return metraje;
        }
        private decimal Calc6030_3Vias_MovilFijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 6030":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Enganche 6030":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Marco Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;

                //*******************Accesorios********************//
                case "Rodin 6030":
                    metraje = 8;
                    break;
                case "Escuadra Union Universal":
                    metraje = 20;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 3;
                    break;
                case "Corta Vientos":
                    metraje = 8;
                    break;
                case "Tapa Rodin":
                    metraje = 8;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Felpa Akari ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 12 + (Convert.ToDecimal(ClsWindows.heigt) * 18);
                    break;
                case "Cerradura Impacto Akari":
                    metraje = 4;
                    break;
                case "Fibra Cedazo Akari":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Empaque Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
            }
            return metraje;
        }
        private decimal Calc6030_3Vias_FijoMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco Superior-Lateral Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco Inferior Akari 3 Vias":
                    metraje = Convert.ToDecimal(ClsWindows.Weight);
                    break;
                case "Marco Hoja 6030":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Marco Hoja Enganche 6030":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Marco Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Adaptador Enganche-Porta Felpa":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Adaptador Marco Akari D74":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;

                    break;

                //*******************Accesorios********************//
                case "Rodin 6030":
                    metraje = 8;
                    break;
                case "Escuadra Union Universal":
                    metraje = 24;
                    break;
                case "Escuadra Contramarco 3 Vias Akari":
                    metraje = 8;
                    break;
                case "Botaguas Akari":
                    metraje = 4;
                    break;
                case "Corta Vientos":
                    metraje = 8;
                    break;
                case "Tapa Rodin":
                    metraje = 8;
                    break;
                case "Empaque Akari":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Felpa Akari ":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 12 + (Convert.ToDecimal(ClsWindows.heigt) * 18);
                    break;
                case "Cerradura Impacto Akari":
                    metraje = 4;
                    break;
                case "Fibra Cedazo Akari":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Empaque Cedazo Akari":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
            }
            return metraje;
        }

        #endregion

        #region EUCorredizo 2 Vias
        private decimal CalcEUCorredizo_2Vias_FijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 8;
                    break;
                case "Escuadra 35,9x13,9mm CJ0421F EU":
                    metraje = 4;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 16;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 1;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU ":
                    metraje = 3;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

            }
            return metraje;
        }

        private decimal CalcEUCorredizo_2Vias_MovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 2;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 2;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 8;
                    break;
                case "Escuadra 35,9x13,9mm CJ0421F EU":
                    metraje = 4;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 16;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 1;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU ":
                    metraje = 3;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizo_2Vias_FijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Adaptador EU D116":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 1;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 12;
                    break;
                case "Escuadra 35,9x13,9mm CJ0421F EU":
                    metraje = 4;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 24;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 2;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 2;
                    break;
                case "Desague Normal VA18 EU ":
                    metraje = 3;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizo_2Vias_MovilFijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 2;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 2;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 12;
                    break;
                case "Escuadra 35,9x13,9mm CJ0421F EU":
                    metraje = 4;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 24;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 2;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU ":
                    metraje = 4;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
            }
            return metraje;

        }

        private decimal CalcEUCorredizo_2Vias_FijoMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Adaptador Hoja EU D116":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 2;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 16;
                    break;
                case "Escuadra 35,9x13,9mm CJ0421F EU":
                    metraje = 4;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 32;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 3;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU ":
                    metraje = 4;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizo_2Vias_FijoMovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 1;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 20;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 24;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 3;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU ":
                    metraje = 4;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizo_2Vias_FijoMovilMovilMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 8;
                    break;
                case "Adaptador Hoja EU D116":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 2;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 32;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 48;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 5;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU ":
                    metraje = 8;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 14);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
            }
            return metraje;
        }

        #endregion

        #region EUCorredizo 3 Vias
        private decimal CalcEUCorredizo_3Vias_FijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Cedazo Corredizo EU D117":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 20;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 24;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 2;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Rueda Regulable P/P70 MEN-1RNP EU":
                    metraje = 2;
                    break;
                case "Felpa EU":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 12) + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Empaque Cedazo Europa":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Fibra de Cedazo 180 Europa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt)) * 1;
                    break;
                case "Haladera Sencilla SL 135-1-BK EU":
                    metraje = 2;
                    break;

            }
            return metraje;
        }

        private decimal CalcEUCorredizo_3Vias_MovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Cedazo Corredizo EU D117":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 2;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 20;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 24;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 2;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Rueda Regulable P/P70 MEN-1RNP EU":
                    metraje = 2;
                    break;
                case "Felpa EU":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 12) + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Empaque Cedazo Europa":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Fibra de Cedazo 180 Europa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt)) * 1;
                    break;
                case "Haladera Sencilla SL 135-1-BK EU":
                    metraje = 2;
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizo_3Vias_FijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Cedazo Corredizo EU D117":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Adaptador EU D116":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                case "Acople Cedazo EU D125":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 24;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 32;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 3;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 6;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Rueda Regulable P/P70 MEN-1RNP EU":
                    metraje = 2;
                    break;
                case "Felpa EU":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 16) + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Empaque Cedazo Europa":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Fibra de Cedazo 180 Europa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt)) * 1;
                    break;
                case "Haladera Sencilla SL 135-1-BK EU":
                    metraje = 2;
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizo_3Vias_MovilFijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Cedazo Corredizo EU D117":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 2;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 28;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 32;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 4;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 6;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Rueda Regulable P/P70 MEN-1RNP EU":
                    metraje = 4;
                    break;
                case "Felpa EU":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 20) + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Empaque Cedazo Europa":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Fibra de Cedazo 180 Europa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Haladera Sencilla SL 135-1-BK EU":
                    metraje = 4;
                    break;
            }
            return metraje;

        }

        private decimal CalcEUCorredizo_3Vias_FijoMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Cedazo Corredizo EU D117":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Adaptador EU D116":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                case "Acople Cedazo EU D125":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 32;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 48;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 4;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 8;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Rueda Regulable P/P70 MEN-1RNP EU":
                    metraje = 4;
                    break;
                case "Felpa EU":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 24) + (Convert.ToDecimal(ClsWindows.heigt) * 18);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Empaque Cedazo Europa":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Fibra de Cedazo 180 Europa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Haladera Sencilla SL 135-1-BK EU":
                    metraje = 4;
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizo_3Vias_FijoMovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco EU 2 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Cedazo Corredizo EU D117":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 1;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 28;
                    break;
                case "Escuadra 35.9x13.9mm CJ0421F EU":
                    metraje = 4;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 40;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 4;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 6;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Rueda Regulable P/P70 MEN-1RNP EU":
                    metraje = 4;
                    break;
                case "Felpa EU":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 20) + (Convert.ToDecimal(ClsWindows.heigt) * 14);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Empaque Cedazo Europa":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Fibra de Cedazo 180 Europa":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3) * (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Haladera Sencilla SL 135-1-BK EU":
                    metraje = 2;
                    break;
            }
            return metraje;
        }

        #endregion

        #region EUCorredizo Puerta 2 Vias
        private decimal CalcEUCorredizoPuerta_2Vias_FijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 8;
                    break;
                case "Escuadra 35,9x13,9mm CJ0421F EU":
                    metraje = 4;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 16;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 1;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

            }
            return metraje;
        }

        private decimal CalcEUCorredizoPuerta_2Vias_MovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 2;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 8;
                    break;
                case "Escuadra 35,9x13,9mm CJ0421F EU":
                    metraje = 4;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 16;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 1;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizoPuerta_2Vias_FijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Adaptador EU D116":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 12;
                    break;
                case "Escuadra 35,9x13,9mm CJ0421F EU":
                    metraje = 4;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 24;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 2;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 2;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizoPuerta_2Vias_MovilFijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 2;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 12;
                    break;
                case "Escuadra 35,9x13,9mm CJ0421F EU":
                    metraje = 4;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 24;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 2;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 6;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 4;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
            }
            return metraje;

        }

        private decimal CalcEUCorredizoPuerta_2Vias_FijoMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Adaptador Hoja EU D116":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 16;
                    break;
                case "Escuadra 35,9x13,9mm CJ0421F EU":
                    metraje = 4;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 32;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 3;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 8;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 4;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizoPuerta_2Vias_FijoMovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 20;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 24;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 3;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 6;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 4;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizoPuerta_2Vias_FijoMovilMovilMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 2 Vias D112":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 8;
                    break;
                case "Adaptador Hoja EU D116":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 32;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 48;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 5;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 8;
                    break;
                case "Felpa EU":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 14);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
            }
            return metraje;
        }

        #endregion

        #region EUCorredizo Puerta 3 Vias
        private decimal CalcEUCorredizoPuerta_3Vias_FijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Cedazo Corredizo EU D117":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 20;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 24;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 2;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Rueda Regulable P/P70 MEN-1RNP EU":
                    metraje = 2;
                    break;
                case "Felpa EU":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 12) + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Empaque Cedazo Europa":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Fibra de Cedazo Europa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt)) * 1;
                    break;
                case "Haladera Sencilla SL 135-1-BK EU":
                    metraje = 2;
                    break;

            }
            return metraje;
        }

        private decimal CalcEUCorredizoPuerta_3Vias_MovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Cedazo Corredizo EU D117":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 2;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 20;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 24;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 2;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Rueda Regulable P/P70 MEN-1RNP EU":
                    metraje = 2;
                    break;
                case "Felpa EU":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 12) + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Empaque Cedazo Europa":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 2) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Fibra de Cedazo Europa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt)) * 1;
                    break;
                case "Haladera Sencilla SL 135-1-BK EU":
                    metraje = 2;
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizoPuerta_3Vias_FijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Cedazo Corredizo EU D117":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Adaptador EU D116":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                case "Acople Cedazo EU D125":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 24;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 32;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 3;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 6;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Rueda Regulable P/P70 MEN-1RNP EU":
                    metraje = 2;
                    break;
                case "Felpa EU":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 16) + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Empaque Cedazo Europa":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Fibra de Cedazo Europa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt)) * 1;
                    break;
                case "Haladera Sencilla SL 135-1-BK EU":
                    metraje = 2;
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizoPuerta_3Vias_MovilFijoMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Cedazo Corredizo EU D117":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 2;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 28;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 40;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 5;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 6;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Rueda Regulable P/P70 MEN-1RNP EU":
                    metraje = 4;
                    break;
                case "Felpa EU":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 20) + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Empaque Cedazo Europa":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Fibra de Cedazo Europa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Haladera Sencilla SL 135-1-BK EU":
                    metraje = 4;
                    break;
            }
            return metraje;

        }

        private decimal CalcEUCorredizoPuerta_3Vias_FijoMovilMovilFijo(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Cedazo Corredizo EU D117":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Adaptador EU D116":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                case "Acople Cedazo EU D125":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 32;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 48;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 4;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 8;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Rueda Regulable P/P70 MEN-1RNP EU":
                    metraje = 4;
                    break;
                case "Felpa EU":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 24) + (Convert.ToDecimal(ClsWindows.heigt) * 18);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Empaque Cedazo Europa":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Fibra de Cedazo Europa":
                    metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Haladera Sencilla SL 135-1-BK EU":
                    metraje = 4;
                    break;
            }
            return metraje;
        }

        private decimal CalcEUCorredizoPuerta_3Vias_FijoMovilMovil(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Contramarco EU 3 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Contramarco EU 2 Vias D119":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Marco Hoja Corredizo EU D113":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Envidriador Hoja Corredizo EU D114":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Conector Enganche EU D115":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Cedazo Corredizo EU D117":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

                //*******************Accesorios********************//
                case "Kit Gancho Perimetral":
                    metraje = 1;
                    break;
                case "Cierre Embutir C/tornillo SL8726-BK EU":
                    metraje = 0;
                    break;
                case "Escuadra 23x14mm CJ0410F EU":
                    metraje = 28;
                    break;
                case "Escuadra 35.9x13.9mm CJ0421F EU":
                    metraje = 4;
                    break;
                case "Escuadra Alineamiento 7x0,5 EAX-705 EU":
                    metraje = 40;
                    break;
                case "Kit perimetral KP70C EU":
                    metraje = 4;
                    break;
                case "Tandem Roller Zink Men-1T EU":
                    metraje = 4;
                    break;
                case "Desague Normal VA18 EU":
                    metraje = 3;
                    break;
                case "Rueda Regulable P/P70 MEN-1RNP EU":
                    metraje = 4;
                    break;
                case "Felpa EU":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 4) * 20) + (Convert.ToDecimal(ClsWindows.heigt) * 14);
                    break;
                case "Empaque Precion 9mm":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                    break;
                case "Empaque Cedazo Europa":
                    metraje = ((Convert.ToDecimal(ClsWindows.Weight) / 3) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Fibra de Cedazo Europa":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) / 3) * (Convert.ToDecimal(ClsWindows.heigt) * 2);
                    break;
                case "Haladera Sencilla SL 135-1-BK EU":
                    metraje = 2;
                    break;
            }
            return metraje;
        }

        #endregion

        #region Ventila
        private decimal CalcVentila1HojaHorizontal(string Description)
        {
            decimal metraje = 0;
           if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Marco Cedazo 1/2":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;


                //*******************Accesorios********************//
                    case "Barra Telescopica VT":
                        metraje = 1;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 4;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Escuadra Cedazo 1/2":
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo VT":
                        metraje = (Convert.ToDecimal(ClsWindows.heigt) * 1);
                        break;

                }


           }
           else {
               switch (Description)
               {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 1;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 4;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                }

            }
            return metraje;
        }

        private decimal CalcVentila2HojasHorizontal(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {

                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Marco Cedazo 1/2": //Cedazo
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;

                    //*******************Accesorios********************//
                    case "Barra Telescopica VT":
                        metraje = 2;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 8;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 16);
                        break;
                    case "Escuadra Cedazo 1/2"://Cedazo
                        metraje = 8;
                        break;
                    case "Empaque Cedazo 1/2"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                    case "Fibra Cedazo VT"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                }
                return metraje;
            }
            else 
            {
                switch (Description)
                {

                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 2;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 8;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 16);
                        break;
                }
                return metraje;
            }

           
        }
        private decimal CalcVentila3HojaHorizontal(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 6;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 6;
                        break;
                    case "Marco Cedazo 1/2": //Cedazo
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 6;
                        break;

                    //*******************Accesorios********************//
                    case "Barra Telescopica VT":
                        metraje = 3;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 12;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 24);
                        break;
                    case "Escuadra Cedazo 1/2"://Cedazo
                        metraje = 12;
                        break;
                    case "Empaque Cedazo 1/2"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                    case "Fibra Cedazo VT"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.heigt) * 3);
                        break;
                }
                return metraje;
            }
            else 
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 6;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 6;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 3;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 12;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 24);
                        break;
                }
                return metraje;
            }
           
        }
        private decimal CalcVentila4HojaHorizontal(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 3;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 8;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 8;
                        break;
                    case "Marco Cedazo 1/2": //Cedazo
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 8;
                        break;

                    //*******************Accesorios********************//
                    case "Barra Telescopica VT":
                        metraje = 4;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 4;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 4;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 4;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 4;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 4;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 16;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 32);
                        break;
                    case "Escuadra Cedazo 1/2"://Cedazo
                        metraje = 16;
                        break;
                    case "Empaque Cedazo 1/2"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Fibra Cedazo VT"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;
                }
                return metraje;
            }
            else 
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 3;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 8;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 8;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 4;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 4;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 4;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 4;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 4;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 4;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 16;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 32);
                        break;
                }
                return metraje;
            }
            
        }
        private decimal CalcVentila5HojaHorizontal(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 10;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 10;
                        break;
                    case "Marco Cedazo 1/2": //Cedazo
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 10;
                        break;

                    //*******************Accesorios********************//
                    case "Barra Telescopica VT":
                        metraje = 5;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 5;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 5;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 5;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 5;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 5;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 20;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 40);
                        break;
                    case "Escuadra Cedazo 1/2"://Cedazo
                        metraje = 20;
                        break;
                    case "Empaque Cedazo 1/2"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 10);
                        break;
                    case "Fibra Cedazo VT"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.heigt) * 5);
                        break;
                }
                return metraje;
            }
            else 
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 10;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 10;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 5;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 5;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 5;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 5;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 5;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 5;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 20;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 40);
                        break;
                }
                return metraje;
            }
           
        }
        private decimal CalcVentila6HojaHorizontal(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 5;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 12;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 12;
                        break;
                    case "Marco Cedazo 1/2": //Cedazo
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 12;
                        break;

                    //*******************Accesorios********************//
                    case "Barra Telescopica VT":
                        metraje = 6;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 6;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 6;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 6;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 6;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 6;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 24;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 48);
                        break;
                    case "Escuadra Cedazo 1/2"://Cedazo
                        metraje = 24;
                        break;
                    case "Empaque Cedazo 1/2"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 12);
                        break;
                    case "Fibra Cedazo VT"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.heigt) * 6);
                        break;
                }
                return metraje;
            }
            else 
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 5;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 12;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 12;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 6;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 6;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 6;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 6;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 6;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 6;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 24;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 48);
                        break;
                }
                return metraje;
            }

          
        }
        private decimal CalcVentila1HojaHorizontal1Fijo(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = (ClsWindows.AnchoVentila * 2) + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Marco Cedazo 1/2": //Cedazo
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 1;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 4;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 16);
                        break;
                    case "Escuadra Cedazo 1/2"://Cedazo
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo VT"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.heigt) * 1);
                        break;
                }
                return metraje;
            }
            else 
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = (ClsWindows.AnchoVentila * 2) + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 1;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.25m && ClsWindows.heigt <= 0.40m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.41m && ClsWindows.heigt <= 0.55m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 0.56m && ClsWindows.heigt <= 0.65m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 0.66m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 0.81m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 4;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 16);
                        break;
                }
                return metraje;
            }

            

            //***********Ventilas Verticales************//
        }
        private decimal CalcVentila2HojasVertical(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Marco Cedazo 1/2": //Cedazo
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Barra Telescopica VT":
                        metraje = 2;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.50m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.80m && ClsWindows.heigt <= 1.10m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt / 2 >= 1.11m && ClsWindows.heigt <= 1.30m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt / 2 >= 1.31m && ClsWindows.heigt <= 1.60m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 1.60m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 8;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 16) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Escuadra Cedazo 1/2"://Cedazo
                        metraje = 8;
                        break;
                    case "Empaque Cedazo 1/2"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo VT"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.heigt) * 1);
                        break;

                }
                return metraje;
            }
            else 
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 2;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.50m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.80m && ClsWindows.heigt <= 1.10m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt / 2 >= 1.11m && ClsWindows.heigt <= 1.30m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt / 2 >= 1.31m && ClsWindows.heigt <= 1.60m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 1.60m)
                        {
                            metraje = 2;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 8;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 16) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;

                }
                return metraje;
            }
           

        }
        private decimal CalcVentila1Hojas1FijoVertical(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) / 2 * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Marco Cedazo 1/2": //Cedazo
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 1;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.50m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.80m && ClsWindows.heigt <= 1.10m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt / 2 >= 1.11m && ClsWindows.heigt <= 1.30m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt / 2 >= 1.31m && ClsWindows.heigt <= 1.60m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 1.60m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 4;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 16) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Escuadra Cedazo 1/2"://Cedazo
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo VT"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.heigt) * 1);
                        break;

                }
                return metraje;
            }
            else 
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) / 2 * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 1;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.50m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.80m && ClsWindows.heigt <= 1.10m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt / 2 >= 1.11m && ClsWindows.heigt <= 1.30m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt / 2 >= 1.31m && ClsWindows.heigt <= 1.60m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 1.60m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 4;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 16) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;

                }
                return metraje;
            }
           
        }
        private decimal CalcVentila3HojasVertical(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 6 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 6 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Marco Cedazo 1/2": //Cedazo
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 6 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 3;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.50m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.80m && ClsWindows.heigt <= 1.10m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 1.11m && ClsWindows.heigt <= 1.30m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 1.31m && ClsWindows.heigt <= 1.60m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 1.60m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 12;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 24) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Escuadra Cedazo 1/2"://Cedazo
                        metraje = 12;
                        break;
                    case "Empaque Cedazo 1/2"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 6) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo VT"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.heigt) * 1);
                        break;

                }
                return metraje;
            }
            else 
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 6 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 6 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 3;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.50m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.80m && ClsWindows.heigt <= 1.10m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 1.11m && ClsWindows.heigt <= 1.30m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 1.31m && ClsWindows.heigt <= 1.60m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 1.60m)
                        {
                            metraje = 3;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 12;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 24) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;

                }
                return metraje;
            }
           
        }
        // Ventila Vertical (NOMBRE DE LA IMAGEN VENTILA VERTICAL FIJO MOVIL FIJO) //
        private decimal CalcVentilaVerticalFijoMovilFijo(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) / 3 * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 6 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Marco Cedazo 1/2": //Cedazo
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 1;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.50m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.80m && ClsWindows.heigt <= 1.10m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 1.11m && ClsWindows.heigt <= 1.30m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 1.31m && ClsWindows.heigt <= 1.60m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 1.60m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 4;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 16) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Escuadra Cedazo 1/2"://Cedazo
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo VT"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.heigt) * 1);
                        break;

                }
                return metraje;
            }
            else 
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) / 3 * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 6 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 1;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.50m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.80m && ClsWindows.heigt <= 1.10m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 1.11m && ClsWindows.heigt <= 1.30m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 1.31m && ClsWindows.heigt <= 1.60m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 1.60m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 4;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 16) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;

                }
                return metraje;
            }
           

        }
        // Ventila Vertical (NOMBRE DE LA IMAGEN VENTILA VERTICAL FIJO MOVIL FIJO) //
        private decimal CalcVentilaEspecialV1(string Description)
        {
            decimal metraje = 0;
            if (Cedazo)
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) / 3 * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 6 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Marco Cedazo 1/2": //Cedazo
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 1;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.50m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.80m && ClsWindows.heigt <= 1.10m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 1.11m && ClsWindows.heigt <= 1.30m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 1.31m && ClsWindows.heigt <= 1.60m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 1.60m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 4;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 16) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;
                    case "Escuadra Cedazo 1/2"://Cedazo
                        metraje = 4;
                        break;
                    case "Empaque Cedazo 1/2"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 2) + (Convert.ToDecimal(ClsWindows.heigt) * 2);
                        break;
                    case "Fibra Cedazo VT"://Cedazo
                        metraje = (Convert.ToDecimal(ClsWindows.heigt) * 1);
                        break;

                }
                return metraje;
            }
            else
            {
                switch (Description)
                {
                    case "Contramarco VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;
                    case "Jamba VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Divicion VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Marco Hoja VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) / 3 * 2;
                        break;
                    case "Envidriador VT M338":
                        metraje = Convert.ToDecimal(ClsWindows.Weight) * 6 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    //*******************Accesorios********************//
                    case "Cierre VT":
                        metraje = 1;
                        break;
                    case "Cremona 8":
                        if (ClsWindows.heigt >= 0.50m && ClsWindows.heigt <= 0.80m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 10":
                        if (ClsWindows.heigt >= 0.80m && ClsWindows.heigt <= 1.10m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 12":
                        if (ClsWindows.heigt >= 1.11m && ClsWindows.heigt <= 1.30m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 14":
                        if (ClsWindows.heigt >= 1.31m && ClsWindows.heigt <= 1.60m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Cremona 16":
                        if (ClsWindows.heigt >= 1.60m)
                        {
                            metraje = 1;
                        }
                        else
                        {
                            metraje = 0;
                        }
                        break;
                    case "Angular 2X2X1/4":
                        metraje = 4;
                        break;
                    case "Empaque VT":
                        metraje = (Convert.ToDecimal(ClsWindows.Weight) * 16) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;

                }
                return metraje;
            }


        }

        // FIJO EN MARCO DE VENTILA// 
        private decimal CalcVentila1Fijo(string Description)
        {
            decimal metraje = 0;

            switch (Description)
            {
                case "Contramarco VT M338":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Jamba VT M338":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                    break;
                case "Envidriador VT M338":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;

                //*******************Accesorios********************//

                case "Angular 2X2X1/4":
                    metraje = 12;
                    break;
                case "Empaque VT":
                    metraje = (Convert.ToDecimal(ClsWindows.Weight) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

            }
            return metraje;
        }

        #endregion

        #region Ventila Euro
        private decimal CalcVentilaEuro1HojaHorizontal(string Descripcion)
        {
            decimal Metraje = 0;
            decimal W = Convert.ToDecimal(ClsWindows.Weight);
            decimal H = Convert.ToDecimal(ClsWindows.heigt);
            switch (Descripcion)
            {
                //Aluminio
                case "Contramarco Recto EU D102":
                    Metraje = W * 2 + H * 2;
                    break;
                case "Marco AP EXT Ventana EU":
                    Metraje = W * 2 + H * 2;
                    break;
                case "Envidriador Curvo 6-8mm D111":
                    Metraje = W * 2 + H * 2;
                    break;
                //Accesorios
                case "Empaque EU V-94":
                    Metraje = W * 4 + H * 4;
                    break;

                case "Empaque Alfin Lengueta":
                    Metraje = W * 4 + H * 4;
                    break;

                case "Escuadra 35.9x13.9mm CJ0421F":
                    Metraje = 4;
                    break;

                case "Escuadra De Bloqueo Cent 15mm CJ2637-15":
                    Metraje = 4;
                    break;

                case "Manija Negra OH507-BK":
                    Metraje = 1;
                    break;

                case "Compas p/ Camara EU 25cm":
                    if (H >= 0.25m && H <= 0.45m)
                    {
                        Metraje = 1;
                    }
                    break;

                case "Compas P/ Camara EU 45cm":
                    if (H >= 0.46m && H <= 1.00m)
                    {
                        Metraje = 1;
                    }
                    break;
            }
            return Metraje;
        }
        private decimal CalcVentilaEuro2HojasHorizontal(string Descripcion)
        {
            decimal Metraje = 0;
            decimal W = Convert.ToDecimal(ClsWindows.Weight);
            decimal H = Convert.ToDecimal(ClsWindows.heigt);
            switch (Descripcion)
            {
                //Aluminio
                case "Contramarco Recto EU D102":
                    Metraje = W * 2 + H * 2;
                    break;
                case "Marco AP EXT Ventana EU":
                    Metraje = W * 2 + H * 4;
                    break;
                case "Envidriador Curvo 6-8mm D111":
                    Metraje = W * 2 + H * 4;
                    break;
                case "Marco Divisor EU 43mm":
                    Metraje = H * 1;
                    break;
                //Accesorios
                case "Empaque EU V-94":
                    Metraje = W * 4 + H * 4;
                    break;

                case "Empaque Alfin Lengueta":
                    Metraje = W * 4 + H * 8;
                    break;

                case "Escuadra 35.9x13.9mm CJ0421F":
                    Metraje = 4;
                    break;

                case "Escuadra De Bloqueo Cent 15mm CJ2637-15":
                    Metraje = 8;
                    break;

                case "Manija Negra OH507-BK":
                    Metraje = 2;
                    break;

                case "Compas p/ Camara EU 25cm":
                    if (H >= 0.25m && H <= 0.45m)
                    {
                        Metraje = 2;
                    }
                    break;

                case "Compas P/ Camara EU 45cm":
                    if (H >= 0.46m && H <= 1.00m)
                    {
                        Metraje = 2;
                    }
                    break;
            }
            return Metraje;
        }
        private decimal CalcVentilaEuro3HojasHorizontal(string Descripcion)
        {
            decimal Metraje = 0;
            decimal W = Convert.ToDecimal(ClsWindows.Weight);
            decimal H = Convert.ToDecimal(ClsWindows.heigt);
            switch (Descripcion)
            {
                //Aluminio
                case "Contramarco Recto EU D102":
                    Metraje = W * 2 + H * 2;
                    break;
                case "Marco AP EXT Ventana EU":
                    Metraje = W * 2 + H * 6;
                    break;
                case "Envidriador Curvo 6-8mm D111":
                    Metraje = W * 2 + H * 6;
                    break;
                case "Marco Divisor EU 43mm":
                    Metraje = H * 2;
                    break;
                //Accesorios
                case "Empaque EU V-94":
                    Metraje = W * 4 + H * 4;
                    break;

                case "Empaque Alfin Lengueta":
                    Metraje = W * 4 + H * 12;
                    break;

                case "Escuadra 35.9x13.9mm CJ0421F":
                    Metraje = 4;
                    break;

                case "Escuadra De Bloqueo Cent 15mm CJ2637-15":
                    Metraje = 12;
                    break;

                case "Manija Negra OH507-BK":
                    Metraje = 3;
                    break;

                case "Compas p/ Camara EU 25cm":
                    if (H >= 0.25m && H <= 0.45m)
                    {
                        Metraje = 3;
                    }
                    break;

                case "Compas P/ Camara EU 45cm":
                    if (H >= 0.46m && H <= 1.00m)
                    {
                        Metraje = 3;
                    }
                    break;
            }
            return Metraje;
        }
        private decimal CalcVentilaEuro4HojasHorizontal(string Descripcion)
        {
            decimal Metraje = 0;
            decimal W = Convert.ToDecimal(ClsWindows.Weight);
            decimal H = Convert.ToDecimal(ClsWindows.heigt);
            switch (Descripcion)
            {
                //Aluminio
                case "Contramarco Recto EU D102":
                    Metraje = W * 2 + H * 2;
                    break;
                case "Marco AP EXT Ventana EU":
                    Metraje = W * 2 + H * 8;
                    break;
                case "Envidriador Curvo 6-8mm D111":
                    Metraje = W * 2 + H * 8;
                    break;
                case "Marco Divisor EU 43mm":
                    Metraje = H * 3;
                    break;
                //Accesorios
                case "Empaque EU V-94":
                    Metraje = W * 4 + H * 4;
                    break;

                case "Empaque Alfin Lengueta":
                    Metraje = W * 4 + H * 16;
                    break;

                case "Escuadra 35.9x13.9mm CJ0421F":
                    Metraje = 4;
                    break;

                case "Escuadra De Bloqueo Cent 15mm CJ2637-15":
                    Metraje = 16;
                    break;

                case "Manija Negra OH507-BK":
                    Metraje = 4;
                    break;

                case "Compas p/ Camara EU 25cm":
                    if (H >= 0.25m && H <= 0.45m)
                    {
                        Metraje = 4;
                    }
                    break;

                case "Compas P/ Camara EU 45cm":
                    if (H >= 0.46m && H <= 1.00m)
                    {
                        Metraje = 4;
                    }
                    break;
            }
            return Metraje;
        }
        private decimal CalcVentilaEuro5HojasHorizontal(string Descripcion)
        {
            decimal Metraje = 0;
            decimal W = Convert.ToDecimal(ClsWindows.Weight);
            decimal H = Convert.ToDecimal(ClsWindows.heigt);
            switch (Descripcion)
            {
                //Aluminio
                case "Contramarco Recto EU D102":
                    Metraje = W * 2 + H * 2;
                    break;
                case "Marco AP EXT Ventana EU":
                    Metraje = W * 2 + H * 10;
                    break;
                case "Envidriador Curvo 6-8mm D111":
                    Metraje = W * 2 + H * 10;
                    break;
                case "Marco Divisor EU 43mm":
                    Metraje = H * 4;
                    break;
                //Accesorios
                case "Empaque EU V-94":
                    Metraje = W * 4 + H * 4;
                    break;

                case "Empaque Alfin Lengueta":
                    Metraje = W * 4 + H * 20;
                    break;

                case "Escuadra 35.9x13.9mm CJ0421F":
                    Metraje = 4;
                    break;

                case "Escuadra De Bloqueo Cent 15mm CJ2637-15":
                    Metraje = 20;
                    break;

                case "Manija Negra OH507-BK":
                    Metraje = 5;
                    break;

                case "Compas p/ Camara EU 25cm":
                    if (H >= 0.25m && H <= 0.45m)
                    {
                        Metraje = 5;
                    }
                    break;

                case "Compas P/ Camara EU 45cm":
                    if (H >= 0.46m && H <= 1.00m)
                    {
                        Metraje = 5;
                    }
                    break;
            }
            return Metraje;
        }
        private decimal CalcVentilaEuro6HojasHorizontal(string Descripcion)
        {
            decimal Metraje = 0;
            decimal W = Convert.ToDecimal(ClsWindows.Weight);
            decimal H = Convert.ToDecimal(ClsWindows.heigt);
            switch (Descripcion)
            {
                //Aluminio
                case "Contramarco Recto EU D102":
                    Metraje = W * 2 + H * 2;
                    break;
                case "Marco AP EXT Ventana EU":
                    Metraje = W * 2 + H * 12;
                    break;
                case "Envidriador Curvo 6-8mm D111":
                    Metraje = W * 2 + H * 12;
                    break;
                case "Marco Divisor EU 43mm":
                    Metraje = H * 5;
                    break;
                //Accesorios
                case "Empaque EU V-94":
                    Metraje = W * 4 + H * 4;
                    break;

                case "Empaque Alfin Lengueta":
                    Metraje = W * 4 + H * 24;
                    break;

                case "Escuadra 35.9x13.9mm CJ0421F":
                    Metraje = 4;
                    break;

                case "Escuadra De Bloqueo Cent 15mm CJ2637-15":
                    Metraje = 24;
                    break;

                case "Manija Negra OH507-BK":
                    Metraje = 6;
                    break;

                case "Compas p/ Camara EU 25cm":
                    if (H >= 0.25m && H <= 0.45m)
                    {
                        Metraje = 6;
                    }
                    break;

                case "Compas P/ Camara EU 45cm":
                    if (H >= 0.46m && H <= 1.00m)
                    {
                        Metraje = 6;
                    }
                    break;
            }
            return Metraje;
        }
        private decimal CalcVentilaEuroEspecialV1(string Descripcion)
        {
            decimal Metraje = 0;
            decimal W = Convert.ToDecimal(ClsWindows.Weight);
            decimal H = Convert.ToDecimal(ClsWindows.heigt);
            decimal WV = Convert.ToDecimal(ClsWindows.WeightV);
            decimal HV = Convert.ToDecimal(ClsWindows.heigtV);

            switch (Descripcion)
            {
                //Aluminio
                case "Contramarco Recto EU D102":
                    Metraje = W * 2 + H * 2;
                    break;
                case "Marco AP EXT Ventana EU":
                    Metraje = WV * 2 + HV * 2;
                    break;
                case "Envidriador Curvo 6-8mm D111":
                    Metraje = W * 4 + H * 7;
                    break;
                case "Marco Divisor EU 43mm":
                    Metraje = H * 2 + W * 1 + HV * 1;
                    break;
                //Accesorios
                case "Empaque EU V-94":
                    Metraje = W * 8 + H * 14;
                    break;
                case "Union Tope Zanca CJ4018 EU":
                    Metraje = 12;
                    break;

                case "Escuadra 35.9x13.9mm CJ0421F":
                    Metraje = 4;
                    break;

                case "Escuadra De Bloqueo Cent 15mm CJ2637-15":
                    Metraje = 4;
                    break;

                case "Manija Negra OH507-BK":
                    Metraje = 1;
                    break;

                case "Compas p/ Camara EU 25cm":
                    if (HV >= 0.25m && HV <= 0.45m)
                    {
                        Metraje = 1;
                    }
                    break;

                case "Compas P/ Camara EU 45cm":
                    if (HV >= 0.46m && HV <= 1.00m)
                    {
                        Metraje = 1;
                    }
                    break;
            }
            return Metraje;
        }
        #endregion

        #region Puerta Lujo
        private decimal CalcPuertaLujo1Hoja(string Description)
        {
            decimal Metraje = 0;

            switch (Description)
            {
                //Aluminio
                case "Tubo 1 Aleta 13/4x4 PL"://"Tubo 1 Aleta 13/4x4 PL"
                    Metraje = 6.4m;
                    break;
                case "Envidriador PL D36":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Inferior Hoja PL D37":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Lateral Hoja PL D38":
                    Metraje = 4.60m;
                    break;
                case "Superior Hoja PL D39":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Umbral 4 PL D40":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Barra de Empuje PL D42":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;

                //Accesorios 	

                case "Haladera Concha PL D41":
                    Metraje = 2;
                    break;

                case "Juego Cerradura 31/32 AL DT1850":
                    Metraje = 1;
                    break;

                case "Juego Pivote AL OP2700":
                    Metraje = 1;
                    break;

                case "Cilindro Tipo RIM RC051 PL":
                    Metraje = 1;
                    break;

                case "Barra Roscada":
                    Metraje = 2;
                    break;

                case "Tuerca 1/4":
                    Metraje = 4;
                    break;

                case "Arandela Precion":
                    Metraje = 4;
                    break;

                case "Arandela Plana":
                    Metraje = 4;
                    break;

                case "Empaque PL":
                    Metraje = (Convert.ToDecimal(ClsWindows.Weight) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;

                case "Felpa Gruesa PL":
                    Metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;

                case "CIERRA PUERTA COMPACTO AL 303":
                    Metraje = 1;
                    break;

                case "PH206":
                    Metraje = 1;
                    break;

            }

            return Metraje;
        }
        private decimal CalcPuertaLujo2Hojas(string Description)
        {
            decimal Metraje = 0;

            switch (Description)
            {
                //Aluminio
                case "Tubo 1 Aleta 13/4x4 PL":
                    decimal Res = (Convert.ToDecimal(ClsWindows.Weight) + Convert.ToDecimal(ClsWindows.heigt) * 2);
                    if (Res >= 6.40m)
                    {
                        Metraje = 12.80m;
                    }
                    else
                    {
                        Metraje = 6.40m;
                    }
                    break;
                case "Envidriador PL D36":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 8;
                    break;
                case "Inferior Hoja PL D37":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Lateral Hoja PL D38":
                    Metraje = 9.20m;
                    break;
                case "Superior Hoja PL D39":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Umbral 4 PL D40":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Barra de Empuje PL D42":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;


                //Accesorios 	

                case "Haladera Concha PL D41":
                    Metraje = 4;
                    break;

                case "Juego Cerradura 31/32 AL DT1850":
                    Metraje = 1;
                    break;

                case "Pasador Embutido 1/4 AL FB1204":
                    Metraje = 2;
                    break;

                case "Juego Pivote AL OP2700":
                    Metraje = 2;
                    break;

                case "Cilindro Tipo RIM RC051 PL":
                    Metraje = 0;
                    break;

                case "Barra Roscada":
                    Metraje = 4;
                    break;

                case "Tuerca 1/4":
                    Metraje = 8;
                    break;

                case "Arandela Precion":
                    Metraje = 8;
                    break;

                case "Arandela Plana":
                    Metraje = 8;
                    break;

                case "Empaque PL":
                    Metraje = (Convert.ToDecimal(ClsWindows.Weight)) * 4 + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Felpa Gruesa PL":
                    Metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "CIERRA PUERTA COMPACTO AL 303":
                    Metraje = 2;
                    break;
                case "PH206":
                    Metraje = 1;
                    break;

            }
            return Metraje;
        }
        private decimal CalcPuertaLujo1HojasDivicion(string Description)
        {
            decimal Metraje = 0;

            switch (Description)
            {
                //********Aluminio*******//
                case "Tubo 1 Aleta 13/4x4 PL":
                    Metraje = 6.4m;
                    break;
                case "Envidriador PL D36":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 8 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Inferior Hoja PL D37":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Lateral Hoja PL D38":
                    Metraje = 4.60m;
                    break;
                case "Superior Hoja PL D39":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Umbral 4 PL D40":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Barra de Empuje PL D42":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Divisor PL D93":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;


                //*******************Accesorios********************//
                case "Haladera Concha PL D41":
                    Metraje = 2;
                    break;
                case "Juego Cerradura 31/32 AL DT1850":
                    Metraje = 1;
                    break;
                case "Juego Pivote AL OP2700":
                    Metraje = 1;
                    break;
                case "Cilindro Tipo RIM RC051 PL":
                    Metraje = 1;
                    break;
                case "Barra Roscada":
                    Metraje = 2;
                    break;
                case "Tuerca 1/4":
                    Metraje = 4;
                    break;
                case "Arandela Precion":
                    Metraje = 4;
                    break;
                case "Arandela Plana":
                    Metraje = 4;
                    break;
                case "Empaque PL":
                    Metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                    break;
                case "Felpa Gruesa PL":
                    Metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "CIERRA PUERTA COMPACTO AL 303":
                    Metraje = 1;
                    break;
                case "PH206":
                    Metraje = 1;
                    break;

            }



            return Metraje;
        }
        private decimal CalcPuertaLujo2HojasDivicion(string Description)
        {
            decimal Metraje = 0;

            switch (Description)
            {
                //********Aluminio*******//
                case "Tubo 1 Aleta 13/4x4 PL":
                    decimal Res = (Convert.ToDecimal(ClsWindows.Weight) + Convert.ToDecimal(ClsWindows.heigt) * 2);
                    if (Res >= 6.40m)
                    {
                        Metraje = 12.80m;
                    }
                    else
                    {
                        Metraje = 6.40m;
                    }
                    break;
                case "Envidriador PL D36":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 8;
                    break;
                case "Inferior Hoja PL D37":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Lateral Hoja PL D38":
                    Metraje = 9.20m;
                    break;
                case "Superior Hoja PL D39":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Umbral 4 PL D40":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Barra de Empuje PL D42":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;
                case "Divisor PL D93":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                    break;

                //*Accesorios*//

                case "Haladera Concha PL D41":
                    Metraje = 4;
                    break;
                case "Juego Cerradura 31/32 AL DT1850":
                    Metraje = 1;
                    break;
                case "Pasador Embutido 1/4 AL FB1204":
                    Metraje = 2;
                    break;
                case "Juego Pivote AL OP2700":
                    Metraje = 2;
                    break;
                case "Cilindro Tipo RIM RC051 PL":
                    Metraje = 1;
                    break;
                case "Barra Roscada":
                    Metraje = 8;
                    break;
                case "Tuerca 1/4":
                    Metraje = 8;
                    break;
                case "Arandela Precion":
                    Metraje = 8;
                    break;
                case "Arandela Plana":
                    Metraje = 8;
                    break;
                case "Empaque PL":
                    Metraje = (Convert.ToDecimal(ClsWindows.Weight) * 8) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                    break;
                case "Felpa Gruesa PL":
                    Metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "CIERRA PUERTA COMPACTO AL 303":
                    Metraje = 2;
                    break;
                case "PH206":
                    Metraje = 1;
                    break;

            }
            return Metraje;
        }

          // POrton de lujo corredizo (PortonCorredizoLujo1Hoja) nombre a utilizar en las imagenes. 
        private decimal CalcPortonCorredizoLujo1Hoja(string Description)
        {
                decimal Metraje = 0;

                switch (Description)
                {
                    //Aluminio
                case "Tubo 1 Aleta 13/4x4 PL":
                    Metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                case "Riel Porton Pesado":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                    break;
                case "Angular 1x4 (cenefa)":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                    break;
                case "Envidriador PL D36":
                        Metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                    case "Inferior Hoja PL D37":
                        Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                        break;
                    case "Lateral Hoja PL D38":
                        Metraje = 4.60m;
                        break;
                    case "Superior Hoja PL D39":
                        Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                        break;
                   

                    //Accesorios 	

                    case "Haladera Sencilla p/ PL":
                        Metraje = 2;
                        break;
                    case "Rodin Porton Pesado":
                        Metraje = 2;
                        break;

                    case "Cerradura Pico Lora Grande":
                        Metraje = 1;
                        break;
                    case "Tope Fijo de Piso Prefabricado Multiuso":
                        Metraje = 1;
                        break;

                    case "Barra Roscada":
                        Metraje = 2;
                        break;

                    case "Tuerca 1/4":
                        Metraje = 4;
                        break;

                    case "Arandela Precion":
                        Metraje = 4;
                        break;

                    case "Arandela Plana":
                        Metraje = 4;
                        break;

                    case "Empaque PL":
                        Metraje = (Convert.ToDecimal(ClsWindows.Weight) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 4);
                        break;

                    case "Felpa Gruesa PL":
                        Metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                        break;

                    

                }

            return Metraje;

        }

        // Porton de lujo corredizo (PortonCorredizoLujo2Hoja) nombre a utilizar en las imagenes. 
        private decimal CalcPortonCorredizoLujo2Hoja(string Description)
        {
        decimal Metraje = 0;

                switch (Description)
                {
                    //Aluminio

                    case "Riel Porton Pesado":
                        Metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Angular 1x4 (cenefa)":
                        Metraje = Convert.ToDecimal(ClsWindows.Weight) * 2;
                        break;
                    case "Envidriador PL D36":
                        Metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 8;
                        break;
                    case "Inferior Hoja PL D37":
                        Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                        break;
                    case "Lateral Hoja PL D38":
                        Metraje = 9.20m;
                        break;
                    case "Superior Hoja PL D39":
                        Metraje = Convert.ToDecimal(ClsWindows.Weight) * 1;
                        break;


                    //Accesorios 	

                    case "Haladera Sencilla p/ PL":
                        Metraje = 4;
                        break;
                    case "Rodin Porton Pesado":
                        Metraje = 4;
                        break;

                    case "Cerradura Pico Lora Grande":
                        Metraje = 1;
                        break;
                    case "Tope Fijo de Piso Prefabricado Multiuso":
                        Metraje = 2;
                        break;

                    case "Barra Roscada":
                        Metraje = 4;
                        break;

                    case "Tuerca 1/4":
                        Metraje = 8;
                        break;

                    case "Arandela Precion":
                        Metraje = 8;
                        break;

                    case "Arandela Plana":
                        Metraje = 8;
                        break;

                    case "Empaque PL":
                        Metraje = (Convert.ToDecimal(ClsWindows.Weight) * 4) + (Convert.ToDecimal(ClsWindows.heigt) * 8);
                        break;

                    case "Felpa Gruesa PL":
                        Metraje = Convert.ToDecimal(ClsWindows.heigt) * 4;
                        break;
                }

                return Metraje;
        }
        
    
        
        #endregion

        #region Puerta Liviana
        private decimal CalcPuertaLiviana1Hoja(string Description)
        {
            decimal Metraje = 0;
            switch (Description)
            {
                case "Tubo 1x2 1 Aleta Desentrada":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Tubo 1x2 1 Aleta":
                    Metraje = 6.40m;
                    break;
                case "Canal 1/2*1/2":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                //Accesorios
                case "Cerradura Doble Manija":
                    Metraje = 1;
                    break;
                case "Visagra Bandera":
                    Metraje = 3;
                    break;
                case "Angular para Puerta Desentrada":
                    Metraje = 4;
                    break;
                case "Barra Roscada 1/4":
                    Metraje = 2;
                    break;
                case "Tuerca 1/4":
                    Metraje = 4;
                    break;
                case "Arandela de Precion":
                    Metraje = 4;
                    break;
                case "Arandela Plana":
                    Metraje = 4;
                    break;
                case "Empaque Precion 7mm":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;

            }
            return Metraje;
        }
        private decimal CalcPuertaLiviana1Hojacondivicion(string Descrption)
        {
            decimal Metraje = 0;
            switch (Descrption)
            {
                case "Tubo 1x2 1 Aleta Desentrada":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 3 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                case "Tubo 1x2 1 Aleta":
                    Metraje = 6.40m;
                    break;
                case "Canal 1/2*1/2":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
                //Accesorios
                case "Cerradura Doble Manija":
                    Metraje = 1;
                    break;
                case "Visagra Bandera":
                    Metraje = 3;
                    break;
                case "Angular para Puerta Desentrada":
                    Metraje = 6;
                    break;
                case "Barra Roscada 1/4":
                    Metraje = 2;
                    break;
                case "Tuerca 1/4":
                    Metraje = 4;
                    break;
                case "Arandela de Precion":
                    Metraje = 4;
                    break;
                case "Arandela Plana":
                    Metraje = 4;
                    break;
                case "Empaque Precion 7mm":
                    Metraje = Convert.ToDecimal(ClsWindows.Weight) * 4 + Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
            }
            return Metraje;
        }
        #endregion

        #region PuertaEuAbatible
        private decimal CalcPuertaEuAbatible1Hoja(string Description)
        {
            decimal Metraje = 0;
            decimal Weigth = ClsWindows.Weight;
            decimal Heigth = ClsWindows.heigt;
            switch (Description)
            {
                //*******************Aluminio********************//
                case "Contramarco Eu 45x62.4":
                    Metraje = Weigth * 1 + Heigth * 2;
                    break;
                case "Marco APINT EU  PUERTA":
                    Metraje = Weigth * 1 + Heigth * 2;
                    break;
                case "Marco INFERIOR EU 150X45":
                    Metraje = Weigth * 1;
                    break;
                case "Envidriador Curvo 6-8mm D111":
                    Metraje = Weigth * 2 + Heigth * 2;
                    break;
                //*******************Accesorios********************//
                case "Cerradura Con Palanca Basculante 196-30":
                    Metraje = 1;
                    break;
                case "Cerradero Para 196-30":
                    Metraje = 1;
                    break;
                case "Escuadra 35.9x13.9mm CJ0421F":
                    Metraje = 2;
                    break;
                case "Escuadra De Bloqueo Cent 15mm CJ2637-15":
                    Metraje = 2;
                    break;
                case "Union de Tope Deslizante CJ1418":
                    Metraje = 4;
                    break;
                case "Cilindro Desentrado CL3040":
                    Metraje = 1;
                    break;
                case "Manija Recuperable C/Escudo OH1500E-BK":
                    Metraje = 1;
                    break;
                case "Visagra Puerta Pesada OH8060-BK":
                    Metraje = 3;
                    break;
                case "Plata de Cilindro EuroML3500-BK":
                    Metraje = 2;
                    break;
                case "Empaque EU V-94":
                    Metraje = Weigth * 2 + Heigth * 4;
                    break;
                case "Empaque Alfin Lengueta":
                    Metraje = Weigth * 4 + Heigth * 4;
                    break;
            }
            return Metraje;
        }
        private decimal CalcPuertaEuAbatible2Hojas(string Description)
        {
            decimal Metraje = 0;
            decimal Weigth = ClsWindows.Weight;
            decimal Heigth = ClsWindows.heigt;
            switch (Description)
            {
                //*******************Aluminio********************//
                case "Contramarco Eu 45x62.4":
                    Metraje = Weigth * 1 + Heigth * 2;
                    break;
                case "Marco APINT EU  PUERTA":
                    Metraje = Weigth * 1 + Heigth * 4;
                    break;
                case "Marco INFERIOR EU 150X45":
                    Metraje = Weigth * 1;
                    break;
                case "Envidriador Curvo 6-8mm D111":
                    Metraje = Weigth * 2 + Heigth * 4;
                    break;
                case "Marco Inversor EU":
                    Metraje = Heigth * 1;
                    break;
                //*******************Accesorios********************//
                case "Cerradura Con Palanca Basculante 196-30":
                    Metraje = 1;
                    break;
                case "Cerradero Para 196-30":
                    Metraje = 1;
                    break;
                case "Escuadra 35.9x13.9mm CJ0421F":
                    Metraje = 2;
                    break;
                case "Escuadra De Bloqueo Cent 15mm CJ2637-15":
                    Metraje = 4;
                    break;
                case "Union de Tope Deslizante CJ1418":
                    Metraje = 8;
                    break;
                case "Cilindro Desentrado CL3040":
                    Metraje = 1;
                    break;
                case "Manija Recuperable C/Escudo OH1500E-BK":
                    Metraje = 1;
                    break;
                case "Visagra Puerta Pesada OH8060-BK":
                    Metraje = 6;
                    break;
                case "Plata de Cilindro EuroML3500-BK":
                    Metraje = 2;
                    break;
                case "Empaque EU V-94":
                    Metraje = Weigth * 4 + Heigth * 8;
                    break;
                case "Empaque Alfin Lengueta":
                    Metraje = Weigth * 4 + Heigth * 8;
                    break;
            }
            return Metraje;
        }
        private decimal CalcPuertaEuAbatible1HojasConDivicion(string Description)
        {
            decimal Metraje = 0;
            decimal Weigth = ClsWindows.Weight;
            decimal Heigth = ClsWindows.heigt;
            switch (Description)
            {
                //*******************Aluminio********************//
                case "Contramarco Eu 45x62.4":
                    Metraje = Weigth * 1 + Heigth * 2;
                    break;
                case "Marco APINT EU  PUERTA":
                    Metraje = Weigth * 1 + Heigth * 2;
                    break;
                case "Marco INFERIOR EU 150X45":
                    Metraje = Weigth * 1;
                    break;
                case "Envidriador Curvo 6-8mm D111":
                    Metraje = Weigth * 4 + Heigth * 2;
                    break;
                case "Marco Divisor EU 43mm":
                    Metraje = Weigth * 1;
                    break;

                //*******************Accesorios********************//
                case "Cerradura Con Palanca Basculante 196-30":
                    Metraje = 1;
                    break;
                case "Cerradero Para 196-30":
                    Metraje = 1;
                    break;
                case "Escuadra 35.9x13.9mm CJ0421F":
                    Metraje = 2;
                    break;
                case "Escuadra De Bloqueo Cent 15mm CJ2637-15":
                    Metraje = 2;
                    break;
                case "Union de Tope Deslizante CJ1418":
                    Metraje = 8;
                    break;
                case "Cilindro Desentrado CL3040":
                    Metraje = 1;
                    break;
                case "Manija Recuperable C/Escudo OH1500E-BK":
                    Metraje = 1;
                    break;
                case "Visagra Puerta Pesada OH8060-BK":
                    Metraje = 3;
                    break;
                case "Plata de Cilindro EuroML3500-BK":
                    Metraje = 2;
                    break;
                case "Empaque EU V-94":
                    Metraje = Weigth * 2 + Heigth * 4;
                    break;
                case "Empaque Alfin Lengueta":
                    Metraje = Weigth * 8 + Heigth * 4;
                    break;
            }
            return Metraje;
        }

        private decimal CalcPuertaEuAbatible2HojasConDivicion(string Description)
        {
            decimal Metraje = 0;
            decimal Weigth = ClsWindows.Weight;
            decimal Heigth = ClsWindows.heigt;
            switch (Description)
            {
                //*******************Aluminio********************//
                case "Contramarco Eu 45x62.4":
                    Metraje = Weigth * 1 + Heigth * 2;
                    break;
                case "Marco APINT EU  PUERTA":
                    Metraje = Weigth * 1 + Heigth * 4;
                    break;
                case "Marco INFERIOR EU 150X45":
                    Metraje = Weigth * 1;
                    break;
                case "Envidriador Curvo 6-8mm D111":
                    Metraje = Weigth * 4 + Heigth * 4;
                    break;
                case "Marco Divisor EU 43mm":
                    Metraje = Weigth * 1;
                    break;
                case "Marco Inversor EU":
                    Metraje = Heigth * 1;
                    break;
                //*******************Accesorios********************//
                case "":
                    Metraje = Heigth * 1;
                    break;
                case "Cerradura Con Palanca Basculante 196-30":
                    Metraje = 1;
                    break;
                case "Cerradero Para 196-30":
                    Metraje = 1;
                    break;
                case "Escuadra 35.9x13.9mm CJ0421F":
                    Metraje = 2;
                    break;
                case "Escuadra De Bloqueo Cent 15mm CJ2637-15":
                    Metraje = 4;
                    break;
                case "Union de Tope Deslizante CJ1418":
                    Metraje = 16;
                    break;
                case "Cilindro Desentrado CL3040":
                    Metraje = 1;
                    break;
                case "Manija Recuperable C/Escudo OH1500E-BK":
                    Metraje = 1;
                    break;
                case "Visagra Puerta Pesada OH8060-BK":
                    Metraje = 6;
                    break;
                case "Plata de Cilindro EuroML3500-BK":
                    Metraje = 2;
                    break;
                case "Empaque EU V-94":
                    Metraje = Weigth * 2 + Heigth * 4;
                    break;
                case "Empaque Alfin Lengueta":
                    Metraje = Weigth * 4 + Heigth * 8;
                    break;
            }
            return Metraje;
        }

        #endregion

        #region Puerta Baño
        private decimal PuertaBañoMovilMovil(string Descripcion)
        {
            decimal Metraje = 0;
            decimal Weigth = clsPuertaBaño.WeightTotal;
            decimal Heigth = clsPuertaBaño.heigt;
            decimal WeigthPanel = clsPuertaBaño.WeightPanel;

            switch (Descripcion)
            {
                //Aluminio
                case "Cargador PB Liso":
                    Metraje = Weigth;//ancho de puerta movil-movil//
                    break;
                case "Umbral PB Liso":
                    Metraje = Weigth;//ancho de puerta movil-movil//
                    break;
                case "Jamba PB Liso":
                    Metraje = 3.6m; // alto de puerta movil-movil//
                    break;
                case "Lateral PB Liso": // alto de puerta movil-movil//
                    Metraje = 7.2m;
                    break;
                case "Superior PB Liso":
                    Metraje = Weigth;//ancho de puerta movil-movil//
                    break;
                case "Inferior PB Liso":
                    Metraje = Weigth;//ancho de puerta movil-movil//
                    break;
                case "Pañera PB Liso":
                    Metraje = Weigth * 2; //ancho de puerta movil-movil//
                    break;
                case "Escuadra Pañera PB Liso":
                    Metraje = 0.4m;
                    break;
                //Lamina
                case "Lamina Plastica 122":
                    if (Weigth <= 1.22m)
                    {
                        Metraje = 1;
                    }
                    else
                    {
                        if (Weigth >= 1.83m && Weigth <= 2.44m)//&&
                        {
                            Metraje = 2;
                        }
                    }
                    break;
                    
                case "Lamina Plastica 65":
                    if (Weigth >= 1.23m && Weigth <= 1.30m)
                    {
                        Metraje = 2;
                    }
                    break;
                case "Lamina Plastica 75":
                    if (Weigth >= 1.31m && Weigth <= 1.50m)
                    {
                        Metraje = 2;
                    }
                    break;
                case "Lamina Plastica 91":
                    if (Weigth >= 1.51m && Weigth <= 1.82m)
                    {
                        Metraje = 2;
                    }
                    break;
                //Accesorios
                case "Empaque U PB":
                    Metraje = (Weigth * 2) + (Heigth * 4);
                    break;
                case "Rodin PB":
                    Metraje = 4;
                    break;
                case "Tope Bumper Plastico Tiffany":
                    Metraje = 4;
                    break;
            }


            return Metraje;
        }

        private decimal PuertaBañoFijoMovilMovil(string Descripcion)
        {
            decimal Metraje = 0;
            decimal Weigth = clsPuertaBaño.WeightTotal;
            decimal Heigth = clsPuertaBaño.heigt;
            decimal WeigthPanel = clsPuertaBaño.WeightPanel;
            switch (Descripcion)
            {
                //Aluminio de puerta // 
                case "Cargador PB Liso":
                    Metraje = Weigth + WeigthPanel;
                    break;
                case "Umbral PB Liso":
                    Metraje = Weigth + WeigthPanel;
                    break;
                case "Jamba PB Liso":
                    Metraje = 7.20m;
                    break;
                case "Lateral PB Liso":
                    Metraje = 10.8m;
                    break;
                case "Superior PB Liso":
                    Metraje = Weigth ;
                    break;
                case "Inferior PB Liso":
                    Metraje = Weigth + WeigthPanel * 2;
                    break;
                case "Pañera PB Liso":
                    Metraje = Weigth * 2;
                    break;
                case "Escuadra Pañera PB Liso":
                    Metraje = 0.4m;
                    break;
                //Lamina panel //
                case "Lamina Plastica 122":
                    if (Weigth <= 1.22m || WeigthPanel <= 1.22m)
                    {
                        Metraje = 1;
                    }
                    else
                    {
                        if (Weigth >= 1.83m && Weigth <= 2.44m)//&&
                        {
                            Metraje = 2;
                        }
                    }
                     
                    break;

                case "Lamina Plastica 65":
                    if (Weigth >= 1.23m && Weigth <= 1.30m)
                    {
                        Metraje = 2;
                    }             
                    break;
                case "Lamina Plastica 75":
                    if (Weigth >= 1.31m && Weigth <= 1.50m)
                    {
                        Metraje = 2;
                    }
                    break;
                case "Lamina Plastica 91":
                    if (Weigth >= 1.51m && Weigth <= 1.82m)
                    {
                        Metraje = 2;
                    }
                    break;

                //Accesorios
                case "Empaque U PB":
                    Metraje = (Weigth * 2) + (Heigth * 6);
                    break;
                case "Rodin PB":
                    Metraje = 4;
                    break;
                case "Tope Bumper Plastico Tiffany":
                    Metraje = 4
                    ;
                    break;
            }
            return Metraje;
        }

        #endregion

        #region CedazoMedia
        private decimal MetrajeCedazoMedia(string Descripcion)
        {
            decimal Metraje = 0;
            decimal Weigth = ClsWindows.Weight;
            decimal Heigth = ClsWindows.heigt;

            switch (Descripcion)
            {
                //Aluminio
                case "Marco Cedazo 1/2 Fijo":
                    Metraje = Weigth * 2 + Heigth * 2;
                    break;
                //Accesorios
                case "Fibra Cedazo 5020":
                    Metraje = Heigth * 1;
                    break;
                case "Empaque Cedazo 1/2":
                    Metraje = Weigth * 2 + Heigth * 2;
                    break;
                case "Escuadra Cedazo 1/2":
                    Metraje = 4;
                    break;
            }
            return Metraje;
        }
        private decimal MetrajeCedazo1(string Descripcion)
        {
            decimal Metraje = 0;
            decimal Weigth = ClsWindows.Weight;
            decimal Heigth = ClsWindows.heigt;

            switch (Descripcion)
            {
                //Aluminio
                case "Marco Cedazo 1 fijo":
                    Metraje = Weigth * 2 + Heigth * 2;
                    break;
                //Accesorios
                case "Fibra Cedazo 5020":
                    Metraje = Heigth * 1;
                    break;
                case "Empaque Cedazo 1":
                    Metraje = Weigth * 2 + Heigth * 2;
                    break;
                case "Escuadra Cedazo 1":
                    Metraje = 4;
                    break;
            }
            return Metraje;
        }
        private decimal MetrajeCedazo2(string Descripcion)
        {
            decimal Metraje = 0;
            decimal Weigth = ClsWindows.Weight;
            decimal Heigth = ClsWindows.heigt;

            switch (Descripcion)
            {
                //Aluminio
                case "Marco Cedazo 2 fijo":
                    Metraje = Weigth * 2 + Heigth * 2;
                    break;
                //Accesorios
                case "Fibra Cedazo 5020":
                    Metraje = Heigth * 1;
                    break;
                case "Empaque Cedazo 1/2":
                    Metraje = Weigth * 2 + Heigth * 2;
                    break;
                case "Escuadra Cedazo 1/2":
                    Metraje = 4;
                    break;
            }
            return Metraje;
        }

        #endregion

        #region Prices
        private decimal CalcPrice(decimal Metraje, decimal SalePrice)
        {
            decimal Price = 0;
           
            Price = Metraje * SalePrice;
            

            return Price;
        }
        public decimal CalcTotalPrice(DataTable dtAluminio, DataTable dtAccesorios, DataTable dtVidrio, DataTable dtLock, string Name, string Suppler)
        {
            decimal TotalPrice = 0;
            try
            {
                if (dtAluminio != null)
                {
                    foreach (DataRow item in dtAluminio.Rows)
                    {
                        TotalPrice += Convert.ToDecimal(item["TotalPrice"]);
                    }
                }
                if (dtAccesorios != null)
                {
                    foreach (DataRow item in dtAccesorios.Rows)
                    {
                        TotalPrice += Convert.ToDecimal(item["TotalPrice"]);
                    }
                }
                if (dtVidrio != null)
                {
                    foreach (DataRow item in dtVidrio.Rows)
                    {
                        TotalPrice += Convert.ToDecimal(item["TotalPrice"]);
                    }
                }

                if (dtLock != null)
                {
                    foreach (DataRow item in dtLock.Rows)
                    {
                        TotalPrice += Convert.ToDecimal(item["TotalPrice"]);
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
            decimal SettingPrice = 0;
            if (UserCache.Name == "InnovaGlass")
            {
                SettingPrice = 0.1856m;
            }
            else
            {
                SettingPrice = loadProduct.loadSettingPrice(Suppler, Name);
            }

            decimal TotalAjuste = TotalPrice * SettingPrice;
            TotalPrice = TotalPrice + TotalAjuste;

            return TotalPrice;
        }
        public decimal LoadAjustePrecio(string Supplier, string Descripcion)
        {
            return loadProduct.loadSettingPrice(Supplier, Descripcion);
        }
        #endregion

        #region Insert
        public bool insertWindows(string Description, string URL, decimal Width, decimal Height, string Glass, string Color, string TypeLock, decimal Price, int IdQuote, string System, string Desing)
        {
            try
            {
                return loadProduct.insertWindows(Description, URL, Width, Height, Glass, Color, TypeLock, Price, IdQuote, System, Desing);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditWindows(string IDWindows, string Description, string URL, decimal Width, decimal Height, string Glass, string Color, string TypeLock, decimal Price, int IdQuote, string System, string Desing)
        {
            try
            {
                return loadProduct.EditWindows(Convert.ToInt32(IDWindows), Description, URL, Width, Height, Glass, Color, TypeLock, Price, IdQuote, System, Desing);
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region fijo con ajuste

        // VENTANAS MATERIAL X12 //

        // VENTANA FIJA CON AJUSTE EN LAS ALTURA SIN DIVCION CANAL X12 // 

        private decimal CalclMetrajeFijoAjusteAlto(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Canal X12":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 1 + Convert.ToDecimal(ClsWindows.heigt2) * 1;
                    break;
                case "Venilla 1/2":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 1 + Convert.ToDecimal(ClsWindows.heigt2) * 1;
                    break;
            }
            return metraje;

            // VENTANA FIJA CON AJUSTE EN LAS ALTURA CON UNA DIVICION CANAL X12 //

        }
        private decimal CalclMetrajeFijoAjusteAlto1Divicion(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Canal X12":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 1 + Convert.ToDecimal(ClsWindows.heigt2) * 1;
                    break;
                case "Venilla 1/2":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Tubo 2 Aleta 1X2":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
            }
            return metraje;

            // VENTANA FIJA CON AJUSTE EN LAS ALTURA CON DOS DIVICIONES CANAL X12 //

        }
        private decimal CalclMetrajeFijoAjusteAlto2Divicion(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Canal X12":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 1 + Convert.ToDecimal(ClsWindows.heigt2) * 1;
                    break;
                case "Venilla 1/2":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 6;
                    break;
                case "Tubo 2 Aleta 1X2":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
            }
            return metraje;

            // VENTANA FIJA CON AJUSTE EN LAS ALTURA SIN DIVCION INVERTIDO CANAL X12 // 
        }
        private decimal CalclMetrajeFijoAjusteAltoinvertido(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Canal X12":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 1 + Convert.ToDecimal(ClsWindows.heigt2) * 1;
                    break;
                case "Venilla 1/2":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 1 + Convert.ToDecimal(ClsWindows.heigt2) * 1;
                    break;
            }
            return metraje;

        }
        // VENTANA FIJA CON AJUSTE EN LAS ALTURA CON UNA DIVICION INVERTIDO CANAL X12 //

        private decimal CalclMetrajeFijoAjusteAlto1DivicionInvertido(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Canal X12":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 1 + Convert.ToDecimal(ClsWindows.heigt2) * 1;
                    break;
                case "Venilla 1/2":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Tubo 2 Aleta 1X2":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
            }
            return metraje;
        }

        // VENTANA FIJA CON AJUSTE EN LAS ALTURA CON DOS DIVICION INVERTIDO CANAL X12 //
        private decimal CalclMetrajeFijoAjusteAlto2DivicionInvertido(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Canal X12":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 1 + Convert.ToDecimal(ClsWindows.heigt2) * 1;
                    break;
                case "Venilla 1/2":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 6;
                    break;
                case "Tubo 2 Aleta 1X2":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
            }
            return metraje;
        }
        // VENTANA FIJA ESCALENO CANAL X12 //
        private decimal CalclMetrajeFijoEscaleno(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Canal X12":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                case "Venilla 1/2":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;

            }
            return metraje;
        }
        // VENTANA FIJA ESCALENO CON 1 DIVICION CANAL X12 //

        private decimal CalclMetrajeFijoEscaleno1Divicion(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Canal X12":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                case "Venilla 1/2":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 4;
                    break;
                case "Tubo 2 Aleta 1X2":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
            }
            return metraje;
        }

        // VENTANA FIJA ESCALENO CON 2 DIVICION CANAL X12 //

        private decimal CalclMetrajeFijoEscaleno2Divicion(string Description)
        {
            decimal metraje = 0;
            switch (Description)
            {
                case "Canal X12":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 1;
                    break;
                case "Venilla 1/2":
                    metraje = Convert.ToDecimal(ClsWindows.Weight) * 2 + Convert.ToDecimal(ClsWindows.heigt) * 6;
                    break;
                case "Tubo 2 Aleta 1X2":
                    metraje = Convert.ToDecimal(ClsWindows.heigt) * 2;
                    break;
            }
            return metraje;


            #endregion
            #region Europa Carbone

            #endregion
        }
    }
}

          
