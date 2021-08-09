﻿using System;
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
    public class CaseOutcomeController : Controller
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

            var model = db.tblCaseOutcome
                .Where(r => r.Deleted == false)
            .ToList();

            var modelsorted = model.OrderBy(r => r.CO_Type).ThenBy(e => e.CO_Name).ToList();

            return View(modelsorted);
        }

        // GET: /CaseOutcome/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseOutcome caseoutcome = await db.tblCaseOutcome.FindAsync(id);
            if (caseoutcome == null)
            {
                return HttpNotFound();
            }
            return View(caseoutcome);
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

            CaseOutcome model = new CaseOutcome();

            var coTypeModel = getTypes();
            SelectList COTypes = new SelectList(coTypeModel);
            ViewData["COTypes"] = COTypes;

            // pass the view model to the view
            return View(model);
        }

        // POST: /CaseOutcome/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="CO_ID,CO_Name,CO_Type,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Deleted")] CaseOutcome caseoutcome)
        {
            if (ModelState.IsValid)
            {

                @ViewData["ErrorMessage"] = "";

                if (IsDuplicate(caseoutcome,"Add") == false)
                {
                    @ViewData["ErrorMessage"] = "Case outcome exists. Please re-check your entry.";
                    var coTypeModel = getTypes();
                    SelectList COTypes = new SelectList(coTypeModel);
                    ViewData["COTypes"] = COTypes;
                    return View(caseoutcome);
                }

                caseoutcome.CreatedBy = User.Identity.Name;
                caseoutcome.CreatedOn = DateTime.Now;
                caseoutcome.ModifiedBy = User.Identity.Name;
                caseoutcome.ModifiedOn = DateTime.Now;
                caseoutcome.Deleted = false;

                db.tblCaseOutcome.Add(caseoutcome);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(caseoutcome);
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
                return RedirectToAction("Alert", "Access", new { alertMessage = "Case Outcome not found, please contact administrator." });
            }
            CaseOutcome caseoutcome = await db.tblCaseOutcome.FindAsync(id);
            if (caseoutcome == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Case Outcome not found, please contact administrator." });
            }

            var selectedItem = db.tblCaseOutcome.Where(r => r.CO_ID == id).Single();
            SelectList COTypes = new SelectList(getTypes(), selectedItem.ToString());
            ViewData["COTypes"] = COTypes;

            // pass the view model to the view
            return View(caseoutcome);
        }

        // POST: /CaseOutcome/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="CO_ID,CO_Name,CO_Type,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Deleted")] CaseOutcome caseoutcome)
        {
            if (ModelState.IsValid)
            {
                @ViewData["ErrorMessage"] = "";

                if (IsDuplicate(caseoutcome, "Edit") == false)
                {
                    @ViewData["ErrorMessage"] = "Case outcome exists. Please re-check your entry.";
                    var coTypeModel = getTypes();
                    SelectList COTypes = new SelectList(coTypeModel);
                    ViewData["COTypes"] = COTypes;
                    return View(caseoutcome);
                }

                caseoutcome.ModifiedBy = User.Identity.Name;
                caseoutcome.ModifiedOn = DateTime.Now;
                caseoutcome.Deleted = false;

                db.Entry(caseoutcome).State = EntityState.Modified; 
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(caseoutcome);
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
                return RedirectToAction("Alert", "Access", new { alertMessage = "Case Outcome not found, please contact administrator." });
            }
            CaseOutcome caseoutcome = await db.tblCaseOutcome.FindAsync(id);
            if (caseoutcome == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Case Outcome not found, please contact administrator." });
            }
            return View(caseoutcome);
        }

        // POST: /CaseOutcome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CaseOutcome caseoutcome = await db.tblCaseOutcome.FindAsync(id);

            caseoutcome.ModifiedBy = User.Identity.Name;
            caseoutcome.ModifiedOn = DateTime.Now;
            caseoutcome.Deleted = true;
            db.Entry(caseoutcome).State = EntityState.Modified;
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
        private bool IsDuplicate(CaseOutcome model, string Flag)
        {
            bool returnValue = false;

            if (Flag == "Add")
            {
                var recCount = db.tblCaseOutcome
                    .Where(r => r.CO_Name == model.CO_Name && r.CO_Type == model.CO_Type && r.Deleted == false)
                .ToList();
                int totalRowsCount = recCount.Count();
                if (totalRowsCount == 0)
                {
                    returnValue = true;
                }
            }
            else if (Flag == "Edit")
            { 
                var recCount = db.tblCaseOutcome
                    .Where(r => r.CO_Name == model.CO_Name && r.CO_Type == model.CO_Type && r.CO_ID != model.CO_ID  && r.Deleted == false)
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
