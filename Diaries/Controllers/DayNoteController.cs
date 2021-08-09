using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Diaries.Models;

namespace Diaries.Controllers
{
    public class DayNoteController : Controller
    {
        private DiariesDB db = new DiariesDB();

        // GET: DayNote
        public class JQueryDataTableParamModel
        {
            public string sEcho { get; set; }
            public string sSearch { get; set; }
            public int iDisplayLength { get; set; }
            public int iDisplayStart { get; set; }
            public string first_data { get; set; }
            public string second_data { get; set; }
        }

        public JsonResult GetAjaxData(JQueryDataTableParamModel param)
        {

            var model = db.tblDayNote
               .Where(r => r.Deleted == false)
               .Select(r => new DayNoteVM { VM_Active = r.IsActive ? "Active" : "Inactive", VM_Year = r.DN_Start_Date.Year , VM_Start = r.DN_Start_Date, VM_End = r.DN_End_Date, VM_Note = r.DN_Note, VM_Id = r.DN_ID})
                .ToList();

            List<DayNoteVM> modelsorted;

            int totalRowsCount = 0;
            int filteredRowsCount = 0;
            string[][] aaData;

            if (param.first_data != null && param.second_data != null)
            {
                List<DayNoteVM> aa = new List<DayNoteVM>();
                foreach (var group in model)
                {
                    var test = group.VM_Active.Replace(" ", "").Trim('-').ToLower();
                    var test1 = group.VM_Year;
                    if (test.Contains(param.first_data) &&
                        test1.ToString().Contains(param.second_data))
                        aa.Add(group);
                }

                modelsorted = aa.OrderBy(r => r.VM_Active).ThenBy(e => e.VM_Year).ToList();
                aa = null;

                aaData = modelsorted.Select(d => new string[] { d.VM_Active, d.VM_Year.ToString(), d.VM_Start.ToString("dd-MM-yyyy"), d.VM_End.ToString("dd-MM-yyyy"), d.VM_Note, d.VM_Id.ToString() }).ToArray();

                totalRowsCount = modelsorted.Count();
                filteredRowsCount = modelsorted.Count();

                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = aaData,
                    iTotalRecords = Convert.ToInt32(totalRowsCount),
                    iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount)
                }, JsonRequestBehavior.AllowGet);


            }

            if (param.sSearch != null)
            {
                if (param.sSearch.Length > 2)
                {

                    List<DayNoteVM> aa = new List<DayNoteVM>();
                    foreach (var group in model)
                    {
                        if (group.VM_Active.ToLower().Contains(param.sSearch.ToLower()) || group.VM_Year.ToString().Contains(param.sSearch.ToLower()) || group.VM_Note.ToLower().Contains(param.sSearch.ToLower()) || group.VM_End.ToString("dd-MM-yyyy").ToLower().Contains(param.sSearch.ToLower()) ||
                            group.VM_Start.ToString("dd-MM-yyyy").ToLower().Contains(param.sSearch.ToLower()))

                            aa.Add(group);

                    }

                    modelsorted = aa.OrderBy(r => r.VM_Active).ThenBy(e => e.VM_Year).ToList();
                    aa = null;
                    aaData = modelsorted.Select(d => new string[] { d.VM_Active, d.VM_Year.ToString(), d.VM_Start.ToString("dd-MM-yyyy"), d.VM_End.ToString("dd-MM-yyyy"), d.VM_Note, d.VM_Id.ToString() }).ToArray();

                    totalRowsCount = modelsorted.Count();
                    filteredRowsCount = modelsorted.Count();

                    return Json(new
                    {
                        sEcho = param.sEcho,
                        aaData = aaData,
                        iTotalRecords = Convert.ToInt32(totalRowsCount),
                        iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount)
                    }, JsonRequestBehavior.AllowGet);
                }

            }

            totalRowsCount = model.Count();
            filteredRowsCount = model.Count();
            var vv = model.Select(r => new { r.VM_Active, r.VM_Year }).Distinct().OrderBy(r => r.VM_Active).ThenBy(e => e.VM_Year).ToList();

            aaData = vv.Select(d => new string[] { d.VM_Active, d.VM_Year.ToString(), "", "", "", "", "", "" }).ToArray();


            return Json(new
            {
                sEcho = param.sEcho,
                aaData = aaData,
                iTotalRecords = Convert.ToInt32(totalRowsCount),
                iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount)
            }, JsonRequestBehavior.AllowGet);

        }
        public async Task<ActionResult> Index()
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
            { ViewData["UserName"] = User.Identity.Name; }

            return View(await db.tblDayNote.ToListAsync());
        }

        // GET: DayNote/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DayNote dayNote = await db.tblDayNote.FindAsync(id);
            if (dayNote == null)
            {
                return HttpNotFound();
            }
            return View(dayNote);
        }

        // GET: DayNote/Create
        public ActionResult Create()
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
            { ViewData["UserName"] = User.Identity.Name; }

            DayNote model = new DayNote();
            
            model.AvailableDiaries = db.tblDiary.ToList(); //Get a List of diaries
            model.DefaultDiaries = db.tblDiary.Where(r => r.D_DiaryName == "All").ToList();  // Get Default Values for diary
            model.DN_Start_Date = DateTime.Now;
            model.DN_End_Date = DateTime.Now;
            model.DN_BackColor = "White";
            model.DN_TextColor = "Black";

            // pass the view model to the view
            return View(model);
            
        }

        // POST: DayNote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DN_ID,DN_Start_Date,DN_End_Date,DN_BackColor,DN_TextColor,DN_Note,DN_All_Diaries,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Deleted,AvailableDiaries,DefaultDiaries,SubmittedDiaries,IsActive")] DayNote dayNote)
        {
            if (ModelState.IsValid)
            {

                @ViewData["ErrorMessage"] = "";

                if (dayNote.DN_Start_Date == null || dayNote.DN_End_Date == null )
                {
                    @ViewData["ErrorMessage"] = "Please enter valid dates.";
                    dayNote.AvailableDiaries = db.tblDiary.ToList();
                    dayNote.DefaultDiaries = db.tblDiary.Where(r => r.D_DiaryName == "All").ToList();
                    return View(dayNote);
                }

                if (IsNewDate(dayNote) == false)
                {
                    @ViewData["ErrorMessage"] = "Listing exists for the dates entered. Please re-check your entry.";
                    dayNote.AvailableDiaries = db.tblDiary.ToList();
                    dayNote.DefaultDiaries = db.tblDiary.Where(r => r.D_DiaryName == "All").ToList();
                    return View(dayNote);
                }
               
                dayNote.CreatedBy = User.Identity.Name;
                dayNote.CreatedOn = DateTime.Now;
                dayNote.ModifiedBy = User.Identity.Name;
                dayNote.ModifiedOn = DateTime.Now;
                dayNote.Deleted = false;
                db.tblDayNote.Add(dayNote);
                await db.SaveChangesAsync();

                var dModel = db.tblDNApplyToList.Where(r => r.DN_ID == dayNote.DN_ID).ToList();
                foreach (var item in dModel)
                {
                    db.tblDNApplyToList.Remove(item);
                    db.SaveChanges();

                }

                foreach (var item in dayNote.SubmittedDiaries)
                {
                    DN_ApplyToList infoLog = new DN_ApplyToList
                    {
                        DN_ID = dayNote.DN_ID,
                        D_id = Convert.ToInt32(item),
                        D_DiaryName = GetDiaryName(Convert.ToInt32(item)),
                        CreatedBy = User.Identity.Name,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = User.Identity.Name,
                        ModifiedOn = DateTime.Now
                    };


                    // Save log information
                    db.Entry(infoLog).State = EntityState.Added;
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }

            //DayNote model1 = new DayNote();
            dayNote.AvailableDiaries = db.tblDiary.ToList();
            dayNote.DefaultDiaries = db.tblDiary.Where(r => r.D_DiaryName == "All").ToList();
            //model.DN_Start_Date = DateTime.Now;
            //model.DN_End_Date = DateTime.Now;
            //model.DN_BackColor = "White";
            //model.DN_TextColor = "Black";

            // pass the view model to the view
            return View(dayNote);
            
        }

        //Returns the diary name based on the diary id
        private string GetDiaryName(int d_id)
        {

            string result="";

            var model = db.tblDiary.Where(r => r.D_id == d_id)
                .ToList();

            result = model.Select(r => r.D_DiaryName).FirstOrDefault();

            return result;
        }

        //Check if day note exits
        private bool IsNewDate(DayNote model)
        {
            bool returnValue = false;

            var recCount = db.tblDayNote
                .Where(r => r.IsActive == true && (r.DN_Start_Date >= model.DN_Start_Date && r.DN_End_Date <= model.DN_End_Date) && r.Deleted == false)
            .ToList();

            int totalRowsCount = recCount.Count();

            if (totalRowsCount == 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        // GET: DayNote/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            { ViewData["UserName"] = User.Identity.Name; }

            if (id == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Day Note not found, please contact administrator." });
            }
            DayNote dayNote = await db.tblDayNote.FindAsync(id);
            if (dayNote == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Day Note not found, please contact administrator." });
            }

            dayNote.AvailableDiaries = db.tblDiary.ToList(); //Get a List of editable diaries
            dayNote.DefaultEditableDiaries = db.tblDNApplyToList.Where(r => r.DN_ID == id).ToList();  // Get Default Values for editable diary

            // pass the view model to the view
            return View(dayNote);
        }

        // POST: DayNote/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DN_ID,DN_Start_Date,DN_End_Date,DN_BackColor,DN_TextColor,DN_Note,DN_All_Diaries,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Deleted,AvailableDiaries,DefaultDiaries,SubmittedDiaries,IsActive")] DayNote dayNote)
        {

            if (ModelState.IsValid)
            {

                @ViewData["ErrorMessage"] = "";

                dayNote.ModifiedBy = User.Identity.Name;
                dayNote.ModifiedOn = DateTime.Now;
                dayNote.Deleted = false;
                db.Entry(dayNote).State = EntityState.Modified;
                await db.SaveChangesAsync();
                
                var dModel = db.tblDNApplyToList.Where(r => r.DN_ID == dayNote.DN_ID).ToList();
                foreach (var item in dModel)
                {
                    db.tblDNApplyToList.Remove(item);
                    db.SaveChanges();

                }

                foreach (var item in dayNote.SubmittedDiaries)
                {
                    DN_ApplyToList infoLog = new DN_ApplyToList
                    {
                        DN_ID = dayNote.DN_ID,
                        D_id = Convert.ToInt32(item),
                        D_DiaryName = GetDiaryName(Convert.ToInt32(item)),
                        CreatedBy = User.Identity.Name,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = User.Identity.Name,
                        ModifiedOn = DateTime.Now
                    };

                    // Save log information
                    db.Entry(infoLog).State = EntityState.Added;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            dayNote.EditableDiaries = db.tblDNApplyToList.Where(r => r.DN_ID == dayNote.DN_ID).ToList(); //Get a List of editable diaries
            dayNote.DefaultEditableDiaries = db.tblDNApplyToList.Where(r => r.DN_ID == dayNote.DN_ID).ToList();  // Get Default Values for editable diary

            // pass the view model to the view
            return View(dayNote);

        }

        // GET: DayNote/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
            { ViewData["UserName"] = User.Identity.Name; }


            if (id == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Day Note not found, please contact administrator." });
            }
            DayNote dayNote = await db.tblDayNote.FindAsync(id);
            if (dayNote == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Day Note not found, please contact administrator." });
            }
            dayNote.AvailableDiaries = db.tblDiary.ToList(); //Get a List of editable diaries
            dayNote.DefaultEditableDiaries = db.tblDNApplyToList.Where(r => r.DN_ID == id).ToList();  // Get Default Values for editable diary
            return View(dayNote);
        }

        // POST: DayNote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            DayNote dayNote = await db.tblDayNote.FindAsync(id); 
            
            dayNote.ModifiedBy = User.Identity.Name;
            dayNote.ModifiedOn = DateTime.Now;
            dayNote.Deleted = true;
            db.Entry(dayNote).State = EntityState.Modified;
            await db.SaveChangesAsync();

            //var dModel = db.tblDNApplyToList.Where(r => r.DN_ID == dayNote.DN_ID)
            //    .Select(r => new DN_ApplyToList  { D_id = r.D_id, DN_ID = r.DN_ID, DNA_ID = r.DNA_ID, D_DiaryName = r.D_DiaryName, CreatedBy = r.CreatedBy, CreatedOn = r.CreatedOn, ModifiedBy = r.ModifiedBy, ModifiedOn=r.ModifiedOn, Deleted = true });

            var dModel = db.tblDNApplyToList.Where(r => r.DN_ID == dayNote.DN_ID).ToList();
            foreach (var item in dModel)
            {
                DN_ApplyToList DNApplyList = await db.tblDNApplyToList.FindAsync(id); 
                item.Deleted = true;
                db.Entry(DNApplyList).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
