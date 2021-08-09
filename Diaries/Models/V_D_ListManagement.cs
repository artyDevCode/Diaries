using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diaries.Models
{
    public class V_D_ListManagement
    {
        public int D_L_id { get; set; }
        public int D_id { get; set; }
        public string D_L_Lists { get; set; }
        public string D_L_ShortName { get; set; }
        public string D_L_Rules { get; set; }
        public string D_L_Type { get; set; }
        public string D_L_RulesApplyTo { get; set; }
        public string D_L_VacateOptions { get; set; }
        public string D_L_Bckground_row_Colour { get; set; }
        public string D_L_Text_Colour { get; set; }
        public string D_L_MandatoryFields { get; set; }
        public string D_L_Default_StartTime { get; set; }
        public virtual ICollection<D_L_Vacate_Reason> D_Attr_Vacate_Reason { get; set; }     
        public virtual ICollection<D_L_Outcome> D_Attr_Outcome { get; set; }
        public virtual ICollection<D_L_Flag> D_Attr_Flag { get; set; }
        public virtual ICollection<D_L_Location> D_Attr_Location { get; set; }
        public virtual ICollection<D_L_Category> D_Attr_Category { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool Deleted { get; set; }
    }
}