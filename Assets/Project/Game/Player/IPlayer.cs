namespace Project
{
    public interface IPlayer
    {
        void SetJumpingStatus(bool status);
        bool TryJump(float height);
        void InvertDirection();
        void SetDirection(PlayerDirection direction);
    }
}