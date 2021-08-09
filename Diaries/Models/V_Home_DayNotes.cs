using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diaries.Models
{
    public class V_Home_DayNotes
    {
        public int V_H_DayNotes_id { get; set; }
        public int D_id { get; set; }
        public string V_H_DayNotes_SName { get { return "Day Notes"; } }
        public string V_H_DayNotes_Notes { get; set; }
    }
}