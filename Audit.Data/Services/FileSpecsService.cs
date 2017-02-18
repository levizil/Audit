using Audit.Data.Entities;
using System.Collections.ObjectModel;

namespace Audit.Data.Services
{
    public interface IFileSpecsService
    {
        ObservableCollection<FileSpecs> GetSpecs();
    }
    public class FileSpecsService : IFileSpecsService
    {
        ObservableCollection<FileSpecs> filesSpecifications;
        public FileSpecsService()
        {
            filesSpecifications = new ObservableCollection<FileSpecs>();
            filesSpecifications.Add(new FileSpecs { Name = "HR" });
        }

        public ObservableCollection<FileSpecs> GetSpecs()
        {
            return filesSpecifications;
        }
    }
}
