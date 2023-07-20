using Project.Game.Effects;
using Project.Game.Levels;
using Project.Game.Player;
using UnityEngine;

namespace Project.Game.CollisionHandling
{
    public class FinishCollisionHandler : PlayerCollisionHandler
    {
        private readonly ILevel _level;
        private bool _shouldFinish;
        private readonly FinishSpeedEffect _finishSpeedEffect;
        private readonly PlayerColorPermanentEffect _playerColorPermanentEffect;
        private IPlayer _player;

        public FinishCollisionHandler(ILevel level, Color finishPlayerColor)
        {
            _level = level;
            _level.OnStarted += Reset;
            
            _finishSpeedEffect = new FinishSpeedEffect();
            _playerColorPermanentEffect = new PlayerColorPermanentEffect(finishPlayerColor);
        }

        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            if (collision.IsOnFloor)
            {
                if (!_shouldFinish)
                    BounceOnceBeforeFinishing(collision);
                return;
            }

            DefaultHandling(collision);
        }

        private void BounceOnceBeforeFinishing(PlayerCollisionInfo collision)
        {
            _level.StopCountingTime();
            ApplyFinishEffect(collision.Player);
            _shouldFinish = true;
        }

        private void ApplyFinishEffect(IPlayer player)
        {
            player.HorizontalSpeed.AddEffect(_finishSpeedEffect);
            player.Color.AddEffect(_playerColorPermanentEffect);
            player.Jump();
            player.OnCollided += FinishLevel;
            _player = player;
        }

        private void FinishLevel(PlayerCollisionInfo playerCollisionInfo)
        {
            if (!playerCollisionInfo.IsOnFloor)
                return;
            
            _player.OnCollided -= FinishLevel;
            _level.Finish();
        }

        private void Reset() =>
            _shouldFinish = false;
    }
}