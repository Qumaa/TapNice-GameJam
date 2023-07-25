namespace Project.Game.Levels
{
    public interface ILevelResolver
    {
        int CurrentLevel { get; }
        bool HasNextLevel(out int levelIndex);
        void SetLevel(int levelIndex);
    }

    public static class NextLevelResolverExtensions
    {
        public static bool TrySwitchToNextLevel(this ILevelResolver resolver, out int levelIndex)
        {
            var hasNextLevel = resolver.HasNextLevel(out levelIndex);
            resolver.SetLevel(levelIndex);

            return hasNextLevel;
        }

        public static bool HasNextLevel(this ILevelResolver resolver) =>
            resolver.HasNextLevel(out _);
    }
}