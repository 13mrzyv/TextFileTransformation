using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace TextFileTransformation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string FilePathA = $"{desktopPath}\\A";
            string FilePathB = $"{desktopPath}\\B";
            string[] FilesOrFoldersB = Directory.GetFileSystemEntries(FilePathB);
            string[] filesA = Directory.GetFiles(FilePathA);
            foreach (var file in filesA)
            {
                File.Delete(file);
            }
            string[] directoriesA = Directory.GetDirectories(FilePathA);
            foreach (var dir in directoriesA)
            {
                Directory.Delete(dir, true);
            }
            CopyFiles(FilePathB, FilePathA);
            CopyDirectories(FilePathB, FilePathA);
            Console.WriteLine("...SUCSESS...");
            Console.ReadLine();
        }
        static void CopyFiles(string sourcePath, string destSource)
        {
            string[] files = Directory.GetFiles(sourcePath);
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFilePath = Path.Combine(destSource, fileName);
                File.Copy(file, destFilePath);
            }
        }

        static void CopyDirectories(string sourcePath, string destPath)
        {
            string[] directories = Directory.GetDirectories(sourcePath);
            foreach (var dir in directories)
            {
                string dirName = Path.GetFileName(dir);
                string destDirPath = Path.Combine(destPath, dirName);
                Directory.CreateDirectory(destDirPath);
                CopyFiles(dir, destDirPath); 
                CopyDirectories(dir, destDirPath); 
            }

        }
    }
}
