using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNWTT.Models.DO
{
    public class Bill
    {
        public int id { get; set; }
        public string TotalMoney { get; set; }
        public string AccountAddress { get; set; }
        public string Create_at { get; set; }
        public string Uptate_at { get; set; }
        public int Account_id { get; set; }
    }
}