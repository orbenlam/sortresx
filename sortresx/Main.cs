using System;
using System.IO;
using System.Reflection;

namespace Codice.SortResX
{
    class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
                ResourceFileSorter fileSorter = null;
                if (args.Length <= 0)
                    return 1;

                //if (!CheckArgs(args[0]))
                //    return 1;

                try
                {
                    foreach(var arg in args)
                    {
                        var files = GetFilePaths(arg);
                        foreach(var file in files)
                        {
                            if (!CheckArgs(file))
                                return 1;
                            Console.WriteLine("Sorting: " + file);
                            fileSorter = new ResourceFileSorter(file);
                            fileSorter.Sort();
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Could not sort resources. Abort.");
                }
                return 0;
        }

        static void PrintUsage(string argument)
        {
            Console.WriteLine("Invalid argument:" + argument + "\nUsage: sortresx file_to_sort");
        }

        static bool CheckArgs(string filepath)
        {
            return File.Exists(filepath);
        }

        static string[] GetFilePaths(string filePathPattern)
        {
            var pattern = System.IO.Path.IsPathRooted(filePathPattern) ? filePathPattern : Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + filePathPattern;
            return Directory.GetFiles(pattern, "*.resx", SearchOption.AllDirectories);
        }
    }

    internal class ResourceFileSorter
    {
        public ResourceFileSorter(string path)
        {
            mFileProcessor = new FileProcessor(path);
        }

        public void Sort()
        {
            if (mFileProcessor != null)
            {
                mFileProcessor.Process();
            }
        }

        private FileProcessor mFileProcessor;
    }
}