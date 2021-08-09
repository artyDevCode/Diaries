using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Diaries.Models
{
    public class D_L_Vacate_Reason
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int VR_id { get; set; }
        public int D_L_id { get; set; }
        public string VR_Name { get; set; }
        public string VR_FilterType { get; set; }
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
