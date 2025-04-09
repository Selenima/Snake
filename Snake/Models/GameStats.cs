using System;
using System.Collections.Generic;

namespace SnakeGame.Models
{
    public class GameStats
    {
        public List<PlayerStats> Players { get; set; } = new();
    }

    public class PlayerStats
    {
        public string PlayerName { get; set; }
        public int HighScore { get; set; }
        public List<GameSession> Sessions { get; set; } = new();
    }

    public class GameSession
    {
        public DateTime Date { get; set; }
        public int Score { get; set; }
    }
}