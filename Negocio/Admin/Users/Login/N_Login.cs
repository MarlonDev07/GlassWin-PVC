using AccesoDatos.Login;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Negocio.Login
{
    public class N_Login
    {
        AccesoDatos.Login.CD_Login datalogin = new AccesoDatos.Login.CD_Login();

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

                return null;
            }        
        }
        #endregion

        #region Login Function
        public bool LoginUser(string User, string Pass) 
        {
            try
            {
                string PassEncoding = ComputeSHA512(Pass);
                return datalogin.Login(User, PassEncoding);
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        #endregion
    }
}
