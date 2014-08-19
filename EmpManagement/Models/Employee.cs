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

        [Required]

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public String Name { get; set; }
        
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public String Designation { get; set; }
        
        [Required]
        public String ContactNo { get; set; }
        
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$)")]
        public string Emailid { get; set; }
        
        // Act as a foriegn key of dept class 
         public int DeptID { get; set; }

        // This is because Emplpoyee has many to 1 relationship with dept

        public virtual Dept department { get; set; }
    }

}