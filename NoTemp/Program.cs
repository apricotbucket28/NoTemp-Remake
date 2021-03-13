using System;
using System.IO;

namespace NoTemp
{
    class Program
    {
        [STAThread]
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

            if (Chrome.IsInstalled() && (!isUserInput || YN("\nDo you want to clear the Chrome cache (you might have to restart the browser)?", false)))
            {
                Console.WriteLine("Cleaning the Chrome cache...");
                Chrome.ClearCache();
                Console.WriteLine("Done");
            }

            total += RecycleBin.Items();
            Console.WriteLine("\nCleaning the recycle bin...");
            uint result = RecycleBin.Empty();
            if (result == 0)
                Console.WriteLine("Done");
            else
                Console.WriteLine("The recycle bin was already empty");

            Console.WriteLine("\nTotal number of files and directories removed: " + total);

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
            switch (Console.ReadLine().ToLower())
            {
                case "y":
                    return true;
                case "yes":
                    return true;
                case "n":
                    return false;
                case "no":
                    return false;
                default:
                    return YN(question, clearConsole);
            }
        }
    }
}