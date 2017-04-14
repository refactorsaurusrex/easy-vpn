using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVpn
{
    class VpnClient
    {
        public void Login(string username, string password)
        {
            var process = Process.GetProcessesByName("PanGPA");
            if (process.Length == 0)
                throw new InvalidOperationException("VPN client does not appear to be running. Please start the VPN client and try again.");

            var mainWindowHandle = process.First().MainWindowHandle;

            if (mainWindowHandle == IntPtr.Zero)
            {
                Process.Start(@"C:\Program Files\Palo Alto Networks\GlobalProtect\PanGPA.exe");
                Task.Delay(2000).Wait();
                mainWindowHandle = Process.GetProcessesByName("PanGPA")[0].MainWindowHandle;
            }

            if (mainWindowHandle == IntPtr.Zero)
                throw new InvalidOperationException("VPN client does not appear to be running. Please start the VPN client and try again.");

            var tabControl = User32.FindWindowEx(mainWindowHandle, IntPtr.Zero, "SysTabControl32", "");
            var dialog1 = User32.FindWindowEx(tabControl, IntPtr.Zero, null, "");
            var dialog2 = User32.FindWindowEx(tabControl, dialog1, null, "");
            var text1 = User32.FindWindowEx(dialog2, IntPtr.Zero, "Edit", "");
            var usernameField = User32.FindWindowEx(dialog2, text1, "Edit", "");
            var passwordField = User32.FindWindowEx(dialog2, usernameField, "Edit", "");

            SetTextBoxText(usernameField, username);
            SetTextBoxText(passwordField, password);
        }

        void SetTextBoxText(IntPtr hTextBox, string value)
        {
            User32.SendMessage(hTextBox, User32.WM_SETTEXT, 0, value);
        }
    }
}
