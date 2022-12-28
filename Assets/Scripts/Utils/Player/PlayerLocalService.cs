using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Utils.Player
{
    public static class PlayerLocalService
    {
        private const string PlayerStatsSavePath = "/unimportantFileYouCanDeleteIt.not";

        public static void SavePlayerLocal(Player player)
        {
            var formatter = new BinaryFormatter();
            var file = new StreamWriter(Application.persistentDataPath + PlayerStatsSavePath);
            var ms = new MemoryStream();
            var json = JsonUtility.ToJson(player);
            formatter.Serialize(ms, json);
            // TODO: You can use some encryption if you want to make it harder to manipulate
            var a = Convert.ToBase64String(ms.ToArray());
            file.WriteLine(a);
            file.Close();
        }


        public static Player LoadPlayerLocal()
        {
            Player localPlayer = null;
            try
            {
                if (File.Exists(Application.persistentDataPath + PlayerStatsSavePath))
                {
                    var formatter = new BinaryFormatter();
                    var file = new StreamReader(Application.persistentDataPath + PlayerStatsSavePath);
                    var a = file.ReadToEnd();
                    var ms = new MemoryStream(Convert.FromBase64String(a));
                    var playerJson = formatter.Deserialize(ms) as string;
                    localPlayer = JsonUtility.FromJson<Player>(playerJson);
                    file.Close();
                }
                else
                {
                    EventHandler.onNewPlayer?.Invoke();
                    localPlayer = new Player();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("LoadPlayer failed: " + e);
            }

            return localPlayer;
        }

        public static void DeleteLocalPlayer()
        {
            try
            {
                if (!File.Exists(Application.persistentDataPath + PlayerStatsSavePath)) return;
                
                File.Delete(Application.persistentDataPath + PlayerStatsSavePath);
                    
                EventHandler.onDeletePlayer?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError("Player deleting failed: " + e);
            }
        }
    }
}