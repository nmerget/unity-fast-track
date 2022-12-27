using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Shared.Dialog
{
    public class DialogOverlayController : MonoBehaviour
    {
        public GameObject background;
        public Button backgroundButton;

        private void OnEnable()
        {
            EventHandler.onToggleDialogContainer += ToggleDialog;
        }

        private void OnDisable()
        {
            EventHandler.onToggleDialogContainer -= ToggleDialog;
        }

        private void ToggleDialog()
        {
            if (background.gameObject.activeSelf)
            {
                background.gameObject.SetActive(false);
                backgroundButton.enabled = false;
            }
            else
            {
                background.gameObject.SetActive(true);
                backgroundButton.enabled = true;
            }
        }
    }
}