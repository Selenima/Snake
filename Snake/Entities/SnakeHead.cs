using System.Collections.Generic;
using System.Drawing;

namespace Snake.Entities
{
    public class SnakeHead : GameObject, ICollidable
    {
        public LinkedList<Point> Body { get; private set; }
        
        public Direction CurrentDirection { get; set; } = Direction.Right;

        public SnakeHead(int x, int y) : base(x, y)
        {
            Body = new LinkedList<Point>();
            Body.AddLast(Position);
        }

        public bool CheckCollision(Point point)
        {
            return Body.Contains(point);
        }
        
        public static SnakeHead operator ++(SnakeHead snake)
        {
            var last = snake.Body.Last.Value;
            snake.Body.AddLast(new Point(last.X, last.Y)); // WT
            return snake;
        }

        public void Move(Point newPosition)
        {
            Position = newPosition;
            Body.AddFirst(newPosition);
            Body.RemoveLast();
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}