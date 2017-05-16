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

namespace Roleplay.CharSel
{
    public class Selection
    {
        public static void CharacterMenu(Client player, int AID)
        { 
            var CharacterMenuEntries = new List<string>();
            var Characters = ContextFactory.Instance.Characters.Where(t => t.ID == AID).ToList();
            if (Characters.Count == 0)
            {
                CharacterMenuEntries.Add("Create Character");
                API.shared.triggerClientEvent(player, "create_menu", 0, null, "Characters", true, CharacterMenuEntries.ToArray());
            }
            else
            {
                foreach (var character in Characters)
                {
                    CharacterMenuEntries.Add(character.Name.Replace("_", " "));
                }
                CharacterMenuEntries.Add("Create new character");
                API.shared.triggerClientEvent(player, "create_menu", 0, null, "Characters", true, CharacterMenuEntries.ToArray());
            }
        }
        public static void CreateCharacter(Client player)
        {
            API.shared.triggerClientEvent(player, "CEFController", "Insert Create Char CEF");
        }

        public CharacterController(Client player, string name)
        {
            int AID = API.shared.getEntitySyncedData(player, "ID");
            Characters.ID = AID;
            Characters.Name = name;
            Characters.RegisterDate = System.DateTime.Now;
            Characters.Model = PedHash.DrFriedlander.GetHashCode(); //Global.GlobalVars._defaultPedModel.GetHashCode();
            Characters.ModelName = "DrFriedlander";
            ContextFactory.Instance.Characters.Add();
            ContextFactory.Instance.SaveChanges();
        }

        public static void SelectCharacter(Client player, int selectId)
        {
            /* AccountController account = player.getData("ACCOUNT");
             if (account == null) return;
             */
            int AID = API.shared.getEntitySyncedData(player, "ID");
            var Characters = ContextFactory.Instance.Characters.Where(t => t.ID == AID).ToList();
            if (Characters.Count == 0)
            {
                API.shared.sendChatMessageToPlayer(player, "You have no characters.");
            }
            else
            {
                int characterid = Characters.ToList()[selectId - 1].ID;
                /*Character characterData = ContextFactory.Instance.Characters.FirstOrDefault(x => x.ID == characterid);
                CharacterController characterController = new CharacterController(account, characterData);
                characterController.LoginCharacter(account);*/
            }
        }
    }
}
