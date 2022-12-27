using UnityEngine;

public class CurrencyContainerAnimation : MonoBehaviour {

    private void OnEnable () {
        EventHandler.onToggleItemsContainer += this.ScaleY;
    }

    private void OnDisable () {
        EventHandler.onToggleItemsContainer -= this.ScaleY;
    }

    private void ScaleY () {
        LeanTween.scaleY (this.gameObject, 0, 0.1f).setOnComplete (() => {
            LeanTween.scaleY (this.gameObject, 1f, 0.2f).setDelay (0.3f);
        });
    }

}