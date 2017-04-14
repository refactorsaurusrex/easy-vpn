using System;

namespace EasyVpn
{
    class Credentials
    {
        public Credentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public static Credentials Empty()
        {
            return new Credentials(string.Empty, string.Empty);
        }

        public string Username { get; }

        public string Password { get; }

        public bool AreIncompleteOrEmpty => Password.IsNullOrEmpty() || Username.IsNullOrEmpty();
    }
}