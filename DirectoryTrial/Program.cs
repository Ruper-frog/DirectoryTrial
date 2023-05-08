using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

class Program
{
    static void ANiceTouch(string Syntax, bool GoBack)
    {
        if (GoBack)
            Console.SetCursorPosition(0, 3);

        foreach (char ch in Syntax) 
        {
            Console.Write(ch);
            Thread.Sleep(15);
        }
        Console.Write("\n\r\t\r");
    }
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;

        Console.CursorVisible = false;

        string sourceDirectory = "";

        bool Reverse = false;

        bool Restart = true;

        while (Restart)
        {
            Console.Clear();

            Console.SetCursorPosition(0, 0);

            ANiceTouch("Please Enter The Directory Of The Folder:", false);

            sourceDirectory = Console.ReadLine();

            Restart = false;

            try
            {
                TheDeed(sourceDirectory, Reverse);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                ANiceTouch("The Program Is Going To Restart Itself Now", false);

                Thread.Sleep(3000);

                Restart = true;
            }
        }

        Console.WriteLine();

        ANiceTouch("Do You Want To Open The Folder?\n\nPress Enter If You Do\n\nTo Continue Press Any Other Button", false);

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

        ANiceTouch("Do You Regret Your Actions And Want To Go Back?\n\nPress Enter If You Do\n\nTo Leave Press Any Other Button", true);

        keyinfo = Console.ReadKey();

        if (keyinfo.Key == ConsoleKey.Enter)
        {
            Reverse = true;
            TheDeed(sourceDirectory, Reverse);
        }

        Console.WriteLine();
    }
    static void TheDeed(string sourceDirectory, bool Reverse)
    {
        // Get all files in the source directory
        List<string> files = new List<string>(Directory.GetFiles(sourceDirectory));

        List<string> folders = new List<string>(Directory.GetDirectories(sourceDirectory));

        foreach (string folder in folders)
            TheDeed(folder, Reverse);

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
            else if (Reverse)
            {
                string fileName = Path.GetFileName(file);
                string newFilePath = Path.Combine(Directory.GetParent(sourceDirectory).FullName, fileName);

                if (!Directory.Exists(newFilePath))
                {
                    // Move the file to the new location
                    File.Move(file, newFilePath);

                    if (!Directory.EnumerateFileSystemEntries(sourceDirectory).Any())
                        Directory.Delete(sourceDirectory, true);
                }
            }
            REPEAT:
                continue;
        }
    }
}