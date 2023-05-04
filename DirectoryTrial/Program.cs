﻿using System.IO;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Reflection.Emit;

class Program
{
    static void Main(string[] args)
    {
        if (!File.Exists(path))
        {
            // This statement ensures that the file is created,
            // but the handle is not kept.
            using (FileStream fs = File.Create(path)) { }
        }

        string sourceDirectory = @"C:\MyPhotos";
        string newDirectoryName = "Sorted";
        string newDirectoryPath = Path.Combine(sourceDirectory, newDirectoryName);

        // Create the new directory
        Directory.CreateDirectory(newDirectoryPath);

        string sourceDirectory = @"C:\Users\ruper\OneDrive\שולחן העבודה\Ruper";
        string destinationDirectory = @"C:\Users\ruper\OneDrive\שולחן העבודה\New folder";

        // Get all files in the source directory
        List<string> files = new List<string> (Directory.GetFiles(sourceDirectory));

        files.Reverse();

        // Iterate through each file and move it to the new directory
        foreach (string file in files)
        {
            Console.WriteLine(file);


            // Determine the new file name and location based on the original file name
            string fileName = Path.GetFileName(file);
            string newFilePath = Path.Combine(destinationDirectory, fileName);

            // Move the file to the new location
            File.Move(file, newFilePath);
        }
    }
}