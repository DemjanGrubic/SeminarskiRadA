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
            try
            {
                string target = MainWindow.directoryListNotFocused.DirectoryPath;

                List<FileSystemInfo> selectedItems = MainWindow.GetSelectedItems();
                foreach (var selectedItem in selectedItems)
                {
                    if (selectedItem is DirectoryInfo)
                    {
                        CoreFunctions.DirectoryCopy(selectedItem.FullName, Path.Combine(target, selectedItem.Name));
                    }
                    else
                    {
                        CoreFunctions.FileCopy(selectedItem.FullName, Path.Combine(target, selectedItem.Name));
                    }
                }

                MainWindow.directoryListNotFocused.Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void Delete(object obj = null)
        {
            try
            {
                List<FileSystemInfo> selectedItems = MainWindow.GetSelectedItems();
                foreach (var selectedItem in selectedItems)
                {
                    if (selectedItem is DirectoryInfo)
                    {
                        CoreFunctions.DirectoryDelete(selectedItem.FullName);
                    }
                    else
                    {
                        CoreFunctions.FileDelete(selectedItem.FullName);
                    }
                }

                MainWindow.directoryListFocused.Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void Move(object obj = null)
        {
            try
            {
                string target = MainWindow.directoryListNotFocused.DirectoryPath;

                List<FileSystemInfo> selectedItems = MainWindow.GetSelectedItems();
                foreach (var selectedItem in selectedItems)
                {
                    if (selectedItem is DirectoryInfo)
                    {
                        CoreFunctions.DirectoryMove(selectedItem.FullName, Path.Combine(target, selectedItem.Name));
                    }
                    else
                    {
                        CoreFunctions.FileMove(selectedItem.FullName, Path.Combine(target, selectedItem.Name));
                    }
                }

                MainWindow.directoryListFocused.Refresh();
                MainWindow.directoryListNotFocused.Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void Enter()
        {
            try
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
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void Back()
        {
            try
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
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void Properties(object obj = null)
        {
            try
            {
                List<FileSystemInfo> selectedItems = MainWindow.GetSelectedItems();
                long size = 0;
                foreach (var selectedItem in selectedItems)
                {
                    if (selectedItem is DirectoryInfo)
                    {
                        size += CoreFunctions.DirectoryProperties((DirectoryInfo)selectedItem);
                    }
                    else
                    {
                        size += CoreFunctions.FileProperties((FileInfo)selectedItem);
                    }
                }

                double MB = size / 1024.0 / 1024.0;
                MessageBox.Show("Size of selected files and folders is: " + Math.Round(MB, 3) + " MB");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
