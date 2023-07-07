using System;

namespace Project.Architecture
{
    public interface ISceneLoadingOperation
    {
        event Action OnLoadingCompleted;
        bool IsDone { get; }
    }
}