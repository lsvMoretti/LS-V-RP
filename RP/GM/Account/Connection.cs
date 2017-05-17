using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Shared.Math;
using GrandTheftMultiplayer.Server.Managers;


namespace Roleplay.Connection
{
    public class Connection : Script
    {

        
        public static readonly Vector3 _startPos = new Vector3(3433.339f, 5177.579f, 39.79541f);
        public static readonly Vector3 _startCamPos = new Vector3(3476.85f, 5228.022f, 9.453369f);

        public Connection()
        {
            API.onPlayerFinishedDownload += API_onPlayerfinishedDownload;
            
            API.onPlayerConnected += API_onPlayerConnected;
            
        }

        public void API_onPlayerfinishedDownload(Client player)
        {
            API.setEntityData(player, "DOWNLOAD FINISHED", true);
            API.setEntitySyncedData(player, "LOGGED", false);
            Login(player);
        }

        public void API_onPlayerConnected(Client player)
        {
            API.setEntitySyncedData(player, "LOGGED", false);
        }
      
        public static void Login(Client player)
        {
            API.shared.triggerClientEvent(player, "interpolateCamera", 20000, _startCamPos, _startCamPos + new Vector3(0.0, -50.0, 50.0), new Vector3(0.0, 0.0, 180.0), new Vector3(0.0, 0.0, 95.0));
            player.position = _startPos;
            player.freeze(true);
            player.transparency = 0;
            PromptLoginScreen(player);
            
        }

        public static void PromptLoginScreen(Client player)
        {
            string url = "Clientside/Resources/boilerplate.html";
            API.shared.triggerClientEvent(player, "CEFController", url);
            API.shared.sendChatMessageToPlayer(player, "Welcome to" + GlobalVars.Vars.ServerName + ".");
            API.shared.sendChatMessageToPlayer(player, "If the CEF hasn't loaded within 15 seconds, please use /login.");
        }
    }
}

    
