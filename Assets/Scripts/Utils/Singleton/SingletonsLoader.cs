using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Sound;
using Utils.User;

namespace Utils.Singleton
{
    public class SingletonsLoader : MonoBehaviour
    {
        public static SingletonsLoader instance;

        #region Private

        private readonly List<ISingleton> singletons = new();
        private bool completelyLoaded;
        private bool loadTriggered;
        private int loadedDependencies;

        private void Awake()
        {
            if (instance == null)
            {
                // Init this as Singleton
                instance = this;

#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
                AddMobileDependencies();
#endif

                AddDefaultDependencies();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (loadTriggered && !completelyLoaded)
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                LoadModule();
        }

        private void LoadModule()
        {
            if (loadedDependencies == singletons.Count)
            {
                completelyLoaded = true;
                onLoadingComplete?.Invoke();
            }
            else
            {
                var singleton = singletons[loadedDependencies];
                if (singleton.IsReady())
                {
                    loadedDependencies++;
                    onDependencyLoaded?.Invoke(loadedDependencies);
                }
                else
                {
                    if (singleton.LoadingStarted() == false)
                        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                        singleton.Load();
                    else
                        singleton.LoadOnUpdateInterval();
                }
            }
        }

        #endregion

        #region Public

        private Action onLoadingComplete;
        private Action<int> onDependencyLoaded;

        /**
        Call this to start Loading all Singletons
     */
        public void LoadAll()
        {
            if (!loadTriggered) loadTriggered = true;
        }

        public float GetLoadingProgress()
        {
            // ReSharper disable once PossibleLossOfFraction
            return loadedDependencies / singletons.Count;
        }

        public int GetAllDependenciesCount()
        {
            return singletons.Count;
        }

        public int GetLoadedDependenciesCount()
        {
            return loadedDependencies;
        }

        public bool IsCompletelyLoaded()
        {
            return completelyLoaded;
        }

        #endregion

        #region Dependencies

        public DebugManager debugManager;
        public SoundManager soundManager;

        public UserManager userManager;

        private static void AddMobileDependencies()
        {
        }

        private void AddDefaultDependencies()
        {
            singletons.AddRange(new List<ISingleton>
            {
                debugManager,
                soundManager,
                userManager
            });
        }

        #endregion
    }
}