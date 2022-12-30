using System;
using UnityEngine;

namespace Utils.User
{
    [Serializable]
    public class User
    {
        /**
         * -TIPS- 
         * 
         * Split your user information, because:
         * 1) it is easier to maintain, e.g. better visibility in unity editor
         * 2) if you save it online in a data base you can reduce amount of send or received data
         * by sending only changed information
         * 
         * Think about the information you need to save.
         * If you add online features you don't need to save something like
         * {"id": "WEAPON_LONG_SWORD_PLUS_3_ATTACK"}
         * instead you could shorten it
         * {"id": "W_LS_3A"}
         */
        public UserMoney money;

        public UserMetaData metaData;
        public UserQuest quest;

        public User()
        {
            money = new UserMoney();
            metaData = new UserMetaData();
            quest = new UserQuest();
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}