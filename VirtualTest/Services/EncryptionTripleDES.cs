using System;
using System.IO;
using System.Security.Cryptography;
using VirtualTest.Services.Interfaces;
using System.Text;

namespace VirtualTest.Services
{
    public class EncryptionTripleDES : IEncryptionService
    {
        public EncryptionTripleDES() { }

        public string Encrypt(string pString)
        {
            string key = "KeyForEncryption";
            byte[] stringArrayToEncrypt = UTF8Encoding.UTF8.GetBytes(pString); 
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] arrayKey = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key)); 
            md5.Clear(); 
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider(); 
            tdes.Key = arrayKey;
            tdes.Mode = CipherMode.ECB; 
            tdes.Padding = PaddingMode.PKCS7; 
            ICryptoTransform cryptoTransform = tdes.CreateEncryptor(); 
            byte[] arrayResult = cryptoTransform.TransformFinalBlock(stringArrayToEncrypt, 0, stringArrayToEncrypt.Length);
            tdes.Clear(); 
            string stringEncrypted = Convert.ToBase64String(arrayResult, 0, arrayResult.Length);
            return stringEncrypted;
        }
        public string Dencrypt(string pString)
        {
            string key = "KeyForEncryption";
            byte[] stringArrayToDencrypt = Convert.FromBase64String(pString); 
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider(); 
            byte[] arrayKey = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key)); 
            md5.Clear(); 
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = arrayKey; 
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7; 
            ICryptoTransform cryptoTransform = tdes.CreateDecryptor(); 
            byte[] arrayResult = cryptoTransform.TransformFinalBlock(stringArrayToDencrypt, 0, stringArrayToDencrypt.Length);
            tdes.Clear(); 
            string stringDencrypted = UTF8Encoding.UTF8.GetString(arrayResult); 
            return stringDencrypted;
        }
    }
}
