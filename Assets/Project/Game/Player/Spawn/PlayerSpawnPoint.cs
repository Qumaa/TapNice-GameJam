using System;
using UnityEngine;

namespace Project.Game.Player
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private PlayerDirection _playerDirection;
        
        public PlayerDirection PlayerDirection => _playerDirection;
        public Vector3 Position => transform.position;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(Position, 0.5f);
        }
    }
}