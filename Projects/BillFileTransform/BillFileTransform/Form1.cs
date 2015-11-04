using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace BillFileTransform
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
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            ofd.Filter = "Unencrypted Files (*.txt)|*.txt|Encrypted Files (*.enc)|*.enc|All Files|*.*";
            ofd.FilterIndex = 3;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtName.Text = ofd.FileName;
            }
        }
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string cypherText;
            string plainText;
            string path;
            string newFile = "c:\\Decrypted_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            string[] lines;

            //MessageBox.Show(System.IO.Directory.
            path = txtName.Text;
            if (! System.IO.File.Exists(path))
            {
                lblMsg.Text = "File not found";
                return;
            }
            else if (System.IO.Path.GetExtension(path).ToLower() != ".enc")
            {
                lblMsg.Text = "File doesn't seem encrypted";
                return;
            }
            cypherText = System.IO.File.ReadAllText(path);
            plainText = Decrypt(cypherText);
            lines = plainText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            System.IO.File.WriteAllLines(newFile, lines);
            lblMsg.Text = "Output written to: " + newFile;
        }
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string cypherText;
            string plainText;
            string path;
            string newFile = "c:\\Encrypted_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".enc";

            path = txtName.Text;
            if (!System.IO.File.Exists(path))
            {
                lblMsg.Text = "File not found";
                return;
            }
            else if (System.IO.Path.GetExtension(path).ToLower() != ".txt")
            {
                lblMsg.Text = "File doesn't seem plain text";
                return;
            }
            plainText = System.IO.File.ReadAllText(path);
            cypherText = Encrypt(plainText);
            System.IO.File.WriteAllText(newFile, cypherText);
            lblMsg.Text = "Output written to: " + newFile;
        }
    }
}
