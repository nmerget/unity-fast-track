using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;
using Utils.Singleton;

namespace Utils.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        #region Private

        private const string PlayerStatsSavePath = "/unimportantFileYouCanDeleteIt.not";

        [SerializeField] private Player player;


        #region Singleton<PlayerManager>

        public override void LoadOnUpdateInterval()
        {
        }

        protected override void OnLoadSync()
        {
            LoadPlayer();
            EventHandler.onOnlineAuthSuccess += LoadPlayer;
            EventHandler.onOnlineRegistration += SavePlayerAll;
            EventHandler.onNewPlayer += CheckForDailyQuests;
        }

        protected override bool IsAsync()
        {
            return true;
        }

        private void OnDestroy()
        {
            EventHandler.onOnlineAuthSuccess -= LoadPlayer;
            EventHandler.onOnlineRegistration -= SavePlayerAll;
            EventHandler.onNewPlayer -= CheckForDailyQuests;
        }

        #endregion

        #region Player

        private void SavePlayerAll()
        {
            SavePlayer(new[]
            {
                PlayerSaveType.MetaData,
                PlayerSaveType.Quests,
                PlayerSaveType.Money
            });
        }

        private void SavePlayer(PlayerSaveType[] saveTypes)
        {
            player.metaData.UpdateLastSave();
            try
            {
                if (PlayerOnlineService.IsUserOnline())
                {
                    PlayerOnlineService.SavePlayerOnline(saveTypes);
                }

                PlayerLocalService.SavePlayerLocal(player);
            }
            catch (Exception e)
            {
                Debug.LogError("SavePlayer failed: " + e);
            }
        }


        private async void LoadPlayer()
        {
            var localPlayer = PlayerLocalService.LoadPlayerLocal();

            if (PlayerOnlineService.IsUserOnline())
            {
                var onlinePlayer = await PlayerOnlineService.LoadPlayerOnline();
                player = IsLocalPlayerNewer(localPlayer, onlinePlayer);
                SavePlayerAll();
            }
            else
            {
                player = localPlayer;
            }

            if (player != null)
            {
                CheckForDailyQuests();
                EventHandler.onPlayerLogin?.Invoke();
            }

            isReady = true;
        }

        private static Player IsLocalPlayerNewer(Player localPlayer, Player onlinePlayer)
        {
            if (localPlayer == null) return onlinePlayer;
            if (onlinePlayer == null || onlinePlayer.metaData.lastSave == 0 || onlinePlayer.metaData.created == 0)
                return localPlayer;

            var onlineCreated = DateTime.FromFileTime(onlinePlayer.metaData.created);
            var localCreated = DateTime.FromFileTime(localPlayer.metaData.created);

            if (onlineCreated != localCreated) return onlinePlayer;

            var onlineLastSave = DateTime.FromFileTime(onlinePlayer.metaData.lastSave);
            var localLastSave = DateTime.FromFileTime(localPlayer.metaData.lastSave);

            return onlineLastSave != localLastSave
                ? DecideWhichPlayerIsCorrect(localPlayer, onlinePlayer)
                : onlinePlayer;
        }

        private static Player DecideWhichPlayerIsCorrect(Player localPlayer, Player onlinePlayer)
        {
            // TODO: Custom Logic to define which is the correct player/ save file
            return onlinePlayer;
        }

        #endregion

        #region Quests

        private void CheckForDailyQuests()
        {
            var daysDifference = GetDaysBetweenNowAndLastDailyLogin();
            if (daysDifference < 1) return;
            player.quest.GenerateNewDailies();
            player.metaData.UpdateLastDailyLogin();
            SavePlayer(new[] { PlayerSaveType.MetaData, PlayerSaveType.Quests });
            EventHandler.onDailyQuestChange?.Invoke();
        }

        private int GetDaysBetweenNowAndLastDailyLogin()
        {
            int difference;
            if (player.quest.dailies.Count == 0)
            {
                difference = 1;
            }
            else if (player.metaData.lastDailyLogin != 0)
            {
                var lastLogin = DateTime.FromFileTime(player.metaData.lastDailyLogin);
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

        public Player GetPlayer()
        {
            return player;
        }

        public void IncreaseMoney(int amount)
        {
            EventHandler.onMoneyIncrease?.Invoke(amount);
            player.money.money += amount;
            player.money.moneyEarned += amount;
            player.money.moneyEarnedToday += amount;
            EventHandler.onMoneyChange?.Invoke(player.money.money);
        }

        public void DecreaseMoney(int amount)
        {
            EventHandler.onMoneyDecrease?.Invoke(amount);
            player.money.money += amount;
            player.money.moneyEarned += amount;
            player.money.moneyEarnedToday += amount;
            EventHandler.onMoneyChange?.Invoke(player.money.money);
        }

        #endregion
    }
}