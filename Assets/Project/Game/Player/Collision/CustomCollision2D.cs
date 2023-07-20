using System.Runtime.InteropServices;
using UnityEngine;

namespace Project.Game.CollisionHandling
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct CustomCollision2D
    {
        public readonly ContactPoint2D Contact;
        public readonly GameObject OtherObject;

        public CustomCollision2D(ContactPoint2D contact, GameObject otherObject)
        {
            Contact = contact;
            OtherObject = otherObject;
        }
    }
}