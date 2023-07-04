namespace Project
{
    public class PausableAndResettable : IPausableAndResettable
    {
        protected bool _isPaused { get; private set; }
        
        public void Pause()
        {
            if (_isPaused)
                return;
            
            _isPaused = true;
            OnPaused();
        }

        protected virtual void OnPaused()
        {
        }

        public void Resume()
        {
            if (!_isPaused)
                return;
            
            _isPaused = false;
            OnResumed();
        }

        protected virtual void OnResumed()
        {
        }

        public void Reset()
        {
            OnReset();
        }

        protected virtual void OnReset()
        {
        }
    }
}