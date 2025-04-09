using System.Text.Json;
using Snake.Entities;
using Snake.Logic;
using SnakeGame.Logic;
using SnakeGame.Models;


namespace Program
{
    public class Program
    {
        public static void Main()
        {
            
            
            const string statsFile = "stats.json";

            PlayerStats stats;

            if (File.Exists(statsFile))
            {
                var json = File.ReadAllText(statsFile);
                stats = JsonSerializer.Deserialize<PlayerStats>(json)!;
                
            }
            else
            {
                Console.Write("Введите ваш ник: ");
                string nickname = Console.ReadLine()!;
                stats = new PlayerStats { PlayerName = nickname };
            }

            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                File.WriteAllText(statsFile, JsonSerializer.Serialize(stats));
            };
            
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
                        var newDirection = key switch
                        {
                            ConsoleKey.W => Direction.Up,
                            ConsoleKey.S => Direction.Down,
                            ConsoleKey.A => Direction.Left,
                            ConsoleKey.D => Direction.Right,
                            _ => game.Snake.CurrentDirection
                        };
                        
                        if (!IsOppositeDirection(game.Snake.CurrentDirection, newDirection))
                        {
                            game.Snake.CurrentDirection = newDirection;
                        }
                    }
                    
                    game.Update();
                    Thread.Sleep(200);

                }
            }
            catch (WallCollisionException ex)
            {
                int score = game.Snake.Body.Count - 1;
                stats.Sessions.Add(new GameSession {Date = DateTime.Now, Score = score});
                
                if (score > stats.HighScore)
                    stats.HighScore = score;
                
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Игрок: {stats.PlayerName}");
                Console.WriteLine($"Final Score: {game.Snake.Body.Count - 1}");
            }

        }

        private static bool IsOppositeDirection(Direction current, Direction newDir)
        {
            return (current == Direction.Up && newDir == Direction.Down) ||
                   (current == Direction.Down && newDir == Direction.Up) ||
                   (current == Direction.Left && newDir == Direction.Right) ||
                   (current == Direction.Right && newDir == Direction.Left);
        }
    }
}

