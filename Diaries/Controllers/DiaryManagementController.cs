using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diaries.Models;
using System.Configuration;
using System.Data.Entity;

namespace Diaries.Controllers
{
    public class DiaryManagementController : Controller
    {
        private DiariesDB db = new DiariesDB();

        // GET: DiaryManagement
        //public ActionResult Index()
        //{
        //    int index = User.Identity.Name.IndexOf("\\");
        //    string user = User.Identity.Name.Substring(index + 1);
        //    List<Access> AccessGroupsModel = db.tblAccess
        //                     .Where(r => r.UserId == user)
        //                     .ToList();

        //    var currentUser = (from u in db.tblAccess
        //                       where u.UserId == user
        //                       select u).FirstOrDefault();

        //    ViewData["InOwnerRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("owner")).Count() > 0 ? "true" : "false";
        //    ViewData["InSysAdminRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("system")).Count() > 0 ? "true" : "false";
        //    ViewData["InReadRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("read")).Count() > 0 ? "true" : "false";
        //    ViewData["InStandardRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("standard")).Count() > 0 ? "true" : "false";
        //    ViewData["InMagistratesReadRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("magistrate")).Count() > 0 ? "true" : "false";

        //    if ((ViewData["InSysAdminRole"] != "true") && (ViewData["InReadRole"] != "true") && (ViewData["InMagistratesReadRole"] != "true") && (ViewData["InStandardRole"] != "true") && (ViewData["InOwnerRole"] != "true"))
        //    {
        //        return RedirectToAction("Unauthorised", "Access");
        //    }

        //    if (currentUser != null)
        //        ViewData["UserName"] = currentUser.UserName;
        //    else
        //    { ViewData["UserName"] = User.Identity.Name; }

        //    var model = db.tblDiary
        //        .Where(r => r.Deleted == false)
        //    .ToList();

            
        //    return View(model);

        //}


       private List<SelectListItem> getDFields()
       {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
                    {
                      Text = "CCV",
                      Value = "1"
                    });
            items.Add(new SelectListItem
                    {
                        Text = "KIDS",
                        Value = "2"
                    });
            items.Add(new SelectListItem
                    {
                        Text = "MCV",
                        Value = "3",
                        Selected = true 
                    });

            return items;
           }

    //private IEnumerable<SelectListItem> getDFields()
        //{
        //    var selectItemList = DFields
               
        //      .Select(p => new SelectListItem { Value = p.RoleUID, Text = p.Name })
        //      .ToList(); 
            
        //    IEnumerable<SelectListItem> model = new IEnumerable<SelectListItem>();
        //    model.Add("CCV");
        //    model.Add("KIDS");
        //    model.Add("MCV");
        //    return model;
        //}
  

        public ActionResult Edit(int? id)
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

            var model = db.tblDiary.Where(r => r.Deleted == false  && r.D_DiaryName.ToLower() != "all").OrderBy(r => r.D_DiaryName);
                //.Where(a => new V_D_DiaryManagement
                //{
                //    D_id = a.D_id,
                //    D_ListCollection = a.D_ListCollection.Select(b => new  V_D_ListManagement { b.D_L_ShortName = D_L_ShortName })
                //}).ToList();

            //Get Droplist values
            //var selectedItem = model.FirstOrDefault().D_Fields;

            //SelectList DFields = new SelectList(getDFields(), selectedItem.ToString());
            //ViewData["DFields"] = DFields;

            return View(model);

            
            //List<V_D_DiaryManagement> diarymngtView = new List<V_D_DiaryManagement>();

            //var model = db.tblDiary
            //    .Where(r => r.Deleted == false && r.D_DiaryName.ToLower() != "all").OrderBy(r => r.D_DiaryName)
            //.ToList();

            //foreach (D_Diaries d in model)
            //{

            //    diarymngtView.Add(new V_D_DiaryManagement() 
            //    {
            //        D_id = d.D_id,
            //        D_DiaryName = d.D_DiaryName,
            //        D_Fields = d.D_Fields,
            //        D_Outline = d.D_Outline,
            //        D_DatePickerButton = d.D_DatePickerButton,
            //        D_Standard_User = d.D_Standard_User,
            //        D_ReadOnly = d.D_ReadOnly,
            //        D_Bckground_Icon_Colour = d.D_Bckground_Icon_Colour,
            //        D_Multiday_Cases = d.D_Multiday_Cases,
            //        D_Dates = d.D_Dates,
            //        D_Vacated = d.D_Vacated,
            //        D_ListSelection = d.D_ListSelection,

            //        D_ListCollection = 


            //    });
            //}
            //return View(diarymngtView);
        }

        //Returns a view if list attributes are available or not
        public ActionResult GetListAttrDetails(string listId, string diaryName)
        {

            int ilistId = Convert.ToInt32(listId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryName);

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

            int dlid = ilistId;

            var selectedList = db.tblDiaryListTypes.Where(r => r.D_L_id == dlid).FirstOrDefault();

            if (selectedList != null)
            {
                IEnumerable<D_L_Category> allCategories = db.tblDiaryListTypeCategories.Where(r => r.D_L_id == selectedList.D_L_id);
                IEnumerable<D_L_Flag> allFlags = db.tblDiaryListTypeFlags.Where(r => r.D_L_id == selectedList.D_L_id);
                IEnumerable<D_L_Outcome> allOutcomes = db.tblDiaryListTypeOutcomes.Where(r => r.D_L_id == selectedList.D_L_id);
                IEnumerable<D_L_Vacate_Reason> allVacates = db.tblDiaryListTypeVacateReasons.Where(r => r.D_L_id == selectedList.D_L_id);
                IEnumerable<D_L_Location> allLocations = db.tblDiaryListTypeLocations.Where(r => r.D_L_id == selectedList.D_L_id);

                V_Diary_List_Attrs listattrView = new V_Diary_List_Attrs()
                {
                    D_Attr_Category = selectedList.D_Attr_Category,
                    D_Attr_Location = selectedList.D_Attr_Location,
                    D_Attr_Vacate_Reason = selectedList.D_Attr_Vacate_Reason,
                    D_Attr_Outcome = selectedList.D_Attr_Outcome,
                    D_Attr_Flag = selectedList.D_Attr_Flag,
                    D_Attr_Category_Count = selectedList.D_Attr_Category.Count() > 0 ? true : false, 
                    D_Attr_Location_Count = selectedList.D_Attr_Location.Count() > 0 ? true : false,
                    D_Attr_Outcome_Count = selectedList.D_Attr_Outcome.Count() > 0 ? true : false,
                    D_Attr_Flag_Count = selectedList.D_Attr_Flag.Count() > 0 ? true : false,
                    D_Attr_Vacate_Reason_Count = selectedList.D_Attr_Vacate_Reason.Count() > 0 ? true : false,
                };

                return PartialView("_ListAttrs", listattrView);

            }
            else
            {
                return View();
            }

        }

        //Returns a view if list vacate reason attributes are available or not
        public ActionResult GetListVacateReasonDetails(string listId, string diaryName, string vrFilter)
        {

            int ilistId = Convert.ToInt32(listId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryName);

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

            int dlid = ilistId;

            var selectedList = db.tblDiaryListTypeVacateReasons.Where(r => r.D_L_id == dlid).OrderBy(x => x.Deleted).ThenBy(y => y.VR_Name).ToList();

            List<V_Diary_List_VacateReasons> listattrView = new List<V_Diary_List_VacateReasons>();

            // Select Fields Values
            IEnumerable<VacateReason> D_L_VRSelect_Type;
            if (vrFilter == "All")
            { D_L_VRSelect_Type = db.tblVacateReason.ToList(); }
            else
            { D_L_VRSelect_Type = db.tblVacateReason.Where(r => r.VR_Type == vrFilter).ToList(); }

            var dselectfields = new List<System.Web.WebPages.Html.SelectListItem>();
            dselectfields.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Select", Value = "Select" });

            foreach (var item in D_L_VRSelect_Type)
            {
                dselectfields.Add(new System.Web.WebPages.Html.SelectListItem { Text = item.VR_Name, Value = item.VR_Name });
            }

            if (selectedList.Count != 0)
            {
                
                foreach (var item in selectedList)
                {

                    listattrView.Add(new V_Diary_List_VacateReasons()
                    {
                        V_VR_id = item.VR_id,
                        V_VR_D_L_id = item.D_L_id,
                        V_VR_Name = item.VR_Name,
                        V_VR_FilterType = item.VR_FilterType,
                        V_VR_CreatedBy = item.CreatedBy,
                        V_VR_CreatedOn = item.CreatedOn,
                        V_VR_ModifiedBy = item.ModifiedBy,
                        V_VR_ModifiedOn = item.ModifiedOn,
                        V_VR_Deleted = item.Deleted,
                        V_VR_AllFilter = "All",
                        V_VR_CivilFilter = "Civil",
                        V_VR_CriminalFilter = "Criminal",
                        V_VR_CommittalsFilter = "Committals",
                        V_VR_InterventionOrdersFilter = "Intervention Orders",
                        V_VR_VOCATFilter = "VOCAT",
                        V_VR_ChildrensCourtFilter = "Childrens Court",
                        V_VR_SelectFilter = vrFilter,
                        V_VRSelect_Type = dselectfields
                    });
                }

                return PartialView("_VacateReasons", listattrView);

            }
            else
            {
                listattrView.Add(new V_Diary_List_VacateReasons()
                {
                    V_VR_id = 0,
                    V_VR_D_L_id = 0,
                    V_VR_Name = "",
                    V_VR_FilterType = "All",
                    V_VR_CreatedBy = "System",
                    V_VR_CreatedOn = DateTime.Now,
                    V_VR_ModifiedBy = "System",
                    V_VR_ModifiedOn = DateTime.Now,
                    V_VR_Deleted = true,
                    V_VR_AllFilter = "All",
                    V_VR_CivilFilter = "Civil",
                    V_VR_CriminalFilter = "Criminal",
                    V_VR_CommittalsFilter = "Committals",
                    V_VR_InterventionOrdersFilter = "Intervention Orders",
                    V_VR_VOCATFilter = "VOCAT",
                    V_VR_ChildrensCourtFilter = "Childrens Court",
                    V_VR_SelectFilter = vrFilter,
                    V_VRSelect_Type = dselectfields
                });
                return PartialView("_VacateReasons", listattrView);
            }

        }
    
        //Update List vacate reasons
        public ActionResult UpdateVacateReasons(string listId, string VRName, string VRType, string diaryName, string updateType, string VRId)
        {

            //System.Threading.Thread.Sleep(1000);

            int ilistId = Convert.ToInt32(listId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryName);

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

            if (updateType == "Add")
            {

                if (VRExists(ilistId, VRName, VRType) == false)
                {
                    @ViewData["ErrorMessage"] = "Vacate Reason exists. Please re-check your entry.";
                    return View();
                }
                else
                {
                    D_L_Vacate_Reason infoLog = new D_L_Vacate_Reason
                    {
                        D_L_id = Convert.ToInt32(ilistId),
                        VR_Name = VRName,
                        Deleted = false,
                        CreatedBy = User.Identity.Name,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = User.Identity.Name,
                        ModifiedOn = DateTime.Now
                    };

                    // Save information
                    db.Entry(infoLog).State = EntityState.Added;
                    db.SaveChanges();

                }

            }
            else 
            {
                int iVRId = Convert.ToInt32(VRId);

                var vrresult = db.tblDiaryListTypeVacateReasons
                    .Where(r => r.VR_id == iVRId)
                .ToList();

                foreach (D_L_Vacate_Reason vr in vrresult)
                {
                    vr.Deleted = updateType == "Active" ? false : true;
                    db.Entry(vr).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            
            return GetListVacateReasonDetails(ilistId.ToString(), diaryName,"All");
        }

        //Update List outcomes
        public ActionResult UpdateOutcomes(string listId, string OName, string OType, string diaryName, string updateType, string OId)
        {
            int ilistId = Convert.ToInt32(listId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryName);

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

            if (updateType == "Add")
            {

                if (OCExists(ilistId, OName, OType) == false)
                {
                    @ViewData["ErrorMessage"] = "Outcome exists. Please re-check your entry.";
                    return View();
                }
                else
                {
                    D_L_Outcome infoLog = new D_L_Outcome
                    {
                        D_L_id = Convert.ToInt32(ilistId),
                        O_Name = OName,
                        Deleted = false,
                        CreatedBy = User.Identity.Name,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = User.Identity.Name,
                        ModifiedOn = DateTime.Now
                    };

                    // Save information
                    db.Entry(infoLog).State = EntityState.Added;
                    db.SaveChanges();

                }

            }
            else
            {
                int iOId = Convert.ToInt32(OId);

                var oresult = db.tblDiaryListTypeOutcomes
                    .Where(r => r.O_id == iOId)
                .ToList();

                foreach (D_L_Outcome o in oresult)
                {
                    o.Deleted = updateType == "Active" ? false : true;
                    db.Entry(o).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }

            return GetListOutcomeDetails(ilistId.ToString(), diaryName, "All");
        }

        //Returns a view if list vacate reason attributes are available or not
        public ActionResult GetListOutcomeDetails(string listId, string diaryName, string oFilter)
        {

            int ilistId = Convert.ToInt32(listId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryName);

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

            int dlid = ilistId;

            var selectedList = db.tblDiaryListTypeOutcomes.Where(r => r.D_L_id == dlid).OrderBy(x => x.Deleted).ThenBy(y => y.O_Name).ToList();

            List<V_Diary_List_Outcomes> listattrView = new List<V_Diary_List_Outcomes>();

            // Select Fields Values
            IEnumerable<CaseOutcome> D_L_OSelect_Type;
            if (oFilter == "All")
            { D_L_OSelect_Type = db.tblCaseOutcome.ToList(); }
            else
            { D_L_OSelect_Type = db.tblCaseOutcome.Where(r => r.CO_Type == oFilter).ToList(); }

            var dselectfields = new List<System.Web.WebPages.Html.SelectListItem>();
            dselectfields.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Select", Value = "Select" });

            foreach (var item in D_L_OSelect_Type)
            {
                dselectfields.Add(new System.Web.WebPages.Html.SelectListItem { Text = item.CO_Name, Value = item.CO_Name });
            }

            if (selectedList.Count != 0)
            {

                foreach (var item in selectedList)
                {

                    listattrView.Add(new V_Diary_List_Outcomes()
                    {
                        V_O_id = item.O_id,
                        V_O_D_L_id = item.D_L_id,
                        V_O_Name = item.O_Name,
                        V_O_FilterType = item.O_FilterType,
                        V_O_CreatedBy = item.CreatedBy,
                        V_O_CreatedOn = item.CreatedOn,
                        V_O_ModifiedBy = item.ModifiedBy,
                        V_O_ModifiedOn = item.ModifiedOn,
                        V_O_Deleted = item.Deleted,
                        V_O_AllFilter = "All",
                        V_O_CivilFilter = "Civil",
                        V_O_CriminalFilter = "Criminal",
                        V_O_CommittalsFilter = "Committals",
                        V_O_InterventionOrdersFilter = "Intervention Orders",
                        V_O_VOCATFilter = "VOCAT",
                        V_O_ChildrensCourtFilter = "Childrens Court",
                        V_O_SelectFilter = oFilter,
                        V_OSelect_Type = dselectfields
                    });
                }

                return PartialView("_Outcomes", listattrView);

            }
            else
            {
                listattrView.Add(new V_Diary_List_Outcomes()
                {
                    V_O_id = 0,
                    V_O_D_L_id = 0,
                    V_O_Name = "",
                    V_O_FilterType = "All",
                    V_O_CreatedBy = "System",
                    V_O_CreatedOn = DateTime.Now,
                    V_O_ModifiedBy = "System",
                    V_O_ModifiedOn = DateTime.Now,
                    V_O_Deleted = true,
                    V_O_AllFilter = "All",
                    V_O_CivilFilter = "Civil",
                    V_O_CriminalFilter = "Criminal",
                    V_O_CommittalsFilter = "Committals",
                    V_O_InterventionOrdersFilter = "Intervention Orders",
                    V_O_VOCATFilter = "VOCAT",
                    V_O_ChildrensCourtFilter = "Childrens Court",
                    V_O_SelectFilter = oFilter,
                    V_OSelect_Type = dselectfields
                });
                return PartialView("_Outcomes", listattrView);
            }

        }
        
        /// <summary>
        /// Flags
        /// </summary>
        /// <param name="diaryName"></param>
        /// <returns></returns>

        //Update List vacate reasons
        public ActionResult UpdateFlags(string listId, string FName, string diaryName, string updateType, string FId)
        {
            int ilistId = Convert.ToInt32(listId);
            int iFId = Convert.ToInt32(FId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryName);

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

            if (updateType == "Add")
            {

                if (FExists(ilistId, FName) == false)
                {
                    @ViewData["ErrorMessage"] = "Flag exists. Please re-check your entry.";
                    return View();
                }
                else
                {
                    D_L_Flag infoLog = new D_L_Flag
                    {
                        D_L_id = Convert.ToInt32(ilistId),
                        F_Name = FName,
                        Deleted = false,
                        CreatedBy = User.Identity.Name,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = User.Identity.Name,
                        ModifiedOn = DateTime.Now
                    };

                    // Save information
                    db.Entry(infoLog).State = EntityState.Added;
                    db.SaveChanges();

                }

            }
            else if (updateType == "Edit")
            {

                D_L_Flag infoLog = db.tblDiaryListTypeFlags
                                    .Where(r => r.F_id == iFId).FirstOrDefault();

                if (infoLog != null)
                {
                    infoLog.F_Name = FName;
                    infoLog.ModifiedBy = User.Identity.Name;
                    infoLog.ModifiedOn = DateTime.Now;
                    db.Entry(infoLog).State = EntityState.Modified;
                };
                // Save listing information
                db.SaveChanges();

            }
            else
            {
                //int iFId = Convert.ToInt32(FId);

                var fresult = db.tblDiaryListTypeFlags
                    .Where(r => r.F_id == iFId)
                .ToList();

                foreach (D_L_Flag f in fresult)
                {
                    f.Deleted = updateType == "Active" ? false : true;
                    db.Entry(f).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }

            return GetListFlagDetails(ilistId.ToString(), diaryName);
        }

        /// <summary>
        /// Update List
        /// </summary>
        /// <param name="Update Type"></param>
        /// <returns></returns>

        //Update List and set deleted flag
        public ActionResult UpdateList(string updateType, string listId)
        {
            int ilistId = Convert.ToInt32(listId);
            int idid;

            if (updateType == "Delete")
            {

                idid = Convert.ToInt32(db.tblDiaryListTypes.Where(r => r.D_L_id == ilistId).Select(s => s.D_id).FirstOrDefault());

                D_Lists infoLog = db.tblDiaryListTypes
                                 .Where(r => r.D_L_id == ilistId).FirstOrDefault();

                if (infoLog != null)
                {
                    infoLog.Deleted = true;
                    infoLog.ModifiedBy = User.Identity.Name;
                    infoLog.ModifiedOn = DateTime.Now;
                    db.Entry(infoLog).State = EntityState.Modified;
                };
                // Save listing information
                db.SaveChanges();

               
                return DiaryToListTypes(idid.ToString());

            }
            else if (updateType == "Reinstate")
            {
                idid = Convert.ToInt32(db.tblDiaryListTypes.Where(r => r.D_L_id == ilistId).Select(s => s.D_id).FirstOrDefault());
                D_Lists infoLog = db.tblDiaryListTypes
                                 .Where(r => r.D_L_id == ilistId).FirstOrDefault();

                if (infoLog != null)
                {
                    infoLog.Deleted = false;
                    infoLog.ModifiedBy = User.Identity.Name;
                    infoLog.ModifiedOn = DateTime.Now;
                    db.Entry(infoLog).State = EntityState.Modified;
                };
                // Save listing information
                db.SaveChanges();
                return DiaryToListTypes(idid.ToString());
            }

            return View();
        }


        //Returns a view if list vacate reason attributes are available or not
        public ActionResult GetListFlagDetails(string listId, string diaryName)
        {

            int ilistId = Convert.ToInt32(listId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryName);

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

            int dlid = ilistId;

            var selectedList = db.tblDiaryListTypeFlags.Where(r => r.D_L_id == dlid).OrderBy(x => x.Deleted).ThenBy(y => y.F_Name).ToList();

            List<V_Diary_List_Flags> listattrView = new List<V_Diary_List_Flags>();

            if (selectedList.Count != 0)
            {

                foreach (var item in selectedList)
                {

                    listattrView.Add(new V_Diary_List_Flags()
                    {
                        V_F_id = item.F_id,
                        V_F_D_L_id = item.D_L_id,
                        V_F_Name = item.F_Name,
                        V_F_Text = "",
                        V_F_CreatedBy = item.CreatedBy,
                        V_F_CreatedOn = item.CreatedOn,
                        V_F_ModifiedBy = item.ModifiedBy,
                        V_F_ModifiedOn = item.ModifiedOn,
                        V_F_Deleted = item.Deleted,
                    });
                }

                return PartialView("_Flags", listattrView);

            }
            else
            {
                listattrView.Add(new V_Diary_List_Flags()
                {
                    V_F_id = 0,
                    V_F_D_L_id = 0,
                    V_F_Name = "",
                    V_F_Text = "",
                    V_F_CreatedBy = "System",
                    V_F_CreatedOn = DateTime.Now,
                    V_F_ModifiedBy = "System",
                    V_F_ModifiedOn = DateTime.Now,
                    V_F_Deleted = true,
                   
                });
                return PartialView("_Flags", listattrView);
            }

        }

        /// 

        /// <summary>
        /// Locations
        /// </summary>
        /// <param name="diaryName"></param>
        /// <returns></returns>

        //Update List vacate reasons
        public ActionResult UpdateLocations(string listId, string LName, string diaryName, string updateType, string LId)
        {
            int ilistId = Convert.ToInt32(listId);
            int iLId = Convert.ToInt32(LId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryName);

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

            if (updateType == "Add")
            {

                if (LExists(ilistId, LName) == false)
                {
                    @ViewData["ErrorMessage"] = "Location exists. Please re-check your entry.";
                    return View();
                }
                else
                {
                    D_L_Location infoLog = new D_L_Location
                    {
                        D_L_id = Convert.ToInt32(ilistId),
                        L_Name = LName,
                        Deleted = false,
                        CreatedBy = User.Identity.Name,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = User.Identity.Name,
                        ModifiedOn = DateTime.Now
                    };

                    // Save information
                    db.Entry(infoLog).State = EntityState.Added;
                    db.SaveChanges();

                }

            }
            else if (updateType == "Edit")
            {

                D_L_Location infoLog = db.tblDiaryListTypeLocations
                                    .Where(r => r.L_id == iLId).FirstOrDefault();

                if (infoLog != null)
                {
                    infoLog.L_Name = LName;
                    infoLog.ModifiedBy = User.Identity.Name;
                    infoLog.ModifiedOn = DateTime.Now;
                    db.Entry(infoLog).State = EntityState.Modified;
                };
                // Save listing information
                db.SaveChanges();

            }
            else
            {
                //int iLId = Convert.ToInt32(LId);

                var lresult = db.tblDiaryListTypeLocations
                    .Where(r => r.L_id == iLId)
                .ToList();

                foreach (D_L_Location l in lresult)
                {
                    l.Deleted = updateType == "Active" ? false : true;
                    db.Entry(l).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }

            return GetListLocationDetails(ilistId.ToString(), diaryName);
        }

        //Returns a view if list location attributes are available or not
        public ActionResult GetListLocationDetails(string listId, string diaryName)
        {

            int ilistId = Convert.ToInt32(listId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryName);

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

            int dlid = ilistId;

            var selectedList = db.tblDiaryListTypeLocations.Where(r => r.D_L_id == dlid).OrderBy(x => x.Deleted).ThenBy(y => y.L_Name).ToList();

            List<V_Diary_List_Locations> listattrView = new List<V_Diary_List_Locations>();

            if (selectedList.Count != 0)
            {

                foreach (var item in selectedList)
                {

                    listattrView.Add(new V_Diary_List_Locations()
                    {
                        V_L_id = item.L_id,
                        V_L_D_L_id = item.D_L_id,
                        V_L_Name = item.L_Name,
                        V_L_Text = "",
                        V_L_CreatedBy = item.CreatedBy,
                        V_L_CreatedOn = item.CreatedOn,
                        V_L_ModifiedBy = item.ModifiedBy,
                        V_L_ModifiedOn = item.ModifiedOn,
                        V_L_Deleted = item.Deleted,
                    });
                }

                return PartialView("_Locations", listattrView);

            }
            else
            {
                listattrView.Add(new V_Diary_List_Locations()
                {
                    V_L_id = 0,
                    V_L_D_L_id = 0,
                    V_L_Name = "",
                    V_L_Text = "",
                    V_L_CreatedBy = "System",
                    V_L_CreatedOn = DateTime.Now,
                    V_L_ModifiedBy = "System",
                    V_L_ModifiedOn = DateTime.Now,
                    V_L_Deleted = true,

                });
                return PartialView("_Locations", listattrView);
            }

        }

        /// <summary>
        /// Categories
        /// </summary>
        /// <param name="diaryName"></param>
        /// <returns></returns>

        //Update List vacate reasons
        public ActionResult UpdateCategories(string listId, string CName, string diaryName, string updateType, string CId)
        {
            int ilistId = Convert.ToInt32(listId);
            int iCId = Convert.ToInt32(CId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryName);

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

            if (updateType == "Add")
            {

                if (CExists(ilistId, CName) == false)
                {
                    @ViewData["ErrorMessage"] = "Category exists. Please re-check your entry.";
                    return View();
                }
                else
                {
                    D_L_Category infoLog = new D_L_Category
                    {
                        D_L_id = Convert.ToInt32(ilistId),
                        C_Name = CName,
                        Deleted = false,
                        CreatedBy = User.Identity.Name,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = User.Identity.Name,
                        ModifiedOn = DateTime.Now
                    };

                    // Save information
                    db.Entry(infoLog).State = EntityState.Added;
                    db.SaveChanges();

                }

            }
            else if (updateType == "Edit")
            {

                D_L_Category infoLog = db.tblDiaryListTypeCategories
                                    .Where(r => r.C_id == iCId).FirstOrDefault();

                if (infoLog != null)
                {
                    infoLog.C_Name = CName;
                    infoLog.ModifiedBy = User.Identity.Name;
                    infoLog.ModifiedOn = DateTime.Now;
                    db.Entry(infoLog).State = EntityState.Modified;
                };
                // Save listing information
                db.SaveChanges();

            }
            else
            {
                //int iCId = Convert.ToInt32(CId);

                var cresult = db.tblDiaryListTypeCategories
                    .Where(r => r.C_id == iCId)
                .ToList();

                foreach (D_L_Category c in cresult)
                {
                    c.Deleted = updateType == "Active" ? false : true;
                    db.Entry(c).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }

            return GetListCategoryDetails(ilistId.ToString(), diaryName);
        }

        //Returns a view if list category attributes are available or not
        public ActionResult GetListCategoryDetails(string listId, string diaryName)
        {

            int ilistId = Convert.ToInt32(listId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryName);

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

            int dlid = ilistId;

            var selectedList = db.tblDiaryListTypeCategories.Where(r => r.D_L_id == dlid).OrderBy(x => x.Deleted).ThenBy(y => y.C_Name).ToList();

            List<V_Diary_List_Categories> listattrView = new List<V_Diary_List_Categories>();

            if (selectedList.Count != 0)
            {

                foreach (var item in selectedList)
                {

                    listattrView.Add(new V_Diary_List_Categories()
                    {
                        V_C_id = item.C_id,
                        V_C_D_L_id = item.D_L_id,
                        V_C_Name = item.C_Name,
                        V_C_Text = "",
                        V_C_CreatedBy = item.CreatedBy,
                        V_C_CreatedOn = item.CreatedOn,
                        V_C_ModifiedBy = item.ModifiedBy,
                        V_C_ModifiedOn = item.ModifiedOn,
                        V_C_Deleted = item.Deleted,
                    });
                }

                return PartialView("_Categories", listattrView);

            }
            else
            {
                listattrView.Add(new V_Diary_List_Categories()
                {
                    V_C_id = 0,
                    V_C_D_L_id = 0,
                    V_C_Name = "",
                    V_C_Text = "",
                    V_C_CreatedBy = "System",
                    V_C_CreatedOn = DateTime.Now,
                    V_C_ModifiedBy = "System",
                    V_C_ModifiedOn = DateTime.Now,
                    V_C_Deleted = true,

                });
                return PartialView("_Categories", listattrView);
            }

        }        
        
        /// 
        public ActionResult DiaryToListTypes(string diaryName)
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

            int did = Convert.ToInt32(diaryName);
            var selectedDiary = db.tblDiary.Where(r => r.D_id == did).FirstOrDefault();

            if (selectedDiary != null)
            {

                IEnumerable<D_Lists> allListTypes;

                if (ViewData["InSysAdminRole"] == "true")
                {
                    allListTypes = db.tblDiaryListTypes.Where(r => r.D_id == selectedDiary.D_id).OrderBy(s => s.D_L_ShortName).ThenBy(t => t.D_L_Lists);
                }
                else
                { 
                    allListTypes = db.tblDiaryListTypes.Where(r => r.D_id == selectedDiary.D_id && r.Deleted == false ).OrderBy(s => s.D_L_ShortName).ThenBy(t => t.D_L_Lists);
                }
                
                if (allListTypes.Any())
                {
                    return PartialView("_ListTypes", allListTypes);
                }
                else
                {
                    List<D_Lists> noListTypes = new List<D_Lists>();
                    noListTypes.Add(new D_Lists()
                    {
                        D_id = 0,
                        D_L_ShortName = "No listings",
                        D_L_Rules = false
                    });

                    return PartialView("_ListTypes", noListTypes);
                }
            }
            else
            {
                List<D_Lists> noListTypes = new List<D_Lists>();
                noListTypes.Add(new D_Lists()
                {
                    D_id = 0,
                    D_L_ShortName = "No listings",
                    D_L_Rules = false
                });

                return PartialView("_ListTypes", noListTypes);
                
            }

        }

        public ActionResult GetListDetails(string listId, string diaryName)
        {

            int ilistId = Convert.ToInt32(listId);

            if (listId == "0" || listId == null)
            {
                int did = Convert.ToInt32(diaryName);

                var model = from d in db.tblDiary
                            join dl in db.tblDiaryListTypes on d.D_id equals dl.D_id into dld
                            from dl in dld.DefaultIfEmpty()
                            where d.D_id == did && d.Deleted == false
                            orderby dl.D_L_ShortName ascending

                            select new ListingIdVM
                            {
                                VM_D_L_Id = dl.D_L_id
                            };

                //var ExistsAny = model.Any(a => a.VM_D_L_Id == 1);
                //var loc = tmp == null ? String.Empty : tmp.name;

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
            D_Lists selectedList = db.tblDiaryListTypes.Where(r => r.D_L_id == did1).FirstOrDefault();

            // Type Values
            var dtype = new List<System.Web.WebPages.Html.SelectListItem>();
            dtype.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Minutes", Value = "Minutes" });
            dtype.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Standard", Value = "Standard" });
            dtype.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Weekly", Value = "Weekly" });

            // Rules Apply To Values
            var drulesapplyto = new List<System.Web.WebPages.Html.SelectListItem>();
            drulesapplyto.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Every Day", Value = "Every Day" });
            drulesapplyto.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Start Date Only", Value = "Start Date Only" });

            // Vacate Options Values
            var dvacateoptions = new List<System.Web.WebPages.Html.SelectListItem>();
            dvacateoptions.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Update Listing Rules (Give Back Space)", Value = "Update Listing Rules (Give Back Space)" });
            dvacateoptions.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Do Not Update Listing Rules", Value = "Do Not Update Listing Rules" });

            if (selectedList != null)
            {

                V_Diary_List_Details diarylistmngtView = new V_Diary_List_Details()
                {
                    D_id = selectedList.D_id,
                    D_L_id = selectedList.D_L_id,
                    D_L_Lists = selectedList.D_L_Lists,
                    D_L_ShortName = selectedList.D_L_ShortName,
                    D_L_Rules = selectedList.D_L_Rules,
                    D_L_Type = selectedList.D_L_Type,
                    D_L_Select_Type = dtype,
                    D_L_RulesApplyTo = selectedList.D_L_RulesApplyTo,
                    D_L_Select_RulesApplyTo = drulesapplyto,
                    D_L_VacateOptions = selectedList.D_L_VacateOptions,
                    D_L_Select_VacateOptions = dvacateoptions,
                    D_L_Bckground_row_Colour = selectedList.D_L_Bckground_row_Colour,
                    D_L_Text_Colour = selectedList.D_L_Text_Colour,
                    D_L_Default_StartTime = selectedList.D_L_Default_StartTime,
                    D_L_MandatoryCategory = selectedList.D_L_MandatoryCategory,
                    D_L_MandatoryDuration = selectedList.D_L_MandatoryDuration,
                    D_L_MandatoryLocation = selectedList.D_L_MandatoryLocation,
                    D_L_MandatoryStartTime = selectedList.D_L_MandatoryStartTime,
                    D_Attr_Category = selectedList.D_Attr_Category,
                    D_Attr_Location = selectedList.D_Attr_Location,
                    D_Attr_Vacate_Reason = selectedList.D_Attr_Vacate_Reason,
                    D_Attr_Outcome = selectedList.D_Attr_Outcome,
                    D_Attr_Flag = selectedList.D_Attr_Flag,
                    D_Attr_Category_Count = selectedList.D_Attr_Category.Count(),
                    D_Attr_Location_Count = selectedList.D_Attr_Location.Count(),
                    D_Attr_Outcome_Count = selectedList.D_Attr_Outcome.Count(),
                    D_Attr_Flag_Count = selectedList.D_Attr_Flag.Count(),
                    D_Attr_Vacate_Reason_Count = selectedList.D_Attr_Vacate_Reason.Count(),
                };

                return PartialView("_Lists", diarylistmngtView);

            }
            else
            {
                V_Diary_List_Details diarylistmngtView = new V_Diary_List_Details()
                {
                    D_id = 0,
                    D_L_id = 0,
                    D_L_Lists = "",
                    D_L_ShortName = "",
                    D_L_Rules = false,
                    D_L_Type = "Standard",
                    D_L_Select_Type = dtype,
                    D_L_RulesApplyTo = "Every Day",
                    D_L_Select_RulesApplyTo = drulesapplyto,
                    D_L_VacateOptions = "Update Listing rules (Give Back Space)",
                    D_L_Select_VacateOptions = dvacateoptions,
                    D_L_Bckground_row_Colour = "black",
                    D_L_Text_Colour = "white",
                    D_L_Default_StartTime = "09:30 AM",
                    D_L_MandatoryCategory = false,
                    D_L_MandatoryDuration = false,
                    D_L_MandatoryLocation = false,
                    D_L_MandatoryStartTime = false
                };
                return PartialView("_Lists", diarylistmngtView);
            }

            //return PartialView("_Lists", selectedList);
        }
    
        public ActionResult GetDiaryDetails(string diaryName)
        {
            int did = Convert.ToInt32(diaryName);
            D_Diaries selectedDiary = db.tblDiary.Where(r => r.D_id == did).FirstOrDefault();
 
            if (selectedDiary != null)
            {

                // Fields Values
                var aa = new List<string> { };

                var dfields = new List<System.Web.WebPages.Html.SelectListItem>{
                                        new System.Web.WebPages.Html.SelectListItem { Text = "CCV", Value = "CCV" },
                                        new System.Web.WebPages.Html.SelectListItem { Text = "KIDS", Value = "KIDS" },
                                        new System.Web.WebPages.Html.SelectListItem { Text = "MCV", Value = "MCV" }};


                //dfields.Add(new System.Web.WebPages.Html.SelectListItem{ Text = "CCV", Value = "CCV" });
                //dfields.Add(new System.Web.WebPages.Html.SelectListItem{ Text = "KIDS", Value = "KIDS" });
                //dfields.Add(new System.Web.WebPages.Html.SelectListItem { Text = "MCV", Value = "MCV" });

                // Datepicker Values
                var ddatepicker = new List<System.Web.WebPages.Html.SelectListItem>();
                ddatepicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Standard", Value = "Standard" });
                ddatepicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Custom", Value = "Custom" });
                ddatepicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Standard + Custom", Value = "Standard + Custom" });
                ddatepicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Off", Value = "Off" });

                // Standard & Readonly Values
                string[] AccessGroups = ConfigurationManager.AppSettings.AllKeys
                                             .Where(key => key.Contains("Group"))
                                             .Select(key => ConfigurationManager.AppSettings[key])
                                             .ToArray();

                var standarduser = new List<System.Web.WebPages.Html.SelectListItem>();
                var readonlyuser = new List<System.Web.WebPages.Html.SelectListItem>();
                foreach (var item in AccessGroups)
                {
                    standarduser.Add(new System.Web.WebPages.Html.SelectListItem { Text = item.ToString() , Value = item.ToString() });
                    readonlyuser.Add(new System.Web.WebPages.Html.SelectListItem { Text = item.ToString(), Value = item.ToString() });
                }

                // Multiday cases Values
                var dmultidaycasespicker = new List<System.Web.WebPages.Html.SelectListItem>();
                dmultidaycasespicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Expand Start Date - End Date", Value = "Expand Start Date - End Date" });
                dmultidaycasespicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Start Date Only", Value = "Start Date Only" });

                // Type Values
                var dtypepicker = new List<System.Web.WebPages.Html.SelectListItem>();
                dtypepicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Custom", Value = "Custom" });
                dtypepicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Standard View", Value = "Standard View" });

                // Dates Values
                var ddatespicker = new List<System.Web.WebPages.Html.SelectListItem>();
                ddatespicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Expand Start Date - End Date", Value = "Expand Start Date - End Date" });
                ddatespicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Start Date Only", Value = "Start Date Only" });

                // Vacated Values
                var dvacatedpicker = new List<System.Web.WebPages.Html.SelectListItem>();
                dvacatedpicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Excluded", Value = "Excluded" });
                dvacatedpicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Included", Value = "Included" });

                // List Selection Values
                var dlistselectionpicker = new List<System.Web.WebPages.Html.SelectListItem>();
                dlistselectionpicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "All Selected", Value = "All Selected" });
                dlistselectionpicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "User Selected", Value = "User Selected" });

                //IEnumerable<Role> Roles;
                //var selectItemList = Roles
                //  .Select(p => new SelectListItem { Value = "1", Text = "item 1" });
                //  //.ToList(); 
                
                //List<SelectListItem> SelectD_Fields = new List<SelectListItem>()
                //{
                //    new SelectListItem { Value = "1", Text = "item 1" },
                //    new SelectListItem { Value = "2", Text = "item 2" },
                //    new SelectListItem { Value = "3", Text = "item 3" },
                //};

                //var SelectOptions = new List<SelectListItem>()
                //{
                //    new SelectListItem { Value = "1", Text = "item 1" },
                //    new SelectListItem { Value = "2", Text = "item 2" },
                //    new SelectListItem { Value = "3", Text = "item 3" },
                //};


                V_Diary_Details diarymngtView = new V_Diary_Details()

                //diarymngtView.Add(new V_Diary_Details()
                {
                    D_id = selectedDiary.D_id,
                    D_DiaryName = selectedDiary.D_DiaryName,
                    D_Fields = selectedDiary.D_Fields,
                    D_SelectD_Fields = dfields,
                    D_Outline = selectedDiary.D_Outline,
                    D_DatePickerButton = selectedDiary.D_DatePickerButton,
                    D_DatePicker_Fields = ddatepicker,
                    D_Standard_User = selectedDiary.D_Standard_User,
                    D_StandardUser_Fields = standarduser,
                    D_ReadOnly = selectedDiary.D_ReadOnly,
                    D_ReadOnly_Fields = readonlyuser,
                    D_Bckground_Icon_Colour = selectedDiary.D_Bckground_Icon_Colour,
                    D_Multiday_Cases = selectedDiary.D_Multiday_Cases,
                    D_Multiday_Cases_Fields = dmultidaycasespicker,
                    D_Dates = selectedDiary.D_Dates,
                    D_Dates_Fields = ddatespicker,
                    D_Vacated = selectedDiary.D_Vacated,
                    D_Vacated_Fields = dvacatedpicker,
                    D_ListSelection = selectedDiary.D_ListSelection,
                    D_ListSelection_Fields = dlistselectionpicker,
                };

                return PartialView("_Diary", diarymngtView);
            }

            return PartialView("_Diary", selectedDiary);
        }

        //Check if diary exists
        private bool DiaryExists(string diaryName)
        {
            bool returnValue = false;

            var recCount = db.tblDiary
                .Where(r => r.D_DiaryName == diaryName && r.Deleted == false)
            .ToList();

            int totalRowsCount = recCount.Count();

            if (totalRowsCount == 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        //Check if vacate reason exists
        private bool VRExists(int listId, string vrName, string vrType)
        {
            bool returnValue = false;

            var recCount = db.tblDiaryListTypeVacateReasons
                .Where(r => r.D_L_id == listId && r.VR_Name == vrName) // && r.VR_FilterType == vrType)
            .ToList();

            int totalRowsCount = recCount.Count();

            if (totalRowsCount == 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        //Check if outcome exists
        private bool OCExists(int listId, string oName, string oType)
        {
            bool returnValue = false;

            var recCount = db.tblDiaryListTypeOutcomes
                .Where(r => r.D_L_id == listId && r.O_Name == oName) //&& r.O_FilterType == oType)
            .ToList();

            int totalRowsCount = recCount.Count();

            if (totalRowsCount == 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        //Check if flag exists
        private bool FExists(int listId, string oName)
        {
            bool returnValue = false;

            var recCount = db.tblDiaryListTypeFlags
                .Where(r => r.D_L_id == listId && r.F_Name == oName) 
            .ToList();

            int totalRowsCount = recCount.Count();

            if (totalRowsCount == 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        //Check if location exists
        private bool LExists(int listId, string lName)
        {
            bool returnValue = false;

            var recCount = db.tblDiaryListTypeLocations
                .Where(r => r.D_L_id == listId && r.L_Name == lName)
            .ToList();

            int totalRowsCount = recCount.Count();

            if (totalRowsCount == 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        //Check if category exists
        private bool CExists(int listId, string lName)
        {
            bool returnValue = false;

            var recCount = db.tblDiaryListTypeCategories
                .Where(r => r.D_L_id == listId && r.C_Name == lName)
            .ToList();

            int totalRowsCount = recCount.Count();

            if (totalRowsCount == 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        //Check if list shortname exists
        private bool ListExists(string listName)
        {
            bool returnValue = false;

            var recCount = db.tblDiaryListTypes
                .Where(r => r.D_L_ShortName == listName && r.Deleted == false)
            .ToList();

            int totalRowsCount = recCount.Count();

            if (totalRowsCount == 0)
            {
                returnValue = true;
            }

            return returnValue;
        }
        public class ListingIdVM
        {
            public int VM_D_L_Id { get; set; }
        }
        public ActionResult GetDiaryListId(string diaryName)
        {
            int listId = 0;
            int did = Convert.ToInt32(diaryName);

            var model = from d in db.tblDiary
                        join dl in db.tblDiaryListTypes on d.D_id equals dl.D_id into dld
                        from dl in dld.DefaultIfEmpty()
                        where d.D_id == did && d.Deleted == false
                        orderby dl.D_L_ShortName ascending

                        select new ListingIdVM
                        {
                            VM_D_L_Id = dl.D_L_id
                        };

            //var recCount = model.ToList();

            //int totalRowsCount = recCount.Count();

            if (model != null)
            {
                listId = model.FirstOrDefault().VM_D_L_Id;
            }

            return Json(listId, JsonRequestBehavior.AllowGet);
        }
        
    
        [HttpPost]  
        //public ContentResult SimplePost(string submittedName)  
        public ActionResult EditDiary(string aD_Did, string aD_DiaryName, string aD_Fields, string aD_Outline, string aD_DatePicker, string aStandardUser, string aD_ReadOnly, string aD_Bckground_Icon_Colour, string aD_Multiday_Cases, string aD_Dates, string aD_Vacated, string aD_ListSelection)  
        {
            //string result = aD_Did + "" + aD_Fields;
            //string result = string.Format("See how easy that was, {0}!?", submittedName);  
            //return new ContentResult { Content = result };
            //return new ContentResult { Content = result };

            if (aD_Did == "cancel")
            {
                D_Diaries model = db.tblDiary.FirstOrDefault();
                {
                    model.D_DiaryName = aD_DiaryName;
                    model.D_Fields = aD_Fields;
                    model.D_Outline = aD_Outline;
                    model.D_DatePickerButton = aD_DatePicker;
                    model.D_Standard_User = aStandardUser;
                    model.D_ReadOnly = aD_ReadOnly;
                    model.D_Bckground_Icon_Colour = aD_Bckground_Icon_Colour;
                    model.D_Multiday_Cases = aD_Multiday_Cases;
                    model.D_Dates = aD_Dates;
                    model.D_Vacated = aD_Vacated;
                    model.D_ListSelection = aD_ListSelection;
                    model.ModifiedBy = User.Identity.Name;
                    model.ModifiedOn = DateTime.Now;
                    model.CreatedBy = User.Identity.Name;
                    model.CreatedOn = DateTime.Now;
                    model.ModifiedBy = User.Identity.Name;
                    model.ModifiedOn = DateTime.Now;
                    model.Deleted = false;
                };
                return RedirectToAction("Edit", "DiaryManagement");
            }
            else if (aD_Did == "")
            {

                if (DiaryExists(aD_DiaryName) == false)
                {
                    @ViewData["ErrorMessage"] = "Diary " + aD_DiaryName + " exists. Please re-check your entry.";
                    @ViewData["hid_Did"] = "exists";
                    return View();
                } 
                else
                {
                    D_Diaries model = new D_Diaries();
                    {
                        model.D_DiaryName = aD_DiaryName;
                        model.D_Fields = aD_Fields;
                        model.D_Outline = aD_Outline;
                        model.D_DatePickerButton = aD_DatePicker;
                        model.D_Standard_User = aStandardUser;
                        model.D_ReadOnly = aD_ReadOnly;
                        model.D_Bckground_Icon_Colour = aD_Bckground_Icon_Colour;
                        model.D_Multiday_Cases = aD_Multiday_Cases;
                        model.D_Dates = aD_Dates;
                        model.D_Vacated = aD_Vacated;
                        model.D_ListSelection = aD_ListSelection;
                        model.ModifiedBy = User.Identity.Name;
                        model.ModifiedOn = DateTime.Now;
                        model.CreatedBy = User.Identity.Name;
                        model.CreatedOn = DateTime.Now;
                        model.ModifiedBy = User.Identity.Name;
                        model.ModifiedOn = DateTime.Now;
                        model.Deleted = false;
                    };


                    // Save log information
                    db.Entry(model).State = EntityState.Added;
                    db.SaveChanges();

                }
                    return RedirectToAction("Edit", "DiaryManagement");
            }
            else
            {
                D_Diaries model = db.tblDiary.Find(Convert.ToInt32(aD_Did));
                if (model != null)
                {
                    model.D_DiaryName = aD_DiaryName;
                    model.D_Fields = aD_Fields;
                    model.D_Outline = aD_Outline;
                    model.D_DatePickerButton = aD_DatePicker;
                    model.D_Standard_User = aStandardUser;
                    model.D_ReadOnly = aD_ReadOnly;
                    model.D_Bckground_Icon_Colour = aD_Bckground_Icon_Colour;
                    model.D_Multiday_Cases = aD_Multiday_Cases;
                    model.D_Dates = aD_Dates;
                    model.D_Vacated = aD_Vacated;
                    model.D_ListSelection = aD_ListSelection;
                    model.ModifiedBy = User.Identity.Name;
                    model.ModifiedOn = DateTime.Now;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();

                    // Fields Values
                    var dfields = new List<System.Web.WebPages.Html.SelectListItem>();
                    dfields.Add(new System.Web.WebPages.Html.SelectListItem { Text = "CCV", Value = "CCV" });
                    dfields.Add(new System.Web.WebPages.Html.SelectListItem { Text = "KIDS", Value = "KIDS" });
                    dfields.Add(new System.Web.WebPages.Html.SelectListItem { Text = "MCV", Value = "MCV" });

                    // Datepicker Values
                    var ddatepicker = new List<System.Web.WebPages.Html.SelectListItem>();
                    ddatepicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Standard", Value = "Standard" });
                    ddatepicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Custom", Value = "Custom" });
                    ddatepicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Standard + Custom", Value = "Standard + Custom" });
                    ddatepicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Off", Value = "Off" });

                    // Standard & Readonly Values
                    string[] AccessGroups = ConfigurationManager.AppSettings.AllKeys
                                                 .Where(key => key.Contains("Group"))
                                                 .Select(key => ConfigurationManager.AppSettings[key])
                                                 .ToArray();

                    var standarduser = new List<System.Web.WebPages.Html.SelectListItem>();
                    var readonlyuser = new List<System.Web.WebPages.Html.SelectListItem>();
                    foreach (var item in AccessGroups)
                    {
                        standarduser.Add(new System.Web.WebPages.Html.SelectListItem { Text = item.ToString(), Value = item.ToString() });
                        readonlyuser.Add(new System.Web.WebPages.Html.SelectListItem { Text = item.ToString(), Value = item.ToString() });
                    }

                    // Multiday cases Values
                    var dmultidaycasespicker = new List<System.Web.WebPages.Html.SelectListItem>();
                    dmultidaycasespicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Expand Start Date - End Date", Value = "Expand Start Date - End Date" });
                    dmultidaycasespicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Start Date Only", Value = "Start Date Only" });

                    // Type Values
                    var dtypepicker = new List<System.Web.WebPages.Html.SelectListItem>();
                    dtypepicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Custom", Value = "Custom" });
                    dtypepicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Standard View", Value = "Standard View" });

                    // Dates Values
                    var ddatespicker = new List<System.Web.WebPages.Html.SelectListItem>();
                    ddatespicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Expand Start Date - End Date", Value = "Expand Start Date - End Date" });
                    ddatespicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Start Date Only", Value = "Start Date Only" });

                    // Vacated Values
                    var dvacatedpicker = new List<System.Web.WebPages.Html.SelectListItem>();
                    dvacatedpicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Excluded", Value = "Excluded" });
                    dvacatedpicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Included", Value = "Included" });

                    // List Selection Values
                    var dlistselectionpicker = new List<System.Web.WebPages.Html.SelectListItem>();
                    dlistselectionpicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "All Selected", Value = "All Selected" });
                    dlistselectionpicker.Add(new System.Web.WebPages.Html.SelectListItem { Text = "User Selected", Value = "User Selected" });

                    V_Diary_Details diarymngtView = new V_Diary_Details()
                    {
                        D_id = model.D_id,
                        D_DiaryName = model.D_DiaryName,
                        D_Fields = model.D_Fields,
                        D_SelectD_Fields = dfields,
                        D_Outline = model.D_Outline,
                        D_DatePickerButton = model.D_DatePickerButton,
                        D_DatePicker_Fields = ddatepicker,
                        D_Standard_User = model.D_Standard_User,
                        D_StandardUser_Fields = standarduser,
                        D_ReadOnly = model.D_ReadOnly,
                        D_ReadOnly_Fields = readonlyuser,
                        D_Bckground_Icon_Colour = model.D_Bckground_Icon_Colour,
                        D_Multiday_Cases = model.D_Multiday_Cases,
                        D_Multiday_Cases_Fields = dmultidaycasespicker,
                        D_Dates = model.D_Dates,
                        D_Dates_Fields = ddatespicker,
                        D_Vacated = model.D_Vacated,
                        D_Vacated_Fields = dvacatedpicker,
                        D_ListSelection = model.D_ListSelection,
                        D_ListSelection_Fields = dlistselectionpicker,
                    };

                    return PartialView("_Diary", diarymngtView);
                }

            }

            return View();

        }

        [HttpPost]
        public ActionResult EditList(string aDL_Did, string aDL_Dlid, string aDL_DLlist, string aDL_Shortname, string aDL_DLRules, string aDL_DLType, string aDL_DLRulesApplyTo, string aDL_DLVacateOptions, string aDL_DLBckground_row_Colour, string aDL_DLText_Colour, string aDL_DLMandatoryCategory, string aDL_DLMandatoryDuration, string aDL_DLMandatoryLocation, string aDL_DLMandatoryStartTime, string aDL_DLDefault_StartTime)
        {

            if (aDL_Dlid == "cancel")
            {
                D_Lists model = db.tblDiaryListTypes.FirstOrDefault();
                {
                    model.D_L_Lists = aDL_DLlist;
                    model.D_L_ShortName = aDL_Shortname;
                    model.D_L_Rules = Convert.ToBoolean(aDL_DLRules);
                    model.D_L_Type = aDL_DLType;
                    model.D_L_RulesApplyTo = aDL_DLRulesApplyTo;
                    model.D_L_VacateOptions = aDL_DLVacateOptions;
                    model.D_L_Bckground_row_Colour = aDL_DLBckground_row_Colour;
                    model.D_L_Text_Colour = aDL_DLText_Colour;
                    model.D_L_MandatoryCategory = Convert.ToBoolean(aDL_DLMandatoryCategory);
                    model.D_L_MandatoryDuration = Convert.ToBoolean(aDL_DLMandatoryDuration);
                    model.D_L_MandatoryLocation = Convert.ToBoolean(aDL_DLMandatoryLocation);
                    model.D_L_MandatoryStartTime = Convert.ToBoolean(aDL_DLMandatoryStartTime);
                    model.D_L_Default_StartTime = aDL_DLDefault_StartTime;
                    model.ModifiedBy = User.Identity.Name;
                    model.ModifiedOn = DateTime.Now;
                    model.CreatedBy = User.Identity.Name;
                    model.CreatedOn = DateTime.Now;
                    model.ModifiedBy = User.Identity.Name;
                    model.ModifiedOn = DateTime.Now;
                    model.Deleted = false;
                };
                return RedirectToAction("Edit", "DiaryManagement");
            }
            else if (aDL_Dlid == "")
            {

                if (ListExists(aDL_Shortname) == false)
                {
                    @ViewData["ErrorMessage"] = "List " + aDL_Shortname + " exists. Please re-check your entry.";
                    @ViewData["hid_DLid"] = "exists";
                    return View();
                }
                else
                {
                    D_Lists model = new D_Lists();
                    {
                        model.D_id = Convert.ToInt32(aDL_Did);
                        model.D_L_Lists = aDL_DLlist;
                        model.D_L_ShortName = aDL_Shortname;
                        model.D_L_Rules = Convert.ToBoolean(aDL_DLRules);
                        model.D_L_Type = aDL_DLType;
                        model.D_L_RulesApplyTo = aDL_DLRulesApplyTo;
                        model.D_L_VacateOptions = aDL_DLVacateOptions;
                        model.D_L_Bckground_row_Colour = aDL_DLBckground_row_Colour;
                        model.D_L_Text_Colour = aDL_DLText_Colour;
                        model.D_L_MandatoryCategory = Convert.ToBoolean(aDL_DLMandatoryCategory);
                        model.D_L_MandatoryDuration = Convert.ToBoolean(aDL_DLMandatoryDuration);
                        model.D_L_MandatoryLocation = Convert.ToBoolean(aDL_DLMandatoryLocation);
                        model.D_L_MandatoryStartTime = Convert.ToBoolean(aDL_DLMandatoryStartTime);
                        model.D_L_Default_StartTime = aDL_DLDefault_StartTime;
                        model.ModifiedBy = User.Identity.Name;
                        model.ModifiedOn = DateTime.Now;
                        model.CreatedBy = User.Identity.Name;
                        model.CreatedOn = DateTime.Now;
                        model.ModifiedBy = User.Identity.Name;
                        model.ModifiedOn = DateTime.Now;
                        model.Deleted = false;
                    };


                    // Save log information
                    db.Entry(model).State = EntityState.Added;
                    db.SaveChanges();

                }
                return RedirectToAction("Edit", "DiaryManagement");
            }
            else
            {
                D_Lists selectedList = db.tblDiaryListTypes.Find(Convert.ToInt32(aDL_Dlid));
                if (selectedList != null)
                {
                    selectedList.D_id = Convert.ToInt32(aDL_Did);
                    selectedList.D_L_Lists = aDL_DLlist;
                    selectedList.D_L_ShortName = aDL_Shortname;
                    selectedList.D_L_Rules = Convert.ToBoolean(aDL_DLRules);
                    selectedList.D_L_Type = aDL_DLType;
                    selectedList.D_L_RulesApplyTo = aDL_DLRulesApplyTo;
                    selectedList.D_L_VacateOptions = aDL_DLVacateOptions;
                    selectedList.D_L_Bckground_row_Colour = aDL_DLBckground_row_Colour;
                    selectedList.D_L_Text_Colour = aDL_DLText_Colour;
                    selectedList.D_L_MandatoryCategory = Convert.ToBoolean(aDL_DLMandatoryCategory);
                    selectedList.D_L_MandatoryDuration = Convert.ToBoolean(aDL_DLMandatoryDuration);
                    selectedList.D_L_MandatoryLocation = Convert.ToBoolean(aDL_DLMandatoryLocation);
                    selectedList.D_L_MandatoryStartTime = Convert.ToBoolean(aDL_DLMandatoryStartTime);
                    selectedList.D_L_Default_StartTime = aDL_DLDefault_StartTime;
                    selectedList.ModifiedBy = User.Identity.Name;
                    selectedList.ModifiedOn = DateTime.Now;
                    selectedList.ModifiedBy = User.Identity.Name;
                    selectedList.ModifiedOn = DateTime.Now;
                    db.Entry(selectedList).State = EntityState.Modified;
                    db.SaveChanges();

                    // Type Values
                    var dtype = new List<System.Web.WebPages.Html.SelectListItem>();
                    dtype.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Minutes", Value = "Minutes" });
                    dtype.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Standard", Value = "Standard" });
                    dtype.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Weekly", Value = "Weekly" });

                    // Rules Apply To Values
                    var drulesapplyto = new List<System.Web.WebPages.Html.SelectListItem>();
                    drulesapplyto.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Every Day", Value = "Every Day" });
                    drulesapplyto.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Start Date Only", Value = "Start Date Only" });

                    // Vacate Options Values
                    var dvacateoptions = new List<System.Web.WebPages.Html.SelectListItem>();
                    dvacateoptions.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Update Listing Rules (Give Back Space)", Value = "Update Listing Rules (Give Back Space)" });
                    dvacateoptions.Add(new System.Web.WebPages.Html.SelectListItem { Text = "Do Not Update Listing Rules", Value = "Do Not Update Listing Rules" });

                    V_Diary_List_Details diarylistmngtView = new V_Diary_List_Details()
                    {
                        D_id = selectedList.D_id,
                        D_L_id = selectedList.D_L_id,
                        D_L_Lists = selectedList.D_L_Lists,
                        D_L_ShortName = selectedList.D_L_ShortName,
                        D_L_Rules = selectedList.D_L_Rules,
                        D_L_Type = selectedList.D_L_Type,
                        D_L_Select_Type = dtype,
                        D_L_RulesApplyTo = selectedList.D_L_RulesApplyTo,
                        D_L_Select_RulesApplyTo = drulesapplyto,
                        D_L_VacateOptions = selectedList.D_L_VacateOptions,
                        D_L_Select_VacateOptions = dvacateoptions,
                        D_L_Bckground_row_Colour = selectedList.D_L_Bckground_row_Colour,
                        D_L_Text_Colour = selectedList.D_L_Text_Colour,
                        D_L_Default_StartTime = selectedList.D_L_Default_StartTime,
                        D_L_MandatoryCategory = selectedList.D_L_MandatoryCategory,
                        D_L_MandatoryDuration = selectedList.D_L_MandatoryDuration,
                        D_L_MandatoryLocation = selectedList.D_L_MandatoryLocation,
                        D_L_MandatoryStartTime = selectedList.D_L_MandatoryStartTime
                    };

                    return PartialView("_Lists", diarylistmngtView);
                }

            }

            return View();

        }  

     }
}