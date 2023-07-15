using Project.Game.Levels;

namespace Project.Game.Levels
{
    public interface INextLevelResolver
    {
        bool HaveNextLevel(out int levelIndex);
        void SetLevel(int levelIndex);
    }
}

namespace Project.Architecture
{
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