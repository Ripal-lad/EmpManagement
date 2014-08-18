using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpManagement.Models
{
        public class Dept
        {
            public int  ID { get; set; }
            public String DName { get; set; }

            // for each dept there are multiple Employee (1 -> m) / nabigation Property
            public virtual ICollection<Employee> ecol { get; set; }
            
        }

    
}