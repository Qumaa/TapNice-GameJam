using UnityEngine;

namespace Project.Game
{
    public class PlayerColors : IPlayerColors
    {
        private readonly Color _playerCanJumpColor;
        private readonly Color _playerDefaultColor;
        private readonly SpriteRenderer _renderer;

        public PlayerColors(SpriteRenderer renderer, Color playerDefaultColor, Color playerCanJumpColor)
        {
            _renderer = renderer;
            _playerDefaultColor = playerDefaultColor;
            _playerCanJumpColor = playerCanJumpColor;
        }

        public void UpdateColors(bool canJump)
        {
            var color = canJump ? _playerCanJumpColor : _playerDefaultColor;

            _renderer.color = color;
        }

        public void Deactivate() =>
            _renderer.enabled = false;

        public void Activate() =>
            _renderer.enabled = true;
    }
}