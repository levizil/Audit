using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Data.Entities
{
    public class Employee
    {
        public Employee()
        {
            FirstName = "NA";
            LastName = "NA";
            FullName = "NA";
            BadgeNumber = -1;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int BadgeNumber { get; set; }

        public bool isEquivalent(Employee employee)
        {
            if(BadgeNumber != -1)
            {
                if(BadgeNumber == employee.BadgeNumber)
                {
                    return true;
                }
            }

            if (FullName != "NA")
            {
                if(FullName == employee.FullName)
                {
                    return true;
                }
            }

            return false;
        }
        public bool isSimilar(Employee employee)
        {
            //if full contains last|full return true
            if(FullName != "NA" && employee.FullName != "NA")
            {
                if (FullName.Contains(employee.FullName) || employee.FullName.Contains(FullName))
                    return true;
            }

            if(FullName != "NA" && employee.LastName != "NA")
            {
                if (FullName.Contains(employee.LastName))
                    return true;
            }

            if(employee.FullName != "NA" && LastName != "NA")
            {
                if (employee.FullName.Contains(LastName))
                    return true;
            }

            return false;
        }
    }
}
