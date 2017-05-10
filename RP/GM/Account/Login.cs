using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Shared.Math;
using GrandTheftMultiplayer.Server.Managers;
using BCr = BCrypt.Net;

namespace Roleplay.LoginHandle
{
    public class Login : Script
    {
        [Command("login", "Usage: /login [email] [password]", SensitiveInfo = true, GreedyArg = true)]
        public void CMD_userlogin(Client player, string email, string password)
        {
            UserAccount account = Roleplay.Main._userRepository.GetAccount(email);

            bool isPasswordCorrect = BCr.BCrypt.Verify(password, account.Hash);
            if (isPasswordCorrect)
            {
                API.sendChatMessageToPlayer(player, "You are now logged in!");
                API.setEntitySyncedData(player, "LOGGED", true);
            }
            else
            {
                API.sendChatMessageToPlayer(player, "Wrong Password!");
            }
            /* bool isPlayerLogged = API.getEntitySyncedData(player, "LOGGED");
             if (isPlayerLogged)
             {
                 if (isPasswordCorrect)
                 {
                     API.sendChatMessageToPlayer(player, "You are now logged in!");
                     API.setEntitySyncedData(player, "LOGGED", true);
                 }
                 else
                 {
                     API.sendChatMessageToPlayer(player, "Wrong Password!");
                 }
             }
             else
             {
                 API.sendChatMessageToPlayer(player, "You are already logged in.");
             }*/
        }

        [Command("register", "Usage: /register [email] [password]", SensitiveInfo = true, GreedyArg = true)]
        public void CMD_userregister(Client player, string email, string password)
        {
            var hash = BCr.BCrypt.HashPassword(password, BCr.BCrypt.GenerateSalt(12));
           // UserAccount acc = Roleplay.Main._userRepository.GetAccount(email);

            UserAccount account = new UserAccount
            {
                Email = email,
                Hash = hash,
                Social = player.socialClubName
            };
            Roleplay.Main._userRepository.RegisterAccount(account);
            API.sendChatMessageToPlayer(player, "You're now registered, you may login.");
            /*           bool EmailExist = email == acc.Email;

                       if (EmailExist)
                       {
                           API.sendChatMessageToPlayer(player, "An account already exists with this email!");
                           API.setEntitySyncedData(player, "EXIST", true);
                       }

                       else if (!EmailExist)
                       {
                           UserAccount account = new UserAccount
                           {
                               Email = email,
                               Hash = hash,
                               Social = player.socialClubName
                           };
                           Roleplay.Main._userRepository.RegisterAccount(account);
                           API.sendChatMessageToPlayer(player, "You're now registered, you may login.");
                       }   */
        }
    }
    

    public class UserAccount
    {
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Social { get; set; }

    }
}