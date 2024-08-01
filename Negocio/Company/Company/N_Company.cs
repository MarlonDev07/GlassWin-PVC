using AccesoDatos.Company;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Company
{
    public class N_Company
    {
        CD_Company ObjCDCompany = new CD_Company();
        public DataTable View()
        {
            try
            {
               DataTable dataTable = new DataTable();
               dataTable = ObjCDCompany.ViewCompany();
               return dataTable;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Create(string ID, string IDCompany, string phone, string Address, string Url, string Name)
        {
            try
            {
                bool result = ObjCDCompany.Create(Convert.ToInt64(ID),Convert.ToInt64(IDCompany), phone, Address, Url, Name);
                return result;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(string ID, string IDCompany, string phone, string Address, string Url, string Name)
        {
            try
            {
                bool result = ObjCDCompany.Update(Convert.ToInt32(ID), Convert.ToInt32(IDCompany), phone, Address, Url, Name);
                return result;
            }
            catch (Exception)
            {
                return false;
               
            }
        }
    }
}
