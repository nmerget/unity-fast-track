using System;

namespace Utils
{
    public static class EventHandler
    {
        #region UI

        /// <summary> Notifies if the dialog container should be toggled.</summary>
        public static Action onToggleDialogContainer;

        /// <summary> Notifies if the foreground was loaded to show other game objects.</summary>
        public static Action onForegroundVisible;

        /// <summary> If the user clicks the play button in main scene.</summary>
        public static Action onPlayClick;

        /// <summary> If the user clicks the settings button in main scene.</summary>
        public static Action onToggleSettings;

        /// <summary> If the user clicks the items button in main scene or if the user closes the container.</summary>
        public static Action onToggleItemsContainer;

        #endregion

        #region Singletons

        public static Action onOnlineAuthSuccess;
        public static Action onOnlineRegistration;

        #region PlayerManager

        public static Action<int> onMoneyChange;
        public static Action<int> onMoneyIncrease;
        public static Action<int> onMoneyDecrease;
        
        public static Action onDailyQuestChange;
        public static Action onPlayerLogin;
        public static Action onNewPlayer;
        public static Action onDeletePlayer;

        #endregion

        #endregion
    }
}