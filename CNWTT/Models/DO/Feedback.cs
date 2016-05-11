using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNWTT.Models.DO
{
    public class Feedback
    {
        public int Id { get; set; }
        public string content { get; set; }
        public int account_id { get; set; }

        public Feedback() { }

        
    }
}