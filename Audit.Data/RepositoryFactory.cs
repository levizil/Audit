using Audit.Data.Entities;
using Audit.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Data
{
    public interface IRepositoryFactory
    {
        IFileDataRepository GetFileDataRepository(FileSpecs fileSpecs);
    }
    public class RepositoryFactory : IRepositoryFactory
    {
        public IFileDataRepository GetFileDataRepository(FileSpecs fileSpecs)
        {
            return new FileDataRepository(fileSpecs);
        }
    }
}
