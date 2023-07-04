using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class TempGameState : GameState
    {
        private readonly PlayerConfig _playerConfig;

        public TempGameState(IGame game, IGameStateMachine stateMachine, PlayerConfig playerConfig) : base(game, stateMachine)
        {
            _playerConfig = playerConfig;
        }

        public override void Enter()
        {
            var player = CreatePlayer();
            InitCollisionHandlers(player);
        }

        private void InitCollisionHandlers(IPlayer player)
        {
            var sceneHandlers =
                Object.FindObjectsByType<SceneCollisionHandlerContainer>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            
            if (sceneHandlers.Length == 0)
                return;
            
            var handler = new FinishCollisionHandler(player);
            
            foreach(var sceneHandler in sceneHandlers)
                sceneHandler.SetHandler(handler);
        }

        private IPlayer CreatePlayer()
        {
            var obj = Object.Instantiate(_playerConfig.PlayerPrefab);
            var player = obj.GetComponent<IPlayer>();
            
            player.HorizontalSpeed = _playerConfig.HorizontalSpeed;
            player.JumpHeight = _playerConfig.JumpHeight;
            player.SetColors(_playerConfig.PlayerDefaultColor, _playerConfig.PlayerCanJumpColor);

            return player;
        }

        public override void Exit()
        {
        }
    }
}