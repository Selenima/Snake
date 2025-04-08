using System.Drawing; // Point

namespace Snake.Entities
{
    public abstract class GameObject
    // Класс централизации. От него буду наследовать все объекты в игре.
    // Игра завязана на положении объекта - центральный класс его констатирует.
    {   
        public Point Position { get; protected set; } // Зачем протект на set - чтобы только наследники могли менять позицию.

        protected GameObject(int x, int y)
        {
            Position = new Point(x, y);
        }
        
    }
}