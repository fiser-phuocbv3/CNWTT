using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNWTT.Models.DO
{
    public class BillDetail
    {
        public int id { get; set; }
        public string Number { get; set; }
        public string Money { get; set; }
        public string Accept { get; set; }
        public int Bill_id { get; set; }
        public int Product_id { get; set; }
    }
}