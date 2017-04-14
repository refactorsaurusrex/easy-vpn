using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EasyVpn
{
    class CredentialManager
    {
        readonly string cipherPath;
        readonly string entropyPath;

        public CredentialManager()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            cipherPath = Path.Combine(appData, "easy-vpn-cipher");
            entropyPath = Path.Combine(appData, "easy-vpn-entropy");
        }

        public void Store(Credentials credentials)
        {
            var creds = Encoding.UTF8.GetBytes($"{credentials.Username}:{credentials.Password}");

            var entropy = new byte[20];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(entropy);

            var cipher = ProtectedData.Protect(creds, entropy, DataProtectionScope.CurrentUser);

            File.WriteAllBytes(cipherPath, cipher);
            File.WriteAllBytes(entropyPath, entropy);
        }

        public Credentials Retrieve()
        {
            if (!File.Exists(cipherPath) || !File.Exists(entropyPath))
                return Credentials.Empty();

            var cipher = File.ReadAllBytes(cipherPath);
            var entropy = File.ReadAllBytes(entropyPath);

            var resultBytes = ProtectedData.Unprotect(cipher, entropy, DataProtectionScope.CurrentUser);
            var creds = Encoding.UTF8.GetString(resultBytes);

            var index = creds.IndexOf(':');
            var username = creds.Substring(0, index);
            var password = creds.Substring(index + 1);

            return new Credentials(username, password);
        }
    }
}
