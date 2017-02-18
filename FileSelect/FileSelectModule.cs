using FileSelect.Views;
using Prism.Modularity;
using Prism.Regions;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Audit.Infra;
using Audit.Data.Services;
using FileSelect.ViewModels;

namespace FileSelect
{
    public class FileSelectModule : IModule
    {
        IRegionManager rm;
        IUnityContainer uc;        

        public FileSelectModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            rm = regionManager;
            uc = unityContainer;
        }
        public void Initialize()
        {
            uc.RegisterType<IFileSpecsService, FileSpecsService>(new ContainerControlledLifetimeManager());
            uc.RegisterType<IFilePathService, FilePathService>(new ContainerControlledLifetimeManager());
            uc.RegisterType<FileSelectViewModel>();
            uc.RegisterType<FileSelectNavigationItemView>();
            uc.RegisterType<FileSelectView>();

            uc.RegisterTypeForNavigation<FileSelectView>();

            rm.RegisterViewWithRegion(RegionNames.NavigateRegion, typeof(FileSelectNavigationItemView));
        }
    }
}
