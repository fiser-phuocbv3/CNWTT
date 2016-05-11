using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNWTT.Models.DO
{
    public class Cart
    {
        public string id { get; set; }
        public string count { get; set; }

        public Cart() { }

        public Cart(string id = "", string count = "")
        {
            this.id = id;
            this.count = count;
        }
    }
}