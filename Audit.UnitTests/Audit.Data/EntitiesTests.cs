using Audit.Data.Entities;
using Audit.Data.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Audit.UnitTests.Audit.Data
{
    [TestFixture]
    public class SystemSpecsFixture
    {
        [Test]
        public void SystemSpecs_create_with_filedata_with_two_rows_assert_dataview_count_is_two()
        {
            FileData fd = Util.createData("testData1");
            SystemSpecs ss = new SystemSpecs(fd);
            Assert.AreEqual(ss.DataView.Count, 2, "Count is: " + ss.DataView.Count);
        }

        [TestCase(0,0,ExpectedResult = "User1", TestName = "SystemSpecs_equal_User1_at_0_0")]
        [TestCase(0,1,ExpectedResult = "Last1", TestName = "SystemSpecs_equal_Last1_at_0_1")]
        public string SystemSpecs_return_string_from_row_col(int row, int col)
        {
            FileData fd = Util.createData("testData1");
            SystemSpecs ss = new SystemSpecs(fd);
            var testData = ss.DataView.Table.Rows[row][col];
            return testData.ToString();
        }
    }
    [TestFixture]
    public class EmployeeFixture
    {
        [Test]
        public void Employee_equal()
        {
            Employee emp1, emp2;
            emp1 = new Employee();
            emp2 = new Employee();

            emp2.FullName = emp1.FullName = "User1Last1";
            Assert.IsTrue(emp1.isEquivalent(emp2), "emp1.FullName: " + emp1.FullName + " emp2.FullName: " + emp2.FullName);
            
        }

    }
    [TestFixture]
    public class ExportInfoFixture
    {
        [Test]
        public void ExportInfo_stuff()
        {
            FileData fd1, hr;
            hr = Util.createData("HR");
            fd1 = Util.createData("testData1");

            List<string> t1 = new List<string>();

            t1.Add("User3");
            t1.Add("Last3");
            fd1.Add(t1);

            SystemSpecsService sss = new SystemSpecsService();
            SystemSpecs hrSpecs, fd1Specs;

            hrSpecs = sss.AddSpecs(hr);
            fd1Specs = sss.AddSpecs(fd1);

            hrSpecs.POI[0] = fd1Specs.POI[0] = 1;
            hrSpecs.POI[1] = fd1Specs.POI[1] = 2;
            

            List<SystemInfo> lsi = sss.GetSysInfo();

            var hrQuery = from si
                          in lsi
                          where si.Name == "HR"
                          select si;
            var sysQuery = from si
                           in lsi
                           where si.Name == "testData1"
                           select si;

            SystemInfo hrInfo = hrQuery.ToList()[0];
            SystemInfo fd1Info = sysQuery.ToList()[0];

            ExportInfo exportInfo = new ExportInfo(fd1Info,hrInfo);
            string testData = exportInfo.MissingEmployees[0].FullName;
            Assert.IsTrue(testData == "User3Last3", "fullname: " + testData);
        }
    }


    static class Util
    {

        public static FileData createData(string name)
        {
            FileData fd = new FileData(name);
            List<string> t1, t2;
            t1 = new List<string>();
            t2 = new List<string>();

            t1.Add("User1");
            t1.Add("Last1");
            t2.Add("User2");
            t2.Add("Last2");

            fd.Add(t1);
            fd.Add(t2);

            return fd;
        }
    }
}
