using UnityEngine;

namespace Project.UI
{
    public interface IGameUIRenderer : ISingleContainer<IGameUI>
    {
        void SetCamera(Camera camera);
    }
}