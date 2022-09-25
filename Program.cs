using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileExtensionCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            try {

                // three args - one file extension type, 2 origin folder, 3 destination folder
                string fileExtension = args[0];
                string originFolder = args[1];
                string destinationFolder = args[2];

                Console.WriteLine($"+++ Copying files with extension {fileExtension} from folder and subfolders {originFolder} to {destinationFolder} +++");

                // first of all get a list of all the files with the given extension

                List<string> files = FindAllFiles(originFolder, fileExtension);

                // now that we have the full list copy the files into the destination directory
                CopyAllFiles(files, destinationFolder);

            } catch (IndexOutOfRangeException) {
                Console.WriteLine($"+++ Not all arguments provided - quitting +++");
            }
            catch (Exception error)     // generic catch all
            {
                Console.WriteLine($"+++ Unknown Error: {error.Message} - quitting... +++");
            }

        }

        public static List<string> FindAllFiles(string path, string extension) {
            IEnumerable<string> rawFiles = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories).Where(s => s.EndsWith($".{extension}")).ToList();
            List<string> foundFiles = new List<string>();
            Console.WriteLine($"+++ Found {rawFiles.Count()} files +++");
            foreach (var rawFile in rawFiles)
            {
                foundFiles.Add(rawFile);
                Console.WriteLine(rawFile);
            }

            return foundFiles;
        }

        public static void CopyAllFiles(List<string> files, string destination) {
            foreach (var file in files)
            {
                var newNameSplit = file.Split("\\");
                var fileName = newNameSplit.Last();
                var finalName = destination + '/' + fileName;

                Console.WriteLine($"+++ Final File Path {finalName} +++");
                File.Copy(file, finalName);

            }
        }
    }
}
