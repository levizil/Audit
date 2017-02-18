using Prism.Mvvm;

namespace Audit.Data.Entities
{
    public class FileSpecs : BindableBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string path;
        public string Path
        {
            get { return path; }
            set { SetProperty(ref path, value); }
        }
    }
}
