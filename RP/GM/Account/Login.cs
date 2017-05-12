using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Shared.Math;
using GrandTheftMultiplayer.Server.Managers;
using BCr = BCrypt.Net;
using MySql.Data.MySqlClient;
using Insight.Database.Providers.MySql;
using Insight.Database;
using Insight;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Roleplay;
using Roleplay.Connection;

namespace Roleplay.LoginHandle
{
    public class Login : Script
    {
        [Command("login", "Usage: /login [email] [password]", SensitiveInfo = true, GreedyArg = true)]
        public void CMD_userlogin(Client player, string Email, string Password)
        {
            UserAccount account = Main._userRepository.GetAccount(Email);

            bool isPasswordCorrect = BCr.BCrypt.Verify(Password, account.Hash);
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

        private void API_onClientEventTrigger(Client sender, string eventName, params object[] arguments)
        {
            switch (eventName)
            {
                case "loginscript_login":
                    ULogin(sender, arguments[0].ToString(), arguments[1].ToString());
                    break;
            }
        }

        public event ExportedEvent OnUserLoggedIn;

        public void ULogin(Client p, string Email, string Password)
        {
            MySqlConnection _connection = API.exported.spl_mysql.GetConnection();
            string query = "SELECT * FROM users WHERE email=@name LIMIT 1";
            MySqlCommand _cmd = new MySqlCommand(query, _connection);
            _cmd.Parameters.AddWithValue("@name", Email);
            using (MySqlDataReader reader = _cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    var pw = reader.GetString("hash");
                    if (ValidatePassword(Password, pw))
                    {
                        API.sendChatMessageToPlayer(p, "~g~Logged in as ~r~" + reader.GetString("email"));
                        API.freezePlayer(p, false);
                        API.setEntitySyncedData(p, "loginscript_logged_in", true);
                        API.setEntitySyncedData(p, "user_id", reader.GetInt32("id"));
                        API.triggerClientEvent(p, "loginscript_loginsuccess");
                        API.triggerClientEvent(p, "CEFDestroy");
                        reader.Close();
                        OnUserLoggedIn(p);
                    }
                    else
                        API.triggerClientEvent(p, "loginscript_loginfailed");
                        API.sendChatMessageToPlayer(p, "Login failed");
                }
                else
                    API.triggerClientEvent(p, "loginscript_loginfailed");
                    API.sendChatMessageToPlayer(p, "Login failed");
            }
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
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