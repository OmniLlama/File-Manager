using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FileManager
{
    class FolderInfo
    {
        string name;
        List<string> tags;
        StorageFolder stFolder;
        public FolderInfo(string n, List<string> tgs)
        {
            name = n;
            tags = tgs;
        }
    }
    class FileInfo
    {
        string name;
        List<string> tags;
        StorageFile stFile;
        public FileInfo(string n, List<string> tgs)
        {
            name = n;
            tags = tgs;
        }
    }
}
