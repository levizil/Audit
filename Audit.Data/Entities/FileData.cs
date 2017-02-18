using System.Collections.Generic;

namespace Audit.Data.Entities
{
    public class FileData : List<List<string>>
    {
        private string nm;

        public FileData(string name)
        {
            nm = name;
        }

        public string Name
        {
            get { return nm; }
        }
    }
}
