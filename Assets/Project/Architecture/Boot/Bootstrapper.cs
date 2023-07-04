using System;
using UnityEngine;

namespace Project.Architecture
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        
        private IGame _game;
        
        private void Start()
        {
            _game = new Game(_playerConfig);
        }
    }
}
