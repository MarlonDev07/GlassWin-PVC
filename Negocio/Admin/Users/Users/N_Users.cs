using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using AccesoDatos.User;
using Dominio.ClassUser;
using Negocio.SMS__WhatsApp_Email;
using Twilio.Rest.Verify.V2.Service;

namespace Negocio.Users
{
    public class N_Users
    {
        CD_Users ObjCDUser = new CD_Users();
        N_SendEmail sendEmail = new N_SendEmail();  

        public DataTable ViewUsers()
        {
            DataTable Table = new DataTable();
            Table = ObjCDUser.View();
            return Table;
        }

        public List<clsUser> FindxID(string ID)
        {
            List<clsUser> UserList = new List<clsUser>();
            UserList = ObjCDUser.FindxID(Convert.ToInt64(ID)); // Cambiar a Int64
            return UserList;
        }

        public bool Create(string ID,string Name, string phone, string Email, string UserName, string PassWord, string Roll)
        {
            DateTime Expiration = CalceExpiration();
            
            string PassWordEncryp = ComputeSHA512(PassWord);

            if(ObjCDUser.Create(Convert.ToInt64(ID), Name,phone, Email, UserName, PassWordEncryp, Roll, Expiration, "Active")) 
            {
                if (sendEmail.SendEmailUser(Email,UserName,PassWord))
                {
                    return true;
                }else { return false; }
               
            }
            else
            {
                return false;
            }
        }

        public bool Update(long ID, string Name, string phone, string Email, string UserName, string Roll, string State = "Active")
        {
            if (ObjCDUser.Update(ID,Name, phone, Email, UserName, Roll, "Active"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DateTime CalceExpiration()
        {
            DateTime fechaActual = DateTime.Now;
            DateTime fechaUnMesDespues = fechaActual.AddMonths(1);
            return fechaUnMesDespues;
        }


        #region Encrypter
        static string ComputeSHA512(string s)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                using (SHA512 sha512 = SHA512.Create())
                {
                    byte[] hashValue = sha512.ComputeHash(Encoding.UTF8.GetBytes(s));
                    foreach (byte b in hashValue)
                    {
                        sb.Append($"{b:X2}");
                    }
                }

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }

}
