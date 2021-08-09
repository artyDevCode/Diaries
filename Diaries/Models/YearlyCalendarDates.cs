using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Diaries.Models
{
    public class YearlyCalendarDates
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Calendar_Id { get; set; }
        
        [Display(Name = "Current Year Start Date")]
        [Required(ErrorMessage = "Please enter the calendar start date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CurrentStartDate { get; set; }

        [Display(Name = "Current Year End Date")]
        [Required(ErrorMessage = "Please enter the calendar start date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CurrentEndDate { get; set; }

        [Display(Name = "Next Calendar Start Date")]
        [Required(ErrorMessage = "Please enter the next years start date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NextStartDate { get; set; }

        [Display(Name = "Next Calendar End Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please enter the next years end date")]
        public DateTime NextEndDate { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [StringLength(100)]
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}