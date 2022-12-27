namespace Utils.Singleton
{
    public interface ISingleton
    {
        /**
        This is called the first time the singleton is loaded
     */
        void Load();

        /**
        Optional: If we want to split loading called on every Update() in SigletonsLoader [can be used for sounds loading in background]
     */
        void LoadOnUpdateInterval();

        /**
        Used by SingletonsLoader to check if the loading process is already triggered
     */
        bool LoadingStarted();

        /**
        Used by SingletonsLoader to check if the loading process is done
     */
        bool IsReady();
    }
}