using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentConsole.Library;

namespace EasyVpn
{
    class VpnClient
    {
        public void Login(Credentials creds)
        {
            Process.Start(@"C:\Program Files\Palo Alto Networks\GlobalProtect\PanGPA.exe");
            Task.Delay(3000).Wait();

            var mainWindowHandle = Process.GetProcessesByName("PanGPA")[0].MainWindowHandle;

            if (mainWindowHandle == IntPtr.Zero)
                throw new InvalidOperationException("VPN client does not appear to be running. Please start the VPN client and try again.");

            var tabControl = ExternalWindow.FindControl(mainWindowHandle, IntPtr.Zero, "SysTabControl32");
            var dialog1 = ExternalWindow.FindControl(tabControl, IntPtr.Zero);
            var dialog2 = ExternalWindow.FindControl(tabControl, dialog1);
            var text1 = ExternalWindow.FindControl(dialog2, IntPtr.Zero, "Edit");
            var usernameField = ExternalWindow.FindControl(dialog2, text1, "Edit");
            var passwordField = ExternalWindow.FindControl(dialog2, usernameField, "Edit");
            var connectButton = ExternalWindow.FindControl(dialog2, IntPtr.Zero, "Button", "Connect");

            ExternalWindow.SetText(usernameField, creds.Username);
            ExternalWindow.SetText(passwordField, creds.Password);

            ExternalWindow.Click(connectButton);
            Task.Delay(3000).Wait();
            ExternalWindow.Close(mainWindowHandle);
        }
    }
}
