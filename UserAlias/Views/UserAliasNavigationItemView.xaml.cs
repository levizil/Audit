using Audit.Infra;
using Prism.Regions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UserAlias.Views
{
    /// <summary>
    /// Interaction logic for UserAliasNavigationItemView.xaml
    /// </summary>
    public partial class UserAliasNavigationItemView : UserControl
    {
        private static Uri viewUri = new Uri("UserAliasView", UriKind.Relative);

        private IRegionManager rm;

        public UserAliasNavigationItemView(IRegionManager rm)
        {
            this.rm = rm;

            InitializeComponent();

            IRegion mainRegion = rm.Regions[RegionNames.MainRegion];
            if(mainRegion == null && mainRegion.NavigationService != null)
            {
                mainRegion.NavigationService.Navigated += MainRegion_Navigated;
            }
        }

        private void MainRegion_Navigated(object sender, RegionNavigationEventArgs e)
        {
            UpdateNavigationButtonState(e.Uri);
        }

        private void UpdateNavigationButtonState(Uri uri)
        {
            //todo
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            rm.RequestNavigate(RegionNames.MainRegion, viewUri);
        }
    }
}
