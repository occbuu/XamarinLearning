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
    
    public partial class WorkingTask
    {
        public string TaskID { get; set; }
        public string TaskName { get; set; }
        public Nullable<int> TechLevel { get; set; }
        public Nullable<int> LangLevel { get; set; }
        public string Description { get; set; }
        public string TaskType { get; set; }
        public string TaskURL { get; set; }
        public string Status { get; set; }
        public string PreviousTask { get; set; }
        public string NextTask { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}