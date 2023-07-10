﻿using System;
using Project.Game;

namespace Project.Architecture
{
    public interface IGame : IUpdatableAndFixedUpdatable, IUpdater, IFixedUpdater
    {
        IGameInputService InputService { get; set; }
        IPlayer Player { get; set; }
        ILevel LoadedLevel { get; set; }
        void Start();

        void LoadLevel(int index);
        /// <returns>Whether is able to switch to next level or not.</returns>
        bool LoadNextLevel();

        void LoadMainMenu();
    }
}