using System;
using System.Collections.Generic;
using UnityEngine;

public class SingletonsLoader : MonoBehaviour {
    public static SingletonsLoader instance;

    #region Private 
    private List<ISingleton> singletons = new List<ISingleton> ();
    private bool completlyLoaded = false;
    private bool loadTriggered = false;
    private int loadedDependecies = 0;

    private void Awake () {
        if (instance == null) {
            // Init this as Singleton
            instance = this;

#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
            this.AddMobileDependencies ();
#endif

            this.AddDefaultDependencies ();

        } else if (instance != this) {
            Destroy (this.gameObject);
        }
        DontDestroyOnLoad (this.gameObject);
    }

    private void Update () {
        if (this.loadTriggered && !this.completlyLoaded) {
            this.LoadModule ();
        }
    }
    private void LoadModule () {
        if (this.loadedDependecies == this.singletons.Count) {
            this.completlyLoaded = true;
            this.onLoadingComplete?.Invoke ();
        } else {
            ISingleton singleton = this.singletons[this.loadedDependecies];
            if (singleton.IsReady ()) {
                this.loadedDependecies++;
                this.onDependencyLoaded?.Invoke (this.loadedDependecies);
            } else {
                if (singleton.LoadingStarted () == false) {
                    singleton.Load ();
                } else {
                    singleton.LoadOnUpdateIntervall ();
                }
            }
        }
    }

    #endregion

    #region  Public 

    public Action onLoadingComplete;
    public Action<int> onDependencyLoaded;

    /**
        Call this to start Loading all Singletons
     */
    public void LoadAll () {
        if (!this.loadTriggered) {
            this.loadTriggered = true;
        }
    }

    public float GetLoadingProgress () {
        return this.loadedDependecies / this.singletons.Count;
    }

    public int GetAllDependenciesCount () {
        return this.singletons.Count;
    }
    public int GetLoadedDependenciesCount () {
        return this.loadedDependecies;
    }

    public bool IsCompletlyLoaded () {
        return this.completlyLoaded;
    }

    #endregion

    #region Dependencies

    public DebugManager debugManager;
    public SoundManager soundManager;

    public PlayerManager playerManager;

    private void AddMobileDependencies () { }
    private void AddDefaultDependencies () {
        this.singletons.AddRange (new List<ISingleton> {
            this.debugManager,
            this.soundManager,
            this.playerManager
        });
    }

    #endregion

}