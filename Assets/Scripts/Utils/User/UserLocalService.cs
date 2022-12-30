using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Utils.User
{
    public static class UserLocalService
    {
        private const string UserStatsSavePath = "/unimportantFileYouCanDeleteIt.not";

        public static void SaveUserLocal(User user)
        {
            var formatter = new BinaryFormatter();
            var file = new StreamWriter(Application.persistentDataPath + UserStatsSavePath);
            var ms = new MemoryStream();
            var json = JsonUtility.ToJson(user);
            formatter.Serialize(ms, json);
            // TODO: You can use some encryption if you want to make it harder to manipulate
            var a = Convert.ToBase64String(ms.ToArray());
            file.WriteLine(a);
            file.Close();
        }


        public static User LoadUserLocal()
        {
            User localUser = null;
            try
            {
                if (File.Exists(Application.persistentDataPath + UserStatsSavePath))
                {
                    var formatter = new BinaryFormatter();
                    var file = new StreamReader(Application.persistentDataPath + UserStatsSavePath);
                    var a = file.ReadToEnd();
                    var ms = new MemoryStream(Convert.FromBase64String(a));
                    var userJson = formatter.Deserialize(ms) as string;
                    localUser = JsonUtility.FromJson<User>(userJson);
                    file.Close();
                }
                else
                {
                    ActionHandler.onNewUser?.Invoke();
                    localUser = new User();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("LoadUserLocal failed: " + e);
            }

            return localUser;
        }

        public static void DeleteLocalUser()
        {
            try
            {
                if (!File.Exists(Application.persistentDataPath + UserStatsSavePath)) return;
                
                File.Delete(Application.persistentDataPath + UserStatsSavePath);
                    
                ActionHandler.onDeleteUser?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError("DeleteLocalUser failed: " + e);
            }
        }
    }
}