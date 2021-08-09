using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Diaries.Models
{
    public class DiaryListingInfoLog
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int D_LIL_id { get; set; }
        public int D_L_id { get; set; }
        public DateTime LogDate { get; set; }
        public string LogText { get; set; }

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