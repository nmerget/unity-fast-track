using UnityEngine;
using UnityEngine.UI;

public class DialogOverlayController : MonoBehaviour {

    public GameObject background;
    public Button backgroundButton;

    private void OnEnable () {
        EventHandler.onToogleDialogContainer += this.ToogleDialog;
    }

    private void OnDisable () {
        EventHandler.onToogleDialogContainer -= this.ToogleDialog;
    }

    private void ToogleDialog () {
        if (this.background.gameObject.activeSelf) {
            this.background.gameObject.SetActive (false);
            this.backgroundButton.enabled = false;
        } else {
            this.background.gameObject.SetActive (true);
            this.backgroundButton.enabled = true;
        }
    }
}