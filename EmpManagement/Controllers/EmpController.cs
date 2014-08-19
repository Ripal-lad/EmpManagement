
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmpManagement.DAL;
using EmpManagement.Models;
using System.Net;
using System.Data.Entity;

namespace EmpManagement.Controllers
{
    public class EmpController : Controller
    {
        private EmpContext db = new EmpContext();
      
        //Home page of the Employee
        public ActionResult Index()
        {
            var empdata = db.emp.ToList();
            if (empdata.Count == 0)
            {
                return RedirectToAction("Datanotavailable");
            }
            var e = from m in db.emp
                    select m;

            return View(e);
        }
        // If there is no data in Employee entity.
        public ActionResult Datanotavailable()
        {
            return View();
        }
      

        // Create
        // Add  new employee details in Database
        public ActionResult Create()
        {
            var depts = db.Dept.ToList();
          
            SelectList deptlist1=new SelectList(depts, "ID", "DName"); 
            ViewBag.DeptList = deptlist1;
            if (depts.Count == 0)
            {
                return RedirectToAction("DeptNotavailable");
            }

           return View();
         }

        // If there is no department in dept table.
        public ActionResult DeptNotavailable()
        {
            return View();
        }
    

        // Post Empmanagement/create
        [HttpPost]
        // Prevents From Cross request site forgery 
        [ValidateAntiForgeryToken]
        public ActionResult create( [Bind(Include = "ID,Name,Designation,ContactNo,Emailid,DeptID")]Employee em)
        {
            var depts = db.Dept.ToList();
            SelectList deptlist1 = new SelectList(depts, "ID", "DName");
            ViewBag.DeptList = deptlist1;
                if (ModelState.IsValid)
                {
                    db.emp.Add(em);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }            
            return View(em);
        }

        // Details
        // Display details of the Employee
        public ActionResult Details(int? id)
        {
            var depts = db.Dept.ToList();
            SelectList deptlist1 = new SelectList(depts);
            ViewBag.DeptList = deptlist1;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var e = db.emp.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }

        //Edit
        // Redirect to the Edit page
        public ActionResult Edit(int? id)
        {
            var depts = db.Dept.ToList();
            SelectList deptlist1 = new SelectList(depts, "ID", "DName");
            ViewBag.DeptList = deptlist1;
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var e = db.emp.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }

        // It will Update data in entitty.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Designation,ContactNo,Emailid,DeptID")] Employee em)
        {
            var depts = db.Dept.ToList();
            SelectList deptlist1 = new SelectList(depts, "ID", "DName");
            ViewBag.DeptList = deptlist1;

            if (ModelState.IsValid)
            {
                db.Entry(em).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(em);
        }

        //Delete
        public ActionResult Delete(int? id)
        {
            var depts = db.Dept.ToList();
            SelectList deptlist1 = new SelectList(depts);
            ViewBag.DeptList = deptlist1;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var e = db.emp.Find(id);
            if(e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
             var e = db.emp.Find(id);
                 db.emp.Remove(e);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        
    }
}