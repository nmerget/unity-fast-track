using System;
using System.Collections.Generic;

namespace Utils.Player
{
    [Serializable]
    public class PlayerQuest
    {
        public List<string> dailies;

        public PlayerQuest()
        {
            dailies = new List<string>();
        }

        public void GenerateNewDailies()
        {
            // TODO
        }
    }
}