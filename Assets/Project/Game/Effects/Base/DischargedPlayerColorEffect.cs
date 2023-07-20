using UnityEngine;

namespace Project.Game.Effects
{
    public class DischargedPlayerColorEffect : UsablePlayerEffect<Color>
    {
        private static readonly Color _dischargedColor = new(0.8509803922f, 0.06666666667f, 0.06666666667f);
        
        public DischargedPlayerColorEffect() : base(0) { }
        public override Color ApplyTo(Color baseValue) =>
            _dischargedColor;
    }
}