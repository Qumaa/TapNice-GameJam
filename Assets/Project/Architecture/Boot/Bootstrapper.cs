using System.Linq;
using Project.Configs;
using Project.Game;
using Project.Game.Levels;
using Project.Game.Player;
using UnityEngine;

namespace Project.Architecture.Boot
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private GameLevelsConfig _levelsConfig;
        [SerializeField] private UIConfig _uiConfig;
        [SerializeField] private VFXConfig _vfxConfig;

        private IGame _game;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            var levels = _levelsConfig.Levels.Select(x => (ILevelDescriptor) x).ToArray();
            var applicationQuitter = new ApplicationQuitter();

            _game = new Game(_playerConfig, _uiConfig, _vfxConfig, levels, applicationQuitter);
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
