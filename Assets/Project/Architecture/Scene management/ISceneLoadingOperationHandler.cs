using System;

namespace Project.Architecture
{
    public interface ISceneLoadingOperationHandler
    {
        void HandleLoading(ISceneLoadingOperation operation, Action onCompleted);
    }
}