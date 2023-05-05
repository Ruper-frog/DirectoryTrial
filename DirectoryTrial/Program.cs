using System.IO;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Reflection.Emit;

class Program
{
    static void Main(string[] args)
    {
        /*string sourceDirectoryQ = @"C:\MyPhotos";
        string newDirectoryName = "Sorted";
        string newDirectoryPath = Path.Combine(sourceDirectory, newDirectoryName);

        // Create the new directory
        Directory.CreateDirectory(newDirectoryPath);*/

        string sourceDirectory = @"C:\Users\ruper\OneDrive\שולחן העבודה\New folder";
        string destinationDirectory = @"C:\Users\ruper\OneDrive\שולחן העבודה\Ruper";

        // Get all files in the source directory
        List<string> files = new List<string> (Directory.GetFiles(sourceDirectory));

        List<string> filesNames = new List<string>();

        foreach (string file in files)
        {
            int index = file.IndexOf("1");

            int DotIndex = file.LastIndexOf(".");

            if (index > 0)
                filesNames.Add(file.Substring(index, file.Length - index - 1 - (file.Length - DotIndex - 1)));
        }

        /*for (int i = 0; i < filesNames.Count; i++)
        {
            Console.WriteLine(files[i]);

            Console.WriteLine(filesNames[i]);
        }*/

        if (!File.Exists(sourceDirectory))
        {

        }
        files.Reverse();

        /*
        // Iterate through each file and move it to the new directory
        foreach (string file in files)
        {
            Console.WriteLine(file);


            // Determine the new file name and location based on the original file name
            string fileName = Path.GetFileName(file);
            string newFilePath = Path.Combine(destinationDirectory, fileName);

            // Move the file to the new location
            File.Move(file, newFilePath);
        }*/
    }
}