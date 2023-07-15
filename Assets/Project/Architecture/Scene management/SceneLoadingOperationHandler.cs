using System;

namespace Project.Architecture.SceneManagement
{
    public class SceneLoadingOperationHandler : ISceneLoadingOperationHandler
    {
        private ISceneLoadingOperation _operation;
        private Action _onCompleted;
        
        public void HandleLoading(ISceneLoadingOperation loadingOperation, Action onCompleted)
        {
            if (loadingOperation.IsDone)
            {
                onCompleted();
                return;
            }

            _operation = loadingOperation;
            _onCompleted = onCompleted;
            _operation.OnLoadingCompleted += HandleLoadingCompleted;
        }

        private void HandleLoadingCompleted()
        {
            _operation.OnLoadingCompleted -= HandleLoadingCompleted;
            _operation = null;
            _onCompleted();
            _onCompleted = null;
        }
    }
}