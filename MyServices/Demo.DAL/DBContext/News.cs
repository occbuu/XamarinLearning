//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Demo.DAL.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class News
    {
        public long NewsID { get; set; }
        public string Subject { get; set; }
        public string Brief { get; set; }
        public string content { get; set; }
        public string Category { get; set; }
        public string NewsType { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ImgUrl { get; set; }
        public string Thumbnail { get; set; }
        public string NewsBy { get; set; }
    }
}
