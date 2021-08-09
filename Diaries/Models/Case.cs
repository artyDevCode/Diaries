using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Diaries.Models
{
    public class Case
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int C_ID { get; set; }
        [Display(Name = "Diary Id")]
        public int D_id { get; set; }
        [Display(Name = "Diary Name")]
        public string C_D_DiaryName { get; set; }
        public int C_D_L_id { get; set; }
        public string C_D_L_ShortName { get; set; }
        public string C_Rules { get; set; }
        public DateTime C_StartDate { get; set; }
        public DateTime C_EndDate { get; set; }
        public int C_NoOfDays { get; set; }
        public string C_Duration { get; set; }
        public DateTime C_StartTime { get; set; }
        [StringLength(30)]
        [Display(Name = "Case Number"), Required]
        public string C_Number { get; set; }
        [StringLength(100)]
        [Display(Name = "Accussed"), Required]
        public string C_Accused { get; set; }
        [StringLength(100)]
        [Display(Name = "Important"), Required]
        public string C_Informant { get; set; }
        [StringLength(100)]
        [Display(Name = "Major Change"), Required]
        public string C_MajorChange { get; set; }
        public string C_Solicitor { get; set; }
        public string C_Location { get; set; }
        public string C_Category { get; set; }
        public List<D_L_Flag>  C_Flags { get; set; }
        public string C_Notes { get; set; }
        public int C_JO_ID { get; set; }
        public string C_JudicialOfficer { get; set; }
        public string C_JO_Minutes { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [StringLength(100)]
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
