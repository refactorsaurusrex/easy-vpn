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
            const int maxTries = 5;
            var counter = 1;
            IntPtr mainWindowHandle;

            while (true)
            {
                Process.Start(@"C:\Program Files\Palo Alto Networks\GlobalProtect\PanGPA.exe");
                Task.Delay(2000).Wait();

                mainWindowHandle = Process.GetProcessesByName("PanGPA")[0].MainWindowHandle;

                if (mainWindowHandle != IntPtr.Zero)
                    break;

                if (counter++ >= maxTries)
                    throw new InvalidOperationException("Unable to start the VPN client. :( You may have to do it manually this time.");

                $"Couldn't get the VPN client's main window handle. Will try {maxTries - counter} more times...".WriteLine(Red);
            }

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
            Task.Delay(3000).Wait();
            ExternalWindow.Close(mainWindowHandle);
        }
    }
}
