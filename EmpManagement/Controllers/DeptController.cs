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
       
        public ActionResult DIndex(String DeptName)
        {
            var DeptLst = new List<string>();

            var DeptQry = from d in db.Dept
                           orderby d.ID
                           select d.DName;

            DeptLst.AddRange(DeptQry.Distinct());
            ViewBag.DeptName = new SelectList(DeptLst);

         
            var Department = from m in db.Dept
                    select m;
          
            if (!string.IsNullOrEmpty(DeptName))
            {
                Department = Department.Where(x => x.DName == DeptName);
            }
                                
            return View(Department);
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
            var depts = db.Dept.ToList();
            SelectList deptlist1 = new SelectList(depts, "ID", "DName");
            ViewBag.DeptList = deptlist1;

                //foreach (var item in depts)
                //{
                //    if (item.DName == d.DName)
                //    {
                //        String msg = "Department already exist";
                //        ViewData.Model = msg;
                //        return ViewBag(msg);
                //    }
                //}
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

        public ActionResult DDetails(int? id,Dept d1)
        {
             var empdetails = from e in db.emp
                             where e.DeptID == d1.ID
                             select e;
               
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(empdetails);
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