using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNWTT.Models.DO
{
    public class Desktop
    {
        public int id { get; set; }
        public string Processor { get; set; }
        public string RAM { get; set; }
        public string GPU { get; set; }
        public string HardDisk { get; set; }
        public string Screen { get; set; }
        public string OS { get; set; }
        public string Connect { get; set; }
        public string Pin { get; set; }
        public string Product_Id { get; set; }
    }
}