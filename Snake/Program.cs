using Snake.Entities;

var snake = new SnakeHead(0, 0);
var food = new Food(5, 5);

Console.WriteLine($"Snake head: {snake.Position}");
Console.WriteLine($"Food: {food.Position}");
