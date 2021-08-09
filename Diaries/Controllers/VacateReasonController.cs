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
    public class VacateReasonController : Controller
    {
        private DiariesDB db = new DiariesDB();

        // GET: /CaseOutcome/
        public ActionResult Index()
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

            var model = db.tblVacateReason
                .Where(r => r.Deleted == false)
            .ToList();

            var modelsorted = model.OrderBy(r => r.VR_Type).ThenBy(e => e.VR_Name).ToList();

            return View(modelsorted);
        }

        // GET: /CaseOutcome/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VacateReason VacateReason = await db.tblVacateReason.FindAsync(id);
            if (VacateReason == null)
            {
                return HttpNotFound();
            }
            return View(VacateReason);
        }

        // GET: /CaseOutcome/Create
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

            VacateReason model = new VacateReason();

            var vrTypeModel = getTypes();
            SelectList VRTypes = new SelectList(vrTypeModel);
            ViewData["VRTypes"] = VRTypes;

            // pass the view model to the view
            return View(model);
        }

        // POST: /CaseOutcome/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VR_ID,VR_Name,VR_Type,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Deleted")] VacateReason vacatereason)
        {
            if (ModelState.IsValid)
            {

                @ViewData["ErrorMessage"] = "";

                if (IsDuplicate(vacatereason, "Add") == false)
                {
                    @ViewData["ErrorMessage"] = "Vacate Reason exists. Please re-check your entry.";
                    var vrTypeModel = getTypes();
                    SelectList VRTypes = new SelectList(vrTypeModel);
                    ViewData["VRTypes"] = VRTypes;
                    return View(vacatereason);
                }

                vacatereason.CreatedBy = User.Identity.Name;
                vacatereason.CreatedOn = DateTime.Now;
                vacatereason.ModifiedBy = User.Identity.Name;
                vacatereason.ModifiedOn = DateTime.Now;
                vacatereason.Deleted = false;

                db.tblVacateReason.Add(vacatereason);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(vacatereason);
        }

        // GET: /CaseOutcome/Edit/5
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
                return RedirectToAction("Alert", "Access", new { alertMessage = "Vacate Reason not found, please contact administrator." });
            }
            VacateReason vacatereason = await db.tblVacateReason.FindAsync(id);
            if (vacatereason == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Vacate Reason not found, please contact administrator." });
            }

            var selectedItem = db.tblVacateReason.Where(r => r.VR_ID == id).Single();
            SelectList VRTypes = new SelectList(getTypes(), selectedItem.ToString());
            ViewData["VRTypes"] = VRTypes;

            // pass the view model to the view
            return View(vacatereason);
        }

        // POST: /CaseOutcome/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VR_ID,VR_Name,VR_Type,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Deleted")] VacateReason vacatereason)
        {
            if (ModelState.IsValid)
            {
                @ViewData["ErrorMessage"] = "";

                if (IsDuplicate(vacatereason, "Edit") == false)
                {
                    @ViewData["ErrorMessage"] = "Vacate Reason exists. Please re-check your entry.";
                    var vrTypeModel = getTypes();
                    SelectList VRTypes = new SelectList(vrTypeModel);
                    ViewData["VRTypes"] = VRTypes;
                    return View(vacatereason);
                }

                vacatereason.ModifiedBy = User.Identity.Name;
                vacatereason.ModifiedOn = DateTime.Now;
                vacatereason.Deleted = false;

                db.Entry(vacatereason).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(vacatereason);
        }

        // GET: /CaseOutcome/Delete/5
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
                return RedirectToAction("Alert", "Access", new { alertMessage = "Vacate Reason not found, please contact administrator." });
            }
            VacateReason vacatereason = await db.tblVacateReason.FindAsync(id);
            if (vacatereason == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Vacate Reason not found, please contact administrator." });
            }
            return View(vacatereason);
        }

        // POST: /CaseOutcome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VacateReason vacatereason = await db.tblVacateReason.FindAsync(id);

            vacatereason.ModifiedBy = User.Identity.Name;
            vacatereason.ModifiedOn = DateTime.Now;
            vacatereason.Deleted = true;
            db.Entry(vacatereason).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private List<string> getTypes()
        {
            List<string> model = new List<string>();
            model.Add("All");
            model.Add("Civil");
            model.Add("Criminal");
            model.Add("Committals");
            model.Add("Childrens Court");
            model.Add("Intervention Orders");
            model.Add("VOCAT");
            return model;
        }

        //Check if day note exits
        private bool IsDuplicate(VacateReason model, string Flag)
        {
            bool returnValue = false;

            if (Flag == "Add")
            {
                var recCount = db.tblVacateReason
                    .Where(r => r.VR_Name == model.VR_Name && r.VR_Type == model.VR_Type && r.Deleted == false)
                .ToList();
                int totalRowsCount = recCount.Count();
                if (totalRowsCount == 0)
                {
                    returnValue = true;
                }
            }
            else if (Flag == "Edit")
            {
                var recCount = db.tblVacateReason
                    .Where(r => r.VR_Name == model.VR_Name && r.VR_Type == model.VR_Type && r.VR_ID != model.VR_ID && r.Deleted == false)
                .ToList();
                int totalRowsCount = recCount.Count();
                if (totalRowsCount == 0)
                {
                    returnValue = true;
                }
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
