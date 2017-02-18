using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Data.Services
{
    public interface IFilePathService
    {
        string GetFilePath();
    }

    public class FilePathService : IFilePathService
    {
        OpenFileDialog openFileDialog;

        public FilePathService()
        {
            openFileDialog = new OpenFileDialog();
        }

        public string GetFilePath()
        {
            openFileDialog.ShowDialog();            
            return openFileDialog.FileName;
        }
    }
}
