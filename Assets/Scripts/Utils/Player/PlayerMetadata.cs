using System;

namespace Utils.Player
{
    [Serializable]
    public class PlayerMetaData
    {
        public long lastDailyLogin;
        public long lastSave;
        public long created;

        public PlayerMetaData()
        {
            var now = DateTime.Now.ToFileTime();
            lastDailyLogin = now;
            lastSave = now;
            created = now;
        }

        public void UpdateLastSave()
        {
            lastSave = DateTime.Now.ToFileTime();
        }

        public void UpdateLastDailyLogin()
        {
            lastDailyLogin = DateTime.Now.ToFileTime();
        }
    }
}