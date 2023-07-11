using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    public class GameUIRenderer : IGameUIRenderer
    {
        private readonly Canvas _canvas;
        private readonly List<IGameUI> _uis;

        public GameUIRenderer(Canvas canvas)
        {
            _canvas = canvas;
            _uis = new List<IGameUI>();
        }

        public void SetCamera(Camera uiCamera)
        {
            _canvas.worldCamera = uiCamera;
            _canvas.planeDistance = 1;
        }

        public void Add(IGameUI item)
        {
            _uis.Add(item);
            item.SetCanvas(_canvas);
        }

        public void Remove(IGameUI item)
        {
            _uis.Remove(item);
            item.Delete();
        }
    }
}