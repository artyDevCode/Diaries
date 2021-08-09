using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Diaries.Models
{
    public class D_Diaries
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int    D_id { get; set; }
        [Display(Name = "Diary:"), Required]
        public string D_DiaryName { get; set; }
        [StringLength(30)]
        [Display(Name = "Fields:"), Required]
        public string D_Fields { get; set; }
        [Display(Name = "Outline:"), Required]
        public string D_Outline  { get; set; }
        [Display(Name = "Date Picker:"), Required]
        public string D_DatePickerButton { get; set; }
        [Display(Name = "Standard User:"), Required]
        public string D_Standard_User { get; set; }
        [Display(Name = "Read Only:"), Required]
        public string D_ReadOnly { get; set; }
        [Display(Name = "Background Icon Colour:"), Required]
        public string D_Bckground_Icon_Colour { get; set; }
        [Display(Name = "Multiday Cases:"), Required]
        public string D_Multiday_Cases { get; set; }
        [Display(Name = "Dates:"), Required]
        public string D_Dates { get; set; }
        [Display(Name = "Vacated:"), Required]
        public string D_Vacated { get; set; }
        [Display(Name = "List Selection:"), Required]
        public string D_ListSelection { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [StringLength(100)]
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }
        public virtual ICollection<D_Lists> D_ListCollection { get; set; }
    }
}