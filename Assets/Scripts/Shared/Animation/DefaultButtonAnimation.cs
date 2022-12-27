using UnityEngine;
using UnityEngine.EventSystems;

namespace Shared.Animation
{
    public class DefaultButtonAnimation : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        private const float ScaleFactor = 0.98f;

        private const float ScaleTime = 0.2f;
        private Vector3 downScale;

        private Vector3 normalScale;

        private void Awake()
        {
            normalScale = gameObject.transform.localScale;
            downScale = new Vector3(
                normalScale.x * ScaleFactor,
                normalScale.x * ScaleFactor,
                normalScale.x * ScaleFactor
            );
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            LeanTween.scale(gameObject, downScale, ScaleTime);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            LeanTween.scale(gameObject, normalScale, ScaleTime);
        }
    }
}