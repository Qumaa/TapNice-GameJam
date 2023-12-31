﻿namespace Project
{
    public static class SOData
    {
        private const string _PLAYER_CONFIG = "Player Config";
        private const string _LEVEL_DESCRIPTOR = "Level Descriptor";
        private const string _LEVELS_CONFIG = "Levels Config";
        private const string _UI_CONFIG = "UI Config";
        private const string _VFX_CONFIG = "VFX Config";
        
        public static class MenuPath
        {
            private const string _PROJECT = "Project/";

            public const string PLAYER_CONFIG = _PROJECT + _PLAYER_CONFIG;
            public const string LEVEL_DESCRIPTOR = _PROJECT + _LEVEL_DESCRIPTOR;
            public const string LEVELS_CONFIG = _PROJECT + _LEVELS_CONFIG;
            public const string UI_CONFIG = _PROJECT + _UI_CONFIG;
            public const string VFX_CONFIG = _PROJECT + _VFX_CONFIG;
        }

        public static class FileName
        {
            public const string PLAYER_CONFIG = _PLAYER_CONFIG;
            public const string LEVEL_DESCRIPTOR = _LEVEL_DESCRIPTOR;
            public const string LEVELS_CONFIG = _LEVELS_CONFIG;
            public const string UI_CONFIG = _UI_CONFIG;
            public const string VFX_CONFIG = _VFX_CONFIG;
        }
    }
}