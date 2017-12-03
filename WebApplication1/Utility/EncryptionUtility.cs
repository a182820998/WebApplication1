using System;
using System.Text;
using System.Web.Security;
using Microsoft.ApplicationInsights.WindowsServer;

namespace WebApplication1.Utility
{
    public class EncryptionUtility
    {
        public static string Encryption(string encryptionString)
        {
            var bytes = Encoding.UTF8.GetBytes(encryptionString);
            return Convert.ToBase64String(MachineKey.Protect(bytes, "Insus.NET"));
        }

        public static string Decryption(string decryptionString)
        {
            try
            {
                var bytes = Convert.FromBase64String(decryptionString);
                return Encoding.UTF8.GetString(MachineKey.Unprotect(bytes, "Insus.NET"));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return decryptionString;
            }
        }
    }
}