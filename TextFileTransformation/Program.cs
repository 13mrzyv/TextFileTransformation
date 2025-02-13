﻿using System;
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
            string SourceFolder = "B";
            string TargetFolder = "A";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string FilePathA = $"{desktopPath}\\{TargetFolder}";
            string FilePathB = $"{desktopPath}\\{SourceFolder}";
            string[] FilesOrFoldersB = Directory.GetFileSystemEntries(FilePathB);
            string[] filesA = Directory.GetFiles(FilePathA);
            foreach (var file in filesA)
            {
                File.Delete(file);
            }
            string[] directoriesA = Directory.GetDirectories(FilePathA);
            foreach (var dir in directoriesA)
            {
                ForceDeleteDirectory(dir);
            }
            CopyFiles(FilePathB, FilePathA);
            CopyDirectories(FilePathB, FilePathA);
            ForceDeleteDirectory(FilePathB);
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
        static void ForceDeleteDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                foreach (string file in Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories))
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        fileInfo.Attributes &= ~FileAttributes.ReadOnly; // Remove read-only attribute
                    }

                    File.Delete(file); // Delete the file
                }

                foreach (string subDirectory in Directory.GetDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly))
                {
                    ForceDeleteDirectory(subDirectory); // Recursively delete subdirectories.
                }

                Directory.Delete(directoryPath, false);
            }
            else
            {
                Console.WriteLine($"Directory '{directoryPath}' does not exist.");
            }
        }
    }
}
