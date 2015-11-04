using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
namespace BillTransDD
{
    public partial class Form1 : Form
    {
        public const string strIV = "C05A8F2F1C83121A";
        public const string keystr = "1234567890abcdefabcdefab";

        public static string HexAsciiConvert(string hex)
        {

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= hex.Length - 2; i += 2)
            {

                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(i, 2),
                System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }
        public static string Decrypt(string cipherString)
        {
            byte[] keyArray;
            byte[] keyIv;
            byte[] toDecryptArray = Convert.FromBase64String(cipherString);

            string ivstr = HexAsciiConvert(strIV);

            keyArray = ASCIIEncoding.ASCII.GetBytes(keystr);
            keyIv = ASCIIEncoding.ASCII.GetBytes(ivstr);
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.KeySize = 192;
            tdes.Key = keyArray;
            tdes.IV = keyIv;
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public static string Encrypt(string plainString)
        {
            byte[] keyArray;
            byte[] keyIv;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(plainString);

            string ivstr = HexAsciiConvert(strIV);

            keyArray = ASCIIEncoding.ASCII.GetBytes(keystr);
            keyIv = ASCIIEncoding.ASCII.GetBytes(ivstr);

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
        public Form1()
        {
            InitializeComponent();
        }

        private void StartDecrypt(string path)
        {
            string cypherText;
            string plainText;
            string newFile = "Dec_" + Path.GetFileNameWithoutExtension(path) + ".txt";
            string[] lines;

            cypherText = System.IO.File.ReadAllText(path);
            plainText = Decrypt(cypherText);
            lines = plainText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            System.IO.File.WriteAllLines(newFile, lines);
            lblMsg.Text += Environment.NewLine + Path.GetFileName(path) + " - Decrypted to: " + newFile;
        }

        private void StartEncrypt(string path)
        {
            string cypherText;
            string plainText;
            string newFile = "Enc_" + Path.GetFileNameWithoutExtension(path) + ".enc";

            plainText = System.IO.File.ReadAllText(path);
            cypherText = Encrypt(plainText);
            System.IO.File.WriteAllText(newFile, cypherText);
            lblMsg.Text += Environment.NewLine + Path.GetFileName(path) + " - Encrypted to: " + newFile;
        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files;
            //lblMsg.Text = "";
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    if (Path.GetExtension(file).ToLower() == ".enc")
                    {
                        StartDecrypt(file);
                    }
                    else if (Path.GetExtension(file).ToLower() == ".txt")
                    {
                        StartEncrypt(file);
                    }
                    else
                    {
                        lblMsg.Text += Environment.NewLine + file + " - Invalid File";
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AllowDrop = true;
        }
    }
}
