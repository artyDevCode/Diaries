using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diaries.Models
{
    public class V_Home_DiaryListInfo
    {

        public int V_H_DiaryId { get; set; }
        public int V_H_DiaryListDetails_id { get; set; }
        public string V_H_DiaryListDetails_SName { get; set; }
        public int V_H_DiaryListDetails_Avail { get; set; }
        public int V_H_DiaryListDetails_Total { get; set; }
        public string V_H_DiaryListDetails_BkgColour { get; set; }
        public string V_H_DiaryListDetails_Colour { get; set; }
        public DateTime V_H_DiaryListDetails_DateCreated { get; set; }
        public string V_H_Notes { get; set; }

    }
}