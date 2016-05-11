using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace CNWTT.Core.Admin
{
    public class AdminCore
    {
        
        public static void caculatorPrice(string selectPrice,out string startCost,out string endCost)
        {
            startCost = "";
            endCost = "";
            switch (selectPrice)
            {
                case "1":
                    startCost = "0";
                    endCost = "5000000";
                    break;
                case "2":
                    startCost = "5000000";
                    endCost = "10000000";
                    break;
                case "3":
                    startCost = "10000000";
                    endCost = "20000000";
                    break;
                case "4":
                    startCost = "20000000";
                    endCost = "100000000";
                    break;
            }
        }

        public static string returnProductType(string content)
        {
            string productType = "";
            switch (content)
            {
                case "mobile":
                    productType = "1";
                    break;
                case "tablet":
                    productType = "2";
                    break;
                case "laptop":
                    productType = "3";
                    break;
                case "desktop":
                    productType = "4";
                    break;
                case "accessories":
                    productType = "5";
                    break;
            }
            return productType;
        }

        public static void saveImage5(HttpPostedFile[] image)
        {

        }

        public static string EncodeMD5(string password)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(password);
            encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }
    }
}