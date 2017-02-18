using Moq;
using NUnit.Framework;
using Audit.Data.Services;
using Prism.Regions;
using FileSelect.ViewModels;
using Audit.Data.Entities;
using System.Collections.ObjectModel;

namespace Audit.UnitTests
{
    [TestFixture]
    public class FileSelectViewModelTestFixture
    {
        [Test]
        public void FSViewModel_created_and_request_FileSpecs_collection_from_IFileSpecService()
        {
            var fssMock = new Mock<IFileSpecsService>();
            bool service_requested = false;
            fssMock
                .Setup(svc => svc.GetSpecs())
                .Callback(() => service_requested = true);

            var vm = new FileSelectViewModel(fssMock.Object, new Mock<IFilePathService>().Object, new Mock<IRegionManager>().Object);

            Assert.IsTrue(service_requested);

        }        
    }

    public class FileTestUtil
    {
        public static IFileSpecsService getFileSpecsServiceWithCollection(ObservableCollection<FileSpecs> oc)
        {
            var fssMock = new Mock<IFileSpecsService>();
            fssMock
                .Setup(svc => svc.GetSpecs())
                .Returns(oc);
            return fssMock.Object;
        }        

        public static IFilePathService getFilePathServiceWithReturnPath(string filePath)
        {
            var fdsMock = new Mock<IFilePathService>();
            fdsMock
                .Setup(svc => svc.GetFilePath())
                .Returns(filePath);
            return fdsMock.Object;
        }
    }
}
