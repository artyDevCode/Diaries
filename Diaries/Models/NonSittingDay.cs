using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
namespace Diaries.Models
{
    public class NonSittingDay
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NSD_ID { get; set; }
        [Display(Name = "Non Sitting Date"), Required]
        public DateTime NSD_Date { get; set; }
        [StringLength(10)]
        [Display(Name = "Back Colour"), Required]
        public string NSD_BackColor { get; set; }
        [StringLength(10)]
        [Display(Name = "Text Colour"), Required]
        public string NSD_TextColor { get; set; }
        [StringLength(30)]
        [Display(Name = "Reason"), Required]
        public string NSD_Reason { get; set; }
        [Display(Name = "Active"), Required]
        public bool NSD_Active { get; set; }
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