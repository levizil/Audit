using Prism.Unity;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using FileSelect;
using PositionConfig;
using UserAlias;
using Audit.Data.Entities;

using Audit.UI.MainVVM;

namespace Audit.UI
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog mc = (ModuleCatalog)ModuleCatalog;
            mc.AddModule(typeof(FileSelectModule));
            mc.AddModule(typeof(PositionConfigModule));
            //mc.AddModule(typeof(UserAliasModule));
        }
        protected override void ConfigureContainer()
        {
            
            base.ConfigureContainer();
        }

    }
}
