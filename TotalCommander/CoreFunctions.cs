using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TotalCommander
{
    // This class doesn't handle exceptions!
    class CoreFunctions
    {
        public static void FileCopy(string source, string target)
        {
            File.Copy(source, target);
        }

        public static void FileMove(string source, string target)
        {
            File.Move(source, target);
        }

        public static void FileDelete(string source)
        {
            File.Delete(source);
        }

        public static long FileProperties(FileInfo file)
        {
            return file.Length;
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName) 
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string target = Path.Combine(destDirName, file.Name);
                FileCopy(file.FullName, target);
            }

            // If copying subdirectories, copy them and their contents to new location.
            foreach (DirectoryInfo subdir in dirs)
            {
                string target = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, target);
            }
        }

        public static long DirectoryProperties(DirectoryInfo dir)
        {
            long size = 0;

            foreach (FileInfo file in dir.GetFiles())
            {
                size += FileProperties(file);
            }

            foreach (DirectoryInfo subdir in dir.GetDirectories())
            {
                size += DirectoryProperties(subdir);
            }

            return size;
        }

        public static void DirectoryMove(string source, string target)
        {
            Directory.Move(source, target);
        }

        public static void DirectoryDelete(string source)
        {
            Directory.Delete(source, true);
        }
    }
}
