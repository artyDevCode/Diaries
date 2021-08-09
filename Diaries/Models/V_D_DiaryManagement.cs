using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diaries.Models
{
    public class V_D_DiaryManagement
    {
        public int D_id { get; set; }
        public string D_DiaryName { get; set; }
        public string D_Fields { get; set; }
        public string D_Outline { get; set; }     
        public string D_DatePickerButton { get; set; }
        public string D_Standard_User { get; set; }
        public string D_ReadOnly { get; set; }
        public string D_Bckground_Icon_Colour { get; set; }
        public string D_Multiday_Cases { get; set; }
        public string D_Dates { get; set; }
        public string D_Vacated { get; set; }
        public string D_ListSelection { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<V_D_ListManagement> D_ListCollection { get; set; }
    }
}