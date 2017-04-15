using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentConsole.Library;
using PowerArgs;

namespace EasyVpn
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Args.InvokeAction<VpnProgram>(args);
            }
            catch (Exception ex)
            {
                ex.WriteLine();
            }

            //if (System.Diagnostics.Debugger.IsAttached)
            //    "End of line".WriteLineWait(ConsoleColor.Yellow);
        }
    }
}
