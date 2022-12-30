using System.Threading.Tasks;

namespace Utils.User
{
    public static class UserOnlineService
    {
        public static void SaveUserOnline(UserSaveType[] saveTypes)
        {
            // TODO: Save User Online
        }

        public static Task<User> LoadUserOnline()
        {
            var onlineUser = new User();
            // TODO: Load all data from database
            return Task.FromResult(onlineUser);
        }

        public static bool IsUserOnline()
        {
            // TODO: Handle this
            return false;
        }
    }
}