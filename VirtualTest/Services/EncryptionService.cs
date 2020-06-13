using System;
using System.IO;
using System.Security.Cryptography;
using VirtualTest.Services.Interfaces;

namespace VirtualTest.Services
{
    public class EncryptionService : IEncryptionService
    {
        private byte[] key;
        private byte[] iv;
        private Aes encrypyAes;

        public EncryptionService()
        {
            using (encrypyAes = Aes.Create())
            {
                encrypyAes.GenerateKey();
                encrypyAes.GenerateIV();

                key = encrypyAes.Key;
                iv = encrypyAes.IV;
            }
        }

        public string Encrypt(string text)
        {
            byte[] encryptedText;

            ICryptoTransform encrypter = encrypyAes.CreateEncryptor(key, iv);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encrypter, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }
                    encryptedText = msEncrypt.ToArray();
                }
            }

            return Convert.ToBase64String(encryptedText);
        }

        public string Dencrypt(string text)
        {
            string decryptedText;

            ICryptoTransform descriptador = encrypyAes.CreateDecryptor(key, iv);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(text)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, descriptador, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        decryptedText = srDecrypt.ReadToEnd();
                    }
                }
            }

            return decryptedText;
        }
    }
}
