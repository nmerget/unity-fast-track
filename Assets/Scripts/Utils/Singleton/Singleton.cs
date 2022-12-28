using UnityEngine;

namespace Utils.Singleton
{
    public abstract class Singleton<T> : MonoBehaviour, ISingleton where T : MonoBehaviour
    {
        /**
         * Override this to load something in a synced state
         * For example to load some PLAYER_PREFS
         */
        protected abstract void OnLoadSync();

        /**
         * Set it to true if you need to load something in the background
         * !!! You need to set isReady to true after the background loading is done to continue the loading process
         */
        protected abstract bool IsAsync();

        #region ISingleton Implementation

        protected bool isReady;
        private bool loadingStarted;

        public static T instance;

        public void Load()
        {
            if (!instance)
            {
                loadingStarted = true;
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                instance = FindObjectOfType<T>();
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                OnLoadSync();
                if (!IsAsync()) isReady = true;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        public bool IsReady()
        {
            return isReady;
        }

        public bool LoadingStarted()
        {
            return loadingStarted;
        }

        public abstract void LoadOnUpdateInterval();

        #endregion
    }
}