using System;
using System.Security.Cryptography;
using System.Text;
using AccesoDatos.User;

namespace Negocio.Users
{
    public class N_RestoreUser
    {
        static string ComputeSHA512(string s)
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

        public bool VerificationData(string ID, string Email, string Phone)
        { 
            CD_RestoreUser _RestoreUser = new CD_RestoreUser();
            bool Result = false;
            Result = _RestoreUser.VerificationData(Convert.ToInt16(ID), Email, Phone);
            return Result;
        }

        public bool VerificationCode(int RandomNumber, string RandomNumberInput) 
        { 
            bool Result = false;
            if (Convert.ToInt64(RandomNumberInput) == RandomNumber) 
            { 
                Result =  true;
            }else 
            {
                Result = false;
            }
            
            return Result;
        }

        public bool ChangePassWord(string ID, string PassWord) 
        {  
            CD_RestoreUser _RestoreUser = new CD_RestoreUser();
            PassWord = ComputeSHA512(PassWord);
            return _RestoreUser.ChangePassWord(Convert.ToInt32(ID), PassWord);
        }

    }
}
