using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour {
    public Slider progress;
    public GameObject foreground;
    private float currentProgress = 0;
    private AsyncOperation asyncLoadedMainMenu;
    private bool startScreenTrigger;
    private void Start () {
        this.UpdateProgress ();
        SingletonsLoader.instance.LoadAll ();
    }

    private void Update () {
        if (this.currentProgress != SingletonsLoader.instance.GetLoadingProgress ()) {
            this.UpdateProgress ();
        } else if (SingletonsLoader.instance.IsCompletlyLoaded () && !this.startScreenTrigger) {
            this.startScreenTrigger = true;
            StartMenuScene ();
        }
    }

    private void UpdateProgress () {
        this.currentProgress = SingletonsLoader.instance.GetLoadingProgress ();
        this.progress.value = this.currentProgress;
    }

    private async void StartMenuScene () {
        this.LoadMainMenuAsync ();
        await this.AnimateScreenTransition ();
        this.asyncLoadedMainMenu.allowSceneActivation = true;
    }
    private void LoadMainMenuAsync () {
        this.asyncLoadedMainMenu = SceneManager.LoadSceneAsync (Scenes.MAIN_MENU);
        this.asyncLoadedMainMenu.allowSceneActivation = false;
    }
    private async Task<bool> AnimateScreenTransition () {
        bool animationComplete = false;
        LeanTween.scale (this.foreground, new Vector3 (0, 0, 0), 0.5f)
            .setEaseInExpo ()
            .setOnComplete (() => {
                animationComplete = true;
            });
        await new WaitUntil (() => animationComplete);
        return true;
    }
}