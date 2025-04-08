using System.Drawing;

namespace Snake.Entities
{
    public class Food : GameObject, ICollidable
    {
        public Food(int x, int y) : base(x, y) { }

        public bool CheckCollision(Point point)
        {
            return Position == point;
        }
        
        
    }
}