using Audit.Data.Services;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NameSystems.ViewModels
{
    public class NameSystemsViewModel : BindableBase
    {
        private IRegionManager rm;
        private ISystemNamesService snr;

        private ObservableCollection<string> names;

        public NameSystemsViewModel(ISystemNamesService systemNamesService, IRegionManager regionManager)
        {
            rm = regionManager;
            snr = systemNamesService;
            names = new ObservableCollection<string>(snr.Names);
        }

        public ObservableCollection<string> Names
        {
            get { return names; }
            set { SetProperty(ref names, value); }
        }

        
    }
}
