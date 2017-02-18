using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Data.Entities
{
    public class SystemInfo
    {
        private string name;
        private List<Employee> employees;
        private bool FirstLast;

        public SystemInfo(string name, List<Dictionary<string, string>> data, bool firstLast)
        {
            this.name = name;
            FirstLast = firstLast;

            extractEmployees(data);
        }

        public string Name { get { return name; } }
        public List<Employee> Employees { get { return employees; } }

        private void extractEmployees(List<Dictionary<string, string>> data)
        {
            employees = new List<Employee>();
            Employee emp;
            
            int badge = -1;

            foreach (Dictionary<string, string> poiList in data)
            {
                emp = new Employee();
                foreach (KeyValuePair<string, string> poi in poiList)
                {
                        switch (poi.Key)
                        {
                            case "First Name":
                                emp.FirstName = poi.Value;
                                break;
                            case "Last Name":
                                emp.LastName = poi.Value;
                                break;
                            case "Full Name":
                                emp.FullName = poi.Value;
                                break;
                            case "ID":
                                Int32.TryParse(poi.Value, out badge);
                                emp.BadgeNumber = badge;
                                break;
                    }
                }
                filterFullName(emp);
                employees.Add(emp);
            }
        }

        private void filterFullName(Employee emp)
        {
            string[] splitName = null;
            if (emp.FullName != "NA")
            {
                emp.FullName.Trim();
                if (emp.FullName.IndexOf(',') > -1)
                {
                    splitName = emp.FullName.Split(',');
                }
                else if (emp.FullName.IndexOf(' ') > -1)
                {
                    splitName = emp.FullName.Split(' ');
                }
                emp.FullName = FirstLast ? splitName[0].Trim() + splitName[1].Trim() : splitName[1].Trim() + splitName[0].Trim();
            } else if(emp.FirstName != "NA" && emp.LastName != "NA")
            {
                emp.FullName = emp.FirstName.Trim() + emp.LastName.Trim();
            }
        }
    }
}
