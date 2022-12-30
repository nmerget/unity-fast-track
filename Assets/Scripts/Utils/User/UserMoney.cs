using System;

namespace Utils.User
{
    [Serializable]
    public class UserMoney
    {
        public int money = 100;
        public int moneyEarned;
        public int moneySpent;
        public int moneyEarnedToday;
        public int moneySpentToday;
    }
}