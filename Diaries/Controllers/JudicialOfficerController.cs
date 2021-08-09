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
using System.DirectoryServices;
using System.Configuration;
using System.DirectoryServices.AccountManagement;

namespace Diaries.Controllers
{
    public class JudicialOfficerController : Controller
    {
        private DiariesDB db = new DiariesDB();

        // GET: judicialOfficer
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

            return View(await db.tblJudicialOfficer.Where(d => d.Deleted == false).OrderBy(r => r.JO_Name).ToListAsync());
        }

        // GET: judicialOfficer/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JudicialOfficer judicialOfficer = await db.tblJudicialOfficer.FindAsync(id);
            if (judicialOfficer == null)
            {
                return HttpNotFound();
            }
            return View(judicialOfficer);
        }

        // GET: judicialOfficer/Create
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

            JudicialOfficer model = new JudicialOfficer();
            return View(model);
        }

        private bool IfJOExists(string joName)
        {
            bool returnValue = false;

            var recCount = db.tblJudicialOfficer
                .Where(r => r.JO_Name == joName && r.Deleted == false)
            .ToList();

            int totalRowsCount = recCount.Count();

            if (totalRowsCount == 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        // POST: judicialOfficer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "JO_ID,JO_Name,JO_BackColor,JO_TextColor")] JudicialOfficer judicialOfficer)
        {
            //Check for 
            if (ModelState.IsValid)
            {
                @ViewData["ErrorMessage"] = "";

                if (IfJOExists(judicialOfficer.JO_Name) == false)
                {
                    @ViewData["ErrorMessage"] = "Judicial Officer exists. Please re-check your entry.";
                    return View(judicialOfficer);
                }

                judicialOfficer.CreatedBy = User.Identity.Name;
                judicialOfficer.CreatedOn = DateTime.Now;
                judicialOfficer.ModifiedBy = User.Identity.Name;
                judicialOfficer.ModifiedOn = DateTime.Now;
                judicialOfficer.Deleted = false;
                db.tblJudicialOfficer.Add(judicialOfficer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(judicialOfficer);
        }

        // GET: judicialOfficer/Edit/5
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
                return RedirectToAction("Alert", "Access", new { alertMessage = "Judicial Officer not found, please contact administrator." });
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JudicialOfficer judicialOfficer = await db.tblJudicialOfficer.FindAsync(id);
            if (judicialOfficer == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Judicial Officer not found, please contact administrator." });
            }
            return View(judicialOfficer);
        }

        // POST: judicialOfficer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "JO_ID,JO_Name,JO_BackColor,JO_TextColor,CreatedBy, CreatedOn")] JudicialOfficer judicialOfficer)
        {
            if (ModelState.IsValid)
            {
                judicialOfficer.ModifiedBy = User.Identity.Name;
                judicialOfficer.ModifiedOn = DateTime.Now;
                judicialOfficer.Deleted = false;
                db.Entry(judicialOfficer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(judicialOfficer);
        }

        // GET: judicialOfficer/Delete/5
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
                return RedirectToAction("Alert", "Access", new { alertMessage = "Judicial Officer not found, please contact administrator." });
            }
            JudicialOfficer judicialOfficer = await db.tblJudicialOfficer.FindAsync(id);
            if (judicialOfficer == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Judicial Officer not found, please contact administrator." });
            }
            return View(judicialOfficer);
        }

        // POST: judicialOfficer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            JudicialOfficerListing judicialOfficerListing = await db.tblJudicialOfficerListing.FindAsync(id);
            //db.tblJudicialOfficerListing.Remove(judicialOfficerListing);
            judicialOfficerListing.ModifiedBy = User.Identity.Name;
            judicialOfficerListing.ModifiedOn = System.DateTime.Now;
            judicialOfficerListing.Deleted = true;
            db.Entry(judicialOfficerListing).State = EntityState.Modified;
            await db.SaveChangesAsync();

            List<JudicialOfficerListing> infoLog = db.tblJudicialOfficerListing
                             .Where(r => r.JO_ID == id).ToList();

            if (infoLog != null)
            {

                foreach (JudicialOfficerListing jol in infoLog)
                {
                    jol.Deleted = true;
                    jol.ModifiedBy = User.Identity.Name;
                    jol.ModifiedOn = DateTime.Now;
                    db.Entry(jol).State = EntityState.Modified;
                }
            };
            // Save listing information
            db.SaveChanges();


            JudicialOfficer judicialOfficer = await db.tblJudicialOfficer.FindAsync(id);
            judicialOfficer.ModifiedBy = User.Identity.Name;
            judicialOfficer.ModifiedOn = System.DateTime.Now;
            judicialOfficer.Deleted = true;
            db.Entry(judicialOfficer).State = EntityState.Modified;
            
            //db.tblJudicialOfficer.Remove(judicialOfficer);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public static DirectoryEntry GetDirectoryEntry()
        {
            DirectoryEntry de = new DirectoryEntry();
            de.Path = ConfigurationManager.AppSettings["ADConnection"];
            de.AuthenticationType = AuthenticationTypes.Secure;
            return de;
        }


        public JsonResult getUsers(string term)
        {

            DirectoryEntry de = GetDirectoryEntry();
            DirectorySearcher deSearch = new DirectorySearcher();
            List<string> groupMembers = new List<string>();

            de.Password = ConfigurationManager.AppSettings["ADConnectionPassword"];
            de.Username = ConfigurationManager.AppSettings["ADConnectionUserName"];

            deSearch.SearchRoot = de;
            deSearch.Filter = "(&(objectClass=user) (cn=" + "*" + "))";

            foreach (SearchResult sr in deSearch.FindAll())
            {
                foreach (string str in sr.Properties["name"])
                {

                    PrincipalContext ctx = new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["ADDomain"], ConfigurationManager.AppSettings["ADConnectionUserName"], ConfigurationManager.AppSettings["ADConnectionPassword"]);
                    UserPrincipal u = UserPrincipal.FindByIdentity(ctx, str);
                    if (u.EmailAddress != null)
                    { groupMembers.Add(str); }

                }
            }

            return Json(groupMembers, JsonRequestBehavior.AllowGet);
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
