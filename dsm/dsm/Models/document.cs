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
    
    public partial class document
    {
        public int id { get; set; }
        public Nullable<System.DateTime> creation_date { get; set; }
        public string name { get; set; }
        public string dir_id { get; set; }
        public Nullable<System.DateTime> modfy_date { get; set; }
        public string identity_1 { get; set; }
        public string deleted { get; set; }
        public string type { get; set; }
        public Nullable<int> size { get; set; }
        public string location_id { get; set; }
        public string remarks { get; set; }
    }
}
