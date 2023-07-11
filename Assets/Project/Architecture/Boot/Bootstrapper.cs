using System.Linq;
using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private GameLevelsConfig _levelsConfig;
        
        private IGame _game;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            var levels = _levelsConfig.Levels.Select(x => (ILevelDescriptor) x).ToArray();
            var applicationQuitter = new ApplicationQuitter();

            _game = new Game(_playerConfig, levels, applicationQuitter);
            _game.InputService = GetComponent<IGameInputService>();
            _game.Start();
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
