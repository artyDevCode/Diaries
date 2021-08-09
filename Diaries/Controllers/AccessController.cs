using Diaries.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Diaries.Controllers
{
    public class AccessController : Controller
    {
        private DiariesDB db = new DiariesDB();
        //
        // GET: /Access/
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

            var model = db.tblAccess
                .Where(r => r.AccessGroup.ToLower() != "system admin").OrderBy(m => m.UserName)
            .ToList();

             var modelsorted = model.OrderBy(r => r.AccessGroup).ThenBy(e => e.UserName).ToList();

            return View(modelsorted);
        }


        // GET: /Access/Create
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
            if (currentUser != null)
                ViewData["UserName"] = currentUser.UserName;
            else
            { ViewData["UserName"] = User.Identity.Name; }

            string[] AccessGroups = ConfigurationManager.AppSettings.AllKeys
                                         .Where(key => key.Contains("Group"))
                                         .Select(key => ConfigurationManager.AppSettings[key])
                                         .ToArray();

            SelectList AGNames = new SelectList(AccessGroups);
            ViewData["AGNames"] = AGNames;

            Access model = new Access();

            return View(model);
        }

        // POST: /Access/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AccessGroup,UserId,UserName")] Access access)
        {

            if (ModelState.IsValid)
            {

                var accessCount = db.tblAccess
                    .Where(r => r.UserName == access.UserName && r.AccessGroup == access.AccessGroup)
                .ToList();

                int totalRowsCount = accessCount.Count();

                if (totalRowsCount == 0)
                { 
                    access.UserId = GetUserId(access.UserName);
                    db.tblAccess.Add(access);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Alert", "Access", new { alertMessage = "User access for " + access.UserName + " as " + access.AccessGroup + " is already defined." });
                }

            }

            string[] AccessGroupsModel = ConfigurationManager.AppSettings.AllKeys
                                         .Where(key => key.Contains("Group"))
                                         .Select(key => ConfigurationManager.AppSettings[key])
                                         .OrderBy(key => ConfigurationManager.AppSettings[key])
                                         .ToArray();
            SelectList AGNames = new SelectList(AccessGroupsModel);
            ViewData["AGNames"] = AGNames;

            return View(access);
        }

        // GET: /ALRS/Delete/5
        public ActionResult Delete(int? id)
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
            if (currentUser != null)
                ViewData["UserName"] = currentUser.UserName;
            else
            { ViewData["UserName"] = User.Identity.Name; }

            if (id == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Access control not found, contact administrator." });
            }
            Access alrs = db.tblAccess.Find(id);
            if (alrs == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Access control not found, contact administrator." });
            }
            return View(alrs);
        }

        // POST: /ALRS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Access access = db.tblAccess.Find(id);
            db.tblAccess.Remove(access);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Unauthorised(string alertMessage)
        {
            //// Check access levels and pass to view
            int index = User.Identity.Name.IndexOf("\\");
            string user = User.Identity.Name.Substring(index + 1);

            Access currentUser = (from u in db.tblAccess
                               where u.UserId == user
                               select u).FirstOrDefault();


            if (currentUser != null)
                ViewData["UserName"] = currentUser.UserName;
            else
            { ViewData["UserName"] = User.Identity.Name; }

            TempData["alertMessage"] = alertMessage;
            return View("Unauthorised");
        }

        public ActionResult Alert(string alertMessage)
        {
            // Check access levels and pass to view
            int index = User.Identity.Name.IndexOf("\\");
            string user = User.Identity.Name.Substring(index + 1);
            List<Access> AccessGroupsModel = db.tblAccess
                             .Where(r => r.UserId == user)
                             .ToList();

            Access currentUser = (from u in db.tblAccess
                                  where u.UserId == user
                                  select u).FirstOrDefault();


            ViewData["InOwnerRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("owner")).Count() > 0 ? "true" : "false";
            ViewData["InSysAdminRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("system")).Count() > 0 ? "true" : "false";
            ViewData["InReadRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("read")).Count() > 0 ? "true" : "false";
            ViewData["InStandardRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("standard")).Count() > 0 ? "true" : "false";
            ViewData["InMagistratesReadRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("magistrate")).Count() > 0 ? "true" : "false";
            if (currentUser != null)
                ViewData["UserName"] = currentUser.UserName;
            else
            { ViewData["UserName"] = User.Identity.Name; }

            if ((ViewData["InEditRole"] != "true") && (ViewData["InReadRole"] != "true") && (ViewData["InReportRole"] != "true") && (ViewData["InOwnerRole"] != "true"))
            {
                return RedirectToAction("Unauthorised", "Access");
            }

            TempData["alertMessage"] = alertMessage;
            return View();
        }

        public string GetUserId(string user)
        {
            string uid = "";

            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["ADDomain"], ConfigurationManager.AppSettings["ADConnectionUserName"], ConfigurationManager.AppSettings["ADConnectionPassword"]);
            UserPrincipal u = UserPrincipal.FindByIdentity(ctx, user);
            if (u != null)
            {
                uid = u.SamAccountName;
            }
            return uid;
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
    
    }
}