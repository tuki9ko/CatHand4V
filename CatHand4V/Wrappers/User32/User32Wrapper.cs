using System;
using System.Runtime.InteropServices;
using CatHand4V.Wrappers.User32.Structs;

namespace CatHand4V.Wrappers.User32
{
    public static class User32Wrapper
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern void SendInput(int nInputs, INPUT[] pInputs, int cbsize);

        [DllImport("user32.dll", EntryPoint = "MapVirtualKeyA")]
        internal extern static int MapVirtualKey(int wCode, int wMapType);
    }
}
