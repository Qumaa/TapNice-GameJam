using Project.Game.Effects;
using UnityEngine;

namespace Project.Game.Player
{
    public class PlayerColors : IPlayerColors
    {
        private readonly SpriteRenderer _renderer;
        private readonly IPlayerTrail _trail;
        private readonly PlayerColorPermanentEffect _canJumpColorEffect;

        public IAffectable<Color> Color { get; }

        public PlayerColors(SpriteRenderer renderer, Color playerCanJumpColor,
            IPlayerTrail trail, IAffectable<Color> color)
        {
            _renderer = renderer;
            _trail = trail;
            Color = color;
            Color.OnAffectedValueChanged += SetColor;
            _canJumpColorEffect = new PlayerColorPermanentEffect(playerCanJumpColor);
        }

        public void UpdateColors(bool canJump)
        {
            if (canJump)
                Color.AddEffect(_canJumpColorEffect);
            else
                Color.RemoveEffect(_canJumpColorEffect);
        }

        public void Deactivate() =>
            _renderer.enabled = false;

        public void Activate() =>
            _renderer.enabled = true;

        private void SetColor(Color color)
        {
            _renderer.color = color;
            _trail.SetColor(color);
        }
    }
}