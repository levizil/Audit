using Prism.Regions;
using System;
using System.Windows.Controls;
using Audit.Infra;
using System.Windows;

namespace PositionConfig.Views
{
    /// <summary>
    /// Interaction logic for PositionConfigNavigationItemView.xaml
    /// </summary>
    public partial class PositionConfigNavigationItemView : UserControl
    {

        private static Uri viewUri = new Uri("PositionConfigView", UriKind.Relative);

        private IRegionManager rm;

        public PositionConfigNavigationItemView(IRegionManager rm)
        {
            this.rm = rm;

            InitializeComponent();

            IRegion mainRegion = rm.Regions[RegionNames.MainRegion];
            if (mainRegion == null && mainRegion.NavigationService != null)
            {
                mainRegion.NavigationService.Navigated += Mainregion_Navigated;
            }
        }

        private void Mainregion_Navigated(object sender, RegionNavigationEventArgs e)
        {
            UpdateNavigationButtonState(e.Uri);
        }

        private void UpdateNavigationButtonState(Uri uri)
        {
            /*
             * Todo:
             * 
             * Toggle button from red to green?
             * 
             * 
            */
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            rm.RequestNavigate(RegionNames.MainRegion, viewUri);
        }        
    }
}
