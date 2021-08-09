using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Diaries.Models
{
    public class CaseOutcome
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CO_ID { get; set; }
        [StringLength(30)]
        [Display(Name = "Case Outcome"), Required]
        public string CO_Name { get; set; }
        [StringLength(30)]
        [Display(Name = "Case Outcome Type"), Required]
        public string CO_Type { get; set; }
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