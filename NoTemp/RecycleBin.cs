using System;
using System.Runtime.InteropServices;

namespace NoTemp
{
    class RecycleBin
    {
        enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000002,
            SHERB_NOSOUND = 0x00000004
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        public static void EmptyRecycleBin()
        {
            uint result = SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOCONFIRMATION);
            if (result == 0)
                Console.WriteLine("Done");
            else
                Console.WriteLine("The recycle bin was already empty");
        }
    }
}
