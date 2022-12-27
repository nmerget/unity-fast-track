using UnityEngine;
using Utils;

namespace Scenes.MenuScene.Animation
{
    public class SimpleRandomRotationAnimation : MonoBehaviour
    {
        private Vector3 initScale;

        private void Start()
        {
            var mGameObject = gameObject;
            initScale = mGameObject.transform.localScale;
            LeanTween.scale(mGameObject, Vector3.zero, 0);
        }

        private void OnEnable()
        {
            EventHandler.onForegroundVisible += StartAnimation;
        }

        private void OnDisable()
        {
            EventHandler.onForegroundVisible -= StartAnimation;
        }

        private void StartAnimation()
        {
            LeanTween.scale(gameObject, initScale, 0.5f)
                .setOnComplete(() =>
                {
                    LeanTween.rotate(gameObject,
                        new Vector3(GetRandomNumber(), GetRandomNumber(), GetRandomNumber()),
                        3f).setLoopPingPong();
                });
        }

        private static float GetRandomNumber()
        {
            return Random.Range(1f, 361f);
        }
    }
}