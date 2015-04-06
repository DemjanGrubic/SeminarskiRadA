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

        public static void DirectoryCopy(string sourceDirectoryPath, string targetDirectoryPath) 
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirectoryPath);

            if (!Directory.Exists(targetDirectoryPath))
            {
                Directory.CreateDirectory(targetDirectoryPath);
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                string target = Path.Combine(targetDirectoryPath, file.Name);
                FileCopy(file.FullName, target);
            }

            foreach (DirectoryInfo subdir in dir.GetDirectories())
            {
                string target = Path.Combine(targetDirectoryPath, subdir.Name);
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
