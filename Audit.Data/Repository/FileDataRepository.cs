using Audit.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace Audit.Data.Repository
{
    public interface IFileDataRepository
    {
        FileData GetAllData();
    }
    public class FileDataRepository : IFileDataRepository
    {
        private FileData fd;

        public FileDataRepository(FileSpecs fileSpecs)
        {
            getContent(fileSpecs);
        }
        public FileData GetAllData()
        {
            return fd;
        }
        private void getContent(FileSpecs fs)
        {
            string[] fileParts = fs.Path.Split('.');
            string ext = fileParts[fileParts.Length - 1];

            switch (ext)
            {
                case "csv":
                case "txt":
                    getCSVContent(fs);
                    break;
                default: //excel file
                    getXLContent(fs);
                    break;
            }
        }

        private void getCSVContent(FileSpecs fs)
        {
            string[] fileContent;
            List<string> fileRow;
            FileData fd = new FileData(fs.Name);
            int row, col = 0;

            fileContent = System.IO.File.ReadAllLines(fs.Path);
            row = fileContent.Length;

            if (row > 0)
            {
                col = fileContent[0].Split(',').Length;
            }

            for (int r = 0; r < row; r++)
            {
                fileRow = fileContent[r].Split(',').ToList<string>();
                fd.Add(fileRow);
            }
        }

        private void getXLContent(FileSpecs fs)
        {
            //TODO: try blocks for error handling
            Excel.Application xlApp = new Excel.Application();
            Excel.Worksheet wkSht = xlApp.Workbooks.Open(fs.Path).Worksheets[1];
            Excel.Range ur = wkSht.UsedRange;
            int columnEnd = ur.Columns.Count + 1;
            int rowEnd = ur.Rows.Count + 1;
            //Console.WriteLine("columEnd: " + columnEnd);
            //Console.WriteLine("rowEnd: " + rowEnd);
            Excel.Range end = wkSht.Cells[rowEnd, columnEnd];
            ur = wkSht.Range["A1", end];
            List<string> line = new List<string>();
            fd = new FileData(fs.Name);
            for (int row = 1; row < ur.Rows.Count; row++)
            {
                line = new List<string>();
                for (int col = 1; col < ur.Columns.Count; col++)
                {
                    //Console.WriteLine("Row: " + row);
                    //Console.WriteLine("Col: " + col);
                    line.Add(Convert.ToString(
                            (wkSht.Cells[row, col] as Excel.Range).Value2
                        ));
                    //Console.WriteLine("Add: " + line[col - 1]);
                }
                fd.Add(line);
            }
            xlApp.Quit();
        }
    }
}
