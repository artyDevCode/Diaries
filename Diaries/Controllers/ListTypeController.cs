using Diaries.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Diaries.Controllers
{
    public class ListTypeController : Controller
    {
        private DiariesDB db = new DiariesDB();

        // GET: ListType
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(string listId, string diaryId)
        {
            int index = User.Identity.Name.IndexOf("\\");
            string user = User.Identity.Name.Substring(index + 1);
            List<Access> AccessGroupsModel = db.tblAccess
                             .Where(r => r.UserId == user)
                             .ToList();

            var currentUser = (from u in db.tblAccess
                               where u.UserId == user
                               select u).FirstOrDefault();

            ViewData["InOwnerRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("owner")).Count() > 0 ? "true" : "false";
            ViewData["InSysAdminRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("system")).Count() > 0 ? "true" : "false";
            ViewData["InReadRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("read")).Count() > 0 ? "true" : "false";
            ViewData["InStandardRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("standard")).Count() > 0 ? "true" : "false";
            ViewData["InMagistratesReadRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("magistrate")).Count() > 0 ? "true" : "false";

            if ((ViewData["InSysAdminRole"] != "true") && (ViewData["InReadRole"] != "true") && (ViewData["InMagistratesReadRole"] != "true") && (ViewData["InStandardRole"] != "true") && (ViewData["InOwnerRole"] != "true"))
            {
                return RedirectToAction("Unauthorised", "Access");
            }

            if (currentUser != null)
                ViewData["UserName"] = currentUser.UserName;
            else
            { ViewData["UserName"] = User.Identity.Name; }

            int did = Convert.ToInt32(diaryId);
            int ilistId = Convert.ToInt32(listId);
            //var model1 = from d in db.tblDiary
            //            join dl in db.tblDiaryListTypes on d.D_id equals dl.D_id into dld
            //            from dl in dld.DefaultIfEmpty()
            //            where d.D_id == did && d.Deleted == false)
            //            orderby dl.D_L_ShortName ascending

            //            //select new V_ListType_Home  

            //            //{
                        //    D_id = d.D_id,
                        //    D_L_id = dl.D_L_id,
                        //    D_DiaryName = d.D_DiaryName,
                        //    D_Bckground_Icon_Colour = d.D_Bckground_Icon_Colour,
                        //    D_ListName = dl.D_L_Lists,
                        //    D_ListShortName = dl.D_L_ShortName,
                        //    D_L_Bckground_Row_Colour = dl.D_L_Bckground_row_Colour
                        //};

            //IEnumerable<V_ListType_Home> model = db.tblDiaryListTypes.Where(x => x.D_L_id == ilistId)
            //      .Join(db.tblDiary, a => a.D_id, b => b.D_id, (a, b) => new V_ListType_Home
            //      {
            //          D_id = b.D_id,
            //          D_L_id = a.D_L_id,
            //          D_DiaryName = b.D_DiaryName,
            //          D_Bckground_Icon_Colour = b.D_Bckground_Icon_Colour,
            //          D_ListName = a.D_L_Lists,
            //          D_ListShortName = a.D_L_ShortName,
            //          D_L_Bckground_Row_Colour = a.D_L_Bckground_row_Colour
            //      }).FirstOrDefault();

            V_ListType_Home model = db.tblDiaryListTypes.Where(x => x.D_L_id == ilistId)
                  .Join(db.tblDiary, a => a.D_id, b => b.D_id, (a, b) => new V_ListType_Home
                  {
                      D_id = b.D_id,
                      D_L_id = a.D_L_id,
                      D_DiaryName = b.D_DiaryName,
                      D_Bckground_Icon_Colour = b.D_Bckground_Icon_Colour,
                      D_ListName = a.D_L_Lists,
                      D_ListShortName = a.D_L_ShortName,
                      D_L_Bckground_Row_Colour = a.D_L_Bckground_row_Colour
                  }).FirstOrDefault();


            if (db.tblYearlyCalendarDates.Count() > 0)
            {
                model.Current_Calendar_Start_Day = db.tblYearlyCalendarDates.FirstOrDefault().CurrentStartDate.Day.ToString();
                model.Current_Calendar_Start_Month = db.tblYearlyCalendarDates.FirstOrDefault().CurrentStartDate.Month.ToString();
                model.Current_Calendar_Start_Year = db.tblYearlyCalendarDates.FirstOrDefault().CurrentStartDate.Year.ToString();
                model.Current_Calendar_End_Day = db.tblYearlyCalendarDates.FirstOrDefault().CurrentEndDate.Day.ToString();
                model.Current_Calendar_End_Month = db.tblYearlyCalendarDates.FirstOrDefault().CurrentEndDate.Month.ToString();
                model.Current_Calendar_End_Year = db.tblYearlyCalendarDates.FirstOrDefault().CurrentEndDate.Year.ToString();
                model.Current_Calendar_Start_Date = db.tblYearlyCalendarDates.FirstOrDefault().CurrentStartDate;
                model.Current_Calendar_End_Date = db.tblYearlyCalendarDates.FirstOrDefault().CurrentEndDate;

                model.Next_Calendar_Start_Day = db.tblYearlyCalendarDates.FirstOrDefault().NextStartDate.Day.ToString();
                model.Next_Calendar_Start_Month = db.tblYearlyCalendarDates.FirstOrDefault().NextStartDate.Month.ToString();
                model.Next_Calendar_Start_Year = db.tblYearlyCalendarDates.FirstOrDefault().NextStartDate.Year.ToString();
                model.Next_Calendar_End_Day = db.tblYearlyCalendarDates.FirstOrDefault().NextEndDate.Day.ToString();
                model.Next_Calendar_End_Month = db.tblYearlyCalendarDates.FirstOrDefault().NextEndDate.Month.ToString();
                model.Next_Calendar_End_Year = db.tblYearlyCalendarDates.FirstOrDefault().NextEndDate.Year.ToString();
                model.Next_Calendar_Start_Date = db.tblYearlyCalendarDates.FirstOrDefault().NextStartDate;
                model.Next_Calendar_End_Date = db.tblYearlyCalendarDates.FirstOrDefault().NextEndDate;

                model.Is_Calendar_Set = true;
            }
            else
            {
                model.Current_Calendar_Start_Day = DateTime.Now.Day.ToString();
                model.Current_Calendar_Start_Month = DateTime.Now.Month.ToString();
                model.Current_Calendar_Start_Year = DateTime.Now.Year.ToString();
                model.Current_Calendar_End_Day = DateTime.Now.Day.ToString();
                model.Current_Calendar_End_Month = DateTime.Now.Month.ToString();
                model.Current_Calendar_End_Year = DateTime.Now.Year.ToString();
                model.Current_Calendar_Start_Date = DateTime.Now;
                model.Current_Calendar_End_Date = DateTime.Now;

                model.Next_Calendar_Start_Day = DateTime.Now.Day.ToString();
                model.Next_Calendar_Start_Month = DateTime.Now.Month.ToString();
                model.Next_Calendar_Start_Year = DateTime.Now.Year.ToString();
                model.Next_Calendar_End_Day = DateTime.Now.Day.ToString();
                model.Next_Calendar_End_Month = DateTime.Now.Month.ToString();
                model.Next_Calendar_End_Year = DateTime.Now.Year.ToString();
                model.Next_Calendar_Start_Date = DateTime.Now;
                model.Next_Calendar_End_Date = DateTime.Now;
                model.Is_Calendar_Set = false;
            }
            return View(model);

        }

        public class ListingIdVM
        {
            public int VM_D_L_Id { get; set; }
        }
        
        public ActionResult GetCurrentListingInformation(string listId, string diaryId, string Start, string End)
        {

            int ilistId = Convert.ToInt32(listId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryId);

                var model = from d in db.tblDiary
                            join dl in db.tblDiaryListTypes on d.D_id equals dl.D_id into dld
                            from dl in dld.DefaultIfEmpty()
                            where d.D_id == did && d.Deleted == false
                            orderby dl.D_L_ShortName ascending

                            select new ListingIdVM
                            {
                                VM_D_L_Id = dl.D_L_id
                            };

                int tmp;

                try
                { tmp = model.FirstOrDefault().VM_D_L_Id; }
                catch
                { tmp = 0; }


                if (tmp > 0)
                {
                    ilistId = model.FirstOrDefault().VM_D_L_Id;
                }
            }

            int did1 = ilistId;
            DateTime start = Convert.ToDateTime(Start);
            DateTime end = Convert.ToDateTime(End);

            List<DiaryListingInfo> selectedList = db.tblDiaryListingInfo.Where(r => r.D_L_id == did1 && r.Deleted == false && (r.D_LI_Date >= start && r.D_LI_Date <= end )).OrderBy(s => s.D_LI_Date).ToList();


            if (selectedList.Count != 0)
            {

                List<V_ListType_Listing> diarylistinginfo = new List<V_ListType_Listing>();

                foreach (var item in selectedList)
                {
                    V_ListType_Listing vitem = new V_ListType_Listing()
                    {
                        D_id = item.D_id,
                        D_L_id = item.D_L_id,
                        V_L_Date = item.D_LI_Date,
                        V_L_Available = item.D_LI_Avail,
                        V_L_Current = (item.D_LI_Total - item.D_LI_Avail),
                        V_L_Limit = item.D_LI_Total,
                        V_L_Day = item.D_LI_Date.DayOfWeek.ToString(),
                    };

                    diarylistinginfo.Add(vitem);
                    //{
                    //    D_id = item.D_id,
                    //    D_L_id = item.D_L_id,
                    //    V_L_Date = item.D_LI_Date,
                    //    V_L_Available = item.D_LI_Avail,
                    //    V_L_Current = (item.D_LI_Total - item.D_LI_Avail),
                    //    V_L_Limit = item.D_LI_Total,
                    //    V_L_Day = item.D_LI_Date.DayOfWeek.ToString(),
                    //});

                }

                return PartialView("_ListingInformation", diarylistinginfo);

            }
            else
            {
                List<V_ListType_Listing> diarylistinginfo = new List<V_ListType_Listing>();

                V_ListType_Listing vitem = new V_ListType_Listing()
                {
                    D_id = 0,
                    D_L_id = 0,
                    V_L_Date = DateTime.Now,
                    V_L_Available = 0,
                    V_L_Current = 0,
                    V_L_Limit = 0,
                    V_L_Day = "No Listings",
                };
                diarylistinginfo.Add(vitem);

                return PartialView("_ListingInformation", diarylistinginfo);
            }

        }

        private bool DaySelectedBetween(string day, List<DiaryListingInfo> listDates) 
         { 

            bool dayselected = false;
            foreach (var item in listDates)
            {
                if (item.D_LI_Date.DayOfWeek.ToString() == day)              
                {
                    dayselected = true;
                    break;
                }
            }
            return dayselected;
         }

        public ActionResult SaveListRules(string diaryId, string listId, string Limit, string Reason, string startDate, string endDate , string mondaySelected, string tuesdaySelected, string wednesdaySelected, string thursdaySelected, string fridaySelected)
        {

            int did = Convert.ToInt32(diaryId);
            int ldid = Convert.ToInt32(listId);
            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);
            int limit = Convert.ToInt32(Limit);

            List<V_ListType_Listing> diarylistinginfo = new List<V_ListType_Listing>();

            string mondayText;
            string tuedayText;
            string weddayText;
            string thudayText;
            string fridayText;
            string dayText;

            mondayText = mondaySelected == "true" ? "Monday" : "";
            tuedayText = tuesdaySelected == "true" ? "Tuesday" : "";
            weddayText = wednesdaySelected == "true" ? "Wednesday" : "";
            thudayText = thursdaySelected == "true" ? "Thursday" : "";
            fridayText = fridaySelected == "true" ? "Friday" : "";
            dayText = mondayText + "/" + tuedayText + "/" + weddayText + "/" + thudayText + "/" + fridayText;


            int i = 0;
            foreach (DateTime day in EachDay(start, end))
            {

                NonSittingDay nsd = db.tblNonSittingDay.Where(r => r.NSD_Date == day && r.NSD_Active == true).FirstOrDefault();
                if (nsd == null)
                {

                    DiaryListingInfo selectedList = db.tblDiaryListingInfo.Where(r => r.D_L_id == ldid && r.D_LI_Date == day).OrderBy(s => s.D_LI_Date).FirstOrDefault();
                    //List<DiaryListingInfo> selectedList = db.tblDiaryListingInfo.Where(r => r.D_L_id == ldid && (r.D_LI_Date >= start && r.D_LI_Date <= end)).OrderBy(s => s.D_LI_Date).ToList();

                    if (selectedList != null)
                    {
                        if (i == 0)
                        {
                            StringBuilder logText = new StringBuilder();
                            logText.AppendLine("  Limits: " + selectedList.D_LI_Total + " To " + Limit);
                            logText.AppendLine("  Dates: " + startDate + " - " + endDate);
                            logText.AppendLine("  Days: " + mondayText + " " + tuedayText + " " + weddayText + " " + thudayText + " " + fridayText);
                            SaveUpdateHistory(ldid, logText.ToString());
                        }
                        i++;

                        //foreach (var item in selectedList)
                        //{
                        if (dayText.Contains(day.DayOfWeek.ToString()))
                        {

                            int current = (selectedList.D_LI_Total - selectedList.D_LI_Avail);
                            int newavail = (limit - current);

                            //update limit and availability
                            DiaryListingInfo infoLog = db.tblDiaryListingInfo
                                                .Where(r => r.D_LI_id == selectedList.D_LI_id).FirstOrDefault();

                            if (infoLog != null)
                            {
                                infoLog.D_LI_Total = limit;
                                infoLog.D_LI_Avail = newavail;
                                infoLog.ModifiedBy = User.Identity.Name;
                                infoLog.ModifiedOn = DateTime.Now;
                                infoLog.D_LI_Notes = Reason;
                                infoLog.Deleted = false;
                                db.Entry(infoLog).State = EntityState.Modified;
                            };
                            db.SaveChanges();
                        }
                        else
                        {

                            //update limit and availability and deletedflag
                            DiaryListingInfo infoLog = db.tblDiaryListingInfo
                                                .Where(r => r.D_LI_id == selectedList.D_LI_id).FirstOrDefault();
                            if (infoLog != null)
                            {
                                int current = (selectedList.D_LI_Total - selectedList.D_LI_Avail);
                                int newavail = (limit - current);

                                infoLog.D_LI_Total = limit;
                                infoLog.D_LI_Avail = newavail;
                                infoLog.ModifiedBy = User.Identity.Name;
                                infoLog.ModifiedOn = DateTime.Now;
                                infoLog.D_LI_Notes = Reason;
                                infoLog.Deleted = true;
                                db.Entry(infoLog).State = EntityState.Modified;
                            };
                            db.SaveChanges();
                            //}
                        }
                    }
                    else
                    {
                        //add new record with limit and availability
                        if (dayText.Contains(day.DayOfWeek.ToString()))
                        {
                            DiaryListingInfo infoLog = new DiaryListingInfo
                            {
                                D_id = did,
                                D_L_id = ldid,
                                D_LI_Total = limit,
                                D_LI_Avail = limit,
                                D_LI_Date = day,
                                CreatedBy = User.Identity.Name,
                                CreatedOn = DateTime.Now,
                                ModifiedBy = User.Identity.Name,
                                ModifiedOn = DateTime.Now,
                                D_LI_Notes = Reason,
                                Deleted = false,
                            };
                            db.Entry(infoLog).State = EntityState.Added;
                            db.SaveChanges();
                        }
                    }


                }

            } 
            
            return PartialView("_ListingInformation", diarylistinginfo);

        }

        public ActionResult DisableListRules(string diaryId, string listId, string Reason, string startDate, string endDate, string mondaySelected, string tuesdaySelected, string wednesdaySelected, string thursdaySelected, string fridaySelected, string entireRange)
        {

            int did = Convert.ToInt32(diaryId);
            int ldid = Convert.ToInt32(listId);
            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);
            List<V_ListType_Listing> diarylistinginfo = new List<V_ListType_Listing>();

            string mondayText;
            string tuedayText;
            string weddayText;
            string thudayText;
            string fridayText;
            string dayText;

            mondayText = mondaySelected == "false" ? "" : "Monday";
            tuedayText = tuesdaySelected == "false" ? "" : "Tuesday";
            weddayText = wednesdaySelected == "false" ? "" : "Wednesday";
            thudayText = thursdaySelected == "false" ? "" : "Thursday";
            fridayText = fridaySelected == "false" ? "" : "Friday";
            dayText = mondayText + "/" + tuedayText + "/" + weddayText + "/" + thudayText + "/" + fridayText;

            // Update History
            StringBuilder logText = new StringBuilder();
            if (entireRange == "false")
            {
                logText.AppendLine("  Disable:");
                logText.AppendLine("  Dates: " + startDate + " - " + endDate);
                logText.AppendLine("  Days: " + mondayText + " " + tuedayText + " " + weddayText + " " + thudayText + " " + fridayText);
            }
            else
            {
                logText.AppendLine("  Disable Entire Range:");
                logText.AppendLine("  Dates: " + startDate + " - " + endDate);
            }

            SaveUpdateHistory(ldid, logText.ToString());


            //if (entireRange == "true")
            //{
            //    DiaryListingInfo selectedList = db.tblDiaryListingInfo.Where(r => r.D_L_id == ldid && r.D_LI_Date == day).OrderBy(s => s.D_LI_Date).FirstOrDefault();
            //    if (selectedList != null)
            //    {
            //        DiaryListingInfo infoLog = db.tblDiaryListingInfo
            //                            .Where(r => r.D_LI_id == selectedList.D_LI_id).FirstOrDefault();

            //        if (infoLog != null)
            //        {
            //            infoLog.ModifiedBy = User.Identity.Name;
            //            infoLog.ModifiedOn = DateTime.Now;
            //            infoLog.D_LI_Notes = Reason;
            //            infoLog.Deleted = true;
            //            db.Entry(infoLog).State = EntityState.Modified;
            //        };
            //        db.SaveChanges();

            //    }
            //}


            foreach (DateTime day in EachDay(start, end))
            {

                DiaryListingInfo selectedList = db.tblDiaryListingInfo.Where(r => r.D_L_id == ldid && r.D_LI_Date == day).OrderBy(s => s.D_LI_Date).FirstOrDefault();
                //List<DiaryListingInfo> selectedList = db.tblDiaryListingInfo.Where(r => r.D_L_id == ldid && (r.D_LI_Date >= start && r.D_LI_Date <= end)).OrderBy(s => s.D_LI_Date).ToList();

                if (selectedList != null)
                {

                    if (entireRange == "false")
                    {

                        //int index = dayText.IndexOfAny(new char[] { day.DayOfWeek.ToString() });

                        if (dayText.Contains(day.DayOfWeek.ToString()))
                        {
                            //set deleted flag to true
                            DiaryListingInfo infoLog = db.tblDiaryListingInfo
                                                .Where(r => r.D_LI_id == selectedList.D_LI_id).FirstOrDefault();

                            if (infoLog != null)
                            {
                                infoLog.ModifiedBy = User.Identity.Name;
                                infoLog.ModifiedOn = DateTime.Now;
                                infoLog.D_LI_Notes = Reason;
                                infoLog.Deleted = true;
                                db.Entry(infoLog).State = EntityState.Modified;
                            };
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        //set deleted flag to true
                        DiaryListingInfo infoLog = db.tblDiaryListingInfo
                                            .Where(r => r.D_LI_id == selectedList.D_LI_id).FirstOrDefault();

                        if (infoLog != null)
                        {
                            infoLog.ModifiedBy = User.Identity.Name;
                            infoLog.ModifiedOn = DateTime.Now;
                            infoLog.D_LI_Notes = Reason;
                            infoLog.Deleted = true;
                            db.Entry(infoLog).State = EntityState.Modified;
                        };
                        db.SaveChanges();
                    }
                }
            }

            return PartialView("_ListingInformation", diarylistinginfo);

        }

        private void SaveUpdateHistory(int dlistId, string LogText)
        {

            int index = User.Identity.Name.IndexOf("\\");
            string user = User.Identity.Name.Substring(index + 1); 
            var currentUser = (from u in db.tblAccess
                               where u.UserId == user
                               select u).FirstOrDefault();


            LogText = DateTime.Now + " " + currentUser.UserName + " - " + LogText;

            DiaryListingInfoLog infoLog = new DiaryListingInfoLog
            {
                LogText = LogText,
                D_L_id = dlistId,
                LogDate = DateTime.Now,
                Deleted = false,
                CreatedBy = User.Identity.Name,
                CreatedOn = DateTime.Now,
                ModifiedBy = User.Identity.Name,
                ModifiedOn = DateTime.Now
            };
            // Save log information
            db.Entry(infoLog).State = EntityState.Added;
            db.SaveChanges();

        }
        
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        public ActionResult GetCurrentListingRules(string listId, string diaryId, string Start, string End)
        {

            int ilistId = Convert.ToInt32(listId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryId);

                var model = from d in db.tblDiary
                            join dl in db.tblDiaryListTypes on d.D_id equals dl.D_id into dld
                            from dl in dld.DefaultIfEmpty()
                            where d.D_id == did && d.Deleted == false
                            orderby dl.D_L_ShortName ascending

                            select new ListingIdVM
                            {
                                VM_D_L_Id = dl.D_L_id
                            };

                int tmp;

                try
                { tmp = model.FirstOrDefault().VM_D_L_Id; }
                catch
                { tmp = 0; }


                if (tmp > 0)
                {
                    ilistId = model.FirstOrDefault().VM_D_L_Id;
                }
            }

            int did1 = ilistId;
            DateTime start = Convert.ToDateTime(Start);
            DateTime end = Convert.ToDateTime(End);

            List<DiaryListingInfo> selectedList = db.tblDiaryListingInfo.Where(r => r.D_L_id == did1 && r.Deleted == false && (r.D_LI_Date >= start && r.D_LI_Date <= end )).OrderBy(s => s.D_LI_Date ).ToList();
            List<DiaryListingInfoLog> dlInfoLog = db.tblDiaryListingInfoLog.Where(r => r.D_L_id == did1).OrderByDescending(s => s.LogDate).ToList();

            if (selectedList.Count != 0)
            {

                bool mondaySelected = DaySelectedBetween("Monday", selectedList);
                bool tuesdaySelected = DaySelectedBetween("Tuesday", selectedList);
                bool wednesdaydaySelected = DaySelectedBetween("Wednesday", selectedList);
                bool thursdaySelected = DaySelectedBetween("Thursday", selectedList);
                bool fridaySelected = DaySelectedBetween("Friday", selectedList); 
                

                V_ListType_ListingRules diarylistinginfo = new V_ListType_ListingRules()
                {
                    D_L_id = did1,
                    MondaySelected = mondaySelected,
                    TuesdaySelected = tuesdaySelected,
                    WednesdaySelected = wednesdaydaySelected,
                    ThursdaySelected = thursdaySelected,
                    FridaySelected = fridaySelected,
                    UpdateHistory = dlInfoLog,
                    dMondaySelected = mondaySelected,
                    dTuesdaySelected = tuesdaySelected,
                    dWednesdaySelected = wednesdaydaySelected,
                    dThursdaySelected = thursdaySelected,
                    dFridaySelected = fridaySelected,
                };

                return PartialView("_ListingRules", diarylistinginfo);

            }
            else
            {
                V_ListType_ListingRules diarylistinginfo = new V_ListType_ListingRules()
                {
                    D_L_id = did1,
                    MondaySelected = false,
                    TuesdaySelected = false,
                    WednesdaySelected = false,
                    ThursdaySelected = false,
                    FridaySelected = false,
                    UpdateHistory = dlInfoLog,
                    dMondaySelected = false,
                    dTuesdaySelected = false,
                    dWednesdaySelected = false,
                    dThursdaySelected = false,
                    dFridaySelected = false,
                };

                return PartialView("_ListingRules", diarylistinginfo);
            }

        }

    }
}

   //IEnumerable<DefendantReceipt> data = db.tblPassportForm.Where(r => r.PF_Id == id)
   //          .Join(db.tblPassportOffice, a => a.PF_Country, b => b.PO_Location_Country, (a, b) => new { PF = a, PO = b })
   //          .Join(db.tblLawCourts, c => c.PF.PF_Current_Location, o => o.LC_Name, (c, o) => new DefendantReceipt
   //          {
   //              DR_Id = c.PF.PF_Id,
   //              DR_Created = c.PF.PF_Created, //User.Identity.Name, 
   //              DR_Location_Country = c.PO.PO_Location_Country, //c.PF.PF_Initial_Location,
   //              DR_Location_Name = c.PO.PO_Location_Name,
   //              DR_Location_Street = c.PO.PO_Location_Street,
   //              DR_Location_Suburb = c.PO.PO_Location_Suburb,
   //              DR_Location_State = c.PO.PO_Location_State,
   //              DR_Location_PostCode = c.PO.PO_Location_PostCode,
   //              DR_Name = o.LC_Name,
   //              DR_Phone = o.LC_Phone,
   //              DR_Street = o.LC_Street,
   //              DR_Suburb = o.LC_Suburb,
   //              DR_State = o.LC_State,
   //              DR_DX = o.LC_DX,
   //              DR_PostCode = o.LC_PostCode,
   //              DR_Fax = o.LC_Fax,
   //              DR_Case_Id = c.PF.PF_Case_Id,
   //              DR_Date_Of_Birth = c.PF.PF_Date_Of_Birth,
   //              DR_Place_Of_Birth = c.PF.PF_Place_Of_Birth,
   //              DR_Name_Def = c.PF.PF_Name,
   //              DR_Passport_Expiry_Date = c.PF.PF_Passport_Expiry_Date,
   //              DR_Passport_Number = c.PF.PF_Passport_Number,
   //              DR_Next_Hearing_Date = c.PF.PF_Next_Hearing_Date,
   //              DR_Jurisdiction = o.LC_Jurisdiction
   //          }).ToList();