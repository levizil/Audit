using Audit.Data.Entities;
using Prism.Commands;
using Prism.Interactivity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace PositionConfig.ViewModels
{
    public class PositionConfigTabItemViewModel : BindableBase
    {
        #region fields
        private DataView spreadSheet;
        private SystemSpecs systemSpecs;
        #endregion
        #region constructor
        public PositionConfigTabItemViewModel(SystemSpecs sysSpecs)
        {
            ColumnPOI = new ObservableCollection<string>(SystemSpecs.POIOptions);
            //ColumnPOI = new ObservableCollection<string>() { "NA", "Full Name", "ID", "First Name", "Last Name" };
            ComboBoxCommand = new DelegateCommand<SelectionChangedEventArgs>(comboBoxCommand);
            ComboBoxLoadedCommand = new DelegateCommand<RoutedEventArgs>(comboBoxLoadedCommand);
            systemSpecs = sysSpecs;            
            SpreadSheet = sysSpecs.DataView;
        }
        #endregion
        #region commandProperties
        public ICommand ComboBoxCommand { get; set; }
        public ICommand ComboBoxLoadedCommand { get; set; }
        #endregion
        #region dataProperties
        public bool FirstLast {
            get { return systemSpecs.FirstLast; }
            set { systemSpecs.FirstLast = value; }
        }
        public ObservableCollection<string> ColumnPOI { get; set; }
        public DataView SpreadSheet
        {
            get { return spreadSheet; }
            set { SetProperty(ref spreadSheet, value); }
        }
        public List<string> POISelections
        {
            get
            {
                List<string> los = new List<string>();
                foreach(int i in systemSpecs.POI)
                {
                    los.Add(ColumnPOI[i]);
                }
                return los;
            }
        }
        #endregion
        #region delegatesForCommands
        private void comboBoxLoadedCommand(RoutedEventArgs args)
        {
            var comboBox = (ComboBox)args.OriginalSource;
            var header = (DataGridColumnHeader)comboBox.TemplatedParent;
            var index = header.DisplayIndex;
            if (index > -1)
            {
                comboBox.SelectedIndex = systemSpecs.POI[index];
            }

        }
        private void comboBoxCommand(SelectionChangedEventArgs args)
        {
            if (args.AddedItems.Count > 0)
            {
                var selectedItem = args.AddedItems[0].ToString();
                var box = (ComboBox)args.OriginalSource;
                var header = (DataGridColumnHeader)box.TemplatedParent;
                int index = header.DisplayIndex;
                var column = header.Column;
                if (systemSpecs.POI.IndexOf(ColumnPOI.IndexOf(selectedItem)) < 0 && selectedItem != "NA")
                {
                    if(selectedItem == "Full Name")
                    {
                        FirstLast = fullNameSelected();
                    }
                    systemSpecs.POI[index] = ColumnPOI.IndexOf(selectedItem);
                } else
                {

                    if (box.SelectedIndex != 0 && selectedItem != "NA")
                    {
                        MessageBox.Show("Can't duplicate values except NA");
                    }
                    box.SelectedIndex = 0;
                    systemSpecs.POI[index] = 0;
                }
            }
        }
        #endregion
        #region helperMethods
        private bool fullNameSelected()
        {
            MessageBoxResult result = MessageBox.Show("Is the fullname in first then last name order?", "caption", MessageBoxButton.YesNo);
            if (result.ToString().ToLower() == "yes")
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
