using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Diaries.Models
{
    public class D_Lists
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int D_L_id { get; set; }
        public int D_id { get; set; }
        [StringLength(50)]
        public string D_L_Lists { get; set; }
        [Display(Name="Short Name:")]
        [StringLength(8)]
        public string D_L_ShortName { get; set; }
        public bool D_L_Rules { get; set; }
        public string D_L_Type { get; set; }
        [Display(Name = "Rules Apply To:")]
        public string D_L_RulesApplyTo { get; set; }
        [Display(Name = "Vacate Options:")]
        public string D_L_VacateOptions { get; set; }
        [Display(Name = "Background Row Colour:")]
        public string D_L_Bckground_row_Colour { get; set; }
        [Display(Name = "Text Colour:")]
        public string D_L_Text_Colour { get; set; }
        [Display(Name = "Default Start Time:")]
        public string D_L_Default_StartTime { get; set; }
         [Display(Name = "Vacate Reasons:")]
        public bool D_L_MandatoryCategory { get; set; }
        public bool D_L_MandatoryDuration { get; set; }
        public bool D_L_MandatoryLocation { get; set; }
        public bool D_L_MandatoryStartTime { get; set; }
        public virtual ICollection<D_L_Vacate_Reason> D_Attr_Vacate_Reason { get; set; }
         [Display(Name = "Case Outcomes:")]
        public virtual ICollection<D_L_Outcome> D_Attr_Outcome { get; set; }
         [Display(Name = "Flag:")]
        public virtual ICollection<D_L_Flag> D_Attr_Flag { get; set; }
         [Display(Name = "Location:")]
        public virtual ICollection<D_L_Location> D_Attr_Location { get; set; }
         [Display(Name = "Category:")]
        public virtual ICollection<D_L_Category> D_Attr_Category { get; set; }
         [StringLength(100)]
         public string CreatedBy { get; set; }
         public DateTime? CreatedOn { get; set; }
         [StringLength(100)]
         public string ModifiedBy { get; set; }
         public DateTime? ModifiedOn { get; set; }
         [DefaultValue(false)]
         public bool Deleted { get; set; }

    }
}