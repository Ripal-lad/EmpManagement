
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
        SelectListItem Department = new SelectListItem();
        List<SelectListItem> items;
        // GET: /EmpManagement/
        public ActionResult Index()
        {
            return View();
        }
   

        public ActionResult EIndex()
        {
            var e = from m in db.emp
                    select m;

            return View(e);
        }
        public ActionResult ECreate()
        {
             var d = new EmpContext();
            IEnumerable<SelectListItem> items = db.Dept
              .Select(Department => new SelectListItem
              {
                  Value = Department.DName.ToString(),
                  Text = Department.DName
              });
            ViewBag.DName = items;
           
            return View();
       
        }



        // Post Empmanagement/Ecreate
        [HttpPost]
        // Prevents From Cross request site forgery 
        [ValidateAntiForgeryToken]
        public ActionResult Ecreate([Bind(Include = "ID,Name,Designation,ContactNo,Emailid,DeptID")] Employee em)
        {
            var data = db.Dept.ToList().Distinct();
            items = new List<SelectListItem>();
          //  Department = new SelectListItem();
            ViewBag.Dept = new SelectList(db.Dept, "DeptID", "DName", em.DeptID);
            foreach (var t in data)
            {
                Department.Text = t.DName;
                Department.Value = t.ID.ToString();
                items.Add(Department);

                if (Department.Selected)
                    {
                        em.DeptID = int.Parse(Department.Value);
                    }
            }
        
        
          
            //    ViewBag.Dept = new SelectList(db.Dept, "DeptID", "DName", em.DeptID);
                if (ModelState.IsValid)
                {
                    db.emp.Add(em);
                    db.SaveChanges();
                    return RedirectToAction("EIndex");
                }
            
            
            return View(em);
        }
        // Display details of the Employee
        public ActionResult EDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee e = db.emp.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }
        // Redirect to the Edit page
        public ActionResult EEdit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee e = db.emp.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }

        // It will Update data in entitty.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EEdit([Bind(Include = "ID,Name,Designation,ContactNo,Emailid,DeptID")] Employee em)
        {
            if (ModelState.IsValid)
            {
                db.Entry(em).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EIndex");
            }
            return View(em);
        }

        public ActionResult EDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee e = db.emp.Find(id);
            if(e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EDelete(int id)
        {
             Employee e = db.emp.Find(id);
                 db.emp.Remove(e);
                db.SaveChanges();
                return RedirectToAction("EIndex");
        }

        // Department
        // GET: Dept
      
    }
}