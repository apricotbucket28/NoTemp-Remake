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
                CleanTemp(false);
            else if (YN("Do you want to clean the temporary folder?"))
                CleanTemp(true);
            else
            {
                Console.Clear();
                Console.WriteLine("Press any key to close the program...");
                Console.ReadKey();
            }
        }

        private static void CleanTemp(bool pressToExit)
        {
            if (pressToExit)
                Console.Clear();

            string temp = Path.GetTempPath();
            DirectoryInfo di = new DirectoryInfo(temp);

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
            if (pressToExit)
            {
                Console.WriteLine("Press any key to close the program...");
                Console.ReadKey();
            }
        }

        private static bool YN(string question)
        {
            Console.Clear();
            Console.Write(question + " (y/n) ");
            return (Console.ReadLine().ToLower()) switch
            {
                "y" => true,
                "yes" => true,
                "n" => false,
                "no" => false,
                _ => YN(question),
            };
        }
    }
}