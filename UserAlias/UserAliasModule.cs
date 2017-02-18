using UserAlias.Views;
using Prism.Regions;
using Prism.Modularity;
using Audit.Infra;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace UserAlias
{
    public class UserAliasModule : IModule
    {
        IRegionManager rm;
        IUnityContainer uc;

        public UserAliasModule(IUnityContainer uc, IRegionManager rm)
        {
            this.rm = rm;
            this.uc = uc;
        }

        public void Initialize()
        {
            uc.RegisterType<UserAliasNavigationItemView>();
            uc.RegisterType<UserAliasView>();

            uc.RegisterTypeForNavigation<UserAliasView>();

            rm.RegisterViewWithRegion(RegionNames.NavigateRegion, typeof(UserAliasNavigationItemView));
        }
    }
}
