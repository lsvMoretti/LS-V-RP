using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Shared.Math;
using GrandTheftMultiplayer.Server.Managers;
using MySql.Data.MySqlClient;
using CryptSharp;
using Insight.Database.Providers.MySql;
using Insight.Database;
using Insight;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Roleplay;
using Roleplay.Connection;
using Roleplay.Context;
using Roleplay.User;
using Roleplay.CharSel;

namespace Roleplay.LoginHandle
{
    public class Login : Script
    {
        public Login()
        {
            API.onClientEventTrigger += API_onClientEventTrigger;
            //API.onPlayerDisconnected += API_onPlayerDisconnected;
        }

        /*public void API_onPlayerDisconnected(Client player, string reason)
        {
            if(API.getEntitySyncedData(player, "logged_in"))
            {
                API.setEntitySyncedData(player, "logged_in", false);
            }
        }*/

        [Command("login", "Usage: /login [email] [password]", GreedyArg = true)]
        public void CMD_userlogin(Client player, string Email, string Password)
        {
            ULogin(player, Email, Password);
        }

        public void API_onClientEventTrigger(Client sender, string eventName, params object[] arguments)
        {
            switch (eventName)
            {
                case "loginscript_login":
                    ULogin(sender, arguments[0].ToString(), arguments[1].ToString());
                    API.shared.consoleOutput("loginscript");
                    break;
            }
        }

        public void ULogin(Client p, string Email, string Password)
        {
            var User = ContextFactory.Instance.Users.FirstOrDefault(up => up.Email == Email);
            bool isPasswordCorrect = Crypter.CheckPassword(Password, User.Hash);

            if (isPasswordCorrect)
            {
                API.sendChatMessageToPlayer(p, "You're now logged in!");
                //API.triggerClientEvent(p, "loginscript_loginsuccess");
                //OnUserLoggedIn(p);
                API.setEntitySyncedData(p, "logged_in", true);
                User.Social = p.socialClubName;
                API.sendChatMessageToPlayer(p, "Your account ID is: " + User.ID + ".");
                API.setEntitySyncedData(p, "ID", User.ID);
                API.triggerClientEvent(p, "CEFDestroy");
                ContextFactory.Instance.SaveChanges();
                Logged(p, User.ID);
                API.shared.consoleOutput("ULogin");

            }
            else
            {
                API.sendChatMessageToPlayer(p, "Wrong password!");
                API.triggerClientEvent(p, "loginscript_loginfailed");
            }
        }

        public void Logged(Client p, int AID)
        {
            CharSel.Selection.CharacterMenu(p, AID);
            API.shared.consoleOutput("Logged()");
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return Crypter.CheckPassword(password, correctHash);
        }

        /*  [Command("register", "Usage: /register [email] [password]", SensitiveInfo = true, GreedyArg = true)]
          public void CMD_userregister(Client player, string email, string password)
          {
              var hash = BCr.BCrypt.HashPassword(password, BCr.BCrypt.GenerateSalt(12));

              if (DoesAccountExist(email))
              {
                  API.sendChatMessageToPlayer(player, "~W~Server: An account already exists, please use /login to continue!");
              }
              UserAccount account = new UserAccount
              {
                  Email = email,
                  Hash = hash,
                  Social = player.socialClubName
              };
              Roleplay.Main._userRepository.RegisterAccount(account);
              API.sendChatMessageToPlayer(player, "You're now registered, you may login.");
          }*/


    }

    public class UserAccount
    {
        public string ID { get; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Social { get; set; }

    }
}