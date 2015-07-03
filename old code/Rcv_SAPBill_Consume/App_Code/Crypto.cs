using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;

public class Crypto
{
    private static string HexAsciiConvert(string hex)
    {

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i <= hex.Length - 2; i += 2)
        {

            sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(i, 2),
            System.Globalization.NumberStyles.HexNumber))));
        }
        return sb.ToString();
    }

    public static string Cipher(string toEncrypt, string key, string iv)
    {
        byte[] keyArray;
        byte[] keyIv;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();


        iv = HexAsciiConvert(iv);

        keyArray = ASCIIEncoding.ASCII.GetBytes(key);
        keyIv = ASCIIEncoding.ASCII.GetBytes(iv);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.KeySize = 192;
        tdes.Key = keyArray;
        tdes.IV = keyIv;
        tdes.Mode = CipherMode.CBC;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        tdes.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }
}