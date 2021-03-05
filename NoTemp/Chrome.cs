using System;
using System.IO;

namespace NoTemp
{
    class Chrome
    {
        public static bool IsInstalled()
        {
            return File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Google\\Chrome\\Application\\chrome.exe");
        }

        public static void ClearCache()
        {
            DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Cache");

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
            }
        }
    }
}