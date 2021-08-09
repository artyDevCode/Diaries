using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Diaries.Models
{
    public class JudicialOfficerListing
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int JOL_ID { get; set; }
        public int JO_ID { get; set; }
        [StringLength(100)]
        [Display(Name = "Judicial Officer"), Required]
        public string JOL_Name { get; set; }
        [StringLength(10)]
        [Display(Name = "Bacground Colour")]
        public string JOL_BackColor { get; set; }
        [StringLength(10)]
        [Display(Name = "Text Colour")]
        public string JOL_TextColor { get; set; }
        [Display(Name = "Date"), Required]
        public DateTime JOL_Date { get; set; }
        [Display(Name = "Limit"), Required]
        public int JOL_Limit { get; set; }
        [Display(Name = "Current")]
        public int JOL_Current { get; set; }
        [Display(Name = "Available")]
        public int JOL_Available { get; set; }
        [StringLength(100)]
        [Display(Name = "Notes")]
        public string JOL_Notes { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [StringLength(100)]
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }
    }
}