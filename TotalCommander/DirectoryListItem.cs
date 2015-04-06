using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TotalCommander
{
    class DirectoryListItem
    {
        public FileSystemInfo FileSystemInfo { get; set; }
        public ImageSource ImageSource { get; set; }

        public DirectoryListItem(string path, bool isDirectory)
        {
            if (isDirectory)
            {
                FileSystemInfo = new DirectoryInfo(path);
                ImageSource = IconExtractor.GetIcon(path, true, true);
            }
            else
            {
                FileSystemInfo = new FileInfo(path);
                ImageSource = IconExtractor.GetIcon(path, true, false);
            }
        }
    }
}
