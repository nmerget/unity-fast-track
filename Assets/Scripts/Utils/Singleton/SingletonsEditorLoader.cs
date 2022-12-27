using UnityEngine;

public class SingletonsEditorLoader : MonoBehaviour {
    public SingletonsLoader singletons;
    public GameObject elementToEnable;

    private bool loaded = false;

    void Awake () {
        if (SingletonsLoader.instance == null || !SingletonsLoader.instance.IsCompletlyLoaded ()) {
            this.elementToEnable.SetActive (false);
            this.singletons.LoadAll ();
        } else {
            this.elementToEnable.SetActive (true);
        }
    }

    private void Update () {
        if (singletons != null && singletons.IsCompletlyLoaded () && !this.loaded) {
            this.loaded = true;
            this.elementToEnable.SetActive (true);
        }
    }
}