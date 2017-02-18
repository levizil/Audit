using FileSelect.ViewModels;
using System.Windows.Controls;

namespace FileSelect.Views
{
    /// <summary>
    /// Interaction logic for SystemNameDialogView.xaml
    /// </summary>
    public partial class SystemNameDialogView : UserControl
    {
        public SystemNameDialogView(SystemNameDialogViewModel systemNameDialogViewModel)
        {
            InitializeComponent();
            ViewModel = systemNameDialogViewModel;
        }

        public SystemNameDialogViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
