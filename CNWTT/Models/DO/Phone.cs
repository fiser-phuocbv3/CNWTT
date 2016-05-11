using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNWTT.Models.DO
{
    public class Phone
    {
        public int id { get; set; }
        public string PhoneType { get; set; }
        public string Screen { get; set; }
        public string Camera { get; set; }
        public string Memory { get; set; }
        public string OS { get; set; }
        public string Chipset { get; set; }
        public string Product_Id { get; set; }
    }
}