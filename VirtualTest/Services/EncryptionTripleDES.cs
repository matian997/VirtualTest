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

        public string Encrypt(string pCadena)
        {
            string key = "ClaveParaEncriptar";
            byte[] vectorCadenaAEncriptar = UTF8Encoding.UTF8.GetBytes(pCadena); //Se obtiene un vector de byte de la cadena ingresada.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider(); //se instancia una clase de MD5 para poder utilizar sus metodos de encriptacion.
            byte[] vectorKey = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key)); //se obtiene un vector de bytes de la clave elegida.
            md5.Clear(); //Se libera los recursos que utiliza md5.
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider(); //se crea una instancia de la clase Triple DES para utilizr sus metodos de encriptacion.
            tdes.Key = vectorKey; //Se le asigna la clave elegida anteriormente.
            tdes.Mode = CipherMode.ECB; //se elige el modo de cifrado.
            tdes.Padding = PaddingMode.PKCS7; //Es el tipo de relleno cuando no se alcanza la cantidad de bytes necesarios para la encriptacion.
            ICryptoTransform cryptoTransform = tdes.CreateEncryptor(); //Se crea un objeto para encriptar.
            byte[] vectorResultado = cryptoTransform.TransformFinalBlock(vectorCadenaAEncriptar, 0, vectorCadenaAEncriptar.Length);
            tdes.Clear(); //Libera los recursos utilizados por tdes.
            string cadenaEncriptada = Convert.ToBase64String(vectorResultado, 0, vectorResultado.Length); //Se transforma el resultado obtenido de bytes a string.
            return cadenaEncriptada;
        }
        public string Dencrypt(string pCadena)
        {
            string key = "ClaveParaEncriptar";//Esta clave tiene que ser la misma que se uso para encriptar los datos.
            byte[] vectorCadenaADesencriptar = Convert.FromBase64String(pCadena); //Se obtiene un vector de byte de la cadena encriptada.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider(); //Se instancia una clase de MD5 para utilizar sus metods de encriptacion.
            byte[] vectorKey = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key)); //Se obtiene un vector de bytes de la clave.
            md5.Clear(); //Se libera los recursos que utiliza md5.
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = vectorKey; //se le asigna la clave elegida anteriormente.
            tdes.Mode = CipherMode.ECB; //Se elige el mismo modo cifrado utilizado para la encriptacion.
            tdes.Padding = PaddingMode.PKCS7; //Se elige el mismo tipo de relleno que se utilizo en la encriptacion.
            ICryptoTransform cryptoTransform = tdes.CreateDecryptor(); //Se creo un objeto para desencriptar.
            byte[] vectorResultado = cryptoTransform.TransformFinalBlock(vectorCadenaADesencriptar, 0, vectorCadenaADesencriptar.Length);
            tdes.Clear(); //Se libera los recursos que utiliza tdes.
            string cadenaDesencriptada = UTF8Encoding.UTF8.GetString(vectorResultado); //Se tranforma el resultado obtenido de bytes a string.
            return cadenaDesencriptada;
        }
    }
}
