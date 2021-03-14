using System;
using System.Runtime.InteropServices;

namespace CatHand4V.Wrappers.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public short wVk;
        public short wScan;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    };
}
