namespace Project.Game.Levels
{
    public interface INextLevelResolver
    {
        bool HasNextLevel(out int levelIndex);
        void SetLevel(int levelIndex);
    }

    public static class NextLevelResolverExtensions
    {
        public static bool TrySwitchToNextLevel(this INextLevelResolver resolver, out int levelIndex)
        {
            var hasNextLevel = resolver.HasNextLevel(out levelIndex);
            resolver.SetLevel(levelIndex);

            return hasNextLevel;
        }

        public static bool HasNextLevel(this INextLevelResolver resolver) =>
            resolver.HasNextLevel(out _);
    }
}