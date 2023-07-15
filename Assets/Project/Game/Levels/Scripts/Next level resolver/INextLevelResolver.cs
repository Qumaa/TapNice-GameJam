namespace Project.Game.Levels
{
    public interface INextLevelResolver
    {
        bool HasNextLevel(out int levelIndex);
        void SetLevel(int levelIndex);
    }

    public static class NextLevelResolverExtensions
    {
        public static bool SwitchToNextLevel(this INextLevelResolver resolver, out int levelIndex)
        {
            var hasNextLevel = resolver.HasNextLevel(out levelIndex);
            resolver.SetLevel(levelIndex);

            return hasNextLevel;
        }
    }
}