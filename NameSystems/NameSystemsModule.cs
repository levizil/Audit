using Audit.Infra;
using Microsoft.Practices.Unity;
using NameSystems.Views;
using Prism.Regions;
using Prism.Unity;

namespace NameSystems
{
    public class NameSystemsModule
    {
        IRegionManager rm;
        IUnityContainer uc;


        public NameSystemsModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            rm = regionManager;
            uc = unityContainer;
        }
        public void Initialize()
        {
            uc.RegisterType<NameSystemsNavigationItemView>();
            uc.RegisterType<NameSystemsView>();

            uc.RegisterTypeForNavigation<NameSystemsView>();

            rm.RegisterViewWithRegion(RegionNames.NavigateRegion, typeof(NameSystemsNavigationItemView));
        }
    }
}
