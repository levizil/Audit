using PositionConfig.Views;
using Prism.Regions;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Audit.Infra;
using Audit.Data.Services;
using PositionConfig.ViewModels;
using Audit.Data;

namespace PositionConfig
{
    public class PositionConfigModule : IModule
    {
        IRegionManager rm;
        IUnityContainer uc;

        public PositionConfigModule(IUnityContainer uc, IRegionManager rm)
        {
            this.rm = rm;
            this.uc = uc;
        }

        public void Initialize()
        {

            uc.RegisterType<IFileSpecsService, FileSpecsService>(new ContainerControlledLifetimeManager());
            uc.RegisterType<IRepositoryFactory, RepositoryFactory>();
            uc.RegisterType<IFileDataService, FileDataService>(new ContainerControlledLifetimeManager());
            uc.RegisterType<ISystemSpecsService, SystemSpecsService>(new ContainerControlledLifetimeManager());
            uc.RegisterType<PositionConfigViewModel>();
            uc.RegisterType<PositionConfigNavigationItemView>();
            uc.RegisterType<PositionConfigView>();

            uc.RegisterTypeForNavigation<PositionConfigView>();

            rm.RegisterViewWithRegion(RegionNames.NavigateRegion, typeof(PositionConfigNavigationItemView));
        }
    }
}
