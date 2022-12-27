using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultButtonAnimation : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    private Vector3 normalScale;
    private Vector3 downScale;
    private float scaleFactor = 0.98f;

    private float scaleTime = 0.2f;

    private void Awake () {
        this.normalScale = this.gameObject.transform.localScale;
        this.downScale = new Vector3 (
            this.normalScale.x * this.scaleFactor,
            this.normalScale.x * this.scaleFactor,
            this.normalScale.x * this.scaleFactor
        );
    }

    public void OnPointerDown (PointerEventData eventData) {
        LeanTween.scale (this.gameObject, downScale, this.scaleTime);
    }

    public void OnPointerUp (PointerEventData eventData) {
        LeanTween.scale (this.gameObject, normalScale, this.scaleTime);
    }
}