using PositionConfig.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PositionConfig.Views
{
    /// <summary>
    /// Interaction logic for PositionConfigTabItemView.xaml
    /// </summary>
    public partial class PositionConfigTabItemView : UserControl
    {
        public PositionConfigTabItemView(PositionConfigTabItemViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public PositionConfigTabItemViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
