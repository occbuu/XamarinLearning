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
    
    public partial class User
    {
        public string UserID { get; set; }
        public string PWD { get; set; }
        public string ObjectID { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public string Status { get; set; }
        public string LoginName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string RoleID { get; set; }
        public string ManageGroup { get; set; }
    }
}
