using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Diagnostics;
using CatHand4V.Wrappers.User32;

namespace CatHand4V
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mainButton_Click(object sender, RoutedEventArgs e)
        {
            var processes = Process.GetProcessesByName("VRChat");

            if (!processes.Any())
            {
                MessageBox.Show("先にVRChatを起動してください。", "エラー", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var vrcWindowHandle = processes[0].MainWindowHandle;

            User32Wrapper.SetForegroundWindow(vrcWindowHandle);

            var inputs = User32Util.CreateKeyboardInputs(
                new (DwFlags dwFlags, VirtualKeyCode virtualKeyCode)[]
                {
                    (DwFlags.NONE, VirtualKeyCode.VK_LCONTROL),
                    (DwFlags.NONE, VirtualKeyCode.VK_OEM_5),
                    (DwFlags.KEYEVENTF_KEYUP, VirtualKeyCode.VK_LCONTROL),
                    (DwFlags.KEYEVENTF_KEYUP, VirtualKeyCode.VK_OEM_5)
                }).ToArray();

            User32Wrapper.SendInput(inputs.Length, inputs, Marshal.SizeOf(inputs[0]));
        }
    }
}
