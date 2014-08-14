using EmpManagement.DAL;
using EmpManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EmpManagement.Controllers
{
    public class DeptController : Controller
    {
        private EmpContext db = new EmpContext();
       
        public ActionResult DIndex()
        {
            var e = from m in db.Dept
                    select m;

            return View(e);
        }
        public ActionResult DCreate()
        {
            return View();
        }

        // Post Empmanagement/Ecreate
        [HttpPost]
        // Prevents From Cross request site forgery 
        [ValidateAntiForgeryToken]
        public ActionResult Dcreate([Bind(Include = "ID,DName")] Dept d)
        {
            if (ModelState.IsValid)
            {
                db.Dept.Add(d);
                db.SaveChanges();
                return RedirectToAction("DIndex");
            }
            return View(d);
        }

        public ActionResult DEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dept e = db.Dept.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }
    

        // It will Update data in entitty.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DEdit([Bind(Include = "ID,DName,")] Dept d)
        {
            if (ModelState.IsValid)
            {
                db.Entry(d).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DIndex");
            }
            return View(d);
        }

        // Display details of the Employee

        public ActionResult DDetails(String DName)
        {
            var DeptList = new List<String>();
            var Deptdetails = from m in db.emp
                              orderby m.DeptID
                              select m.DeptID;

            //DeptList.AddRange(Deptdetails.Distinct());
            ViewBag.DName = new SelectList(DeptList);
        }
        public ActionResult DDetails(int? id)
        {

          
                     

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dept e = db.Dept.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        
        }

        public ActionResult DDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dept e = db.Dept.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DDelete(int id)
        {
            Dept e = db.Dept.Find(id);
            db.Dept.Remove(e);
            db.SaveChanges();
            return RedirectToAction("DIndex");
        }


    }
}