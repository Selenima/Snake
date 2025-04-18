#### В рамках проекта необходимо реализовать 4 модуля:
- [x] Entities - отвечает за сущности (Объекты игры, еда, змейка, коллизия)
- [x] Logic - отвечает за игровую механику и логику игры (карта и границы, скрипты, рендер)
- [x] Models - содержит абстрактные модели (отвечает за модель статистики)
- [x] Program.cs - центральный файл инициализации запуска.


#### Entities:

###### GameObject.cs:
#public #abstract **class GameObject** -- 
	Класс централизации. От него будут наследоваться все остальные игровые объекты. 
	 #public **Point Position** - атрибут выражающий текущую позицию (положение на карте по осям X и Y) объекта. #public get и #private set (только наследники могут менять значение)
	 #protected **GameObject** (int x, int y) - конструктор, принимающий в себя координаты и задающий значения атрибуту Position.

###### SnakeHead.cs:
#public **enum Direction** -- 
	Содержит 4 атрибута выражающих направления движения в двумерной плоскости - Up, Down, Left, Right.

#public **class SnakeHead : GameObject, ICollidable** --
	#public **LinkedList\<Point\> Body** - содержит информацию о каждом сегменте тела змеи.
	 #public **Direction CurrentDirection** - хранит текущее направление движения
	 #public **SnakeHead(int x, int y) : base (x, y)** - конструктор, создающий змею (инициализирует Body)
	 #public #overload  **bool CheckCollision(Point point)** - проверяет находятся ли в одной точке (координате) переданная точка и текущие сегменты змеи.
	 #overload **++(SnakeHead snake)** - увеличивает длину змеи на один сегмент.
	 #public **void Move(Point newPosition)** - изменяет положение головы (выражает движение).

###### ICollidable.cs:
#public **intarface ICollidable** -- содержит функцию проверки коллизии (будет перегружаться и описываться в наследниках)
	#public **bool CheckCollision(Point point)**


###### Food.cs:
#public **class Food : GameObject, ICollidable** -- 
	#public **Food(int x, int y) : base (x, y)** - конструктор, инициализирует текущее положение объекта Еда.
	 #public #overload **bool CheckCollision(Point point)** - функция проверки коллизии



#### Logic:

###### GameField.cs:
#public **class GameField** -- 
	#public **int Wigth** - атрибут, выражающий высоту поля.
	#public **int Height** - атрибут, выражающий ширину поля.
	#public **SnakeHead Snake** - атрибут, содержащий объект змеи.
	#public **Food CurrentFood** - атрибут, содержащий объект еды.
	#public **event Action GameOver** - ивент, для остановки игры.
	#public **GameField(int width, int height)** - конструктор, инициализирует игровое поле.
	#private **void SpawnFood** - функция создает объект еды в случайной позиции.
	#public **void Update** - функция обновляет позиции объектов.
	#private **bool IsWallCollision(Point point)** - функция проверяет не врезалась ли змея в стену.
	#public **void Render** - функция выводит в консоль изменения.

###### WallCollisionException.cs:
// Фиктивная ошибка (избыточная задача при наличии ивента).


#### Models:

###### GameStats.cs:
#public **class GameStats** -- класс содержит атрибут списка пользователей и их статистики.
#public **class PlayerStats** -- 
	#public **string PlayerName** 
	#public **int HighScore**
	#public **List\<GameSession\> Session** - атрибут содержит список записей о пользователе.
#public **class GameSession** -- 
	#public **DateTime Data** - атрибут дататайма записи.
	#public **int Score** - атрибут количества очков.


Program.cs:
#public class Program
	#public static void Main
	#const string statsFile - путь к файлу записи статистики
	GameStats stats
	#operation Процедура проверки и создания файла статистики
	#operation Процедура запроса имени пользователя
	#operation Процедура инициализации пользовательской сессии
	#operation Подписка на GameOver
	#operation Вход в основной блок команд и игровой цикл
	#operation Игровой цикл до срабатывания триггера по подписке на GameOver
	#operation Завершение программы с выводом статистики пользователя
