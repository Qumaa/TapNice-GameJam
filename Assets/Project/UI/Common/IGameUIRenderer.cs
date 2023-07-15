using UnityEngine;

namespace Project.UI
{
    public interface IGameUIRenderer : ISingletonRegistry<IGameUI>
    {
        void SetCamera(Camera camera);
    }
}