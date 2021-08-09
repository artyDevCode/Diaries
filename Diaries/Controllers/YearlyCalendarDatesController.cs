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
    public class YearlyCalendarDatesController : Controller
    {
        private DiariesDB db = new DiariesDB();

        // GET: /YearlyCalendarDates/
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

            var count = (from result1
                in db.tblYearlyCalendarDates
                         select result1).Count();

            if (count == 0)
            {
                var datesList = new YearlyCalendarDates();

                datesList.CurrentStartDate = Convert.ToDateTime("01-01-" + DateTime.Now.Year);
                datesList.CurrentEndDate = Convert.ToDateTime("31-12-" + DateTime.Now.Year);

                datesList.NextStartDate = Convert.ToDateTime("01-01-" + (DateTime.Now.Year + 1));
                datesList.NextEndDate = Convert.ToDateTime("31-12-" + (DateTime.Now.Year + 1));

                datesList.CreatedBy = "System";
                datesList.CreatedOn = DateTime.Now;
                datesList.ModifiedBy = "System";
                datesList.ModifiedOn = DateTime.Now;

                db.tblYearlyCalendarDates.Add(datesList);
                db.SaveChanges();
            }

            return View(db.tblYearlyCalendarDates.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Calendar_Id,CurrentStartDate,CurrentEndDate,NextStartDate,NextEndDate,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy")] YearlyCalendarDates yearlycalendardates)
        {
            if (ModelState.IsValid)
            {
                yearlycalendardates.ModifiedOn = DateTime.Now;
                yearlycalendardates.ModifiedBy = User.Identity.Name;
                db.Entry(yearlycalendardates).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(yearlycalendardates);
        }

        // GET: /YearlyCalendarDates/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearlyCalendarDates yearlycalendardates = await db.tblYearlyCalendarDates.FindAsync(id);
            if (yearlycalendardates == null)
            {
                return HttpNotFound();
            }
            return View(yearlycalendardates);
        }

        // GET: /YearlyCalendarDates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /YearlyCalendarDates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Calendar_Id,CurrentStatDate,CurrentEndDate,NextStatDate,NextEndDate")] YearlyCalendarDates yearlycalendardates)
        {
            if (ModelState.IsValid)
            {
                db.tblYearlyCalendarDates.Add(yearlycalendardates);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(yearlycalendardates);
        }

        // GET: /YearlyCalendarDates/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearlyCalendarDates yearlycalendardates = await db.tblYearlyCalendarDates.FindAsync(id);
            if (yearlycalendardates == null)
            {
                return HttpNotFound();
            }
            return View(yearlycalendardates);
        }

        // POST: /YearlyCalendarDates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Calendar_Id,CurrentStatDate,CurrentEndDate,NextStatDate,NextEndDate")] YearlyCalendarDates yearlycalendardates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yearlycalendardates).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(yearlycalendardates);
        }

        // GET: /YearlyCalendarDates/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearlyCalendarDates yearlycalendardates = await db.tblYearlyCalendarDates.FindAsync(id);
            if (yearlycalendardates == null)
            {
                return HttpNotFound();
            }
            return View(yearlycalendardates);
        }

        // POST: /YearlyCalendarDates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            YearlyCalendarDates yearlycalendardates = await db.tblYearlyCalendarDates.FindAsync(id);
            db.tblYearlyCalendarDates.Remove(yearlycalendardates);
            await db.SaveChangesAsync();
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
