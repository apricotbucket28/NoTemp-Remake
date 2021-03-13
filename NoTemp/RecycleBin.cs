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

        public static uint Empty()
        {
            uint result = SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOCONFIRMATION | RecycleFlags.SHERB_NOSOUND);
            return result;
        }

        public static int Items()
        {
            var shell = new Shell32.Shell();
            return shell.NameSpace(10).Items().Count;
        }
    }
}