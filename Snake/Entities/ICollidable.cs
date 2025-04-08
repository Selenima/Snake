using System.Drawing; // Point

namespace Snake.Entities
{
    public interface ICollidable
    {
        public bool CheckCollision(Point point); // Нужен для проверки коллизии с переданной точкой. 
        
        // Позже добавится метод для проверки текущего положения головы.
    }
}