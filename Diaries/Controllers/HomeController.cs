using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diaries.Models;

namespace Diaries.Controllers
{
    public class HomeController : Controller
    {
        private DiariesDB db = new DiariesDB();
        static public DateTime DTMonday { get; set; }
        static public DateTime DTTuesday { get; set; }
        static public DateTime DTWednesday { get; set; }
        static public DateTime DTThursday { get; set; }
        static public DateTime DTFriday { get; set; }
        static public DateTime DTSaturday { get; set; }
        static public DateTime DTSunday { get; set; }
        public ActionResult Index(string term = null)
        {
            // Check access levels and pass to view
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
                ViewData["UserName"] = User.Identity.Name;

            V_HomePage modelAjax;
            if (Request.IsAjaxRequest())
            {
                if (term != null)
                    modelAjax = GetWeek(term);
                else
                    modelAjax = GetWeek(DateTime.Now.ToString("dd-MM-yyyy"));
                return PartialView("_HomeArticle", modelAjax);
                //   return View(modelAjax);
            }
            else
            {
                var model = GetWeek(DateTime.Now.ToString("dd-MM-yyyy"));
                return View(model);
            }
        }



        public V_HomePage GetWeek(string term)
        {
            DateTime dt = Convert.ToDateTime(term);

            int span = dt.DayOfWeek - DayOfWeek.Monday;
            int span1 = dt.DayOfWeek - DayOfWeek.Tuesday;
            int span2 = dt.DayOfWeek - DayOfWeek.Wednesday;
            int span3 = dt.DayOfWeek - DayOfWeek.Thursday;
            int span4 = dt.DayOfWeek - DayOfWeek.Friday;
            int span5 = dt.DayOfWeek - DayOfWeek.Saturday;
            int span6 = dt.DayOfWeek - DayOfWeek.Sunday;


            DTMonday = dt.AddDays(-span);
            DTTuesday = dt.AddDays(-span1);
            DTWednesday = dt.AddDays(-span2);
            DTThursday = dt.AddDays(-span3);
            DTFriday = dt.AddDays(-span4);
            DTSaturday = dt.AddDays(-span5);
            DTSunday = dt.AddDays(-span6);



            //var modelData1 = db.tblDiary;  //get the id of the first record diary into model so we can use it to filter TBLCase
            //var diaryid = modelData1.OrderBy(r => r.D_id).Skip(1).Select(a => a.D_id).First();
            //var modelData = new V_HomePage
            //{
            //    V_HomePage_DiaryNames = modelData1 //db.tblDiary                 
            //      .Select(q => new V_Home_DiaryNames { V_H_DiaryName_Name = q.D_DiaryName, V_H_DiaryName_id = q.D_id, V_H_DiaryName_BkgColour = q.D_Bckground_Icon_Colour }),

            //    V_HomePage_NSDays = db.tblNonSittingDay.Where(a => a.NSD_Date >= DTMonday && a.NSD_Date <= DTFriday).Select(w => new V_Home_NSDays { V_H_NSDays_Reason = w.NSD_Reason, V_H_NSDays_id = w.NSD_ID }),

            //    V_HomePage_DiaryListDetails = db.tblDiaryListingInfo.Where(d => d.D_id == diaryid)
            //         .Select(z => new V_Home_DiaryListInfo
            //         {
            //             V_H_DiaryId = z.D_id,
            //             V_H_DiaryListDetails_id = z.D_L_id,
            //             V_H_DiaryListDetails_Total = z.D_LI_Total,
            //             V_H_DiaryListDetails_SName = z.D_LI_ShortName,
            //             V_H_DiaryListDetails_DateCreated = z.D_LI_Date,
            //             V_H_DiaryListDetails_Avail = z.D_LI_Avail,
            //             V_H_DiaryListDetails_BkgColour = z.D_LI_BkgColour,
            //             V_H_DiaryListDetails_Colour = z.D_LI_Colour,
            //             V_H_Notes = z.D_LI_Notes
            //         }),

            //    V_HomePage_DayNotes = db.tblDayNote.Where(c => c.DN_Start_Date >= DTMonday && c.DN_End_Date <= DTFriday && c.DN_ID == diaryid)
            //    .Select(y => new V_Home_DayNotes
            //    {
            //        D_id = y.DN_DiaryListCollection.Where(v => v.D_id == diaryid).Select(v => v.D_id).FirstOrDefault(),
            //        V_H_DayNotes_id = y.DN_ID,
            //        V_H_DayNotes_Notes = y.DN_Note
            //    }),

            //    V_HomePage_Cases = db.tblCase.Where(d => d.D_id == diaryid && d.C_StartDate >= DTMonday && d.C_EndDate <= DTFriday).Select(e => new V_Home_Case
            //    {
            //        V_H_Case_id = e.C_ID,
            //        V_H_Case_SName = e.C_D_L_ShortName,
            //        V_H_Case_MajorChange = e.C_MajorChange,  //MC:
            //        V_H_Case_Solicitor = e.C_Solicitor,   //S:
            //        V_H_Case_Location = e.C_Location,  //Location under the flag
            //        V_H_Case_Category = e.C_Number,
            //        V_H_Case_Flag = e.C_Flags,  // placed in the top right side
            //        V_H_Case_Notes = e.C_Notes, //Notes:
            //        V_H_Case_Accused = e.C_Accused,      //a:
            //        V_H_Case_Number = e.C_Number,
            //        V_H_Case_Informant = e.C_Informant,  //I:
            //        V_H_Case_NoOfDays = e.C_NoOfDays,
            //        V_H_Case_JudicialOfficer = e.C_JudicialOfficer,  //JOFF:
            //        V_H_Case_StartTime = e.C_StartTime,  //T:
            //        V_H_Case_DateFrom = e.C_StartDate, //Date:
            //        V_H_Case_DateTo = e.C_EndDate
            //    }),                    
            //};



            V_HomePage modelData = new V_HomePage
            {
                V_HomePage_DiaryNames = new List<V_Home_DiaryNames> 
                    {
                            new V_Home_DiaryNames {  V_H_DiaryName_Name = "All", V_H_DiaryName_id = 1 , V_H_DiaryName_BkgColour = "peachpuff"},
                            new V_Home_DiaryNames {  V_H_DiaryName_Name = "CiVil", V_H_DiaryName_id = 2 , V_H_DiaryName_BkgColour = "seagreen"}, 
                            new V_Home_DiaryNames {  V_H_DiaryName_Name = "Committals", V_H_DiaryName_id = 3 , V_H_DiaryName_BkgColour = "lightblue"},
                            new V_Home_DiaryNames {  V_H_DiaryName_Name = "County Court", V_H_DiaryName_id = 4 , V_H_DiaryName_BkgColour = "lightred"},
                            new V_Home_DiaryNames {  V_H_DiaryName_Name = "Criminal", V_H_DiaryName_id = 5 , V_H_DiaryName_BkgColour = "gray"}, 
                            new V_Home_DiaryNames {  V_H_DiaryName_Name = "VOCAT", V_H_DiaryName_id = 6 , V_H_DiaryName_BkgColour = "yellow"}                          
                     },
                V_HomePage_DiaryListDetails = new List<V_Home_DiaryListInfo>
                     {
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 1, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTMonday ,V_H_DiaryListDetails_Avail = 3, V_H_DiaryListDetails_BkgColour = "peachpuff", V_H_DiaryListDetails_Colour = "lightcyan"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTMonday , V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "lightgray", V_H_DiaryListDetails_Colour = "red"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 3, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTMonday , V_H_DiaryListDetails_Avail = 1, V_H_DiaryListDetails_BkgColour = "pink", V_H_DiaryListDetails_Colour = "lightgray"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 4, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTMonday ,V_H_DiaryListDetails_Avail = 4, V_H_DiaryListDetails_BkgColour = "red", V_H_DiaryListDetails_Colour = "lightcyan"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 5, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTTuesday , V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "gray", V_H_DiaryListDetails_Colour = "pink"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 6, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTWednesday ,V_H_DiaryListDetails_Avail = 1, V_H_DiaryListDetails_BkgColour = "pink", V_H_DiaryListDetails_Colour = "lightgray"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 1, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTThursday , V_H_DiaryListDetails_Avail = 5, V_H_DiaryListDetails_BkgColour = "orange", V_H_DiaryListDetails_Colour = "blue"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 6, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTFriday , V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "blue", V_H_DiaryListDetails_Colour = "lawngreen"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 4, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTMonday ,V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "pink", V_H_DiaryListDetails_Colour = "lightblue"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 3, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTMonday ,V_H_DiaryListDetails_Avail = 3, V_H_DiaryListDetails_BkgColour = "brown", V_H_DiaryListDetails_Colour = "lightcyan"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 2 ,V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTTuesday ,V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "lightbrown", V_H_DiaryListDetails_Colour = "yellow"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 3, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTTuesday , V_H_DiaryListDetails_Avail = 1, V_H_DiaryListDetails_BkgColour = "silver", V_H_DiaryListDetails_Colour = "brown"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTTuesday ,V_H_DiaryListDetails_Avail = 3, V_H_DiaryListDetails_BkgColour = "lightred", V_H_DiaryListDetails_Colour = "lightcyan"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTWednesday ,V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "lightgray", V_H_DiaryListDetails_Colour = "purple"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 3, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTWednesday , V_H_DiaryListDetails_Avail = 9, V_H_DiaryListDetails_BkgColour = "lightyellow", V_H_DiaryListDetails_Colour = "orange"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 1, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTThursday ,V_H_DiaryListDetails_Avail = 3, V_H_DiaryListDetails_BkgColour = "peachpuff", V_H_DiaryListDetails_Colour = "lightcyan"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTThursday ,V_H_DiaryListDetails_Avail = 8, V_H_DiaryListDetails_BkgColour = "lightgray", V_H_DiaryListDetails_Colour = "pink"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 5, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTThursday , V_H_DiaryListDetails_Avail = 9, V_H_DiaryListDetails_BkgColour = "lightviolet", V_H_DiaryListDetails_Colour = "lightgray"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 5, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTFriday ,V_H_DiaryListDetails_Avail = 3, V_H_DiaryListDetails_BkgColour = "skyblue", V_H_DiaryListDetails_Colour = "silver"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTFriday ,V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "purple", V_H_DiaryListDetails_Colour = "lawngreen"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 6, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTMonday ,V_H_DiaryListDetails_Avail = 1, V_H_DiaryListDetails_BkgColour = "pink", V_H_DiaryListDetails_Colour = "lightgray"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 6, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTWednesday , V_H_DiaryListDetails_Avail = 8, V_H_DiaryListDetails_BkgColour = "grren", V_H_DiaryListDetails_Colour = "lightcyan"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTWednesday , V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "cream", V_H_DiaryListDetails_Colour = "orange"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 3, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTMonday ,V_H_DiaryListDetails_Avail = 1, V_H_DiaryListDetails_BkgColour = "hazel", V_H_DiaryListDetails_Colour = "lightgray"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 1, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CV1", V_H_DiaryListDetails_DateCreated = DTFriday , V_H_DiaryListDetails_Avail = 3, V_H_DiaryListDetails_BkgColour = "peachpuff", V_H_DiaryListDetails_Colour = "lightcyan"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 2, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CVC", V_H_DiaryListDetails_DateCreated = DTFriday , V_H_DiaryListDetails_Avail = 7, V_H_DiaryListDetails_BkgColour = "lightgray", V_H_DiaryListDetails_Colour = "peachpuff"},
                         new V_Home_DiaryListInfo {  V_H_DiaryListDetails_id = 3, V_H_DiaryListDetails_Total = 10, V_H_DiaryListDetails_SName = "CC6", V_H_DiaryListDetails_DateCreated = DTMonday , V_H_DiaryListDetails_Avail = 4, V_H_DiaryListDetails_BkgColour = "pink", V_H_DiaryListDetails_Colour = "lightgray"}

                     },
                V_HomePage_NSDays = new List<V_Home_NSDays>
                     {
                         new V_Home_NSDays { V_H_NSDays_id = 1, V_H_NSDays_Reason = "Holiday"},
                         new V_Home_NSDays { V_H_NSDays_id = 2, V_H_NSDays_Reason = "Holiday1"},                     
                     },
                V_HomePage_DayNotes = new List<V_Home_DayNotes>
                     {
                        new V_Home_DayNotes { V_H_DayNotes_id = 1, D_id = 1, V_H_DayNotes_Notes = "lkjc kd lsd lasda asdhgkasd"},
                        new V_Home_DayNotes { V_H_DayNotes_id = 2, D_id = 1, V_H_DayNotes_Notes = "wqe we ewreww ret ert tyrt"},
                        new V_Home_DayNotes { V_H_DayNotes_id = 3, D_id = 1, V_H_DayNotes_Notes = "asd fsd sdfdsfsdf erwfwef gdr gdg"}
                     },
                V_HomePage_Cases = new List<V_Home_Case>
                     {

                       new V_Home_Case { V_H_Case_id   = 1,
                        V_H_Case_SName   = "NONVP" ,
                        V_H_Case_MajorChange = "Theft"       ,  //MC:
                        V_H_Case_Solicitor        = "Mr Wend"  ,   //S:
                        V_H_Case_Location         = ""  ,  //Location under the flag
                        V_H_Case_Category         = "CFV"  , 
                        V_H_Case_Flag = new List<D_L_Flag> { new D_L_Flag {  
                                D_L_id = 1,
                                F_Name = "Flag1",
                                F_FilterType = "" ,
                                CreatedBy = "Arty",
                                CreatedOn =  DateTime.Parse("08/08/2014"),  
                                ModifiedBy = "Arty",
                                ModifiedOn =  DateTime.Parse("08/08/2014"),    
                                Deleted = false
                      }},
                        V_H_Case_Notes            = "test notes"  , //Notes:
                        V_H_Case_Accused         = "John"  ,      //a:
                        V_H_Case_Number          = "A1000RT55"  ,
                        V_H_Case_Informant       = "Mr Peter"  ,  //I:
                        V_H_Case_NoOfDays         = 2  ,  
                        V_H_Case_JudicialOfficer  = "Magistrate Doherty"  ,  //JOFF:
                        V_H_Case_StartTime        = Convert.ToDateTime("15:00"),  //T:
                        V_H_Case_DateFrom         = Convert.ToDateTime("07/07/2014")  , //Date:
                        V_H_Case_DateTo           = Convert.ToDateTime("09/07/2014")  }, 

                      new V_Home_Case { V_H_Case_id   = 2,
                        V_H_Case_SName   = "ABCCN"           ,
                        V_H_Case_MajorChange = "Drug Pocession"       ,
                        V_H_Case_Solicitor        = "Mr Groov"  ,
                        V_H_Case_Location         = ""  ,
                        V_H_Case_Category         = "CCG"  ,
                        V_H_Case_Flag = new List<D_L_Flag> { new D_L_Flag {  
                                D_L_id = 1,
                                F_Name = "Flag1",
                                F_FilterType = "" ,
                                CreatedBy = "Arty",
                                CreatedOn =  DateTime.Parse("08/08/2014"),  
                                ModifiedBy = "Arty",
                                ModifiedOn =  DateTime.Parse("08/08/2014"),    
                                Deleted = false
                      }},
                        V_H_Case_Accused         = "Marty seen"  ,      
                        V_H_Case_Number          = "TYHM89I"  ,
                        V_H_Case_Informant       = "Mr Richards"  ,
                        V_H_Case_NoOfDays         = 1  ,
                        V_H_Case_Notes            = "Test More Notes"  ,  //Notes:
                        V_H_Case_JudicialOfficer  = "Magistrate Lorry"  ,
                        V_H_Case_StartTime        = Convert.ToDateTime("09:00"), 
                        V_H_Case_DateFrom         = Convert.ToDateTime("01/07/2014")  ,
                        V_H_Case_DateTo           = Convert.ToDateTime("07/07/2014")  },

                      new V_Home_Case { V_H_Case_id   = 3,
                        V_H_Case_SName   = "PPO"           ,
                        V_H_Case_MajorChange = "Hit And Run"       ,
                        V_H_Case_Solicitor        = "Mr Cartman"  ,
                        V_H_Case_Location         = ""  ,
                        V_H_Case_Category         = "MMN"  ,
                         V_H_Case_Flag = new List<D_L_Flag> { new D_L_Flag {  
                                D_L_id = 1,
                                F_Name = "Flag1",
                                F_FilterType = "" ,
                                CreatedBy = "Arty",
                                CreatedOn =  DateTime.Parse("08/08/2014"),  
                                ModifiedBy = "Arty",
                                ModifiedOn =  DateTime.Parse("08/08/2014"),    
                                Deleted = false
                      }},
                        V_H_Case_Accused         = "Mr Person"  ,      
                        V_H_Case_Number          = "MMJJ**("  ,
                        V_H_Case_Informant       = "Graham"  ,
                        V_H_Case_NoOfDays         = 1  ,
                        V_H_Case_Notes            = "gkh g hjjk jhgfjk"  , //Notes:
                        V_H_Case_JudicialOfficer  = "Magistrate Frill"  ,
                        V_H_Case_StartTime        = Convert.ToDateTime("11:00") ,
                        V_H_Case_DateFrom         = Convert.ToDateTime("20/06/2014")   ,
                        V_H_Case_DateTo           = Convert.ToDateTime("28/06/2014")   }
                     }
            };

            return modelData;
        }

        public ActionResult About()
        {
            ViewBag.Title = "MCV Diary"; //TODO Get from web.config
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "MCV Diary"; //TODO Get from web.config
            ViewBag.Message = "Contacts";

            return View();
        }
        public void GetDiaryDetails(int? id)
        {

        }

    }
}