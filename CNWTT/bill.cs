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
    
    public partial class bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bill()
        {
            this.billDetails = new HashSet<billDetail>();
        }
    
        public int id { get; set; }
        public string totalMoney { get; set; }
        public string accountAddress { get; set; }
        public string create_at { get; set; }
        public string update_at { get; set; }
        public Nullable<int> account_id { get; set; }
    
        public virtual account account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<billDetail> billDetails { get; set; }
    }
}
