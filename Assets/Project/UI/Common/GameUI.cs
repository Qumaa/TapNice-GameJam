using UnityEngine;

namespace Project.UI
{
    public abstract class GameUI : MonoBehaviour, IGameUI
    {
        protected RectTransform _transform { get; private set; }

        protected virtual void Awake()
        {
            _transform = (RectTransform) transform;
        }

        public void SetCanvas(Canvas canvas) =>
            _transform.SetParent(canvas.transform, false);

        public void Delete()
        {
            OnDelete();
            Destroy(_transform.gameObject);
        }

        protected abstract void OnDelete();
    }
}