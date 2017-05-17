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
using Roleplay.CharSel;
using Roleplay.Character;

namespace Roleplay.Server
{
    public class SpawnManager : Script
    {

        private static readonly Vector3 _newPlayerPosition = new Vector3(-1034.794, -2727.422, 13.75663);
        private static readonly Vector3 _newPlayerRotation = new Vector3(0.0, 0.0, -34.4588);
        private static readonly int _newPlayerDimension = 0;

        

        public SpawnManager()
        {
        }

        public static Characters Character = new Characters();

        public static void SpawnCharacter(Selection character)
        {
            Client target = character.Client;

            API.shared.triggerClientEvent(character.Client, "destroyCamera");
            API.shared.consoleOutput("SpawnCharacter1");

            API.shared.setPlayerSkin(character.Client, (PedHash)Character.Model);

            API.shared.resetPlayerNametagColor(character.Client);
            API.shared.removeAllPlayerWeapons(character.Client);

            if (Character.RegistrationStep == 0)
            {
                API.shared.setEntityPosition(character.Client, _newPlayerPosition);
                API.shared.setEntityRotation(character.Client, _newPlayerRotation);
                Character.RegistrationStep = -1; // 'Tutorial Done'
                Character.ModelName = API.shared.getEntityModel(character.Client).ToString();
            }
            else
            {
                API.shared.setEntityPosition(character.Client, new Vector3(Character.PosX, Character.PosY, Character.PosZ));
                API.shared.setEntityRotation(character.Client, new Vector3(0.0f, 0.0f, Character.Rot));
                Character.ModelName = API.shared.getEntityModel(character.Client).ToString();

            }

            ContextFactory.Instance.SaveChanges();
        }


        public static Vector3 GetSpawnPosition() { return _newPlayerPosition; }
        public static int GetSpawnDimension() { return _newPlayerDimension; }
    }
}