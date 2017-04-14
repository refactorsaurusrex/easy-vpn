using System;
using FluentConsole.Library;
using PowerArgs;

namespace EasyVpn
{
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    class VpnProgram
    {
        [ArgActionMethod]
        [ArgDescription("Automatically connect to the VPN client.")]
        public void Login(CredentialArgs credentialArgs)
        {
            var credentialManager = new CredentialManager();
            var cliCreds = credentialArgs.ToCredentials();

            if (cliCreds.AreIncompleteOrEmpty)
            {
                var storedCreds = credentialManager.Retrieve();
                if (storedCreds.AreIncompleteOrEmpty)
                {
                    "No stored credentials exist. Please use the -username parameter to enter new credentials.".WriteLine(ConsoleColor.Red);
                    return;
                }

                Login(storedCreds);
            }

            Login(cliCreds);
            credentialManager.Store(cliCreds);
        }

        void Login(Credentials creds)
        {
            var client = new VpnClient();
            try
            {
                client.Login(creds.Username, creds.Password);
            }
            catch (Exception ex)
            {
                ex.Message.WriteLine(ConsoleColor.Red);
            }
        }
    }
}