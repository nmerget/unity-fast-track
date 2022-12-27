using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils.Singleton;

namespace Scenes.LoadingScene
{
    public class LoadingScreenController : MonoBehaviour
    {
        public Slider progress;
        public GameObject foreground;
        private AsyncOperation asyncLoadedMainMenu;
        private float currentProgress;
        private bool startScreenTrigger;

        private void Start()
        {
            UpdateProgress();
            SingletonsLoader.instance.LoadAll();
        }

        private void Update()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (currentProgress != SingletonsLoader.instance.GetLoadingProgress())
            {
                UpdateProgress();
            }
            else if (SingletonsLoader.instance.IsCompletelyLoaded() && !startScreenTrigger)
            {
                startScreenTrigger = true;
                StartMenuScene();
            }
        }

        private void UpdateProgress()
        {
            currentProgress = SingletonsLoader.instance.GetLoadingProgress();
            progress.value = currentProgress;
        }

        private async void StartMenuScene()
        {
            LoadMainMenuAsync();
            await AnimateScreenTransition();
            asyncLoadedMainMenu.allowSceneActivation = true;
        }

        private void LoadMainMenuAsync()
        {
            asyncLoadedMainMenu = SceneManager.LoadSceneAsync(Constants.Scenes.MainMenu);
            asyncLoadedMainMenu.allowSceneActivation = false;
        }

        private async Task AnimateScreenTransition()
        {
            var animationComplete = false;
            LeanTween.scale(foreground, new Vector3(0, 0, 0), 0.5f)
                .setEaseInExpo()
                .setOnComplete(() => { animationComplete = true; });
            await new WaitUntil(() => animationComplete);
        }
    }
}