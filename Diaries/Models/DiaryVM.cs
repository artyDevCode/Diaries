using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Diaries.Models
{
    public class JOListingVM
    {
        public int VM_Id { get; set; }
        public int VM_JoId { get; set; }
        public string VM_JOName { get; set; }
        public string VM_JOBackColor { get; set; }
        public string VM_JOTextColor { get; set; }
        //[Display(Name = "Date")]
        //public string? VM_Day { get; set; }
        [Display(Name = "Dates"), Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Column("Dates", TypeName = "datetime")]
        public DateTime? VM_Date { get; set; }
        [Display(Name = "Limit"),Required]
        [Column("Limit", TypeName = "smallint")]
        [UIHint("NumberTemplate")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Limit must be entered as numeric")]
        public int? VM_Limit { get; set; }
        [Display(Name = "Current")]
        public int? VM_Current { get; set; }
        [Display(Name = "Available")]
        public int? VM_Available { get; set; }
        [StringLength(100)]
        [Display(Name = "Notes")]
        public string VM_Notes { get; set; }
        [StringLength(100)]
        public string VM_CreatedBy { get; set; }
        public DateTime VM_CreatedOn { get; set; }
        [StringLength(100)]
        public string VM_ModifiedBy { get; set; }
        public DateTime VM_ModifiedOn { get; set; }
        [DefaultValue(false)]
        public bool VM_Deleted { get; set; }
    }

    public class DayNoteVM
    {
        public int VM_Id { get; set; }
        public string VM_Active { get; set; }
        public int VM_Year { get; set; }
        public DateTime VM_Start { get; set; }
        public DateTime VM_End { get; set; }
        public string VM_Note { get; set; }
    }

    public class NonSittingDdayVM
    {
        public int VM_Id { get; set; }
        public string VM_Active { get; set; }
        public int VM_Year { get; set; }
        public DateTime VM_Date { get; set; }
        public string VM_Reason { get; set; }
    }
}