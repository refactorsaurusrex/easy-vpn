using System;
using PowerArgs;

namespace EasyVpn
{
    [TabCompletion]
    class CredentialArgs
    {
        [ArgShortcut("u")]
        [ArgDescription("Your VPN username")]
        public string Username { get; set; }

        [ArgDescription("Your VPN password")]
        public SecureStringArgument Password { get; set; }

        public Credentials ToCredentials()
        {
            return Username.IsNullOrEmpty() ? Credentials.Empty() : new Credentials(Username, Password.ConvertToNonsecureString());
        }
    }
}