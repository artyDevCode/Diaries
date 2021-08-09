using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diaries.Models
{
    public class V_HomePage
    {
        public int V_HomePage_id { get; set; }
        public IEnumerable<V_Home_DiaryNames> V_HomePage_DiaryNames { get; set; }
        public IEnumerable<V_Home_DiaryListInfo> V_HomePage_DiaryListDetails { get; set; }
        public IEnumerable<V_Home_DayNotes> V_HomePage_DayNotes { get; set; }
        public IEnumerable<V_Home_NSDays> V_HomePage_NSDays { get; set; }
        public IEnumerable<V_Home_Case> V_HomePage_Cases { get; set; }
    }
}