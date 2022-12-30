using UnityEngine;
using Utils;

namespace Scenes.MenuScene.Animation
{
    public class CurrencyContainerAnimation : MonoBehaviour
    {
        private void OnEnable()
        {
            ActionHandler.onToggleItemsContainer += ScaleY;
        }

        private void OnDisable()
        {
            ActionHandler.onToggleItemsContainer -= ScaleY;
        }

        private void ScaleY()
        {
            LeanTween.scaleY(gameObject, 0, 0.1f).setOnComplete(() =>
            {
                LeanTween.scaleY(gameObject, 1f, 0.2f).setDelay(0.3f);
            });
        }
    }
}