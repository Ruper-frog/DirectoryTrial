using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please Enter The Directory Of The Folder");

        string sourceDirectory = Console.ReadLine();

        TheDeed(sourceDirectory);

        Console.WriteLine();

        Console.WriteLine("Do You Want To Open The Folder?\n\nPress Enter If You Do\n\nTo Leave Press Any Other Button");

        ConsoleKeyInfo keyinfo = Console.ReadKey();

        if (keyinfo.Key == ConsoleKey.Enter)
        {
            // Start a new process for the file explorer
            Process fileExplorer = new Process();
            fileExplorer.StartInfo.FileName = sourceDirectory;
            fileExplorer.StartInfo.UseShellExecute = true;
            fileExplorer.Start();

            // Refresh the file explorer window
            fileExplorer.Refresh();
        }
        Console.WriteLine();

        Console.ReadKey();
    }
    static void TheDeed(string sourceDirectory)
    {
        // Get all files in the source directory
        List<string> files = new List<string>(Directory.GetFiles(sourceDirectory));

        List<string> folders = new List<string>(Directory.GetDirectories(sourceDirectory));

        foreach (string folder in folders)
            TheDeed(folder);

        foreach (string file in files)
        {
            int YearIndex = 0;

            string Year = "";

            while (true)
            {
                YearIndex = file.IndexOf("1", YearIndex + 1);

                if (!(YearIndex != -1 && file.Length - YearIndex >= 0))
                    goto REPEAT;

                Year = file.Substring(YearIndex, 4);

                if (Regex.IsMatch(Year, @"^\d+$"))
                    break;
            }
            if (!sourceDirectory.Substring(sourceDirectory.Length - 1 - 3, 4).Equals(Year))
            {
                string newDirectoryName = Year;
                string newDirectoryPath = Path.Combine(sourceDirectory, newDirectoryName);

                if (!Directory.Exists(newDirectoryPath))
                {
                    // Create the new directory
                    Directory.CreateDirectory(newDirectoryPath);
                }

                // Determine the new file name and location based on the original file name
                string fileName = Path.GetFileName(file);
                string newFilePath = Path.Combine(newDirectoryPath, fileName);

                // Move the file to the new location

                File.Move(file, newFilePath);
            }
            REPEAT:
                continue;
        }
    }
}