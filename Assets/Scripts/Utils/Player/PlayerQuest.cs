using System.Collections.Generic;
[System.Serializable]
public class PlayerQuest {
    public List<string> dailies;

    public PlayerQuest () {
        this.dailies = new List<string> ();
    }

    public void GenerateNewDailies () {
        // TODO
    }
}