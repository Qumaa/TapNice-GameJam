using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    public class GameUIRenderer : IGameUIRenderer
    {
        private readonly Canvas _canvas;
        private readonly Dictionary<Type, IGameUI> _uis;

        public GameUIRenderer(Canvas canvas)
        {
            _canvas = canvas;
            _uis = new Dictionary<Type, IGameUI>();
        }

        public void SetCamera(Camera uiCamera)
        {
            _canvas.worldCamera = uiCamera;
            _canvas.planeDistance = 1;
        }

        public void Add<T>(T item) where T : IGameUI
        {
            _uis.Add(typeof(T), item);
            item.SetCanvas(_canvas);
        }

        public T Get<T>() where T : IGameUI =>
            (T) _uis[typeof(T)];

        public void Remove<T>() where T : IGameUI
        {
            _uis.Remove(typeof(T), out var ui);
            ui.Delete();
        }
    }
}