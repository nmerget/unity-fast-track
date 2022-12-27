using System;

namespace Utils.Player
{
    [Serializable]
    public class PlayerMoney
    {
        public int money = 100;
        public int moneyEarned;
        public int moneySpent;
        public int moneyEarnedToday;
        public int moneySpentToday;
    }
}