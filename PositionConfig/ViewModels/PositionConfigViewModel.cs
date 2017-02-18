using Audit.Data.Entities;
using Audit.Data.Services;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using PositionConfig.Views;
using System.Data;
using System.Windows.Input;
using Prism.Commands;

namespace PositionConfig.ViewModels
{
    public class PositionConfigViewModel : BindableBase
    {
        IFileSpecsService fss;
        IFileDataService fds;
        ISystemSpecsService sss;
        IRegionManager rm;

        IExportService es = new ExportService();

        
        private ObservableCollection<FileSpecs> lfs;
        ObservableCollection<KeyValuePair<string, PositionConfigTabItemView>> tabViewList;


        public PositionConfigViewModel(IFileSpecsService fileSpecsService, IFileDataService fileDataService, ISystemSpecsService systemSpecsService, IRegionManager regionManager)
        {
            fss = fileSpecsService;
            fds = fileDataService;
            rm = regionManager;
            sss = systemSpecsService;
            ExportCommand = new DelegateCommand(Export);
            Initialize();            
        }

        public ICommand ExportCommand { get; set; }

        public ObservableCollection<KeyValuePair<string, PositionConfigTabItemView>> TabViewList
        {
            get { return tabViewList; }
            set { SetProperty(ref tabViewList, value); }
        }

        private void Initialize()
        {
            tabViewList = new ObservableCollection<KeyValuePair<string, PositionConfigTabItemView>>();
            lfs = fss.GetSpecs();
            lfs.CollectionChanged += lfsChanged;
            FileData tempData;
            PositionConfigTabItemViewModel tempViewModel;
            KeyValuePair<string, PositionConfigTabItemView> ksp;

            foreach (FileSpecs fs in lfs)
            {
                fds.LoadData(fs);
                tempData = fds.GetData(fs.Name);
                tempViewModel = new PositionConfigTabItemViewModel(sss.AddSpecs(tempData));
                ksp = new KeyValuePair<string, PositionConfigTabItemView>(
                        fs.Name,
                        new PositionConfigTabItemView(tempViewModel)
                    );
                tabViewList.Add(ksp);
            }
        }

        private void lfsChanged(object o, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddToList(e.NewItems);
                    break;
            }
        }

        private void AddToList(IList newItems)
        {
            List<FileSpecs> fsList = new List<FileSpecs>();
            foreach(var i in newItems)
            {
                fsList.Add((FileSpecs)i);
            }
            foreach(FileSpecs fs in fsList)
            {
                fds.LoadData(fs);
                TabViewList.Add(
                        new KeyValuePair<string, PositionConfigTabItemView>(
                            fs.Name,
                            new PositionConfigTabItemView(
                                new PositionConfigTabItemViewModel(
                                    sss.AddSpecs(fds.GetData(fs.Name))
                                )
                            )
                        )
                    );
            }
        }

        private void Export()
        {
            List<SystemInfo> systemInfoList = sss.GetSysInfo();

            es.Export(systemInfoList);
        }
    }
}
