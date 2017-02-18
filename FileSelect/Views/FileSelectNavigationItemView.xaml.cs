using Prism.Regions;
using System;
using System.Windows;
using System.Windows.Controls;
using Audit.Infra;

namespace FileSelect.Views
{
    /// <summary>
    /// Interaction logic for FileSelectNavigationItemView.xaml
    /// </summary>
    public partial class FileSelectNavigationItemView : UserControl
    {
        private static Uri viewUri = new Uri("FileSelectView", UriKind.Relative);

        private IRegionManager _rm;

        public FileSelectNavigationItemView(IRegionManager rm)
        {
            _rm = rm;

            InitializeComponent();

            IRegion mainRegion = _rm.Regions[RegionNames.MainRegion];
            if (mainRegion != null && mainRegion.NavigationService != null)
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
            /*
             * Todo:
             * 
             * Toggle button from red to green
             * 
             *                
            */
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _rm.RequestNavigate(RegionNames.MainRegion, viewUri);
        }
    }
}
