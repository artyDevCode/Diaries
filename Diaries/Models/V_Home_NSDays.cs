using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diaries.Models
{
    public class V_Home_NSDays
    {
        public int V_H_NSDays_id { get; set; }
        public string V_H_NSDays_SName { get { return "NSD"; } }
        public string V_H_NSDays_Reason { get; set; }
    }
}