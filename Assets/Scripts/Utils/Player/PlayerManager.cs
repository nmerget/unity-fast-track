using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {

    #region Private
    private static string PLAYER_STATS_SAVE_PATH = "/unimportantFileYouCanDeleteIt.not";

    [SerializeField] private Player player;

    private enum PlayerSaveType { META_DATA, MONEY, QUESTS }

    #region Singleton<PlayerManager>

    public override void LoadOnUpdateIntervall () { }

    protected override void OnLoadSync () {
        this.LoadPlayer ();
        EventHandler.onOnlineAuthSuccess += this.LoadPlayer;
        EventHandler.onOnlineRegistration += this.SavePlayerAll;
    }

    protected override bool IsAsync () {
        return true;
    }
    private void OnDestroy () {
        EventHandler.onOnlineAuthSuccess -= this.LoadPlayer;
        EventHandler.onOnlineRegistration -= this.SavePlayerAll;
    }
    #endregion

    #region SavePlayer
    private void SavePlayerAll () {
        this.SavePlayer (new PlayerSaveType[] {
            PlayerSaveType.META_DATA,
                PlayerSaveType.QUESTS,
                PlayerSaveType.MONEY
        });
    }
    private void SavePlayer (PlayerSaveType[] saveTypes) {
        this.player.metaData.UpdateLastSave ();
        try {
            if (this.IsUserOnline ()) {
                SavePlayerOnline (saveTypes);
            }
            SavePlayerLocal ();
        } catch (Exception e) {
            Debug.LogError ("SavePlayer failed: " + e);
        }
    }

    private void SavePlayerLocal () {
        BinaryFormatter formatter = new BinaryFormatter ();
        StreamWriter file = new StreamWriter (Application.persistentDataPath + PLAYER_STATS_SAVE_PATH);
        MemoryStream ms = new MemoryStream ();
        string json = JsonUtility.ToJson (this.player);
        formatter.Serialize (ms, json);
        string a = System.Convert.ToBase64String (ms.ToArray ());
        file.WriteLine (a);
        file.Close ();
    }

    private void SavePlayerOnline (PlayerSaveType[] saveTypes) {
        // TODO: Save User Online
    }

    #endregion

    #region LoadPlayer
    private async Task<Player> LoadPlayerOnline () {
        Player onlinePlayer = new Player ();
        // TODO: Load all data from database
        return onlinePlayer;
    }
    private bool IsUserOnline () {
        // TODO: Handle this
        return false;
    }

    public async void LoadPlayer () {
        Player localPlayer = this.LoadPlayerLocal ();

        if (this.IsUserOnline ()) {
            Player onlinePlayer = await this.LoadPlayerOnline ();
            this.player = this.IsLocalPlayerNewer (localPlayer, onlinePlayer);
            this.SavePlayerAll ();
        } else {
            this.player = localPlayer;
        }
        if (this.player != null) {
            this.CheckForDailyQuests ();
            EventHandler.onPlayerLogin?.Invoke ();
        }
        this.isReady = true;
    }

    private Player IsLocalPlayerNewer (Player localPlayer, Player onlinePlayer) {
        if (localPlayer == null) return onlinePlayer;
        if (onlinePlayer == null || onlinePlayer.metaData.lastSave == 0 || onlinePlayer.metaData.created == 0) return localPlayer;

        DateTime onlineCreated = DateTime.FromFileTime (onlinePlayer.metaData.created);
        DateTime localCreated = DateTime.FromFileTime (localPlayer.metaData.created);

        if (onlineCreated != localCreated) return onlinePlayer;

        DateTime onlineLastSave = DateTime.FromFileTime (onlinePlayer.metaData.lastSave);
        DateTime localLastSave = DateTime.FromFileTime (localPlayer.metaData.lastSave);

        if (onlineLastSave != localLastSave) {
            return this.DecideWhichPlayerIsCorrect (localPlayer, onlinePlayer);
        }
        return onlinePlayer;
    }

    private Player DecideWhichPlayerIsCorrect (Player localPlayer, Player onlinePlayer) {
        // TODO: Custom Logic to define which is the correct player/ savefile
        return onlinePlayer;
    }

    private Player LoadPlayerLocal () {
        Player player = null;
        try {
            if (File.Exists (Application.persistentDataPath + PLAYER_STATS_SAVE_PATH)) {
                BinaryFormatter formatter = new BinaryFormatter ();
                StreamReader file = new StreamReader (Application.persistentDataPath + PLAYER_STATS_SAVE_PATH);
                string a = file.ReadToEnd ();
                MemoryStream ms = new MemoryStream (System.Convert.FromBase64String (a));
                string playerJson = formatter.Deserialize (ms) as string;
                player = JsonUtility.FromJson<Player> (playerJson);
                file.Close ();
            } else {
                EventHandler.onNewPlayer?.Invoke ();
                player = this.CreateNewLocalPlayer ();
            }
        } catch (Exception e) {
            Debug.LogError ("LoadPlayer failed: " + e);
        }
        return player;
    }

    #endregion

    private Player CreateNewLocalPlayer () {
        this.player = new Player ();
        this.CheckForDailyQuests ();
        this.SavePlayerAll ();
        return this.player;
    }

    public void DeleteLocalPlayer () {
        try {
            if (File.Exists (Application.persistentDataPath + PLAYER_STATS_SAVE_PATH)) {
                File.Delete (Application.persistentDataPath + PLAYER_STATS_SAVE_PATH);
            }
            this.player = null;
        } catch (Exception e) {
            Debug.LogError ("Player deleting failed: " + e);
        }
    }

    private void CheckForDailyQuests () {
        int daysDifference = this.GetDaysBetweenNowAndLastDailyLogin ();
        if (daysDifference < 0) {
            // The Person did jumped in the past (maybe he changed the time of the device)
        } else if (daysDifference >= 1) {
            this.player.quest.GenerateNewDailies ();
            this.player.metaData.UpdateLastDailyLogin ();
            this.SavePlayer (new PlayerSaveType[] { PlayerSaveType.META_DATA, PlayerSaveType.QUESTS });
            EventHandler.onDailyQuestChange?.Invoke ();
        }
    }

    private int GetDaysBetweenNowAndLastDailyLogin () {
        int difference = 0;
        if (this.player.quest.dailies.Count == 0) {
            difference = 1;
        } else if (this.player.metaData.lastDailyLogin != 0) {
            DateTime lastLogin = DateTime.FromFileTime (this.player.metaData.lastDailyLogin);
            difference = (DateTime.Now - lastLogin).Days;
        } else {
            difference = 1;
        }
        return difference;
    }

    #endregion

    #region Public

    public Player GetPlayer () {
        return this.player;
    }

    public void IncreaseMoney (int amount) {
        EventHandler.onMoneyIncrease?.Invoke (amount);
        this.player.money.money += amount;
        this.player.money.moneyEarned += amount;
        this.player.money.moneyEarnedToday += amount;
        EventHandler.onMoneyChange?.Invoke (this.player.money.money);
    }

    public void DecreaseMoney (int amount) {
        EventHandler.onMoneyDecrease?.Invoke (amount);
        this.player.money.money += amount;
        this.player.money.moneyEarned += amount;
        this.player.money.moneyEarnedToday += amount;
        EventHandler.onMoneyChange?.Invoke (this.player.money.money);
    }
    #endregion

}