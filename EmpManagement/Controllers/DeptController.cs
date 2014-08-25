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
       
        // display the home page of the department
        public ActionResult Index(String DeptName)
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

        //Create
        public ActionResult Create()
        {
             return View();
        }

        // Post Empmanagement/Ecreate
        [HttpPost]
        // Prevents From Cross request site forgery 
        [ValidateAntiForgeryToken]
        public ActionResult create( Dept d)
        {
            var depts = db.Dept.ToList();
            SelectList deptlist1 = new SelectList(depts, "ID", "DName");
            ViewBag.DeptList = deptlist1;
       
             ViewBag.dname = d.DName;
             for (var i = 0; i < depts.Count(); i++)  // Check if deptartment already exist in dept entity.
             {
                 if (depts[i].DName == d.DName)
                 {
                    return RedirectToAction("DeptAlreadyExist");
                 }
             }
            
                if (ModelState.IsValid)
                {
                    db.Dept.Add(d);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
             return View(d);
        }
        
        public ActionResult DeptAlreadyExist()
        {
            return View();
        }

        //Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var e = db.Dept.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }

       
        // It will Update data in entitty.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Dept d)
        {
            if (ModelState.IsValid)
            {
                db.Entry(d).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(d);
        }

      
        // Display details of the Employee

        public ActionResult Details(int? id)
        {
            var empdata = db.emp.ToList();
            for (int i = 0; i < empdata.Count; i++) // It will Check wheather employees exist in respective department
            {
                if (empdata[i].DeptID == id)
                {
                    var empdetails = from e in db.emp
                                     where e.DeptID == id
                                     select e;
                    return View(empdetails);
                }
             }
            
                 if (id == null)
                 {
                     return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                 }
                 return RedirectToAction("NoDataFound");
           }

        public ActionResult NoDataFound(int? id)
        {
            return View();
        }

        //Delete
        public ActionResult Delete(int? id)
        {
         /*   var empdetail = db.emp.ToList();
            for (int i = 0; i < empdetail.Count(); i++)
            {
                if (empdetail[i].DeptID == id)
                {
                    var empdata = from emplist in db.emp
                                  select emplist;
                    return View(empdata);
                }
            }*/
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            var deptdata = db.Dept.Find(id);
            if (deptdata == null)
            {
                return HttpNotFound();
            }
            return View(deptdata);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, String submit)
        {
            if (submit.Equals("Yes"))
            {
                var e = db.Dept.Find(id);
                db.Dept.Remove(e);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


    }
}