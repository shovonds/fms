//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dsm.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class directory
    {
        public int id { get; set; }
        public string dir_id { get; set; }
        public string name { get; set; }
        public string parent_id { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public string deleted { get; set; }
        public Nullable<System.DateTime> lastModify_date { get; set; }
        public string type { get; set; }
        public string remarks { get; set; }
    }
}
