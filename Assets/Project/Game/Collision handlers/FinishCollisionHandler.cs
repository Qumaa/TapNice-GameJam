using UnityEngine;

namespace Project.Game
{
    public class FinishCollisionHandler : PlayerCollisionHandler
    {
        public override void HandleCollision(PlayerCollisionInfo collision)
        {
            DefaultHandling(collision);
            Debug.Log("Finish");
        }
    }
}