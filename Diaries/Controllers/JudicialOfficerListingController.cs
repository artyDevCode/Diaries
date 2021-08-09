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
    public class JudicialOfficerListingController : Controller
    {
        private DiariesDB db = new DiariesDB();

        public class JQueryDataTableParamModel
        {
            public string sEcho { get; set; }
            public string sSearch { get; set; }
            public int iDisplayLength { get; set; }
            public int iDisplayStart { get; set; }
            public string first_data { get; set; }
            public string second_data { get; set; }
        }


        //public JsonResult GetAjaxData(JQueryDataTableParamModel param)
        //{

            //var model = db.tblJudicialOfficerListing
            //   .Where(r => r.JO_ID == JOListingVM)
            //   .Select(r => new ALRSVM { VM_Date = r.JOL_Date, VM_Limit = r.JOL_Limit, VM_Current = r.JOL_Current, VM_Available = r.JOL_Available, VM_Id = r.JOL_ID })
            //    .ToList();

            //List<JOListingVM> modelsorted;

            //int totalRowsCount = 0;
            //int filteredRowsCount = 0;
            //string[][] aaData;


            //if (param.first_data != null && param.second_data != null)
            //{
            //    //throw new Exception("first=" + param.first_data + "second=" + param.second_data);

            //    List<ALRSVM> aa = new List<ALRSVM>();
            //    foreach (var group in model)
            //    {
            //        var test = group.VM_Name.Replace(" ", "").Trim('-').ToLower();
            //        if (test.Contains(param.first_data) &&
            //            group.VM_Status.ToLower().Contains(param.second_data.Trim()))
            //            aa.Add(group);
            //    }

            //    modelsorted = aa.OrderBy(r => r.VM_Name).ThenBy(e => e.VM_Status).ToList();
            //    aa = null;
            //    aaData = modelsorted.Select(d => new string[] { d.VM_Name, d.VM_Status, d.VM_LeaveType, d.VM_StartDate.ToString("dd-MM-yyyy"), d.VM_EndDate.ToString("dd-MM-yyyy"), d.VM_Duration.ToString(), d.VM_Id.ToString() }).ToArray();

            //    totalRowsCount = modelsorted.Count();
            //    filteredRowsCount = modelsorted.Count();

            //    return Json(new
            //    {
            //        sEcho = param.sEcho,
            //        aaData = aaData,
            //        iTotalRecords = Convert.ToInt32(totalRowsCount),
            //        iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount)
            //    }, JsonRequestBehavior.AllowGet);


            //}

            //if (param.sSearch != null)
            //{
            //    if (param.sSearch.Length > 2)
            //    {

            //        List<ALRSVM> aa = new List<ALRSVM>();
            //        foreach (var group in model)
            //        {
            //            if (group.VM_Name.ToLower().Contains(param.sSearch.ToLower()) || group.VM_Status.ToLower().Contains(param.sSearch.ToLower()) ||
            //                group.VM_LeaveType.ToLower().Contains(param.sSearch.ToLower()))

            //                aa.Add(group);

            //        }

            //        modelsorted = aa.OrderBy(r => r.VM_Name).ThenBy(e => e.VM_Status).ToList();
            //        aa = null;
            //        aaData = modelsorted.Select(d => new string[] { d.VM_Name, d.VM_Status, d.VM_LeaveType, d.VM_StartDate.ToString("dd-MM-yyyy"), d.VM_EndDate.ToString("dd-MM-yyyy"), d.VM_Duration.ToString() }).ToArray();

            //        totalRowsCount = modelsorted.Count();
            //        filteredRowsCount = modelsorted.Count();

            //        return Json(new
            //        {
            //            sEcho = param.sEcho,
            //            aaData = aaData,
            //            iTotalRecords = Convert.ToInt32(totalRowsCount),
            //            iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount)
            //        }, JsonRequestBehavior.AllowGet);
            //    }

            //}
            ////}

            //totalRowsCount = model.Count();
            //filteredRowsCount = model.Count();
            //var vv = model.Select(r => new { r.VM_Name, r.VM_Status }).Distinct().OrderBy(r => r.VM_Name).ThenBy(e => e.VM_Status).ToList();

            //aaData = vv.Select(d => new string[] { d.VM_Name, d.VM_Status, "", "", "", "" }).ToArray();

            ////logger2.Debug("return model2=" + totalRowsCount.ToString());

            //return Json(new
            //{
            //    sEcho = param.sEcho,
            //    aaData = aaData,
            //    iTotalRecords = Convert.ToInt32(totalRowsCount),
            //    iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount)
            //}, JsonRequestBehavior.AllowGet);

        //}

        // GET: JudicialOfficerListing
        public async Task<ActionResult> Index(int joid)
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

            //var model = db.tblJudicialOfficerListing
            //   .Where(r => r.JO_ID == joid)
            //   .ToListAsync();

            //var jomodel = from jol in db.tblJudicialOfficerListing
            //              join jo in db.tblJudicialOfficer
            //              on jol.JO_ID equals jo.JO_ID
            //              where jo.JO_ID == joid

            //var jomodel = from jo in db.tblJudicialOfficer
            //              join jol in db.tblJudicialOfficerListing.DefaultIfEmpty()
            //              on jo.JO_ID equals jol.JO_ID
            //              where jo.JO_ID == joid

            //var jomodel1 = from jo in db.tblJudicialOfficer
            //               from jol in db.tblJudicialOfficerListing.DefaultIfEmpty()
            //               where jo.JO_ID == joid, jol.JO_ID == joid


            var jolmodel = db.tblJudicialOfficerListing
               .Where(r => r.JO_ID == joid && r.Deleted == false)
                .ToList();

            //int totalRowsCount = jolmodel.Count();


            if (jolmodel.Count() > 0)
            {
                var jomodel = from jo in db.tblJudicialOfficer
                    join jol in db.tblJudicialOfficerListing on jo.JO_ID equals jol.JO_ID into jold
                    from jol in jold.DefaultIfEmpty()
                    where jo.JO_ID == joid && jol.Deleted ==false
                    orderby jol.JOL_Date ascending

                    select new JOListingVM 
                              {
                                VM_Id = jol.JOL_ID,
                                VM_JoId= jo.JO_ID,
                                VM_JOName = jo.JO_Name,
                                VM_JOBackColor = jo.JO_BackColor,
                                VM_JOTextColor = jo.JO_TextColor,
                                VM_Date = jol.JOL_Date,
                                VM_Limit = jol.JOL_Limit,
                                VM_Current = jol.JOL_Current,
                                VM_Available = jol.JOL_Available,
                                VM_CreatedBy = jol.CreatedBy,
                                VM_CreatedOn = jol.CreatedOn,
                                VM_ModifiedBy = jol.ModifiedBy,
                                VM_ModifiedOn = jol.ModifiedOn
                              };
                return View(await jomodel.ToListAsync());
            }
            else
            {
                var jomodel = from jo in db.tblJudicialOfficer
                              //join jol in db.tblJudicialOfficerListing on jo.JO_ID equals jol.JO_ID into jold
                              //from jol in jold.DefaultIfEmpty()
                              where jo.JO_ID == joid 
                              //orderby jol.JOL_Date ascending

                              select new JOListingVM
                              {
                                  VM_Id = 0,
                                  VM_JoId = jo.JO_ID,
                                  VM_JOName = jo.JO_Name,
                                  VM_JOBackColor = jo.JO_BackColor,
                                  VM_JOTextColor = jo.JO_TextColor,
                                  VM_Date = null,
                                  VM_Limit = null,
                                  VM_Current = null,
                                  VM_Available = null,
                                  VM_CreatedBy = null,
                                  VM_CreatedOn = DateTime.Now,
                                  VM_ModifiedBy = null,
                                  VM_ModifiedOn = DateTime.Now
                              };
                return View(await jomodel.ToListAsync());
            }



    }

        // GET: JudicialOfficerListing/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JudicialOfficerListing judicialOfficerListing = await db.tblJudicialOfficerListing.FindAsync(id);
            if (judicialOfficerListing == null)
            {
                return HttpNotFound();
            }
            return View(judicialOfficerListing);
        }

        // GET: JudicialOfficerListing/Create
        public ActionResult Create(int joid)
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

            var jomodel = from jo in db.tblJudicialOfficer
                          join jol in db.tblJudicialOfficerListing on jo.JO_ID equals jol.JO_ID into jold
                          from jol in jold.DefaultIfEmpty()
                          where jo.JO_ID == joid
                          orderby jol.JOL_Date ascending

                          select new JOListingVM
                          {
                              VM_Id = 0,
                              //VM_Id = jol.JOL_ID,
                              VM_JoId = jo.JO_ID,
                              VM_JOName = jo.JO_Name,
                              VM_JOBackColor = jo.JO_BackColor,
                              VM_JOTextColor = jo.JO_TextColor,
                              VM_Date = DateTime.Now,
                              VM_Limit = 1,
                              VM_Current = 0,
                              VM_Available = 1,
                          };

            return View(jomodel.FirstOrDefault());

        }

        // POST: JudicialOfficerListing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VM_Id,VM_JoId,VM_JOName,VM_JOBackColor,VM_JOTextColor,VM_Date,VM_Limit,VM_Current,VM_Available,VM_Notes")] JOListingVM model)
        {
            if (ModelState.IsValid)
            {
                @ViewData["ErrorMessage"] = "";

                if (model.VM_Date.Value == null)
                {
                    @ViewData["ErrorMessage"] = "Please enter a valid date.";
                    return View(model);
                }

                if (IsNewDate(model) == false)
                {
                    @ViewData["ErrorMessage"] = "Listing exists for the date " + model.VM_Date.Value.ToString("dd-MM-yyyy") + ", please re-check your entry.";
                    return View(model);
                }


                JudicialOfficerListing info = new JudicialOfficerListing
                {
                    JO_ID = model.VM_JoId,
                    JOL_Name = model.VM_JOName,
                    JOL_BackColor = model.VM_JOBackColor,
                    JOL_TextColor = model.VM_JOTextColor,
                    JOL_Date = model.VM_Date.Value,
                    JOL_Limit = model.VM_Limit.Value,
                    JOL_Available = model.VM_Limit.Value,
                    JOL_Current = 0,
                    JOL_Notes = model.VM_Notes,
                    CreatedBy = User.Identity.Name,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = User.Identity.Name,
                    ModifiedOn = DateTime.Now,
                    Deleted = false
                };


                // Save log information
                db.Entry(info).State = EntityState.Added;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { joid = model.VM_JoId });

            }

            return View(model);

        }

        // GET: JudicialOfficerListing/Edit/5
        public async Task<ActionResult> Edit(int? joid)
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

            if (joid == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Judicial Officer Listing not found, please contact administrator." });
            }
            JudicialOfficerListing judicialOfficerListing = await db.tblJudicialOfficerListing.FindAsync(joid);
            if (judicialOfficerListing == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Judicial Officer Listing not found, please contact administrator." });
            }

            var jomodel = from jo in db.tblJudicialOfficer
                          join jol in db.tblJudicialOfficerListing on jo.JO_ID equals jol.JO_ID into jold
                          from jol in jold.DefaultIfEmpty()
                          where jol.JOL_ID == joid
                          select new JOListingVM
                          {
                              VM_Id = jol.JOL_ID,
                              VM_JoId = jo.JO_ID,
                              VM_JOName = jo.JO_Name,
                              VM_JOBackColor = jo.JO_BackColor,
                              VM_JOTextColor = jo.JO_TextColor,
                              VM_Date = jol.JOL_Date,
                              VM_Limit = jol.JOL_Limit,
                              VM_Current = jol.JOL_Current,
                              VM_Available = jol.JOL_Available,
                              VM_Notes = jol.JOL_Notes,
                              VM_CreatedBy = jol.CreatedBy,
                              VM_CreatedOn = jol.CreatedOn,
                              VM_ModifiedBy = jol.ModifiedBy,
                              VM_ModifiedOn = jol.ModifiedOn
                          };

            return View(jomodel.FirstOrDefault());

        }

        // POST: JudicialOfficerListing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VM_Id,VM_JoId,VM_JOName,VM_JOBackColor,VM_JOTextColor,VM_Date,VM_Limit,VM_Current,VM_Available,VM_Notes")] JOListingVM model)
        {
            if (ModelState.IsValid)
            {
                @ViewData["ErrorMessage"] = "";

                if (model.VM_Date.Value == null)
                {
                    @ViewData["ErrorMessage"] = "Please enter a valid date.";
                    return View(model);
                }

                //if (IsNewDate(model) == false)
                //{
                //    @ViewData["ErrorMessage"] = "Listing exists for the date " + model.VM_Date.Value.ToString("dd-MM-yyyy") + ", please re-check your entry.";
                //    return View(model);
                //}


                JudicialOfficerListing info = new JudicialOfficerListing
                {
                    JOL_ID = model.VM_Id,
                    JO_ID = model.VM_JoId,
                    JOL_Name = model.VM_JOName,
                    JOL_BackColor = model.VM_JOBackColor,
                    JOL_TextColor = model.VM_JOTextColor,
                    JOL_Date = model.VM_Date.Value,
                    JOL_Limit = model.VM_Limit.Value,
                    JOL_Available = model.VM_Limit.Value,
                    JOL_Current = 0,
                    JOL_Notes = model.VM_Notes,
                    CreatedBy = User.Identity.Name,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = User.Identity.Name,
                    ModifiedOn = DateTime.Now,
                    Deleted = false
                };


                // Save log information
                db.Entry(info).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { joid = model.VM_JoId });

            }

            return View(model);
        }

        // GET: JudicialOfficerListing/Delete/5
        public async Task<ActionResult> Delete(int joid)
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

            if (joid == 0)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Judicial Officer Listing not found, please contact administrator." });
            }
            JudicialOfficerListing judicialOfficerListing = await db.tblJudicialOfficerListing.FindAsync(joid);
            if (judicialOfficerListing == null)
            {
                return RedirectToAction("Alert", "Access", new { alertMessage = "Judicial Officer Listing not found, please contact administrator." });
            }

            var jomodel = from jo in db.tblJudicialOfficer
                          join jol in db.tblJudicialOfficerListing on jo.JO_ID equals jol.JO_ID into jold
                          from jol in jold.DefaultIfEmpty()
                          where jol.JOL_ID == joid
                          select new JOListingVM
                          {
                              VM_Id = jol.JOL_ID,
                              VM_JoId = jo.JO_ID,
                              VM_JOName = jo.JO_Name,
                              VM_JOBackColor = jo.JO_BackColor,
                              VM_JOTextColor = jo.JO_TextColor,
                              VM_Date = jol.JOL_Date,
                              VM_Limit = jol.JOL_Limit,
                              VM_Current = jol.JOL_Current,
                              VM_Available = jol.JOL_Available,
                              VM_Notes = jol.JOL_Notes,
                              VM_CreatedBy = jol.CreatedBy,
                              VM_CreatedOn = jol.CreatedOn,
                              VM_ModifiedBy = jol.ModifiedBy,
                              VM_ModifiedOn = jol.ModifiedOn
                          };

            return View(jomodel.FirstOrDefault());
        
        }

        // POST: JudicialOfficerListing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int joid)
        {
            JudicialOfficerListing judicialOfficerListing = await db.tblJudicialOfficerListing.FindAsync(joid);
            //db.tblJudicialOfficerListing.Remove(judicialOfficerListing);
            judicialOfficerListing.ModifiedBy = User.Identity.Name;
            judicialOfficerListing.ModifiedOn = System.DateTime.Now;
            judicialOfficerListing.Deleted = true;
            db.Entry(judicialOfficerListing).State = EntityState.Modified; 
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "JudicialOfficerListing", new { joid = judicialOfficerListing.JO_ID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IsNewDate(JOListingVM model)
        {
            bool returnValue=false;

            var recCount = db.tblJudicialOfficerListing
                .Where(r => r.JO_ID == model.VM_JoId && r.JOL_Date == model.VM_Date && r.Deleted == false)
            .ToList();

            int totalRowsCount = recCount.Count();

            if (totalRowsCount == 0)
            {
                returnValue = true;
            }

            return returnValue;
        }
    }
}
