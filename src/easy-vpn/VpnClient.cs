using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentConsole.Library;
using static System.ConsoleColor;

namespace EasyVpn
{
    class VpnClient
    {
        public void Login(Credentials creds)
        {
            var vpnProcess = Process.GetProcessesByName("PanGPA").FirstOrDefault();
            var mainWindowHandle = vpnProcess?.MainWindowHandle ?? IntPtr.Zero;

            if (mainWindowHandle == IntPtr.Zero)
                throw new InvalidOperationException(Resources.CantFindClientWindowException);

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
            "VPN connection initiated!".WriteLine(Cyan);
            Task.Delay(2000).Wait();
            ExternalWindow.Close(mainWindowHandle);
        }
    }
}
