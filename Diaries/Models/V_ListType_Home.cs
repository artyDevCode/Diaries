using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Diaries.Models
{
    public class V_ListType_Home
    {
        public int D_id { get; set; }
        public int D_L_id { get; set; }
        public string D_DiaryName { get; set; }
        public string D_Bckground_Icon_Colour { get; set; }
        public string D_ListName { get; set; }
        public string D_ListShortName { get; set; }
        public string D_L_Bckground_Row_Colour { get; set; }

        public bool Is_Calendar_Set { get; set; }

        public DateTime Current_Calendar_Start_Date { get; set; }
        public DateTime Current_Calendar_End_Date { get; set; }
        public DateTime Next_Calendar_Start_Date { get; set; }
        public DateTime Next_Calendar_End_Date { get; set; }

        public string Current_Calendar_Start_Day { get; set; }
        public string Current_Calendar_Start_Month { get; set; }
        public string Current_Calendar_Start_Year { get; set; }
        public string Current_Calendar_End_Day { get; set; }
        public string Current_Calendar_End_Month { get; set; }
        public string Current_Calendar_End_Year { get; set; }
        public string Next_Calendar_Start_Day { get; set; }
        public string Next_Calendar_Start_Month { get; set; }
        public string Next_Calendar_Start_Year { get; set; }
        public string Next_Calendar_End_Day { get; set; }
        public string Next_Calendar_End_Month { get; set; }
        public string Next_Calendar_End_Year { get; set; }
    }

    public class V_ListType_Listing
    {
        public int D_id { get; set; }
        public int D_L_id { get; set; }
        public string V_L_Day { get; set; }
        public DateTime V_L_Date { get; set; }
        public int V_L_Limit { get; set; }
        public int V_L_Current { get; set; }
        public int V_L_Available { get; set; }
    }

    public class V_ListType_ListingRules
    {
        public int D_id { get; set; }
        public int D_L_id { get; set; }
        public int V_Limit { get; set; }
        public int V_Notes { get; set; }
        public bool MondaySelected { get; set; }
        public bool TuesdaySelected { get; set; }
        public bool WednesdaySelected { get; set; }
        public bool ThursdaySelected { get; set; }
        public bool FridaySelected { get; set; }
        public bool dMondaySelected { get; set; }
        public bool dTuesdaySelected { get; set; }
        public bool dWednesdaySelected { get; set; }
        public bool dThursdaySelected { get; set; }
        public bool dFridaySelected { get; set; }
        public virtual ICollection<DiaryListingInfoLog> UpdateHistory { get; set; }

    }
}