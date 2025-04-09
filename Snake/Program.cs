using Snake.Entities;
using Snake.Logic;
using SnakeGame.Logic;


namespace Program
{
    public class Program
    {
        public static void Main()
        {

            var game = new GameField(20, 20);
            Console.CursorVisible = false;

            try
            {
                while (true)
                {
                    game.Render();

                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true).Key;
                        game.Snake.CurrentDirection = key switch
                        {
                            ConsoleKey.W => Direction.Up,
                            ConsoleKey.S => Direction.Down,
                            ConsoleKey.A => Direction.Left,
                            ConsoleKey.D => Direction.Right,
                            _ => game.Snake.CurrentDirection
                        };
                    }

                    game.Update();
                    Thread.Sleep(200);

                }
            }
            catch (WallCollisionException ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Final Score: {game.Snake.Body.Count - 1}");
            }

        }
    }
}

