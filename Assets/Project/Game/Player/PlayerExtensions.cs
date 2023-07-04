namespace Project.Game
{
    public static class PlayerExtensions
    {
        public static bool TryJump(this IPlayer player)
        {
            if (!player.CanJump)
                return false;

            player.Jump();
            return true;
        }
    }
}