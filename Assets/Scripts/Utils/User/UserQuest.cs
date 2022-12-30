using System;
using System.Collections.Generic;

namespace Utils.User
{
    [Serializable]
    public class UserQuest
    {
        public List<string> dailies;

        public UserQuest()
        {
            dailies = new List<string>();
        }

        public void GenerateNewDailies()
        {
            // TODO
        }
    }
}