using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Diaries.Models
{
    public class DN_ApplyToList
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DNA_ID { get; set; }
        public int DN_ID { get; set; }
        public int D_id { get; set; }
        [Display(Name = "Diary Name")]
        public string D_DiaryName { get; set; }
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