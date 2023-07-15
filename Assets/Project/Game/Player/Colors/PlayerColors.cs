using UnityEngine;

namespace Project.Game.Player
{
    public class PlayerColors : IPlayerColors
    {
        private readonly Color _playerCanJumpColor;
        private readonly Color _playerDefaultColor;
        private readonly SpriteRenderer _renderer;
        private readonly IPlayerTrail _trail;

        public PlayerColors(SpriteRenderer renderer, Color playerDefaultColor, Color playerCanJumpColor, IPlayerTrail trail)
        {
            _renderer = renderer;
            _playerDefaultColor = playerDefaultColor;
            _playerCanJumpColor = playerCanJumpColor;
            _trail = trail;
        }

        public void UpdateColors(bool canJump)
        {
            var color = canJump ? _playerCanJumpColor : _playerDefaultColor;

            if (_renderer.color == color)
                return;
            
            _renderer.color = color;
            _trail.SetColor(color);
        }

        public void Deactivate() =>
            _renderer.enabled = false;

        public void Activate() =>
            _renderer.enabled = true;
    }
}