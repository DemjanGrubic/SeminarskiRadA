using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TotalCommander
{
    class DirectoryList : INotifyPropertyChanged
    {
        private const string defaultDirectory = @"C:\";

        private List<FileSystemInfo> directoryEntries = new List<FileSystemInfo>();
        private ObservableCollection<string> shownEntriesNames = new ObservableCollection<string>();

        public List<FileSystemInfo> DirectoryEntries
        {
            get { return directoryEntries; }
            set { directoryEntries = value; }
        }

        public ObservableCollection<string> ShownEntriesName
        {
            get { return shownEntriesNames; }
            set { shownEntriesNames = value; }
        }

        private string directoryPath;
        public string DirectoryPath
        {
            get { return directoryPath; }
            set { SetField(ref directoryPath, value, "DirectoryPath"); }
        }

        public DirectoryList()
        {
            DirectoryPath = defaultDirectory;
            UpdateDirectoryEntries(DirectoryPath);
        }

        public DirectoryList(string directoryPath)
        {
            DirectoryPath = directoryPath;
            UpdateDirectoryEntries(DirectoryPath);
        }

        public void Refresh()
        {
            UpdateDirectoryEntries(DirectoryPath);
        }

        public void UpdateDirectoryEntries(string directoryPath)
        {
            DirectoryPath = directoryPath;

            directoryEntries.Clear();
            foreach (var folder in Directory.GetDirectories(directoryPath))
            {
                directoryEntries.Add(new DirectoryInfo(folder));
            }

            foreach (var file in Directory.GetFiles(directoryPath))
            {
                directoryEntries.Add(new FileInfo(file));
            }

            // update shown names
            shownEntriesNames.Clear();
            foreach (var item in directoryEntries)
            {
                if (item is DirectoryInfo)
                {
                    shownEntriesNames.Add("[" + item.Name + "]");
                }
                else
                {
                    shownEntriesNames.Add(item.Name);
                }
            }
        }

        internal FileSystemInfo FindFileSystemInfoWithName(string name)
        {
            // folder ???!!!
            if (name.StartsWith("[") && name.EndsWith("]"))
            {
                name = name.Substring(1, name.Length - 2);
            }

            foreach (var item in directoryEntries)
            {
                if (item.Name.Equals(name))
                {
                    return item;
                }
            }

            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
