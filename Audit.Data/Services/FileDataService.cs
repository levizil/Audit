

using Audit.Data.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using Audit.Data.Repository;

namespace Audit.Data.Services
{
    public interface IFileDataService
    {
        void LoadData(FileSpecs fileSpecs);
        FileData GetData(string sysName);
        ObservableCollection<ObservableCollection<string>> GetObservableData(string sysName);

    }
    public class FileDataService : IFileDataService
    {
        private IRepositoryFactory rf;
        Dictionary<string, IFileDataRepository> fddr;

        public FileDataService(IRepositoryFactory repoFactory)
        {
            rf = repoFactory;
            fddr = new Dictionary<string, IFileDataRepository>();
        }

        public FileData GetData(string sysName)
        {
            return fddr[sysName].GetAllData();
        }

        public void LoadData(FileSpecs fileSpecs)
        {
            fddr[fileSpecs.Name] = rf.GetFileDataRepository(fileSpecs);
        }

        public ObservableCollection<ObservableCollection<string>> GetObservableData(string sysName)
        {
            FileData fd = fddr[sysName].GetAllData();
            ObservableCollection<ObservableCollection<string>> oList = new ObservableCollection<ObservableCollection<string>>();
            foreach(List<string> ls in fd)
            {
                oList.Add(
                    new ObservableCollection<string>(ls)
                    );
            }
            return oList;
        }
    }
}
