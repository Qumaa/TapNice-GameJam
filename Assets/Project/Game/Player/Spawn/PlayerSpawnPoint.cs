using UnityEngine;

namespace Project.Game
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private PlayerDirection _playerDirection;
        
        public PlayerDirection PlayerDirection => _playerDirection;
        public Vector3 Position => transform.position;

        public void Destroy()
        {
            Object.Destroy(this);
        }
    }
}