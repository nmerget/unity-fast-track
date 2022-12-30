using System;

namespace Utils.User
{
    [Serializable]
    public class UserMetaData
    {
        public long lastDailyLogin;
        public long lastSave;
        public long created;

        public UserMetaData()
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