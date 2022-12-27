using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shared.Animation
{
    public class ActionTriggerAnimation : MonoBehaviour
    {
        public GameObject childContainer;
        public float openAnimationTime = 0.25f;

        private bool isOpen;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                Debug.LogErrorFormat("ActionTriggerAnimation, RectTransform is null");
            }
            else
            {
                LeanTween.scale(gameObject, Vector3.zero, 0);
                childContainer.SetActive(true);
            }
        }

        public bool IsOpen()
        {
            return isOpen;
        }

        public async Task Toggle(bool open = true)
        {
            var animationDone = false;
            isOpen = open;

            if (open)
            {
                var mousePosition = Mouse.current.position.ReadValue();
                var pivotX = mousePosition.x / Screen.width;
                var pivotY = mousePosition.y / Screen.height;
                rectTransform.pivot = new Vector2(pivotX, pivotY);
            }

            LeanTween.scale(
                    gameObject,
                    open ? Vector3.one : Vector3.zero, openAnimationTime
                )
                .setEaseInOutExpo()
                .setOnComplete(() => { animationDone = true; });
            await new WaitUntil(() => animationDone);
        }
    }
}