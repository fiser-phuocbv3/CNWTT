using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNWTT.Models.DO
{
    public class Manufacturer
    {
        public int id { get; set; }
        public string name { get; set; }

        public Manufacturer() { }

        public Manufacturer(int id = 0, string name = "")
        {
            this.id = id;
            this.name = name;
        }
    }
}