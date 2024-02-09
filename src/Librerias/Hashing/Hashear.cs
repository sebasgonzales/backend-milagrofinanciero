using System.Security.Cryptography;
using System.Text;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Hashing
{
    public class Hashear
    {

        /*public Hashing()
        {
            InitializeComponent();
        }*/


        public string HashearConSHA256(string datoEntrada)
        {
            string salida = "";
            try
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(datoEntrada));

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    salida = builder.ToString();
                }
            }
            catch
            {
                salida = "Error";
            }
            return salida;
        }

    }

}

