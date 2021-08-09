using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Diaries.Models
{
    public class DiariesDB :DbContext
    {
        public DiariesDB()
            : base("name=DefaultConnection")
        {

        }
        //Master Vacate Reasons setup by system admins
        public DbSet<VacateReason> tblVacateReason { get; set; }
        //Master Case Outcomes setup by system admins
        public DbSet<CaseOutcome> tblCaseOutcome { get; set; }
        //Master Non Sittings Days 
        public DbSet<NonSittingDay> tblNonSittingDay { get; set; }
        //Master Jusdicial Officers 
        public DbSet<JudicialOfficer> tblJudicialOfficer { get; set; }
        //Judicial Officer Listings
        public DbSet<JudicialOfficerListing> tblJudicialOfficerListing { get; set; }
        //Master Day Notes 
        public DbSet<DayNote> tblDayNote { get; set; }
        //Master Day Notes Apply to Diary List
        public DbSet<DN_ApplyToList> tblDNApplyToList { get; set; }
        //Master Diary 
        public DbSet<YearlyCalendarDates> tblYearlyCalendarDates { get; set; }
        //Diaries
        public DbSet<D_Diaries> tblDiary { get; set; }
        //Diary List Types 
        public DbSet<D_Lists> tblDiaryListTypes { get; set; }
        public DbSet<D_L_Vacate_Reason> tblDiaryListTypeVacateReasons { get; set; }
        public DbSet<D_L_Outcome> tblDiaryListTypeOutcomes { get; set; }
        public DbSet<D_L_Category> tblDiaryListTypeCategories { get; set; }
        public DbSet<D_L_Location> tblDiaryListTypeLocations { get; set; }
        public DbSet<D_L_Flag> tblDiaryListTypeFlags { get; set; }
        //Cases 
        public DbSet<Case> tblCase { get; set; }
        //Access Groups 
        public DbSet<Access> tblAccess { get; set; }
        //Audit/Trace Logs 
        public DbSet<Log> tblLog { get; set; }
        public DbSet<DiaryListingInfo> tblDiaryListingInfo { get; set; }
        public DbSet<DiaryListingInfoLog> tblDiaryListingInfoLog { get; set; }
    }
}