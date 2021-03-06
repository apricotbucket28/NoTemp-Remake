using System;
using System.IO;

namespace NoTemp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "NoTemp";
            if (args.Length > 0 && args[0].Equals("/y"))
                Clean(false);
            else if (YN("Do you want to clean your PC?", true))
                Clean(true);
            else
            {
                Console.Clear();
                Console.WriteLine("Press any key to close the program...");
                Console.ReadKey();
            }
        }

        private static void Clean(bool isUserInput)
        {
            if (isUserInput)
                Console.Clear();

            DirectoryInfo di = new DirectoryInfo(Path.GetTempPath());

            int total = 0;

            foreach (FileInfo file in di.EnumerateFiles())
            {
                try
                {
                    file.Delete();
                }
                catch (Exception)
                {
                    continue;
                }
                Console.WriteLine("File deleted: " + file);
                total++;
            }

            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                try
                {
                    dir.Delete(true);
                }
                catch (Exception)
                {
                    continue;
                }
                Console.WriteLine("Directory removed: " + dir);
                total++;
            }

            Console.WriteLine("\nTotal number of files and directories removed: " + total);

            if (Chrome.IsInstalled() && (!isUserInput || YN("\nDo you want to clear the Chrome cache (you might have to restart the browser)?", false)))
            {
                Console.WriteLine("Cleaning the Chrome cache...");
                Chrome.ClearCache();
                Console.WriteLine("Done");
            }

            Console.WriteLine("\nCleaning the recycle bin...");
            RecycleBin.Empty();

            if (isUserInput)
            {
                Console.WriteLine("\nPress any key to close the program...");
                Console.ReadKey();
            }
        }

        private static bool YN(string question, bool clearConsole)
        {
            if (clearConsole)
                Console.Clear();
            Console.Write(question + " (y/n) ");
            return (Console.ReadLine().ToLower()) switch
            {
                "y" => true,
                "yes" => true,
                "n" => false,
                "no" => false,
                _ => YN(question, clearConsole),
            };
        }
    }
}