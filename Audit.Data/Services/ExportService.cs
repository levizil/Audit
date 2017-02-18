using Audit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace Audit.Data.Services
{
    public interface IExportService
    {
        void Export(List<SystemInfo> systemInfoList);
    }
    public class ExportService : IExportService
    {
        public void Export(List<SystemInfo> systemInfoList)
        {
            List<ExportInfo> exportInfoList = PrepData(systemInfoList);

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWkBook = xlApp.Workbooks.Add();
            int row;
            Excel.Worksheet xlSheet;

            foreach(ExportInfo exportInfo in exportInfoList)
            {
                xlSheet = xlWkBook.Sheets.Add();
                xlSheet.Name = exportInfo.Name;
                row = 0;

                foreach(Employee emp in exportInfo.MissingEmployees)
                {
                    printMissingEmployee(emp, xlSheet, ++row);
                    if (exportInfo.PossibleAlias.ContainsKey(emp))
                    {
                        foreach (Employee empAlias in exportInfo.PossibleAlias[emp])
                        {
                            printEmployeeAlias(empAlias, xlSheet, ++row);
                        }
                    }
                }
                xlSheet.Columns.AutoFit();
            }
            ((Excel.Worksheet)xlWkBook.Worksheets["Sheet1"]).Delete();
            xlApp.Visible = true;
        }
        private List<ExportInfo> PrepData(List<SystemInfo> systemInfoList)
        {
            var hrInfoQuery = from si
                              in systemInfoList
                              where si.Name == "HR"
                              select si;

            SystemInfo hrInfo = hrInfoQuery.ToList()[0];

            var nonHRInfoQuery = from si
                                 in systemInfoList
                                 where si.Name != "HR"
                                 select si;

            List<SystemInfo> systemsInfoList = nonHRInfoQuery.ToList();

            List<ExportInfo> exportInfoList = new List<ExportInfo>();
            ExportInfo exportInfo;

            foreach (SystemInfo sysInfo in systemsInfoList)
            {
                exportInfo = new ExportInfo(sysInfo, hrInfo);
                exportInfoList.Add(exportInfo);
            }
            return exportInfoList;
        }
        private void printMissingEmployee(Employee emp, Excel.Worksheet sht, int row)
        {
            Excel.Range rg = sht.Range["A" + row, "D" + row];
            rg.Interior.Color = ColorTranslator.ToOle(Color.Orange);
            printData(emp, sht, row, 0);
        }
        private void printEmployeeAlias(Employee emp, Excel.Worksheet sht, int row)
        {
            Excel.Range rg = sht.Range["B" + row, "E" + row];
            rg.Interior.Color = ColorTranslator.ToOle(Color.Yellow);
            printData(emp, sht, row, 1);
        }
        private void printData(Employee emp, Excel.Worksheet sht, int row, int offset)
        {
            ((Excel.Range)sht.Cells[row, 1 + offset]).Value2 = emp.FullName;
        }
    }
}
