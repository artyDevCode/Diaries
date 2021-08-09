using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Diaries.Models
{
    public class V_Diary_Details
    {
        public int D_id { get; set; }
        public string D_DiaryName { get; set; }
        public string D_Fields { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> D_SelectD_Fields { get; set; }
        public string D_Outline { get; set; }
        public string D_DatePickerButton { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> D_DatePicker_Fields { get; set; }
        public string D_Standard_User { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> D_StandardUser_Fields { get; set; }
        public string D_ReadOnly { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> D_ReadOnly_Fields { get; set; }
        public string D_Bckground_Icon_Colour { get; set; }
        public string D_Multiday_Cases { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> D_Multiday_Cases_Fields { get; set; }
        public string D_Dates { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> D_Dates_Fields { get; set; }
        public string D_Vacated { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> D_Vacated_Fields { get; set; }
        public string D_ListSelection { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> D_ListSelection_Fields { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool Deleted { get; set; }
    }

    public class V_Diary_List_Attrs
    {
        public virtual ICollection<D_L_Vacate_Reason> D_Attr_Vacate_Reason { get; set; }
        public virtual ICollection<D_L_Outcome> D_Attr_Outcome { get; set; }
        public virtual ICollection<D_L_Flag> D_Attr_Flag { get; set; }
        public virtual ICollection<D_L_Location> D_Attr_Location { get; set; }
        public virtual ICollection<D_L_Category> D_Attr_Category { get; set; }
        public bool D_Attr_Vacate_Reason_Count { get; set; }
        public bool D_Attr_Flag_Count { get; set; }
        public bool D_Attr_Outcome_Count { get; set; }
        public bool D_Attr_Location_Count { get; set; }
        public bool D_Attr_Category_Count { get; set; }
    }

    public class V_Diary_List_Details
    {
        public int D_L_id { get; set; }
        public int D_id { get; set; }
        public string D_L_Lists { get; set; }
        public string D_L_ShortName { get; set; }
        public bool D_L_Rules { get; set; }
        public string D_L_Type { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> D_L_Select_Type { get; set; }
        public string D_L_RulesApplyTo { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> D_L_Select_RulesApplyTo { get; set; }
        public string D_L_VacateOptions { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> D_L_Select_VacateOptions { get; set; }
        public string D_L_Bckground_row_Colour { get; set; }
        public string D_L_Text_Colour { get; set; }
        public string D_L_Default_StartTime { get; set; }
        public bool D_L_MandatoryCategory { get; set; }
        public bool D_L_MandatoryDuration { get; set; }
        public bool D_L_MandatoryLocation { get; set; }
        public bool D_L_MandatoryStartTime { get; set; }
        public virtual ICollection<D_L_Vacate_Reason> D_Attr_Vacate_Reason { get; set; }
        public virtual ICollection<D_L_Outcome> D_Attr_Outcome { get; set; }
        public virtual ICollection<D_L_Flag> D_Attr_Flag { get; set; }
        public virtual ICollection<D_L_Location> D_Attr_Location { get; set; }
        public virtual ICollection<D_L_Category> D_Attr_Category { get; set; }
        public int D_Attr_Vacate_Reason_Count { get; set; }
        public int D_Attr_Flag_Count { get; set; }
        public int D_Attr_Outcome_Count { get; set; }
        public int D_Attr_Location_Count { get; set; }
        public int D_Attr_Category_Count { get; set; }
    }

    public class V_Diary_List_VacateReasons
    {
        public int V_VR_id { get; set; }
        public int V_VR_D_L_id { get; set; }
        public string V_VR_Name { get; set; }
        public string V_VR_FilterType { get; set; }
        public string V_VR_CreatedBy { get; set; }
        public DateTime V_VR_CreatedOn { get; set; }
        public string V_VR_ModifiedBy { get; set; }
        public DateTime V_VR_ModifiedOn { get; set; }
        public bool V_VR_Deleted { get; set; }
        public string V_VR_AllFilter { get; set; }
        public string V_VR_CivilFilter { get; set; }
        public string V_VR_CriminalFilter { get; set; }
        public string V_VR_CommittalsFilter { get; set; }
        public string V_VR_InterventionOrdersFilter { get; set; }
        public string V_VR_VOCATFilter { get; set; }
        public string V_VR_ChildrensCourtFilter { get; set; }
        public string V_VR_SelectFilter { get; set; }
        public string V_VR_SelectName { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> V_VRSelect_Type { get; set; }
    }

    public class V_Diary_List_Outcomes
    {
        public int V_O_id { get; set; }
        public int V_O_D_L_id { get; set; }
        public string V_O_Name { get; set; }
        public string V_O_FilterType { get; set; }
        public string V_O_CreatedBy { get; set; }
        public DateTime V_O_CreatedOn { get; set; }
        public string V_O_ModifiedBy { get; set; }
        public DateTime V_O_ModifiedOn { get; set; }
        public bool V_O_Deleted { get; set; }
        public string V_O_AllFilter { get; set; }
        public string V_O_CivilFilter { get; set; }
        public string V_O_CriminalFilter { get; set; }
        public string V_O_CommittalsFilter { get; set; }
        public string V_O_InterventionOrdersFilter { get; set; }
        public string V_O_VOCATFilter { get; set; }
        public string V_O_ChildrensCourtFilter { get; set; }
        public string V_O_SelectFilter { get; set; }
        public string V_O_SelectName { get; set; }
        public IEnumerable<System.Web.WebPages.Html.SelectListItem> V_OSelect_Type { get; set; }
    }

    public class V_Diary_List_Locations
    {
        public int V_L_id { get; set; }
        public int V_L_D_L_id { get; set; }
        public string V_L_Name { get; set; }
        public string V_L_Text { get; set; }
        public string V_L_CreatedBy { get; set; }
        public DateTime V_L_CreatedOn { get; set; }
        public string V_L_ModifiedBy { get; set; }
        public DateTime V_L_ModifiedOn { get; set; }
        public bool V_L_Deleted { get; set; }
    }

    public class V_Diary_List_Flags
    {
        public int V_F_id { get; set; }
        public int V_F_D_L_id { get; set; }
        public string V_F_Name { get; set; }
        public string V_F_Text { get; set; }
        public string V_F_CreatedBy { get; set; }
        public DateTime V_F_CreatedOn { get; set; }
        public string V_F_ModifiedBy { get; set; }
        public DateTime V_F_ModifiedOn { get; set; }
        public bool V_F_Deleted { get; set; }
    }

    public class V_Diary_List_Categories
    {
        public int V_C_id { get; set; }
        public int V_C_D_L_id { get; set; }
        public string V_C_Name { get; set; }
        public string V_C_Text { get; set; }
        public string V_C_CreatedBy { get; set; }
        public DateTime V_C_CreatedOn { get; set; }
        public string V_C_ModifiedBy { get; set; }
        public DateTime V_C_ModifiedOn { get; set; }
        public bool V_C_Deleted { get; set; }
    }



    public class V_DM_DiaryNames
    {

        public int V_D_id { get; set; }
        public string V_D_DiaryName { get; set; }
        public string V_D_Fields { get; set; }
        public string V_D_Outline { get; set; }
        public string V_D_DatePickerButton { get; set; }
        public string V_D_Standard_User { get; set; }
        public string V_D_ReadOnly { get; set; }
        public string V_D_Bckground_Icon_Colour { get; set; }
        public string V_D_Multiday_Cases { get; set; }
        public string V_D_Dates { get; set; }
        public string V_D_Vacated { get; set; }
        public string V_D_ListSelection { get; set; }
        public string V_CreatedBy { get; set; }
        public DateTime V_CreatedOn { get; set; }
        public string V_ModifiedBy { get; set; }
        public DateTime V_ModifiedOn { get; set; }
        public bool V_Deleted { get; set; }

    }

    public class V_DM_DiaryListDetails
    {
        public int V_D_L_id { get; set; }
        public int V_D_id { get; set; }
        public string V_D_L_Lists { get; set; }
        public string V_D_L_ShortName { get; set; }
        public bool V_D_L_Rules { get; set; }
        public string V_D_L_Type { get; set; }

        public string V_D_L_RulesApplyTo { get; set; }

        public string V_D_L_VacateOptions { get; set; }

        public string V_D_L_Bckground_row_Colour { get; set; }

        public string V_D_L_Text_Colour { get; set; }

        public string V_D_L_MandatoryFields { get; set; }

        public string V_D_L_Default_StartTime { get; set; }

        public virtual ICollection<D_L_Vacate_Reason> V_D_Attr_Vacate_Reason { get; set; }

        public virtual ICollection<D_L_Outcome> V_D_Attr_Outcome { get; set; }

        public virtual ICollection<D_L_Flag> V_D_Attr_Flag { get; set; }

        public virtual ICollection<D_L_Location> V_D_Attr_Location { get; set; }

        public virtual ICollection<D_L_Category> V_D_Attr_Category { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public bool Deleted { get; set; }        
    }
}