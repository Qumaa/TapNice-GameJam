namespace Project
{
    public static class SOData
    {
        private const string _PLAYER_CONFIG = "Player Config";
        private const string _LEVEL_DESCRIPTOR = "Level Descriptor";
        
        public static class MenuPath
        {
            private const string _PROJECT = "Project/";

            public const string PLAYER_CONFIG = _PROJECT + _PLAYER_CONFIG;
            public const string LEVEL_DESCRIPTOR = _PROJECT + _LEVEL_DESCRIPTOR;
        }

        public static class FileName
        {
            public const string PLAYER_CONFIG = _PLAYER_CONFIG;
            public const string LEVEL_DESCRIPTOR = _LEVEL_DESCRIPTOR;

        }
    }
}