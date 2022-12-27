﻿using Utils.Singleton;

namespace Utils
{
    public class DebugManager : Singleton<DebugManager>
    {
        public bool debug;

        public override void LoadOnUpdateInterval()
        {
        }

        protected override bool IsAsync()
        {
            return false;
        }

        protected override void OnLoadSync()
        {
        }
    }
}