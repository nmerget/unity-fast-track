using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour {

    public CanvasGroup[] foregroundObjects;
    public ActionTriggerAnimation itemsContainer;
    public ActionTriggerAnimation settingsContainer;

    private void Awake () {
        foreach (CanvasGroup foregroundObj in this.foregroundObjects) {
            foregroundObj.alpha = 0;
        }
    }

    private void Start () {
        LeanTween.value (this.gameObject, 0, 1, 1f)
            .setEaseInExpo ()
            .setOnUpdate ((float val) => {
                foreach (CanvasGroup foregroundObj in this.foregroundObjects) {
                    foregroundObj.alpha = val;
                }
            })
            .setOnComplete (() => {
                this.TriggerOnForegroundVisible ();
            });
    }

    private async void TriggerOnForegroundVisible () {
        await new WaitForSeconds (0.5f);
        EventHandler.onForgroundVisible?.Invoke ();
    }

    private void OnEnable () {
        EventHandler.onPlayClick += this.StartPlay;
        EventHandler.onToggleSettings += this.ToogleSettings;
        EventHandler.onToggleItemsContainer += this.ToogleItems;
    }

    private void OnDisable () {
        EventHandler.onPlayClick -= this.StartPlay;
        EventHandler.onToggleSettings -= this.ToogleSettings;
        EventHandler.onToggleItemsContainer -= this.ToogleItems;
    }

    private void StartPlay () {
        SceneManager.LoadScene (Scenes.PLAY);
    }

    private async void ToogleItems () {
        if (this.itemsContainer.IsOpen ()) {
            await this.itemsContainer.Toogle (false);
        } else {
            await this.itemsContainer.Toogle ();
        }
    }

    private async void ToogleSettings () {
        EventHandler.onToogleDialogContainer?.Invoke ();
        if (this.settingsContainer.IsOpen ()) {
            await this.settingsContainer.Toogle (false);
        } else {
            await this.settingsContainer.Toogle ();
        }
    }

}