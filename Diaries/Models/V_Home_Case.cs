using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diaries.Models
{
    public class V_Home_Case
    {
        public int V_H_Case_id { get; set; }
        public string V_H_Case_SName { get; set; }
        public string V_H_Case_MajorChange { get; set; }
        public string V_H_Case_Solicitor { get; set; }
        public string V_H_Case_Location { get; set; }
        public string V_H_Case_Category { get; set; }

        public string V_H_Case_Accused { get; set; }
        public string V_H_Case_Number { get; set; }
        public string V_H_Case_Informant { get; set; }
        public int V_H_Case_NoOfDays { get; set; }
        public List<D_L_Flag> V_H_Case_Flag { get; set; }
        public string V_H_Case_Notes { get; set; }
        public string V_H_Case_JudicialOfficer { get; set; }
        public DateTime V_H_Case_StartTime { get; set; }
        public DateTime V_H_Case_DateFrom { get; set; }
        public DateTime V_H_Case_DateTo { get; set; }

    }
}