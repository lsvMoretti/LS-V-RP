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
using Roleplay.Character;
using Roleplay.Server;
using System;

namespace Roleplay.CharSel
{
    public class Selection : Script
    {
        public static Characters Character = new Characters();
        public static Users User = new Users();
        public Client Client { get; private set; }

        public static void CharacterMenu(Client player, int UAID)
        {
            var CharacterMenuEntries = new List<string>();
            var Characters = ContextFactory.Instance.Characters.Where(t => t.AID == UAID).ToList();
            if (Characters.Count == 0)
            {
                CharacterMenuEntries.Add("Create Character");
                API.shared.triggerClientEvent(player, "create_menu", 0, "LS-V", "Characters", true, CharacterMenuEntries.ToArray());
            }
            else
            {
                foreach (var character in Characters)
                {
                    CharacterMenuEntries.Add(character.Name.Replace("_", " "));
                }
                CharacterMenuEntries.Add("Create new character");
                API.shared.triggerClientEvent(player, "create_menu", 0, "LS-V", "Characters", true, CharacterMenuEntries.ToArray());
            }
        }

        public static void CreateCharacter(Client player)
        {
            //API.shared.triggerClientEvent(player, "CEFController", "Insert Create Char CEF");
            API.shared.sendChatMessageToPlayer(player, "Please use /createchar.");
        }

        public void CharacterController(Client player, string name)
        {
            int AID = API.shared.getEntitySyncedData(player, "ID");
            //Character.AID = User.ID;
            Character.AID = AID;
            Character.Name = name;
            Character.RegisterDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            Character.Model = PedHash.DrFriedlander.GetHashCode(); //Global.GlobalVars._defaultPedModel.GetHashCode();
            Character.ModelName = "DrFriedlander";
            ContextFactory.Instance.Characters.Add(Character);
            ContextFactory.Instance.SaveChanges();
            CharacterMenu(player, AID);
        }

        [Command("createchar", "Usage: /createchar [Name]", GreedyArg = true)]
        public void CMD_CreateCharacter(Client player, string Name)
        {
            CharacterController(player, Name);
            API.shared.sendChatMessageToPlayer(player, "You have created a character named: " + Name + ".");
        }

        public static void SelectCharacter(Client Player, int selectId)
        {
            /* AccountController account = player.getData("ACCOUNT");
             if (account == null) return;
             */
           /* bool isPlayerLogged = API.shared.getEntitySyncedData(Player, "LOGGED_IN");
            if (!isPlayerLogged)
            {
                return;
            }*/
            int UID = API.shared.getEntitySyncedData(Player, "ID");
            var Characters = ContextFactory.Instance.Characters.Where(t => t.AID == UID).ToList();
            if (Characters.Count == 0)
            {
                API.shared.sendChatMessageToPlayer(Player, "You have no characters.");
            }
            else
            {
                /*int characterid = Characters.ToList()[selectId - 1].ID;
                Character characterData = ContextFactory.Instance.Characters.FirstOrDefault(x => x.ID == characterid);
                CharacterController characterController = new CharacterController(account, characterData);
                characterController.LoginCharacter(account);*/
                LoginCharacter(Player, UID);
                API.shared.consoleOutput("Login Character");
            }
        }

        public static void LoginCharacter(Client Player, int UID)
        {
            /*AccountController = accountController;
            accountController.CharacterController = this;*/
            
            SpawnManager.SpawnCharacter(this);
            API.shared.consoleOutput("SpawnCharacter");
            API.shared.triggerClientEvent(Player, "stopAudio");
            Player.freeze(false);
            Player.transparency = 255;
            //Player.nametag = FormatName + " (" + accountController.PlayerId + ")";
            API.shared.triggerClientEvent(Player, "update_money_display", Character.Cash);
            API.shared.sendChatMessageToPlayer(Player, "Your character ID is: " + Character.AID + ".");
        }

        private static readonly Vector3 _newPlayerPosition = new Vector3(-1034.794, -2727.422, 13.75663);
        private static readonly Vector3 _newPlayerRotation = new Vector3(0.0, 0.0, -34.4588);
        //private static readonly int _newPlayerDimension = 0;
        
    }
}
