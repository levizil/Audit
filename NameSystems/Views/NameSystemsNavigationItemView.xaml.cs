using Audit.Infra;
using Prism.Regions;
using System;
using System.Windows.Controls;

namespace NameSystems.Views
{
    /// <summary>
    /// Interaction logic for NameSystemsNavigationItemView.xaml
    /// </summary>
    public partial class NameSystemsNavigationItemView : UserControl
    {
        private static Uri viewUri = new Uri("NameSystemsView", UriKind.Relative);

        private IRegionManager rm;

        public NameSystemsNavigationItemView(IRegionManager regionManager)
        {
            rm = regionManager;

            InitializeComponent();

            IRegion mainRegion = rm.Regions[RegionNames.MainRegion];
            if (mainRegion != null && mainRegion.NavigationService != null)
            {
                mainRegion.NavigationService.Navigated += MainRegion_Navigated;
            }
        }

        private void MainRegion_Navigated(object sender, RegionNavigationEventArgs e)
        {
            UpdatedNavigationButtonState(e.Uri);
        }

        private void UpdatedNavigationButtonState(Uri uri)
        {
            /*
             * 
             * Toggle button
             * 
             */
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            rm.RequestNavigate(RegionNames.MainRegion, viewUri);
        }
    }
}
