using Audit.Data.Entities;
using Audit.Data.Repository;
using Audit.Data.Services;
using Moq;
using NUnit.Framework;
using PositionConfig.ViewModels;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Audit.UnitTests
{
    [TestFixture]
    public class PositionConfigViewModelFixture
    {
        /*[Test]
        public void PCVM_()
        {
            var ocf = new ObservableCollection<FileSpecs>();
            ocf.Add(new FileSpecs { Name = "AD" });

            IFileSpecsService fss = PCUtil.GetFileSpecsService(ocf);
            IFileDataService fds = PCUtil.GetFileDataService("AD");
            var vm = new PositionConfigViewModel(fss, fds, new Mock<IRegionManager>().Object);

            Assert.IsTrue(vm.FileDataDictionary["AD"][0][0] == "csharp");
        }*/
    }

    public class PCUtil
    {
        public static IFileSpecsService GetFileSpecsService(ObservableCollection<FileSpecs> ocf)
        {
            var fssMock = new Mock<IFileSpecsService>();
            fssMock
                .Setup(svc => svc.GetSpecs())
                .Returns(ocf);
            return fssMock.Object;
        }

        public static IFileDataService GetFileDataService(string sysName)
        {
            var fdsMock = new Mock<IFileDataService>();
            fdsMock
                .Setup(svc => svc.GetData(It.IsAny<string>()))
                .Returns(new FileData(sysName) { new List<string> { "csharp", "rocks" } });
            return fdsMock.Object;
        }
    }
}
