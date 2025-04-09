using System;
using System.Collections.Generic;
using System.Drawing;
using Snake.Entities;
using SnakeGame.Logic;

namespace Snake.Logic
{
    public class GameField
    {
        public int Width { get; }
        public int Height { get; }
        
        public SnakeHead Snake {get; private set;}
        public Food CurrentFood {get; private set;}
        
        public event Action GameOver;

        public GameField(int width, int height)
        {
            Width = width;
            Height = height;
            Snake = new SnakeHead(width / 2, height / 2);
            SpawnFood();
        }

        private void SpawnFood()
        {
            var random = new Random();
            Point foodPos;
            do
            {
                
                foodPos = new Point(random.Next(1, Width - 1), random.Next(1, Height - 1));
                
            } while (Snake.CheckCollision(foodPos));
            
            CurrentFood = new Food(foodPos.X, foodPos.Y);
            
        }

        public void Update()
        {

            var head = Snake.Body.First.Value;

            Point newHeadPos = Snake.CurrentDirection switch
            {
                Direction.Up => new Point(head.X, head.Y - 1),
                Direction.Down => new Point(head.X, head.Y + 1),
                Direction.Left => new Point(head.X - 1, head.Y),
                Direction.Right => new Point(head.X + 1, head.Y),
                _ => throw new InvalidOperationException("Unknown direction") // Невозможный кейс
            };

            if (IsWallCollision(newHeadPos) || Snake.CheckCollision(newHeadPos))
            {
                GameOver?.Invoke(); 
                return;
                //throw new WallCollisionException();
            }

            if (CurrentFood.CheckCollision(newHeadPos))
            {
                Snake++;
                SpawnFood();
            }
            
            Snake.Move(newHeadPos);

        }

        private bool IsWallCollision(Point point)
        {
            return point.X < 0 || point.X >= Width - 1 || point.Y < 0 || point.Y >= Height - 1;
        }

        public void Render()
        {
            Console.Clear();
            
            Console.WriteLine(new string('#', Width));

            for (int y = 1; y < Height - 1; y++)
            {
                
                Console.Write('#');

                for (int x = 1; x < Width - 1; x++)
                {
                    var point = new Point(x, y);
                    if (Snake.Body.First.Value == point)
                        Console.Write('o');
                    else if (Snake.CheckCollision(point))
                        Console.Write('o');
                    else if (CurrentFood.Position == point)
                        Console.Write('*');
                    else
                        Console.Write(' ');
                }
                Console.WriteLine('#');
                
            }
            Console.WriteLine(new string('#', Width));
            Console.WriteLine($"Score: {Snake.Body.Count - 1}");
            
        }
        
    }
};


