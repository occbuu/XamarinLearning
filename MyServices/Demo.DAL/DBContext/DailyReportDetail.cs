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
    
    public partial class DailyReportDetail
    {
        public long id { get; set; }
        public Nullable<long> DRId { get; set; }
        public string TaskID { get; set; }
        public string Note { get; set; }
        public Nullable<int> WorkingHours { get; set; }
        public Nullable<int> EstimateToFinish { get; set; }
        public Nullable<int> PerOfComplete { get; set; }
    }
}
