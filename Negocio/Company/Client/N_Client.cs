using AccesoDatos.Client;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data;

namespace Negocio.Client
{
    public class N_Client
    {
        AD_Client ADClient = new AD_Client();

        public DataTable LoadClient()
        {
            try
            {
                return ADClient.LoadClient();
            }
            catch (Exception )
            {
                return null;
            }
        } 

        public List<clsClient> ListClient(string ID)
        {
            try
            {
                return ADClient.ListClient(Convert.ToInt64(ID));
            }
            catch (Exception )
            {
                return null;
            }
        }

        public DataTable FindClient(int ID)
        {
            try
            {
                return ADClient.FindClient(ID);
            }
            catch (Exception )
            {
                return null;
            }
        }

        public bool Create(string Name, string Phone, string Address, string Correo)
        {
            try
            {
                return ADClient.Create(Name, Phone,Address, Correo);
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool update(int ID, string Name, string Phone, string Address, string Correo)
        {
            try
            {
                return ADClient.Update(ID, Name, Phone,Address,Correo);
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool DeleteClientData(int ID)
        {
            try
            {
                return ADClient.DeleteClientData(ID);
            }
            catch (Exception )
            {
                return false;
            }
        }

        public DataTable CargarProformasCliente(int IdCliente) 
        {
            try
            {
                return ADClient.CargarProformasCliente(IdCliente);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
