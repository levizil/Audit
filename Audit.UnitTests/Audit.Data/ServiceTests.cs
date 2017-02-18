using Audit.Data;
using Audit.Data.Entities;
using Audit.Data.Repository;
using Audit.Data.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Audit.UnitTests.Audit.Data
{
    [TestFixture]
    public class FileSpecsServiceFixture
    {
        [Test]
        public void FSService_add_FileSpec_inbetween_two_GetSpecs_check_first_item_for_name_HR()
        {
            FileSpecsService fss = new FileSpecsService();
            ObservableCollection<FileSpecs> oc;
            oc = fss.GetSpecs();

            Assert.IsTrue(oc[0].Name == "HR");
        }        
    }

    [TestFixture]
    public class FileDataServiceFixture
    {        
        [Test]
        public void FDService_get_data_first_element_equals_csharp()
        {
            var fdrMock = new Mock<IFileDataRepository>();
            fdrMock
                .Setup(svc => svc.GetAllData())
                .Returns(new FileData("sysName") { new List<string>() { "csharp", "rocks" } });

            var fdrFactoryMock = new Mock<IRepositoryFactory>();
            fdrFactoryMock
                .Setup(fry => fry.GetFileDataRepository(It.IsAny<FileSpecs>()))
                .Returns(fdrMock.Object);

            var fds = new FileDataService(fdrFactoryMock.Object);

            fds.LoadData(new FileSpecs { Name = "sysName" });

            var fdList = fds.GetData("sysName");

            Assert.IsTrue(fdList[0][0] == "csharp");
        }

        [Test]
        public void FDService_get_observable_data_first_element_equals_csharp()
        {
            var fdrMock = new Mock<IFileDataRepository>();
            fdrMock
                .Setup(svc => svc.GetAllData())
                .Returns(new FileData("sysName") { new List<string>() { "csharp", "rocks" } });


            var fdrFactoryMock = new Mock<IRepositoryFactory>();
            fdrFactoryMock
                .Setup(fry => fry.GetFileDataRepository(It.IsAny<FileSpecs>()))
                .Returns(fdrMock.Object);

            var fds = new FileDataService(fdrFactoryMock.Object);

            fds.LoadData(new FileSpecs { Name = "sysName" });

            var fdoList = fds.GetObservableData("sysName");

            Assert.IsTrue(fdoList[0][0] == "csharp");
        }
    }

    [TestFixture]
    public class SystemSpecsServiceFixture
    {
        [Test]
        public void SystemSpecsService_get_first_element_FirstLast_equal_false()
        {
            SystemSpecsService sss = new SystemSpecsService();
            FileData fd = Util.createData("testData");

            SystemSpecs ss = sss.AddSpecs(fd);

            Assert.AreEqual(sss.GetSpecs("testData").FirstLast, false);
        }

        [Test]
        public void SystemSpecsService_addspecs_getSysInfo_get_employee_count_equal_2()
        {
            SystemSpecsService sss = new SystemSpecsService();
            FileData fd = Util.createData("testData");

            SystemSpecs ss = sss.AddSpecs(fd);

            Assert.AreEqual(sss.GetSysInfo()[0].Employees.Count, 2);
        }
        [Test]
        public void SystemSpecsService_Employee_isEquivalent_return_true()
        {
            SystemSpecsService sss = new SystemSpecsService();

            FileData fd1, fd2;
            fd1 = Util.createData("testData1");
            fd2 = Util.createData("testData2");

            SystemSpecs ss1, ss2;
            ss1 = sss.AddSpecs(fd1);
            ss2 = sss.AddSpecs(fd2);

            ss1.POI[0] = 1;
            ss1.POI[1] = 2;

            ss2.POI[0] = 1;
            ss2.POI[1] = 2;

            List<SystemInfo> sysInfo = sss.GetSysInfo();

            Employee emp1, emp2;
            emp1 = sysInfo[0].Employees[0];
            emp2 = sysInfo[1].Employees[0];

            Assert.IsTrue(emp1.isEquivalent(emp2),"emp1.FullName: " + emp1.FullName + " emp2.FullName: " + emp2.FullName);

        }
    }
}
