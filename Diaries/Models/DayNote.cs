using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Web.Mvc;

namespace Diaries.Models
{
    public class DayNote
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DN_ID { get; set; }
        [Display(Name = "Start Date"), Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DN_Start_Date { get; set; }
        [Display(Name = "End Date"), Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DN_End_Date { get; set; }
        
        [StringLength(10)]
        [Display(Name = "Background Colour"), Required]
        public string DN_BackColor { get; set; }
        [StringLength(10)]
        [Display(Name = "Text Colour"), Required]
        public string DN_TextColor { get; set; }
        [StringLength(100)]
        [Display(Name = "Notes"), Required]
        public string DN_Note { get; set; }
        //[DefaultValue(true)]
        //[Display(Name = "Apply To"), Required]
        //public bool DN_All_Diaries { get; set; }
        //public IEnumerable<DN_Diaries> Features { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [StringLength(100)]
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }

        //this list has all movies available for dropdown
        public List<D_Diaries> AvailableDiaries { get; set; }
        //this list has our default values 
        public List<D_Diaries> DefaultDiaries { get; set; }
        //this will retrieve the ids of movies selected in list when submitted
        [Display(Name = "Submitted Diaries")]
        public List<string> SubmittedDiaries { get; set; }

        public List<DN_ApplyToList> EditableDiaries { get; set; }
        //this list has our default values 
        public List<DN_ApplyToList> DefaultEditableDiaries { get; set; }

        //public virtual IEnumerable<int> DN_SelectedApplyToListCollection { get; set; }
        //public virtual IEnumerable<SelectListItem> DN_ApplyToListCollection { get; set; }

        public virtual ICollection<DN_ApplyToList> DN_DiaryListCollection { get; set; }
    }
}