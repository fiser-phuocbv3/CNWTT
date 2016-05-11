using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNWTT.Models.DO;
using System.Text;
using System.Security.Cryptography;

namespace CNWTT.Core.User
{
    public class HomeCore
    {
        public static int sumMoney(IList<Cart> listCart, IList<product> listProduct)
        {
            int sum = 0;
            if (listCart != null && listProduct != null)
            {
                foreach (var listP in listProduct)
                {
                    foreach (var listC in listCart)
                    {
                        if (listP.id == Int32.Parse(listC.id))
                        {
                            sum += (Int32.Parse(listC.count) * Int32.Parse(listP.cost));
                        }
                    }
                }
            }
            return sum;
        }

        public static IList<billDetail> getListBillByListCartAndListProduct(IList<Cart> listC, IList<product> listP, int bill_id)
        {
            IList<billDetail> list = new List<billDetail>();
            if (listC != null && listP != null && bill_id > 0)
            {
                foreach (var p in listP)
                {
                    foreach (var c in listC)
                    {
                        if (p.id == Int32.Parse(c.id))
                        {
                            var billDetail = new billDetail
                            {
                                number = c.count,
                                money = p.cost,
                                accept = "1",
                                bill_id = bill_id,
                                product_id = p.id
                            };
                            list.Add(billDetail);
                        }
                    }
                }
            }
            return list;
        }

        public static void caculatorPrice(string selectPrice, out string startCost, out string endCost)
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