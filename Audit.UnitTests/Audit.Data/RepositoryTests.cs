using Audit.Data;
using Audit.Data.Entities;
using Audit.Data.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.UnitTests.Audit.Data
{
    [TestFixture]
    public class FileDataRepositoryFixture
    {
        [Test]
        public void FDRepository_get_data_first_element_equal_csharp()
        {
            FileSpecs fs = new FileSpecs { Name = "sysName", Path = @"C:\Users\leonardebarnes\Documents\AuditTestData1.xlsx" };
            FileDataRepository fdr = new FileDataRepository(fs);
            FileData fd = fdr.GetAllData();

            Assert.IsTrue(fd[0][0] == "csharp");
        }

        [Test]
        public void RepoFactory_get_data_first_element_equal_csharp()
        {
            FileSpecs fs = new FileSpecs { Name = "sysName", Path = @"C:\Users\leonardebarnes\Documents\AuditTestData1.xlsx" };
            var fdr = new RepositoryFactory().GetFileDataRepository(fs);
            var fd = fdr.GetAllData();

            Assert.IsTrue(fd[0][0] == "csharp");
        }
    }
}
