using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diaries.Models
{
    public class DiaryListingInfo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int D_LI_id { get; set; }
        public int D_id { get; set; }
        public int D_L_id { get; set; }
        public int D_LI_Total { get; set; }
        //public string D_LI_ShortName { get; set; }
        public DateTime D_LI_Date { get; set; }
        public int D_LI_Avail { get; set; }
        public int D_LI_Current { get; set; }
        //public string D_LI_BkgColour { get; set; }
        //public string D_LI_Colour { get; set; }
        public string D_LI_Notes { get; set; }
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