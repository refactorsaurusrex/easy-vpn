using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EasyVpn
{
    static class ExternalWindow
    {
        static uint WM_SETTEXT = 0x000C;
        const int BM_CLICK = 0x00F5;

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        static extern int SendMessage(IntPtr hwndControl, uint msg, int wParam, string text);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll")]
        static extern void ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void Close(IntPtr window)
        {
            ShowWindow(window, 0);
        }

        public static void SetText(IntPtr editControl, string text)
        {
            SendMessage(editControl, WM_SETTEXT, 0, text);
        }

        public static void Click(IntPtr button)
        {
            SendMessage(button, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }

        public static IntPtr FindControl(IntPtr parentControl, IntPtr priorChildControl, string controlType = null, string controlTitle = "")
        {
            return FindWindowEx(parentControl, priorChildControl, controlType, controlTitle);
        }
    }
}
