using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileManager
{
  
    internal class FilesClass
    {
        public string FilePath { get; set; } = string.Empty;
        public string FilePathDate { get; set; } = string.Empty;
        public FilesClass() { }

        public FilesClass(string filePath,string filedate)
        {
            FilePath = filePath;
            FilePathDate = filedate;
        }
    }
}
