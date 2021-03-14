using System.Runtime.InteropServices;

namespace CatHand4V.Wrappers.User32.Structs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct INPUT
    {
        public int type;
        public INPUT_UNION ui;
    };
}
