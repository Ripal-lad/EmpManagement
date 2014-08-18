using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpManagement.Models
{
    public class Employee
    {
        public int ID { get; set; }

       
        public String Name { get; set; }
        
       
        public String Designation { get; set; }

        public String ContactNo { get; set; }

     
       
        public string Emailid { get; set; }
        
        // Act as a foriegn key of dept class 
        public int DeptID { get; set; }

        // This is because Emplpoyee has many to 1 relationship with dept

        public virtual Dept department { get; set; }
    }

}