//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CNWTT
{
    using System;
    using System.Collections.Generic;
    
    public partial class laptop
    {
        public int id { get; set; }
        public string processor { get; set; }
        public string ram { get; set; }
        public string gpu { get; set; }
        public string hardDisk { get; set; }
        public string screen { get; set; }
        public string os { get; set; }
        public string connect { get; set; }
        public string pin { get; set; }
        public Nullable<int> product_id { get; set; }
    
        public virtual product product { get; set; }
    }
}
