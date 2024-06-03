using AccesoDatos.SettingPrice;
using Dominio.SettingPrice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Negocio.SettingPrice
{
    public class N_SettingPrice
    {
        CD_SettingPrice SettingPrice = new CD_SettingPrice();

        #region Read
        public DataTable View()
        {
            try
            {
                DataTable dt = new DataTable();
                CD_SettingPrice products = new CD_SettingPrice();
                dt = products.View();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable Find(string Name)
        {
            try
            {
                DataTable dt = new DataTable();
                CD_SettingPrice products = new CD_SettingPrice();
                StringBuilder Query = new StringBuilder("SELECT * from SettingPrice Where ");
                Query.Append($" Name = '{Name}'");
                dt = products.Find(Query.ToString());
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Create
        public bool CreateSettingPrice(string Name, string Percentage, string Supplier)
        {
            try
            {
                bool Res = false;
                Res = SettingPrice.CreateSettingPrice(Name, Convert.ToDecimal(Percentage), Supplier);
                return Res;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Update
        public List<SettingPriceClass> DataSettingsPrice(string ID)
        {
            try
            {
                List<SettingPriceClass> settingPrices = SettingPrice.DataSettingsPrice(Convert.ToInt32(ID));
                return settingPrices;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateSettingPrice(string id, string Name, string Percentage, string Supplier)
        {
            try
            {
                bool Res = false;
                Res = SettingPrice.UpdateSettingPrice(Convert.ToInt32(id), Name, Convert.ToDecimal(Percentage), Supplier);
                return Res;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
