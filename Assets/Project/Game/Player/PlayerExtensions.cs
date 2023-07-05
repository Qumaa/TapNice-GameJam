namespace Project.Game
{
    public static class PlayerExtensions
    {
        /// <summary>
        /// If player's flag <see cref="IPlayer.CanJump"/> is true, calls <see cref="IPlayer.Jump"/> and sets the flag to false
        /// </summary>
        public static void JumpIfPossible(this IPlayer player)
        {
            if (!player.CanJump)
                return;

            player.Jump();
            player.CanJump = false;
        }
    }
}