
using Audit.Data.Entities;
using Audit.Data.Services;
using FileSelect.Views;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;

namespace FileSelect.ViewModels
{
    public class FileSelectViewModel : BindableBase
    {
        private IFileSpecsService fss;
        private IRegionManager rm;
        private IFilePathService fps;

        private DelegateCommand addFileSpecsCommand;
        private DelegateCommand<object> addFilePathCommand;
        private ObservableCollection<FileSpecs> fileSpecs;

        public FileSelectViewModel(IFileSpecsService fileSpecService, IFilePathService filePathService, IRegionManager regionManager)
        {
            fss = fileSpecService;
            fps = filePathService;
            rm = regionManager;
            
            GetSystemNameCommand = new DelegateCommand(AddFileSpecs);
            SystemNameRequest = new InteractionRequest<INotification>();
            addFileSpecsCommand = new DelegateCommand(AddFileSpecs);
            addFilePathCommand = new DelegateCommand<object>(AddFilePath);
            fileSpecs = fss.GetSpecs();
            
        }


        public InteractionRequest<INotification> SystemNameRequest { get; set; }

        public ObservableCollection<FileSpecs> FileSpecs
        {
            get { return fileSpecs; }
            set { SetProperty(ref fileSpecs, value); }
        }

        public ICommand GetSystemNameCommand { get; set; }
        public ICommand AddFilePathCommand { get { return addFilePathCommand; } }
        public ICommand AddFileSpecsCommand
        {
            get { return addFileSpecsCommand; }
        }

        private void AddFileSpecs()
        {
            SystemNameDialogViewModel vm = new SystemNameDialogViewModel();
            FileSpecs fs = new FileSpecs();

            SystemNameRequest.Raise(
                new Notification
                {
                    Title = "Enter Systems Name",
                    Content = new SystemNameDialogView(vm)
                },
                i => fs.Name = vm.FilePath
            );
            fs.Path = fps.GetFilePath();
            fileSpecs.Add(fs);
        }

        private void GetFilePath(object o, RoutedEventArgs e)
        {
            Button b = (Button)e.Source;
            FileSpecs fs = (FileSpecs)b.DataContext;
            fs.Path = fps.GetFilePath();
        }

        private void AddFilePath(object obj)
        {
            FileSpecs fs = (FileSpecs)obj;
            fs.Path = fps.GetFilePath();
        }
    }
}
