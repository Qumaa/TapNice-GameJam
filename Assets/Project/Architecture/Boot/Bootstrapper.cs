using System;
using UnityEngine;

namespace Project.Architecture
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        
        private IGame _game;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _game = new Game(_playerConfig);
        }

        private void Update()
        {
            _game.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _game.FixedUpdate(Time.deltaTime);
        }
    }
}
