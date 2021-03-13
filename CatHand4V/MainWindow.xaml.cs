using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace CatHand4V
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void SendInput(int nInputs, INPUT[] pInputs, int cbsize);

        [DllImport("user32.dll", EntryPoint = "MapVirtualKeyA")]
        public extern static int MapVirtualKey(int wCode, int wMapType);

        [StructLayout(LayoutKind.Sequential)]
        public struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT_UNION
        {
            [FieldOffset(0)] public MOUSEINPUT mouse;
            [FieldOffset(0)] public KEYBDINPUT keyboard;
            [FieldOffset(0)] public HARDWAREINPUT hardware;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct INPUT
        {
            public int type;
            public INPUT_UNION ui;
        };

        private const int INPUT_KEYBOARD = 1;
        private const int KEYEVENTF_KEYDOWN = 0x0;
        private const int KEYEVENTF_KEYUP = 0x2;
        private const int KEYEVENTF_EXTENDEDKEY = 0x1;

        private IntPtr vrcWindowHandle = default;
        private INPUT[] inputs = new INPUT[4];

        public MainWindow()
        {
            InitializeComponent();

            inputs[0] = new INPUT();
            inputs[0].type = 1;
            inputs[0].ui.keyboard.wVk = 0xA2;
            inputs[0].ui.keyboard.wScan = (short)MapVirtualKey((int)0xA2, 0);
            inputs[0].ui.keyboard.dwFlags = 0;
            inputs[0].ui.keyboard.time = 0;
            inputs[0].ui.keyboard.dwExtraInfo = IntPtr.Zero;

            inputs[1] = new INPUT();
            inputs[1].type = 1;
            inputs[1].ui.keyboard.wVk = 0xDC;
            inputs[1].ui.keyboard.wScan = (short)MapVirtualKey((int)0xDC, 0);
            inputs[1].ui.keyboard.dwFlags = 0;
            inputs[1].ui.keyboard.time = 0;
            inputs[1].ui.keyboard.dwExtraInfo = IntPtr.Zero;

            inputs[2] = new INPUT();
            inputs[2].type = 1;
            inputs[2].ui.keyboard.wVk = 0xA2;
            inputs[2].ui.keyboard.wScan = (short)MapVirtualKey((int)0xA2, 0);
            inputs[2].ui.keyboard.dwFlags = KEYEVENTF_KEYUP;
            inputs[2].ui.keyboard.time = 0;
            inputs[2].ui.keyboard.dwExtraInfo = IntPtr.Zero;

            inputs[3] = new INPUT();
            inputs[3].type = 1;
            inputs[3].ui.keyboard.wVk = 0xDC;
            inputs[3].ui.keyboard.wScan = (short)MapVirtualKey((int)0xDC, 0);
            inputs[3].ui.keyboard.dwFlags = KEYEVENTF_KEYUP;
            inputs[3].ui.keyboard.time = 0;
            inputs[3].ui.keyboard.dwExtraInfo = IntPtr.Zero;

        }

        private void mainButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.MainWindowTitle.IndexOf("VRChat") >= 0)
                {
                    vrcWindowHandle = p.MainWindowHandle;
                }
            }

            if (vrcWindowHandle == default)
            {
                MessageBox.Show("先にVRChatを起動してください。", "エラー", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //this.Close();
            }

            SetForegroundWindow(vrcWindowHandle);
            SendInput(inputs.Length, inputs, Marshal.SizeOf(inputs[0]));
        }
    }
}
