using Audit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Data.Services
{
    public interface ISystemSpecsService
    {
        SystemSpecs GetSpecs(string name);
        SystemSpecs AddSpecs(FileData data);
        List<SystemInfo> GetSysInfo();
    }
    public class SystemSpecsService : ISystemSpecsService
    {
        private Dictionary<string, SystemSpecs> systemSpecsDictionary;

        public SystemSpecsService()
        {
            systemSpecsDictionary = new Dictionary<string, SystemSpecs>();
        }

        public SystemSpecs AddSpecs(FileData data)
        {
            SystemSpecs ss = new SystemSpecs(data);
            systemSpecsDictionary[data.Name] = ss;
            return ss;
        }

        public SystemSpecs GetSpecs(string name)
        {
            return systemSpecsDictionary[name];
        }

        public List<SystemInfo> GetSysInfo()
        {
            List<SystemInfo> lsi = new List<SystemInfo>();
            SystemInfo si;

            List<Dictionary<string, string>> rows;
            Dictionary<string, string> row;
            string poi, value;
            DataColumn dataCol;
            DataTable dataTable;
            List<string> DataPOI;

            foreach (var kvm in systemSpecsDictionary)
            {

                dataTable = kvm.Value.DataView.ToTable();
                var DataPOIQuery = from poiInt
                               in kvm.Value.POI
                               select SystemSpecs.POIOptions[poiInt];
                DataPOI = DataPOIQuery.ToList();

                rows = new List<Dictionary<string, string>>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    row = new Dictionary<string, string>();
                    for (int index = 0; index < DataPOI.Count; index++)
                    {
                        poi = DataPOI[index];
                        if (poi.ToLower() != "na")
                        {
                            dataCol = dataTable.Columns[index];

                            value = dataRow[dataCol].ToString();

                            row[poi] = value;
                        }
                    }
                    rows.Add(row);
                }

                si = new SystemInfo(kvm.Key, rows, kvm.Value.FirstLast);

                lsi.Add(si);
            }
            return lsi;
        }
    }
}
