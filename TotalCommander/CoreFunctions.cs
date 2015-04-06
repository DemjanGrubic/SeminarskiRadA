using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TotalCommander
{
    class CoreFunctions
    {
        public static void FileCopy(string source, string target, List<Exception> exceptions)
        {
            try
            {
                File.Copy(source, target);
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }
        }

        public static void FileMove(string source, string target, List<Exception> exceptions)
        {
            try
            {
                File.Move(source, target);
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }
        }

        public static void FileDelete(string source, List<Exception> exceptions)
        {
            try
            {
                File.Delete(source);
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }
        }

        public static long FileProperties(FileInfo file, List<Exception> exceptions)
        {
            try
            {
                return file.Length;
            }
            catch (Exception e)
            {
                exceptions.Add(e);
                return 0;
            }
        }

        public static void DirectoryCopy(string sourceDirectoryPath, string targetDirectoryPath, List<Exception> exceptions) 
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirectoryPath);

                if (!Directory.Exists(targetDirectoryPath))
                {
                    Directory.CreateDirectory(targetDirectoryPath);
                }

                foreach (FileInfo file in dir.GetFiles())
                {
                    string target = Path.Combine(targetDirectoryPath, file.Name);
                    FileCopy(file.FullName, target, exceptions);
                }

                foreach (DirectoryInfo subdir in dir.GetDirectories())
                {
                    string target = Path.Combine(targetDirectoryPath, subdir.Name);
                    DirectoryCopy(subdir.FullName, target, exceptions);
                }
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }
        }

        public static long DirectoryProperties(DirectoryInfo dir, List<Exception> exceptions)
        {
            long size = 0;

            try
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    size += FileProperties(file, exceptions);
                }

                foreach (DirectoryInfo subdir in dir.GetDirectories())
                {
                    size += DirectoryProperties(subdir, exceptions);
                }
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }

            return size;
        }

        public static void DirectoryMove(string source, string target, List<Exception> exceptions)
        {
            try
            {
                Directory.Move(source, target);
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }
        }

        public static void DirectoryDelete(string source, List<Exception> exceptions)
        {
            try
            {
                Directory.Delete(source, true);
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }
        }
    }
}
