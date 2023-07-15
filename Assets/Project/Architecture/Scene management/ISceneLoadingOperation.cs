using System;

namespace Project.Architecture.SceneManagement
{
    public interface ISceneLoadingOperation
    {
        event Action OnLoadingCompleted;
        bool IsDone { get; }
    }
}