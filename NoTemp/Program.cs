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
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Press any key to close the program...");
                Console.ReadKey();
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("File deleted: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(file);
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
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("Directory removed: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(dir);
                total++;
            }

            if (Chrome.IsInstalled() && (!isUserInput || YN("\nDo you want to clear the Chrome cache (you might have to restart the browser)?", false)))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Cleaning the Chrome cache... ");
                Chrome.ClearCache();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done");
            }

            total += RecycleBin.Items();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\nCleaning the recycle bin... ");
            uint result = RecycleBin.Empty();
            if (result == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The recycle bin was already empty");
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\nTotal number of files and directories removed: " + string.Format("{0:n0}", total));
            if (isUserInput)
            {
                Console.WriteLine("Press any key to close the program...");
                Console.ReadKey();
                Console.ResetColor();
            }
        }

        private static bool YN(string question, bool clearConsole)
        {
            if (clearConsole)
                Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(question);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" (y/n) ");
            Console.ResetColor();
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