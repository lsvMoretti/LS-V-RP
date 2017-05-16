using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Shared.Math;
using GrandTheftMultiplayer.Server.Managers;
using MySql.Data.MySqlClient;
using Insight.Database.Providers.MySql;
using Insight.Database;
using Roleplay.LoginHandle;
using System.Linq;
using Roleplay.Context;

namespace Roleplay
{
    public class Main : Script
    {
        public static MySqlConnectionStringBuilder _database;
        public static IUserRepository _userRepository;
        public Main()
        {   
            API.onResourceStart += API_onResourceStart;
        }

        public void API_onResourceStart()
        {
            MySqlInsightDbProvider.RegisterProvider();
            _database = new MySqlConnectionStringBuilder("server=localhost;user=root;database=gamemode;port=3306;password=;");
            _userRepository = _database.Connection().As<IUserRepository>();
            ContextFactory.SetConnectionParameters("127.0.0.1", "root", "", "gamemode");
            var uniqueUsers = ContextFactory.Instance.Users.Count();
            API.consoleOutput("Unique Accounts: " + uniqueUsers);
            var uniqueChars = ContextFactory.Instance.Characters.Count();
            API.consoleOutput("Character Count: " + uniqueChars);

        }

    }
    public interface IUserRepository
    {
        UserAccount RegisterAccount(UserAccount userAccount);
        UserAccount GetAccount(string name);
        UserAccount SaveAccount(string name);
    }
}
