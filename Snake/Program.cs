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

            GameStats stats;

            if (File.Exists(statsFile))
            {
                var json = File.ReadAllText(statsFile);
                stats = JsonSerializer.Deserialize<GameStats>(json) ?? new GameStats();
                
            }
            else
            {
                
                stats = new GameStats();
            }
            
            Console.Write("Введите ваш ник: ");
            string nickname = Console.ReadLine()!;
            
            
            var player = stats.Players.FirstOrDefault(p => p.PlayerName == nickname);
            if (player == null)
            {
                player = new PlayerStats { PlayerName = nickname };
                stats.Players.Add(player);
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
                    Thread.Sleep(400);

                }
            }
            catch (WallCollisionException ex)
            {
                int score = game.Snake.Body.Count - 1;
                player.Sessions.Add(new GameSession 
                { 
                    Date = DateTime.Now, 
                    Score = score 
                });
                
                if (score > player.HighScore)
                    player.HighScore = score;
                
                
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Игрок: {player.PlayerName}");
                Console.WriteLine($"Final Score: {game.Snake.Body.Count - 1}");
                Console.WriteLine("Нажмите любую клавишу для выхода...");
                Console.ReadKey();
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

