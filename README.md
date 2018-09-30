# UPDATE 9/30/18
The latest version of the GlobalProtect VPN client has rendered this project moot because the popup window only remains visible while it has focus. As soon as you bring another window into the foreground, the GlobalProtect VPN window disappears. It was good while it lasted, but I guess I have to suck it up now and type in my credentials manually.

# What's easy-vpn?

`easy-vpn` is a command line tool that automates entering your credentials into the GlobalProtect VPN client. If you don't use GlobalProtect VPN, this library isn't going to do a whole lot for ya. :)

# But I *do* use GlobalProtect!

Oh, well, in that case: Are you sick and tired and entering your username and password into the GlobalProtect VPN client over and over and over and over and over again? Yeah, me too. With `easy-vpn`, those days are over. Now you can just open your terminal window and type `vpn login` and you're done. Nice, right?

# Ok, so how do I use easy-vpn?

You have two options...

## Option 1 (Consumer Instructions)

1. Download this repository as a zip file and extract it to a location of your choosing. (I like `C:\tools\easy-vpn`)
2. [Add that path to your system path variable][path]\*.
3. Load your terminal and type `vpn creds -username <YOUR_USERNAME>`. When you're prompted to enter your password, enter that too. This will encrypt your credentials and store then locally on your machine.\*\*
4. Enter `vpn login` and you *should* be good to go.\*\*\*

## Option 2 (Developer Instructions)

1. Create a directory structure like this: `C:\tools\easy-vpn`.
1. Clone this repo.
1. Open `easy-vpn.sln`.
1. Set the solution configuration to `Release` and build it. This will compile the application and xcopy the artifacts to `C:\tools\easy-vpn`.
1. Add `C:\tools\easy-vpn` to your system path.
1. Load your terminal and type `vpn creds -username <YOUR_USERNAME>`. When you're prompted to enter your password, enter that too. This will encrypt your credentials and store then locally on your machine.\*\*
1. Enter `vpn login` and you *should* be good to go.\*\*\*

<hr />

\* Be sure **NOT** to include a trailing slash on the path that you add to your path variable. **Correct:** `C:\tools\easy-vpn`. **Incorrect:** `C:\tools\easy-vpn\`.

\*\* I've made some *minimal* safeguards to keep credentials safe, but don't assume this is a super secure program. Credentials are stored encrypted on disk, but are decrypted when loaded by `easy-vpn`. Anyone with a debugger who knows what they're looking at can easily decrypt the files. Of course, in order to do that, an attacker would need direct access to your machine in the first place. You'd never forget to lock your computer, would you? ;) In any case, there's still 2FA to contend with, right?

\*\*\* This entire program is essentially a big hack, so don't be surprised if it's not 100% reliable. Let me know if you have any problems with it, and I'll try to fix it.

[path]: http://bit.ly/2nSIvGI
