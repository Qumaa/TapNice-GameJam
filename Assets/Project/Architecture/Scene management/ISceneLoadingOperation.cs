using System;

namespace Project.Architecture
{
    public interface ISceneLoadingOperation
    {
        event Action OnLoadingComplete;
        bool IsDone { get; }
    }
}