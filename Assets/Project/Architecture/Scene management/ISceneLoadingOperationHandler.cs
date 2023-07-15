using System;

namespace Project.Architecture.SceneManagement
{
    public interface ISceneLoadingOperationHandler
    {
        void HandleLoading(ISceneLoadingOperation operation, Action onCompleted);
    }
}