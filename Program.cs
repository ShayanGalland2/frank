using System;
using System.IO;

namespace LesChroniquesDeLArcaTech
{
    class Game
    {
        private char[,] map;
        private int playerX = 1, playerY = 1;
        private bool isRunning;
        private Random random = new Random();

        public Game()
        {
            InitializeMap("map.txt");
            isRunning = true;
        }

        private void InitializeMap(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int width = lines[0].Length;
            int height = lines.Length;
            map = new char[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (lines[y][x] == 'P')
                    {
                        playerX = x;
                        playerY = y;
                        map[y, x] = ' '; // Clear the initial player position
                    }
                    else
                    {
                        map[y, x] = lines[y][x];
                    }
                }
            }
        }

        public void Run()
        {
            while (isRunning)
            {
                RenderMap();
                var key = Console.ReadKey(true).Key;
                HandleInput(key);
            }
        }

        private void RenderMap()
        {
            Console.Clear();
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.Write("P");
                    }
                    else if (char.IsDigit(map[y, x]))
                    {
                        Console.Write(map[y, x]); // Affiche le niveau de l'ennemi
                    }
                    else
                    {
                        Console.Write(map[y, x]);
                    }
                }
                Console.WriteLine();
            }
        }


        private void HandleInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    MovePlayer(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    MovePlayer(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    MovePlayer(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    MovePlayer(1, 0);
                    break;
                case ConsoleKey.Escape:
                    isRunning = false;
                    break;
            }
        }

        private void MovePlayer(int deltaX, int deltaY)
        {
            int newX = playerX + deltaX;
            int newY = playerY + deltaY;

            if (CanMove(newX, newY))
            {
                playerX = newX;
                playerY = newY;
                CheckForCombat(newX, newY);
            }
        }

        private bool CanMove(int x, int y)
        {
            return x >= 0 && x < map.GetLength(1) && y >= 0 && y < map.GetLength(0) && map[y, x] != '#';
        }

        private void CheckForCombat(int x, int y)
        {
            if (char.IsDigit(map[y, x]))
            {
                int enemyDifficulty = map[y, x] - '0'; // Convert char to int
                StartCombat(enemyDifficulty, x, y);

            }
        }


        private void StartCombat(int enemyDifficulty, int x, int y)
        {
            Console.WriteLine($"\nEncountered an enemy of difficulty {enemyDifficulty}! Combat starts!");

            // Configuration basique : HP et puissance d'attaque varient selon la difficulté
            int playerHp = 100;
            int playerAttack = 20;
            int enemyHp = enemyDifficulty * 10;
            int enemyAttack = enemyDifficulty * 5;

            Enemy enemy = new Enemy(enemyHp, enemyAttack);
            CombatSystem combatSystem = new CombatSystem(playerHp, playerAttack, enemy);
            bool playerWon = combatSystem.StartCombat();

            if (playerWon)
            {
                Console.WriteLine("You won the battle!");
                map[y, x] = ' ';
            }
            else
            {
                Console.WriteLine("You lost the battle...");
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }
}
