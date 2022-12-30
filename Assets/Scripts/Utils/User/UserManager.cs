using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Utils.Singleton;

namespace Utils.User
{
    public class UserManager : Singleton<UserManager>
    {
        #region Private

        [SerializeField] private User user;


        #region Singleton<UserManager>

        public override void LoadOnUpdateInterval()
        {
        }

        protected override void OnLoadSync()
        {
            LoadUser();
            ActionHandler.onOnlineAuthSuccess += LoadUser;
            ActionHandler.onOnlineRegistration += SaveUserAll;
            ActionHandler.onNewUser += CheckForDailyQuests;
        }

        protected override bool IsAsync()
        {
            return true;
        }

        private void OnDestroy()
        {
            ActionHandler.onOnlineAuthSuccess -= LoadUser;
            ActionHandler.onOnlineRegistration -= SaveUserAll;
            ActionHandler.onNewUser -= CheckForDailyQuests;
        }

        #endregion

        #region User

        private void SaveUserAll()
        {
            SaveUser(new[]
            {
                UserSaveType.MetaData,
                UserSaveType.Quests,
                UserSaveType.Money
            });
        }

        private void SaveUser(UserSaveType[] saveTypes)
        {
            user.metaData.UpdateLastSave();
            try
            {
                if (UserOnlineService.IsUserOnline())
                {
                    UserOnlineService.SaveUserOnline(saveTypes);
                }

                UserLocalService.SaveUserLocal(user);
            }
            catch (Exception e)
            {
                Debug.LogError("SaveUser failed: " + e);
            }
        }


        private async void LoadUser()
        {
            var localUser = UserLocalService.LoadUserLocal();

            if (UserOnlineService.IsUserOnline())
            {
                var onlineUser = await UserOnlineService.LoadUserOnline();
                user = IsLocalUserNewer(localUser, onlineUser);
                SaveUserAll();
            }
            else
            {
                user = localUser;
            }

            if (user != null)
            {
                CheckForDailyQuests();
                ActionHandler.onUserLogin?.Invoke();
            }

            isReady = true;
        }

        private static User IsLocalUserNewer(User localUser, User onlineUser)
        {
            if (localUser == null) return onlineUser;
            if (onlineUser == null || onlineUser.metaData.lastSave == 0 || onlineUser.metaData.created == 0)
                return localUser;

            var onlineCreated = DateTime.FromFileTime(onlineUser.metaData.created);
            var localCreated = DateTime.FromFileTime(localUser.metaData.created);

            if (onlineCreated != localCreated) return onlineUser;

            var onlineLastSave = DateTime.FromFileTime(onlineUser.metaData.lastSave);
            var localLastSave = DateTime.FromFileTime(localUser.metaData.lastSave);

            return onlineLastSave != localLastSave
                ? DecideWhichUserIsCorrect(localUser, onlineUser)
                : onlineUser;
        }

        private static User DecideWhichUserIsCorrect(User localUser, User onlineUser)
        {
            // TODO: Custom Logic to define which is the correct user or save file
            return onlineUser;
        }

        #endregion

        #region Quests

        private void CheckForDailyQuests()
        {
            var daysDifference = GetDaysBetweenNowAndLastDailyLogin();
            if (daysDifference < 1) return;
            user.quest.GenerateNewDailies();
            user.metaData.UpdateLastDailyLogin();
            SaveUser(new[] { UserSaveType.MetaData, UserSaveType.Quests });
            ActionHandler.onDailyQuestChange?.Invoke();
        }

        private int GetDaysBetweenNowAndLastDailyLogin()
        {
            int difference;
            if (user.quest.dailies.Count == 0)
            {
                difference = 1;
            }
            else if (user.metaData.lastDailyLogin != 0)
            {
                var lastLogin = DateTime.FromFileTime(user.metaData.lastDailyLogin);
                difference = (DateTime.Now - lastLogin).Days;
            }
            else
            {
                difference = 1;
            }

            return difference;
        }

        #endregion

        #endregion

        #region Public

        public User GetUser()
        {
            return user;
        }

        public void IncreaseMoney(int amount)
        {
            ActionHandler.onMoneyIncrease?.Invoke(amount);
            user.money.money += amount;
            user.money.moneyEarned += amount;
            user.money.moneyEarnedToday += amount;
            ActionHandler.onMoneyChange?.Invoke(user.money.money);
        }

        public void DecreaseMoney(int amount)
        {
            ActionHandler.onMoneyDecrease?.Invoke(amount);
            user.money.money += amount;
            user.money.moneyEarned += amount;
            user.money.moneyEarnedToday += amount;
            ActionHandler.onMoneyChange?.Invoke(user.money.money);
        }

        #endregion
    }
}