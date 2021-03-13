using System.Runtime.InteropServices;

namespace CatHand4V.Wrappers.User32.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    };
}
