using System;
using FluentConsole.Library;
using PowerArgs;
using static System.ConsoleColor;

namespace EasyVpn
{
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    class VpnProgram
    {
        readonly CredentialManager credentialManager = new CredentialManager();

        [ArgActionMethod]
        [ArgDescription("Automatically connect to the VPN client.")]
        public void Login()
        {
            try
            {
                var storedCreds = credentialManager.Retrieve();
                if (storedCreds.AreIncompleteOrEmpty)
                {
                    "No stored credentials exist. Please use the 'creds' verb to enter new credentials.".WriteLine(Red);
                    return;
                }

                var vpnClient = new VpnClient();
                vpnClient.Login(storedCreds);
            }
            catch (Exception ex)
            {
                ex.Message.WriteLine(Red);
            }
        }

        [ArgActionMethod]
        [ArgDescription("Cache your VPN credentials locally.")]
        public void Creds([ArgRequired, ArgDescription("Your VPN username")]string username)
        {
            var password = Args.Parse<PasswordArgs>().Password.ConvertToNonsecureString();
            string.Empty.WriteLine();
            var cliCreds = new Credentials(username, password);
            credentialManager.Store(cliCreds);
        }
    }
}