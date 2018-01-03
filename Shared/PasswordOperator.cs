using SecureAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecureAPI.Shared
{
    public static class PasswordOperator
    {
        public static string GetPasswordHash(User user, string password)
        {
            return PerformHashOnString(password, user.PasswordSalt);
        }


        public static bool ValidatePassword(User user, string password)
        {
            return PerformHashOnString(password, user.PasswordSalt) == user.PasswordHash ?  true : false;
        }

        public static string BuildSaltForUser(User user)
        {
            return PerformHashOnString(user.PasswordCreated.ToString() + "j1muJJhns", user.Username + "O18C71nes5");
        }

        public static string PerformHashOnString(string stringToHash, string salt)
        {
            SHA256 shaHasher = SHA256Managed.Create();

            byte[] tempHash = shaHasher.ComputeHash(Encoding.UTF32.GetBytes(stringToHash + salt));

            int i;
            StringBuilder sOutput = new StringBuilder(tempHash.Length);
            for (i = 0; i < tempHash.Length; i++)
            {
                sOutput.Append(tempHash[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}
