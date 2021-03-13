using System.Runtime.InteropServices;

namespace CatHand4V.Wrappers.User32.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Win32Point
    {
        public int X;
        public int Y;
    };
}
