using UnityEngine;

namespace Project.Game.Effects
{
    public class PlayerColorPermanentEffect : PermanentPlayerEffect<Color>
    {
        private readonly Color _color;

        public PlayerColorPermanentEffect(Color color)
        {
            _color = color;
        }

        public override Color ApplyTo(Color baseValue) =>
            _color;
    }
}