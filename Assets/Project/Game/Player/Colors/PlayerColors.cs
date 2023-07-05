using UnityEngine;

namespace Project.Game
{
    public class PlayerColors : IPlayerColors
    {
        private Color _playerCanJumpColor;
        private Color _playerDefaultColor;
        private SpriteRenderer _renderer;

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
    }
}