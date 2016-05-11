using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNWTT.Models.DO
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string cost { get; set; }
        public string picture { get; set; }
        public string number { get; set; }
        public string describle { get; set; }
        public string productType { get; set; } 
        public string manufacturer_id { get; set; }
    }
}