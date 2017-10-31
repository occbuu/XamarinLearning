﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PSNSPEntities : DbContext
    {
        public PSNSPEntities()
            : base("name=PSNSPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AnnualLeave> AnnualLeaves { get; set; }
        public virtual DbSet<AnnualLeaveDetail> AnnualLeaveDetails { get; set; }
        public virtual DbSet<CandidateInfo> CandidateInfoes { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<CommonCode> CommonCodes { get; set; }
        public virtual DbSet<CommonType> CommonTypes { get; set; }
        public virtual DbSet<DailyReport> DailyReports { get; set; }
        public virtual DbSet<DailyReportDetail> DailyReportDetails { get; set; }
        public virtual DbSet<DocumentSample> DocumentSamples { get; set; }
        public virtual DbSet<DRFeedback> DRFeedbacks { get; set; }
        public virtual DbSet<EmployeeInfo> EmployeeInfoes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamLib> ExamLibs { get; set; }
        public virtual DbSet<ExamResult> ExamResults { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Interview> Interviews { get; set; }
        public virtual DbSet<InterviewInfo> InterviewInfoes { get; set; }
        public virtual DbSet<LeaveApply> LeaveApplies { get; set; }
        public virtual DbSet<LeaveApplyDetail> LeaveApplyDetails { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationInfo> NotificationInfoes { get; set; }
        public virtual DbSet<Object> Objects { get; set; }
        public virtual DbSet<OfficialStatement> OfficialStatements { get; set; }
        public virtual DbSet<Payroll> Payrolls { get; set; }
        public virtual DbSet<PayrollEmployee> PayrollEmployees { get; set; }
        public virtual DbSet<PayrollEmployeeDetail> PayrollEmployeeDetails { get; set; }
        public virtual DbSet<PeopleSkill> PeopleSkills { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<PositionInfo> PositionInfoes { get; set; }
        public virtual DbSet<Punishment> Punishments { get; set; }
        public virtual DbSet<QA> QAs { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Role_Menu_Module> Role_Menu_Module { get; set; }
        public virtual DbSet<SkillSet> SkillSets { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TimeKeeping> TimeKeepings { get; set; }
        public virtual DbSet<TimeOutIn> TimeOutIns { get; set; }
        public virtual DbSet<TKDetail> TKDetails { get; set; }
        public virtual DbSet<UserLog> UserLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WorkingTaskHistory> WorkingTaskHistories { get; set; }
        public virtual DbSet<WorkingTask> WorkingTasks { get; set; }
    }
}
