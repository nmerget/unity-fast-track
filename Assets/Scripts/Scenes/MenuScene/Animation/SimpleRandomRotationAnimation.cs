using UnityEngine;

public class SimpleRandomRotationAnimation : MonoBehaviour {

    private Vector3 initScale;
    private void Start () {
        this.initScale = this.gameObject.transform.localScale;
        LeanTween.scale (this.gameObject, Vector3.zero, 0);
    }

    private void OnEnable () {
        EventHandler.onForgroundVisible += StartAnimation;
    }

    private void OnDisable () {
        EventHandler.onForgroundVisible -= StartAnimation;
    }

    private void StartAnimation () {
        LeanTween.scale (this.gameObject, initScale, 0.5f)
            .setOnComplete (() => {
                LeanTween.rotate (this.gameObject,
                    new Vector3 (this.GetRandomNumber (), this.GetRandomNumber (), this.GetRandomNumber ()),
                    3f).setLoopPingPong ();
            });

    }

    private float GetRandomNumber () {
        return Random.Range (1f, 361f);
    }
}