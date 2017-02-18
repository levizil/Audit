using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Data.Entities
{
    public class ExportInfo
    {
        private string name;
        private List<Employee> missingEmployees;
        private Dictionary<Employee, List<Employee>> possibleAlias;

        public ExportInfo(SystemInfo sysInfo, SystemInfo hrInfo)
        {
            name = sysInfo.Name;
            missingEmployees = new List<Employee>();
            possibleAlias = new Dictionary<Employee, List<Employee>>();

            compareInfo(sysInfo, hrInfo);
        }

        public string Name { get { return name; } }
        public List<Employee> MissingEmployees { get { return missingEmployees; } }
        public Dictionary<Employee, List<Employee>> PossibleAlias { get { return possibleAlias; } }

        private void compareInfo(SystemInfo sysInfo, SystemInfo hrInfo)
        {
            //find missing employees
            //find similar employees in hr that kinda match missing employee stats
            bool found = false;
            foreach(Employee sysEmp in sysInfo.Employees)
            {
                found = false;
                foreach(Employee hrEmp in hrInfo.Employees)
                {
                    if (hrEmp.isEquivalent(sysEmp))
                        found = true;
                }
                if (!found)
                    missingEmployees.Add(sysEmp);
            }

            foreach(Employee mEmp in missingEmployees)
            {                
                foreach(Employee hrEmp in hrInfo.Employees)
                {
                    if (mEmp.isSimilar(hrEmp))
                    {
                        if (!possibleAlias.ContainsKey(mEmp))
                        {
                            possibleAlias[mEmp] = new List<Employee>();
                        }
                        possibleAlias[mEmp].Add(hrEmp);
                    }
                }
            }
        }
    }
}
