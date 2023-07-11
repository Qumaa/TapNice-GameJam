using UnityEngine;

namespace Project.UI
{
    public interface IGameUIRenderer : IInstanceContainer<IGameUI>
    {
        void SetCamera(Camera camera);
    }
}