using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TotalCommander
{
    class DirectoryList : INotifyPropertyChanged
    {
        private const string defaultDirectory = @"C:\";

        public ObservableCollection<DirectoryListItem> directoryListItems = new ObservableCollection<DirectoryListItem>();

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

            directoryListItems.Clear();
            foreach (var folder in Directory.GetDirectories(directoryPath))
            {
                directoryListItems.Add(new DirectoryListItem(folder,true));
            }

            foreach (var file in Directory.GetFiles(directoryPath))
            {
                directoryListItems.Add(new DirectoryListItem(file, false));
            }
        }

        internal FileSystemInfo FindFileSystemInfoWithName(string name)
        {
            foreach (var item in directoryListItems)
            {
                if (item.FileSystemInfo.FullName.Equals(name))
                {
                    return item.FileSystemInfo;
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
