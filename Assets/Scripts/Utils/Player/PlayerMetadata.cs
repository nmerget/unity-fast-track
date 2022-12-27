[System.Serializable]
public class PlayerMetaData {

    public long lastDailyLogin;
    public long lastSave;
    public long created;

    public PlayerMetaData () {
        long now = System.DateTime.Now.ToFileTime ();
        this.lastDailyLogin = now;
        this.lastSave = now;
        this.created = now;
    }

    public void UpdateLastSave () {
        this.lastSave = System.DateTime.Now.ToFileTime ();
    }
    public void UpdateLastDailyLogin () {
        this.lastDailyLogin = System.DateTime.Now.ToFileTime ();
    }
}