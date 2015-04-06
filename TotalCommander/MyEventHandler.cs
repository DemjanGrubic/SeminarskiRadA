using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TotalCommander
{
    public class MyEventHandler
    {
        public static void Copy(object obj = null)
        {
            List<Exception> exceptions = new List<Exception>();

            string target = MainWindow.directoryListNotFocused.DirectoryPath;

            List<FileSystemInfo> selectedItems = MainWindow.GetSelectedItems();
            foreach (var selectedItem in selectedItems)
            {
                if (selectedItem is DirectoryInfo)
                {
                    CoreFunctions.DirectoryCopy(selectedItem.FullName, Path.Combine(target, selectedItem.Name), exceptions);
                }
                else
                {
                    CoreFunctions.FileCopy(selectedItem.FullName, Path.Combine(target, selectedItem.Name), exceptions);
                }
            }

            MainWindow.directoryListNotFocused.Refresh();

            ShowExceptions(exceptions);
        }

        public static void Delete(object obj = null)
        {
            List<Exception> exceptions = new List<Exception>();

            List<FileSystemInfo> selectedItems = MainWindow.GetSelectedItems();
            foreach (var selectedItem in selectedItems)
            {
                if (selectedItem is DirectoryInfo)
                {
                    CoreFunctions.DirectoryDelete(selectedItem.FullName, exceptions);
                }
                else
                {
                    CoreFunctions.FileDelete(selectedItem.FullName, exceptions);
                }
            }

            MainWindow.directoryListFocused.Refresh();

            ShowExceptions(exceptions);
        }

        public static void Move(object obj = null)
        {
            List<Exception> exceptions = new List<Exception>();

            string target = MainWindow.directoryListNotFocused.DirectoryPath;

            List<FileSystemInfo> selectedItems = MainWindow.GetSelectedItems();
            foreach (var selectedItem in selectedItems)
            {
                if (selectedItem is DirectoryInfo)
                {
                    CoreFunctions.DirectoryMove(selectedItem.FullName, Path.Combine(target, selectedItem.Name), exceptions);
                }
                else
                {
                    CoreFunctions.FileMove(selectedItem.FullName, Path.Combine(target, selectedItem.Name), exceptions);
                }
            }

            MainWindow.directoryListFocused.Refresh();
            MainWindow.directoryListNotFocused.Refresh();

            ShowExceptions(exceptions);
        }

        public static void Enter()
        {
            List<FileSystemInfo> selectedItems = MainWindow.GetSelectedItems();
            if (selectedItems.Count == 1)
            {
                string source = MainWindow.directoryListFocused.DirectoryPath;
                FileSystemInfo fileSystemInfo = selectedItems.First();
                if (fileSystemInfo is DirectoryInfo)
                {
                    MainWindow.directoryListFocused.UpdateDirectoryEntries(Path.Combine(source, fileSystemInfo.Name));
                }
                else
                {
                    System.Diagnostics.Process.Start(fileSystemInfo.FullName);
                }
            }
        }

        public static void Back()
        {
            string directoryPath = MainWindow.directoryListFocused.DirectoryPath;
            if (directoryPath.Split('\\').Length != 1)
            {
                string[] partsOfPath = directoryPath.Split('\\');
                string newDirectoryPath = partsOfPath[0] + "\\";
                for (int i = 1; i < partsOfPath.Length - 1; ++i)
                {
                    newDirectoryPath = Path.Combine(newDirectoryPath, partsOfPath[i]);
                }

                MainWindow.directoryListFocused.UpdateDirectoryEntries(newDirectoryPath);
            }
        }

        public static void Properties(object obj = null)
        {
            List<Exception> exceptions = new List<Exception>();

            List<FileSystemInfo> selectedItems = MainWindow.GetSelectedItems();
            long size = 0;
            foreach (var selectedItem in selectedItems)
            {
                if (selectedItem is DirectoryInfo)
                {
                    size += CoreFunctions.DirectoryProperties((DirectoryInfo)selectedItem, exceptions);
                }
                else
                {
                    size += CoreFunctions.FileProperties((FileInfo)selectedItem, exceptions);
                }
            }

            double MB = size / 1024.0 / 1024.0;
            MessageBox.Show("Size of selected files and folders is: " + Math.Round(MB, 3) + " MB");

            ShowExceptions(exceptions);
        }

        private static void ShowExceptions(List<Exception> exceptions)
        {
            if (exceptions.Count > 0)
            {
                string errorMessage = string.Join(Environment.NewLine, exceptions.Select(x => x.Message));
                MessageBox.Show(errorMessage);
            }
        }
    }
}
