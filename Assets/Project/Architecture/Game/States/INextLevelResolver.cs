namespace Project.Architecture
{
    public interface INextLevelResolver
    {
        bool HaveNextLevel(out int levelIndex);
        void SetLevel(int levelIndex);
    }

    public static class NextLevelResolverExtensions
    {
        public static bool SwitchToNextLevel(this INextLevelResolver resolver, out int levelIndex)
        {
            var haveNextLevel = resolver.HaveNextLevel(out levelIndex);
            resolver.SetLevel(levelIndex);

            return haveNextLevel;
        }
    }
}