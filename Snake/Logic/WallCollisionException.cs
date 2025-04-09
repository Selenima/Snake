namespace SnakeGame.Logic
{
    public class WallCollisionException : Exception
    {
        public WallCollisionException() : base("Game Over: You hit the wall!") { }
        
    }
}