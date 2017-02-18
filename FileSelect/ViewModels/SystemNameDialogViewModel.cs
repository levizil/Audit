using Prism.Mvvm;
using System;

namespace FileSelect.ViewModels
{
    public class SystemNameDialogViewModel : BindableBase
    {
        private string filePath;
        
        public string FilePath
        {
            get { return filePath; }
            set { SetProperty(ref filePath, value); }
        }
        
    }
}
