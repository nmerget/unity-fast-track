using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionTriggerAnimation : MonoBehaviour {

    public GameObject childContainer;
    public float openAnimationTime = 0.25f;

    private RectTransform rectTransform;

    private bool isOpen;

    private void Awake () {
        this.rectTransform = this.GetComponent<RectTransform> ();
        if (this.rectTransform == null) {
            Debug.LogErrorFormat ("ToogleOverlay, RectTransform is null");
        } else {
            LeanTween.scale (this.gameObject, Vector3.zero, 0);
            this.childContainer.SetActive (true);
        }
    }

    public bool IsOpen () {
        return this.isOpen;
    }

    public async Task<bool> Toogle (bool open = true) {
        bool animationDone = false;
        this.isOpen = open;

        if (open) {
            Vector2 mousePosition = Mouse.current.position.ReadValue ();
            float pivotX = mousePosition.x / Screen.width;
            float pivotY = mousePosition.y / Screen.height;
            rectTransform.pivot = new Vector2 (pivotX, pivotY);
        }

        LeanTween.scale (
                this.gameObject,
                open ? Vector3.one : Vector3.zero, this.openAnimationTime
            )
            .setEaseInOutExpo ()
            .setOnComplete (() => {
                animationDone = true;
            });
        await new WaitUntil (() => animationDone);
        return true;
    }

}