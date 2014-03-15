using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MiniTrello.Api.Controllers.Helpers
{
    public class PasswordHelper
    {
        public string key = "miKey";
        public string Encriptar(string texto)
        {
            //arreglo de bytes donde guardaremos la llave 
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto 
            //que vamos a encriptar 
            byte[] arrayToEncode = Encoding.UTF8.GetBytes(texto);

            //se utilizan las clases de encriptación 
            //provistas por el Framework 
            //Algoritmo MD5 
            var hashmd5 =  new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice 
            //hashing 
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            //Algoritmo 3DAS 
            var tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena 
            ICryptoTransform cTransform = tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la 
            //cadena cifrada 
            byte[] arrayResult = cTransform.TransformFinalBlock(arrayToEncode, 0, arrayToEncode.Length);

            tdes.Clear();

            //se regresa el resultado en forma de una cadena 
            return Convert.ToBase64String(arrayResult, 0, arrayResult.Length);
        } 
    }
}