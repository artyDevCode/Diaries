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
    public class NonSittingDayController : Controller
    {
        private DiariesDB db = new DiariesDB();

        // GET: /NonSittingDay/
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

            var model = db.tblNonSittingDay
               .Where(r => r.Deleted == false)
               .Select(r => new NonSittingDdayVM { VM_Active = r.NSD_Active ? "Active" : "Inactive", VM_Year = r.NSD_Date.Year, VM_Date = r.NSD_Date, VM_Reason = r.NSD_Reason, VM_Id = r.NSD_ID })
                .ToList();

            List<NonSittingDdayVM> modelsorted;

            int totalRowsCount = 0;
            int filteredRowsCount = 0;
            string[][] aaData;

            if (param.first_data != null && param.second_data != null)
            {
                List<NonSittingDdayVM> aa = new List<NonSittingDdayVM>();
                foreach (var group in model)
                {
                    var test = group.VM_Active.Replace(" ", "").Trim('-').ToLower();
                    var test1 = group.VM_Year.ToString();
                    if (test.Contains(param.first_data) &&
                        test1.ToString().Contains(param.second_data))
                        aa.Add(group);
                }

                modelsorted = aa.OrderBy(r => r.VM_Active).ThenBy(e => e.VM_Year).ToList();
                aa = null;

                aaData = modelsorted.Select(d => new string[] { d.VM_Active, d.VM_Year.ToString(), d.VM_Date.ToString("dd-MM-yyyy"), d.VM_Reason, d.VM_Id.ToString() }).ToArray();

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

                    List<NonSittingDdayVM> aa = new List<NonSittingDdayVM>();
                    foreach (var group in model)
                    {
                        if (group.VM_Active.ToLower().Contains(param.sSearch.ToLower()) || group.VM_Year.ToString().Contains(param.sSearch.ToLower()) || group.VM_Reason.ToLower().Contains(param.sSearch.ToLower()) || 
                            group.VM_Date.ToString("dd-MM-yyyy").ToLower().Contains(param.sSearch.ToLower()))
                            
                            aa.Add(group);

                    }

                    modelsorted = aa.OrderBy(r => r.VM_Active).ThenBy(e => e.VM_Year).ToList();
                    aa = null;
                    aaData = modelsorted.Select(d => new string[] { d.VM_Active, d.VM_Year.ToString(), d.VM_Date.ToString("dd-MM-yyyy"), d.VM_Reason, d.VM_Id.ToString() }).ToArray();

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

            return View(await db.tblNonSittingDay.ToListAsync());
        }

        // GET: /NonSittingDay/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NonSittingDay nonsittingday = await db.tblNonSittingDay.FindAsync(id);
            if (nonsittingday == null)
            {
                return HttpNotFound();
            }
            return View(nonsittingday);
        }

        // GET: /NonSittingDay/Create
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

            NonSittingDay model = new NonSittingDay();

            model.NSD_Date = DateTime.Now;
            model.NSD_BackColor = "White";
            model.NSD_TextColor = "Black";

            // pass the view model to the view
            return View(model);
        }

        // POST: /NonSittingDay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="NSD_ID,NSD_Date,NSD_BackColor,NSD_TextColor,NSD_Reason,NSD_Active,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Deleted")] NonSittingDay nonsittingday)
        {
            if (ModelState.IsValid)
            {

                @ViewData["ErrorMessage"] = "";

                if (nonsittingday.NSD_Date == null )
                {
                    @ViewData["ErrorMessage"] = "Please enter a valid date.";
                    return View(nonsittingday);
                }

                if (IsNewDate(nonsittingday) == false)
                {
                    @ViewData["ErrorMessage"] = "Non sitting day exists for the date entered. Please re-check your entry.";
                    return View(nonsittingday);
                }

                nonsittingday.CreatedBy = User.Identity.Name;
                nonsittingday.CreatedOn = DateTime.Now;
                nonsittingday.ModifiedBy = User.Identity.Name;
                nonsittingday.ModifiedOn = DateTime.Now;
                nonsittingday.Deleted = false;
                db.tblNonSittingDay.Add(nonsittingday);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // pass the view model to the view
            return View(nonsittingday);

        }

        // GET: /NonSittingDay/Edit/5
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
                return RedirectToAction("Alert", "Access", new { alertMessage = "Non Sitting Day not found, please contact administrator." });
            }
            NonSittingDay nonsittingday = await db.tblNonSittingDay.FindAsync(id);
            if (nonsittingday == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Non Sitting Day not found, please contact administrator." });
            }

            return View(nonsittingday);
        }

        // POST: /NonSittingDay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="NSD_ID,NSD_Date,NSD_BackColor,NSD_TextColor,NSD_Reason,NSD_Active,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Deleted")] NonSittingDay nonsittingday)
        {
            if (ModelState.IsValid)
            {

                @ViewData["ErrorMessage"] = "";

                nonsittingday.ModifiedBy = User.Identity.Name;
                nonsittingday.ModifiedOn = DateTime.Now;
                nonsittingday.Deleted = false;
                db.Entry(nonsittingday).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // pass the view model to the view
            return View(nonsittingday);

        }

        // GET: /NonSittingDay/Delete/5
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
                return RedirectToAction("Alert", "Access", new { alertMessage = "Non Sitting Day not found, please contact administrator." });
            }
            NonSittingDay nonsittingday = await db.tblNonSittingDay.FindAsync(id);
            if (nonsittingday == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Non Sitting Day not found, please contact administrator." });
            }
            return View(nonsittingday);


        }

        // POST: /NonSittingDay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NonSittingDay nonsittingday = await db.tblNonSittingDay.FindAsync(id);
            nonsittingday.ModifiedBy = User.Identity.Name;
            nonsittingday.ModifiedOn = DateTime.Now;
            nonsittingday.Deleted = true;
            db.Entry(nonsittingday).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Check if day note exits
        private bool IsNewDate(NonSittingDay model)
        {
            bool returnValue = false;

            var recCount = db.tblNonSittingDay
                .Where(r => r.NSD_Active == true && r.NSD_Date == model.NSD_Date && r.Deleted == false)
            .ToList();

            int totalRowsCount = recCount.Count();

            if (totalRowsCount == 0)
            {
                returnValue = true;
            }

            return returnValue;
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
