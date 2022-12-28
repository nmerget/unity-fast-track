using System.Threading.Tasks;

namespace Utils.Player
{
    public static class PlayerOnlineService
    {
        public static void SavePlayerOnline(PlayerSaveType[] saveTypes)
        {
            // TODO: Save User Online
        }

        public static Task<Player> LoadPlayerOnline()
        {
            var onlinePlayer = new Player();
            // TODO: Load all data from database
            return Task.FromResult(onlinePlayer);
        }

        public static bool IsUserOnline()
        {
            // TODO: Handle this
            return false;
        }
    }
}