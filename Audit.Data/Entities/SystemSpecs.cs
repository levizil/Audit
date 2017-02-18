using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Data.Entities
{
    public class SystemSpecs
    {
        private DataView dataView;
        private List<int> poi;
        public static string[] POIOptions = new string[] { "NA", "First Name", "Last Name", "Full Name", "ID" };

        public SystemSpecs(FileData data)
        {
            FirstLast = false;
            poi = new List<int>();

            Initialize(data);
        }

        public bool FirstLast { get; set; }
        public DataView DataView { get { return dataView; } }
        public List<int> POI { get { return poi; } }

        private void Initialize(FileData fileData)
        {
            DataTable employeeTable = new DataTable();

            DataColumn column;

            for (int count = 0; count < fileData[0].Count; count++)
            {
                poi.Add(0);
                column = new DataColumn();
                column.ColumnName = count.ToString();
                column.DataType = typeof(string);
                employeeTable.Columns.Add(column);
            }

            DataRow row;

            foreach (List<string> fdRow in fileData)
            {
                row = employeeTable.NewRow();
                for (int rowCount = 0; rowCount < fdRow.Count; rowCount++)
                {
                    row[rowCount.ToString()] = fdRow[rowCount];
                }
                employeeTable.Rows.Add(row);
            }

            dataView = employeeTable.AsDataView();
        }
    }
}
