using UnityEngine;

namespace Utils.Singleton
{
    public class SingletonsEditorLoader : MonoBehaviour
    {
        public SingletonsLoader singletons;
        public GameObject elementToEnable;

        private bool loaded;

        private void Awake()
        {
            if (SingletonsLoader.instance == null || !SingletonsLoader.instance.IsCompletelyLoaded())
            {
                elementToEnable.SetActive(false);
                singletons.LoadAll();
            }
            else
            {
                elementToEnable.SetActive(true);
            }
        }

        private void Update()
        {
            if (!singletons || !singletons.IsCompletelyLoaded() || loaded) return;
            loaded = true;
            elementToEnable.SetActive(true);
        }
    }
}