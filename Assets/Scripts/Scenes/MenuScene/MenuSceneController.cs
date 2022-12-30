using Shared.Animation;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Scenes.MenuScene
{
    public class MenuSceneController : MonoBehaviour
    {
        public CanvasGroup[] foregroundObjects;
        public ActionTriggerAnimation itemsContainer;
        public ActionTriggerAnimation settingsContainer;

        private void Awake()
        {
            foreach (var foregroundObj in foregroundObjects) foregroundObj.alpha = 0;
        }

        private void Start()
        {
            LeanTween.value(gameObject, 0, 1, 1f)
                .setEaseInExpo()
                .setOnUpdate(val =>
                {
                    foreach (var foregroundObj in foregroundObjects) foregroundObj.alpha = val;
                })
                .setOnComplete(TriggerOnForegroundVisible);
        }

        private void OnEnable()
        {
            ActionHandler.onPlayClick += StartPlay;
            ActionHandler.onToggleSettings += ToggleSettings;
            ActionHandler.onToggleItemsContainer += ToggleItems;
        }

        private void OnDisable()
        {
            ActionHandler.onPlayClick -= StartPlay;
            ActionHandler.onToggleSettings -= ToggleSettings;
            ActionHandler.onToggleItemsContainer -= ToggleItems;
        }

        private static async void TriggerOnForegroundVisible()
        {
            await new WaitForSeconds(0.5f);
            ActionHandler.onForegroundVisible?.Invoke();
        }

        private static void StartPlay()
        {
            SceneManager.LoadScene(Constants.Scenes.Play);
        }

        private async void ToggleItems()
        {
            if (itemsContainer.IsOpen())
                await itemsContainer.Toggle(false);
            else
                await itemsContainer.Toggle();
        }

        private async void ToggleSettings()
        {
            ActionHandler.onToggleDialogContainer?.Invoke();
            if (settingsContainer.IsOpen())
                await settingsContainer.Toggle(false);
            else
                await settingsContainer.Toggle();
        }
    }
}